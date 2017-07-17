Detects whether your application is running inside a Virtual Machine.

Employs an extensible framework for detecting virtual machines.

Compatibility:
---
  - .NET Framework 4.0+

At a glance:
---

var hypervisorName = "";
if (VirtualMachineDetector.Assert(out hypervisorName))
	Console.WriteLine("DETECTED {0}!", hypervisorName);

Latest Changes:
---
	- 2017-07-17 - initial version


Contact:
---

Robson Felix
	- robson dot felix at gmail dot com