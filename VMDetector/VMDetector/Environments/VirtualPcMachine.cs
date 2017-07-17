using System.Collections.Generic;
using System.Linq;

class VirtualPcMachine : BaseVirtualEnvironment
{
    public override string Name
    {
        get
        {
            return "Microsoft Virtual PC";
        }
    }

    public override bool ContainsProcess(IEnumerable<string> services)
    {
        return (services.Contains("vpcmap") && services.Contains("vmsrvc")) || services.Contains("vmusrvc");
    }
}
