Detects whether your application is running inside a Virtual Machine.

Platform status:
---
  - VMWare Workstation Player - _COMPLETED_
  - Microsoft Hyper-V under Windows Server Datacenter 2012 - _COMPLETED_
  - Microsoft Virtual PC - _COMPLETED_
  - QEMU - _PARTIALLY COMPLETED_
  - VirtualBox - _IN PROGRESS_
  - Xen - _TBD_

Compatibility:
---
  - .NET Framework 4.0+

At a glance:
---
**C#:**
```csharp
var hypervisorName = "";
if (VirtualMachineDetector.Assert(out hypervisorName))
   Console.WriteLine("DETECTED {0}!", hypervisorName);
```

Latest Changes:
---
	- 2017-07-17 - initial version


Contact:
---

Robson Felix
	- robson dot felix at gmail dot com (_use this for everything that is not available via GitHub features_)