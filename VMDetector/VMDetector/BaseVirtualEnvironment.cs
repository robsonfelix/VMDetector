using System;
using System.Collections.Generic;

abstract class BaseVirtualEnvironment : IVirtualEnvironment
{
    public abstract string Name { get; }

    public virtual bool ContainsDevice(IEnumerable<PnPEntity> devices)
    {
        return false;
    }

    public virtual bool ContainsDisk(IEnumerable<DiskDrive> disks)
    {
        return false;
    }

    public virtual bool ContainsProcess(IEnumerable<string> processes)
    {
        return false;
    }

    public virtual bool ContainsService(IEnumerable<WindowsService> services)
    {
        return false;
    }

    public virtual bool IsVirtual(BIOS bios)
    {
        return false;
    }

    public virtual bool IsVirtual(ComputerSystem computer)
    {
        return false;
    }

    public virtual bool Assert(ComputerSystem computer, BIOS bios, IEnumerable<DiskDrive> disks, IEnumerable<PnPEntity> devices, IEnumerable<string> processes, IEnumerable<WindowsService> services)
    {
#if DEBUG
        Console.WriteLine();
        Console.WriteLine("--------------------------------------------------------------");
        Console.WriteLine("Asserting {0}", this.GetType().Name);
#endif

        bool computerIsVirtual = IsVirtual(computer);
        bool biosIsVirtual = IsVirtual(bios);
        bool containsVirtualDisk = ContainsDisk(disks);
        bool containsVirtualDevice = ContainsDevice(devices);
        bool containsVirtualProcess = ContainsProcess(processes);
        bool containsVirtualService = ContainsService(services);

#if DEBUG
        if (computerIsVirtual)
            Console.WriteLine("Detected as virtual machine given key computer information.");

        if (biosIsVirtual)
            Console.WriteLine("Detected as virtual machine given bios information.");

        if (containsVirtualDisk)
            Console.WriteLine("Detected as virtual machine given hard disk information.");

        if (containsVirtualDevice)
            Console.WriteLine("Detected as virtual machine given PnP devices information.");

        if (containsVirtualProcess)
            Console.WriteLine("Detected as virtual machine given processes information.");

        if (containsVirtualService)
            Console.WriteLine("Detected as virtual machine given Windows services information.");
#endif

        return computerIsVirtual
            || biosIsVirtual
            || containsVirtualDisk
            || containsVirtualDevice
            || containsVirtualProcess
            || containsVirtualService;
    }
}