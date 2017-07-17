using System;

namespace VMDetectorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var hypervisor = "";
            if (VirtualMachineDetector.Assert(out hypervisor))
                Console.WriteLine("DETECTED {0}!", hypervisor);
        }
    }
}
