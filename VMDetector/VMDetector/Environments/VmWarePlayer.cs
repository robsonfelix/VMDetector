using System.Collections.Generic;
using System.Linq;

class VmWarePlayer : VmWareMachine
{
    public override bool IsVirtual(BIOS bios)
    {
        return bios.SerialNumber.Contains("vmware");
    }

    public override bool IsVirtual(ComputerSystem computer)
    {
        return computer.Manufacturer.Contains("vmware")
            || computer.Model.Contains("vmware")
            || computer.OEMStringArray.Contains("virtual");
    }

    public override bool ContainsDisk(IEnumerable<DiskDrive> disks)
    {
        return disks.Any(d => d.Model.Contains("vmware"));
    }

    public override bool ContainsDevice(IEnumerable<PnPEntity> devices)
    {
        return devices.Any(d => d.Name.Equals("vmware pointing device"))
            || devices.Any(d => d.Name.Contains("vmware sata"))
            || devices.Any(d => d.Name.Equals("vmware usb pointing device"))
            || devices.Any(d => d.Name.Equals("vmware vmci bus device"))
            || devices.Any(d => d.Name.Equals("vmware virtual s scsi disk device"))
            || devices.Any(d => d.Name.StartsWith("vmware svga"))
            ;
    }

    public override bool ContainsService(IEnumerable<WindowsService> services)
    {
        return services.Any(s => s.CommandLine.Contains("vmware") && s.Name.Equals("vmtools"))
            || services.Any(s => s.CommandLine.Contains("vmware") && s.Name.Equals("tpvcgateway"))
            || services.Any(s => s.CommandLine.Contains("vmware") && s.Name.Equals("tpautoconnsvc"))
            ;
    }
}
