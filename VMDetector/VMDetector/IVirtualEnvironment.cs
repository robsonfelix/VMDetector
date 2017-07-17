using System.Collections.Generic;
interface IVirtualEnvironment
{
    string Name { get; }
    bool IsVirtual(ComputerSystem computer);
    bool IsVirtual(BIOS bios);
    bool ContainsDisk(IEnumerable<DiskDrive> disks);
    bool ContainsDevice(IEnumerable<PnPEntity> devices);
    bool ContainsProcess(IEnumerable<string> processes);
    bool ContainsService(IEnumerable<WindowsService> services);
    bool Assert(ComputerSystem computer, BIOS bios, IEnumerable<DiskDrive> disks, IEnumerable<PnPEntity> devices, IEnumerable<string> processes, IEnumerable<WindowsService> services);
}