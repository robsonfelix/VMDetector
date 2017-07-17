using System.Management;

class DiskDrive : BaseWin32Entity
{
    // https://msdn.microsoft.com/en-us/library/aa394132(v=vs.85).aspx

    public DiskDrive(ManagementBaseObject obj)
        : base(obj)
    {
    }
}