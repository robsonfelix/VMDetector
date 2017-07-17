using System.Management;

class MotherboardDevice : BaseWin32Entity
{
    // https://msdn.microsoft.com/en-us/library/aa394204(v=vs.85).aspx

    public MotherboardDevice(ManagementBaseObject obj)
        : base(obj)
    {
    }

    public override string ToString()
    {
        return PrintProperties();
    }
}