tests: TestLoadInternalRuntime
	./test-load-internal-runtime \
		--runtime-class-name QarnotSDK.Worker.Internal.DummyInternalRuntime \
		--runtime-dll-location ../DummyInternalRuntime/bin/Release/DummyInternalRuntime.dll \
		--test-executable bin/Release/TestLoadInternalRuntime

TestLoadInternalRuntime:
	dotnet publish -c Release -o bin/Release

.PHONY: tests TestLoadInternalRuntime

