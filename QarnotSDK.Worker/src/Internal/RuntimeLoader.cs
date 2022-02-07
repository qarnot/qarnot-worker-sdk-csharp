namespace QarnotSDK.Worker.Internal
{
    using System;
    using System.Reflection;

    public sealed class RuntimeLoader
    {
        private const string InternalDLLEnvVar = "QARNOT_WORKER_INTERNAL_DLL";
        private const string InternalClassNameEnvVar = "QARNOT_WORKER_INTERNAL_CLASS_NAME";

        public IRuntime Load()
        {
            var dllPath = ReadEnvVar(InternalDLLEnvVar);
            var className = ReadEnvVar(InternalClassNameEnvVar);

            if (dllPath is null && className is null)
            {
                return new LocalRuntime();
            }

            if (dllPath is null || className is null)
            {
                throw new ArgumentNullException(
                    $"Both {InternalDLLEnvVar} and {InternalClassNameEnvVar} must be specified"
                    + " when running remotely"
                );
            }

            var assembly = Assembly.LoadFrom(dllPath);
            var type_ = assembly.GetType(className, throwOnError: true);
            var ctor = type_.GetConstructor(Type.EmptyTypes);
            if (ctor is null)
            {
                throw new Exception($"No default constructor found for {className}");
            }

            return (IRuntime)ctor.Invoke(null);
        }

        private string ReadEnvVar(string name)
        {
            var value = Environment.GetEnvironmentVariable(name);
            if (value != null)
            {
                return value;
            }

            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Machine);
        }
    }
}
