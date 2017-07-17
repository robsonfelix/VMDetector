using System.Management;

class PnPEntity : BaseWin32Entity
{
    // https://msdn.microsoft.com/en-us/library/aa394353(v=vs.85).aspx

    public PnPEntity(ManagementBaseObject obj)
        : base(obj)
    {
    }
}