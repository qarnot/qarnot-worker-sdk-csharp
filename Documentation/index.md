# Welcome to the QarnotSDK.Worker SDK C#

The Qarnot Worker SDK is a C# library designed to interface long-running services with
the Qarnot cloud computing runtime.\

## QarnotSDK vs. QarnotSDK.Worker

The two library serve different purpose:

* `QarnotSDK` is used to interact with Qarnot in configure and launch tasks, pools, etc.

* `QarnotSDK.Worker` is library that helps creating long-running services. This services
  can then be submitted as tasks via the regular `QarnotSDK` library.


## See Also

* Examples are available in the [Github repository](https://github.com/qarnot/qarnot-worker-sdk-csharp)
* For more information about the Qarnot computing platform as a whole, check [http://computing.qarnot.com](http://computing.qarnot.com).
