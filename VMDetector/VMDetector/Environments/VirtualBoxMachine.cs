using System.Collections.Generic;
using System.Linq;

class VirtualBoxMachine : BaseVirtualEnvironment
{
    public override string Name
    {
        get
        {
            return "VirtualBox";
        }
    }

    public override bool ContainsDisk(IEnumerable<DiskDrive> disks)
    {
        return disks.Any(d => d.Model.Contains("vbox"));
    }

    public override bool ContainsProcess(IEnumerable<string> services)
    {
        return services.Contains("vboxservice");
    }
}