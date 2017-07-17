using System.Collections.Generic;
using System.Linq;

abstract class VmWareMachine : BaseVirtualEnvironment
{
    public override string Name
    {
        get
        {
            return "VMware";
        }
    }

    public override bool ContainsDisk(IEnumerable<DiskDrive> disks)
    {
        return disks.Any(d => d.Name.Contains("vmware"));
    }

    public override bool IsVirtual(ComputerSystem computer)
    {
        return computer.Manufacturer.Contains("vmware")
            && computer.Model.Contains("virtual");
    }
}
