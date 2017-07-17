using System.Collections.Generic;
using System.Linq;

class HyperVMachine : BaseVirtualEnvironment
{
    public override string Name
    {
        get
        {
            return "Microsoft Hyper-V";
        }
    }

    public override bool ContainsDisk(IEnumerable<DiskDrive> disks)
    {
        return disks.Any(d => d.Caption.Contains("virtual"));
    }

    public override bool IsVirtual(ComputerSystem computer)
    {
        return computer.Manufacturer.Contains("microsoft")
                        && computer.Model.Contains("virtual");
    }
}