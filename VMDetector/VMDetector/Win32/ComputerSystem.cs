using System.Management;

class ComputerSystem : BaseWin32Entity
{
    // https://msdn.microsoft.com/en-us/library/aa394102(v=vs.85).aspx

    public string OEMStringArray { get; set; }

    public ComputerSystem(ManagementBaseObject obj)
        : base(obj)
    {
        var oemString = obj["OEMStringArray"];
        if (oemString != null)
            this.OEMStringArray = ToJSON(oemString).ToLower();
    }

    public override string ToString()
    {
        return PrintProperties();
    }
}
