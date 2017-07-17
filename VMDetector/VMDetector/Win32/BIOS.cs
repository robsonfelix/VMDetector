using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

// https://msdn.microsoft.com/en-us/library/aa394077(v=vs.85).aspx
enum BiosCharacteristics : ushort
{
    Reserved0 = 0,
    Reserved1 = 1,
    Unknown = 2,
    BIOS_Characteristics_Not_Supported = 3,
    ISA_is_supported = 4,
    MCA_is_supported = 5,
    EISA_is_supported = 6,
    PCI_is_supported = 7,
    PC_Card_PCMCIA_is_supported = 8,
    Plug_and_Play_is_supported = 9,
    APM_is_supported = 10,
    BIOS_is_Upgradeable_Flash = 11,
    BIOS_shadowing_is_allowed = 12,
    VL_VESA_is_supported = 13,
    ESCD_support_is_available = 14,
    Boot_from_CD_is_supported = 15,
    Selectable_Boot_is_supported = 16,
    BIOS_ROM_is_socketed = 17,
    Boot_From_PC_Card_PCMCIA_is_supported = 18,
    EDD_Enhanced_Disk_Drive_Specification_is_supported = 19,
    Int_13h_Japanese_Floppy_for_NEC_9800_is_supported = 20,
    Int_13h_Japanese_Floppy_for_Toshiba_is_supported = 21,
    Int_13h_5_25_360_KB_Floppy_Services_are_supported = 22,
    Int_13h_5_25_1_2MB_Floppy_Services_are_supported = 23,
    Int_13h_3_5_720_KB_Floppy_Services_are_supported = 24,
    Int_13h_3_5_2_88_MB_Floppy_Services_are_supported = 25,
    Int_5h_Print_Screen_Service_is_supported = 26,
    Int_9h_8042_Keyboard_services_are_supported = 27,
    Int_14h_Serial_Services_are_supported = 28,
    Int_17h_printer_services_are_supported = 29,
    Int_10h_CGA_Mono_Video_Services_are_supported = 30,
    NEC_PC_98 = 31,
    ACPI_supported = 32,
    USB_Legacy_is_supported = 33,
    AGP_is_supported = 34,
    I2O_boot_is_supported = 35,
    LS120_boot_is_supported = 36,
    ATAPI_ZIP_Drive_boot_is_supported = 37,
    Firewire_1394_boot_is_supported = 38,
    Smart_Battery_supported = 39,
    Reserved_Bios_Vendor_1 = 40,
    Reserved_Bios_Vendor_2 = 41,
    Reserved_Bios_Vendor_3 = 42,
    Reserved_Bios_Vendor_4 = 43,
    Reserved_Bios_Vendor_5 = 44,
    Reserved_Bios_Vendor_6 = 45,
    Reserved_Bios_Vendor_7 = 46,
    Reserved_System_Vendor_1 = 48,
    Reserved_System_Vendor_2 = 49,
    Reserved_System_Vendor_3 = 50,
    Reserved_System_Vendor_4 = 51,
    Reserved_System_Vendor_5 = 52,
    Reserved_System_Vendor_6 = 53,
    Reserved_System_Vendor_7 = 54,
    Reserved_System_Vendor_8 = 55,
    Reserved_System_Vendor_9 = 56,
    Reserved_System_Vendor_10 = 57,
    Reserved_System_Vendor_11 = 58,
    Reserved_System_Vendor_12 = 59,
    Reserved_System_Vendor_13 = 60,
    Reserved_System_Vendor_14 = 61,
    Reserved_System_Vendor_15 = 62,
    Reserved_System_Vendor_16 = 63,
}

// https://msdn.microsoft.com/en-us/library/aa394077(v=vs.85).aspx
class BIOS : BaseWin32Entity
{
    public IEnumerable<BiosCharacteristics> Characteristics { get; private set; }

    public string SerialNumber { get; private set; }

    public BIOS(ManagementBaseObject obj)
        : base(obj)
    {
        var temp = (ushort[])obj["BiosCharacteristics"];
        this.Characteristics = temp.Select(c => (BiosCharacteristics)c).ToArray();

        this.SerialNumber = ParseValue<string>(obj, "SerialNumber");

        if (!string.IsNullOrEmpty(this.SerialNumber))
            this.SerialNumber = this.SerialNumber.ToLower();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(PrintProperties());

        foreach (var bc in this.Characteristics)
        {
            var name = Enum.GetName(typeof(BiosCharacteristics), bc);
            sb.AppendLine(name);
        }

        return sb.ToString();
    }
}