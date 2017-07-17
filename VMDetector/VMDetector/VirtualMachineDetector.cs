using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.ServiceProcess;

public class VirtualMachineDetector
{
    static IVirtualEnvironment[] _detectors;
    static ComputerSystem _computer;
    static BIOS _bios;
    static MotherboardDevice _motherboard;
    static IEnumerable<DiskDrive> _disks;
    static IEnumerable<PnPEntity> _devices;
    static IEnumerable<WindowsService> _services;

    static VirtualMachineDetector()
    {
        _detectors = new IVirtualEnvironment[]
       {
                new VmWarePlayer(),
                new HyperVMachine(),
                new QEmuMachine(),
                new VirtualBoxMachine(),
       };

        _computer = Create<ComputerSystem>("Win32_ComputerSystem");
        _bios = Create<BIOS>("Win32_BIOS");
        _motherboard = Create<MotherboardDevice>("Win32_MotherboardDevice");
        _devices = CreateList<PnPEntity>("Win32_PnPEntity");
        _disks = CreateList<DiskDrive>("Win32_DiskDrive");
        _services = GetWindowsServices();

        #region DEBUG PRINT
#if DEBUG
        Console.WriteLine();
        Console.WriteLine("MOTHERBOARD INFO");
        Console.WriteLine("================");
        Console.WriteLine(_motherboard);
        Console.WriteLine();

        Console.WriteLine("BIOS INFO");
        Console.WriteLine("=========");
        Console.WriteLine(_bios);
        Console.WriteLine();

        Console.WriteLine("COMPUTER INFO");
        Console.WriteLine("=============");
        Console.WriteLine(_computer);
        Console.WriteLine();

        Console.WriteLine("DEVICES INFO");
        Console.WriteLine("============");
        foreach (var device in _devices)
            Console.WriteLine(device);
        Console.WriteLine();

        Console.WriteLine("HARD DRIVES INFO");
        Console.WriteLine("================");
        foreach (var disk in _disks)
            Console.WriteLine(disk);
        Console.WriteLine();

        Console.WriteLine("WINDOWS SERVICES");
        Console.WriteLine("================");
        foreach (var service in _services)
            Console.WriteLine(service);
        Console.WriteLine();
#endif
        #endregion
    }

    public static bool Assert(out string name)
    {
        var processes = Process.GetProcesses()
                               .Select(p => p.ProcessName.ToLower())
                               .OrderBy(p => p)
                               .ToArray();

        var detected = _detectors.FirstOrDefault(c => c.Assert(_computer, _bios, _disks, _devices, processes, _services));

        var success = detected != null;

        name = success ? detected.Name : null;

        return success;
    }

    public static bool Assert()
    {
        string hypervisorName;
        return Assert(out hypervisorName);
    }

    static IEnumerable<WindowsService> GetWindowsServices()
    {
        return ServiceController.GetServices()
                                .Select(s => new WindowsService(s))
                                .OrderBy(s => s.Name)
                                .ToArray();
    }

    static T Create<T>(string key)
    {
        var info = new ManagementClass(key);
        foreach (var mo in info.GetInstances())
            return (T)Activator.CreateInstance(typeof(T), mo);

        return default(T);
    }

    static List<T> CreateList<T>(string key)
    {
        var info = new ManagementClass(key);
        var items = new List<T>();
        foreach (var mo in info.GetInstances())
            items.Add((T)Activator.CreateInstance(typeof(T), mo));

        return items;
    }
}
