using System.Collections.Generic;
using System.Linq;

class QEmuMachine : BaseVirtualEnvironment
{
    public override string Name
    {
        get
        {
            return "QEMU";
        }
    }

    public override bool ContainsDisk(IEnumerable<DiskDrive> disks)
    {
        return disks.Any(d => d.Name.IndexOf("qemu") >= 0);
    }
}
