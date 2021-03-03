/*HardwareInfo.cs*/

using System;
using System.Management;

public static class hardware_info
{
    /*Retrieving Processor Id.*/
    /*Call : hardware_info.GetProcessorId*/
    public static String GetProcessorId()
    {
        ManagementClass mc = new ManagementClass("win32_processor");
        ManagementObjectCollection moc = mc.GetInstances();
        String Id = String.Empty;
        foreach (ManagementObject mo in moc)
        {
            Id = mo.Properties["processorID"].Value.ToString();
            break;
        }
        return Id;
    }

    /*Retrieving HDD Serial No.*/
    /*Call : hardware_info.GetHDDSerialNo*/
    public static String GetHDDSerialNo()
    {
        ManagementClass mangnmt = new ManagementClass("Win32_LogicalDisk");
        ManagementObjectCollection mcol = mangnmt.GetInstances();
        string result = "";
        foreach (ManagementObject strt in mcol){
            result += Convert.ToString(strt["VolumeSerialNumber"]);
        }
        return result;
    }

    /*Retrieving System MAC Address.*/
    /*Call : hardware_info.GetMACAddress*/
    public static string GetMACAddress()
    {
        ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
        ManagementObjectCollection moc = mc.GetInstances();
        string MACAddress = String.Empty;
        foreach (ManagementObject mo in moc)
        {
            if (MACAddress == String.Empty){
                if ((bool)mo["IPEnabled"] == true) MACAddress = mo["MacAddress"].ToString();
            }
            mo.Dispose();
        }
        MACAddress = MACAddress.Replace(":", "");
        return MACAddress;
    }

    /*Retrieve Motherboard Manufacturer*/
    /*Call : hardware_info.GetBoardMaker*/
    public static string GetBoardMaker()
    {
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
        foreach (ManagementObject wmi in searcher.Get())
        {
            try
            {
                return wmi.GetPropertyValue("Manufacturer").ToString();
            }
            catch { }
        }
        return "Board Maker: Unknown";
    }

    /*Retrieving Motherboard Product Id.*/
    /*Call : hardware_info.GetBoardProductId*/
    public static string GetBoardProductId()
    {
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
        foreach (ManagementObject wmi in searcher.Get())
        {
            try
            {
                return wmi.GetPropertyValue("Product").ToString();
            }
            catch { }
        }
        return "Product: Unknown";
    }

    /*Retrieving CD-DVD Drive Path.*/
    /*Call : hardware_info.GetCdRomDrive*/
    public static string GetCdRomDrive()
    {
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_CDROMDrive");
        foreach (ManagementObject wmi in searcher.Get())
        {
            try
            {
                return wmi.GetPropertyValue("Drive").ToString();
            }
            catch { }
        }
        return "CD ROM Drive Letter: Unknown";
    }

    /*Retrieving BIOS manufacturer*/
    /*Call : hardware_info.GetBIOSmaker*/
    public static string GetBIOSmaker()
    {
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");
        foreach (ManagementObject wmi in searcher.Get())
        {
            try
            {
                return wmi.GetPropertyValue("Manufacturer").ToString();
            }
            catch { }
        }
        return "BIOS Maker: Unknown";
    }

    /*BIOS Serial Number*/
    /*Call : hardware_info.GetBIOSserNo*/
    public static string GetBIOSserNo()
    {
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");
        foreach (ManagementObject wmi in searcher.Get())
        {
            try
            {
                return wmi.GetPropertyValue("SerialNumber").ToString();
            }
            catch { }
        }
        return "BIOS Serial Number: Unknown";
    }

    /*Retrieving BIOS Caption.*/
    /*Call : hardware_info.GetBIOScaption*/
    public static string GetBIOScaption()
    {
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");
        foreach (ManagementObject wmi in searcher.Get())
        {
            try
            {
                return wmi.GetPropertyValue("Caption").ToString();
            }
            catch { }
        }
        return "BIOS Caption: Unknown";
    }

    /*Retrieving System Account Name.*/
    /*Call : hardware_info.GetAccountName*/
    public static string GetAccountName()
    {

        ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_UserAccount");

        foreach (ManagementObject wmi in searcher.Get())
        {
            try
            {
                return wmi.GetPropertyValue("Name").ToString();
            }
            catch { }
        }
        return "User Account Name: Unknown";

    }

    /*Retrieving Retrieving Physical Ram Memory.*/
    /*Call : hardware_info.GetPhysicalMemory*/
    public static string GetPhysicalMemory()
    {
        ManagementScope oMs = new ManagementScope();
        ObjectQuery oQuery = new ObjectQuery("SELECT Capacity FROM Win32_PhysicalMemory");
        ManagementObjectSearcher oSearcher = new ManagementObjectSearcher(oMs, oQuery);
        ManagementObjectCollection oCollection = oSearcher.Get();
        long MemSize = 0;
        long mCap = 0;
        foreach (ManagementObject obj in oCollection)
        {
            mCap = Convert.ToInt64(obj["Capacity"]);
            MemSize += mCap;
        }
        MemSize = (MemSize / 1024) / 1024;
        return MemSize.ToString() + "MB";
    }

    /*Retrieving No of Ram Slot on Motherboard.*/
    /*Call : hardware_info.GetNoRamSlots*/
    public static string GetNoRamSlots()
    {
        int MemSlots = 0;
        ManagementScope oMs = new ManagementScope();
        ObjectQuery oQuery2 = new ObjectQuery("SELECT MemoryDevices FROM Win32_PhysicalMemoryArray");
        ManagementObjectSearcher oSearcher2 = new ManagementObjectSearcher(oMs, oQuery2);
        ManagementObjectCollection oCollection2 = oSearcher2.Get();
        foreach (ManagementObject obj in oCollection2)
        {
            MemSlots = Convert.ToInt32(obj["MemoryDevices"]);
        }
        return MemSlots.ToString();
    }

    /*Retrieve the CPU Manufacturer*/
    /*Call : hardware_info.GetCPUManufacturer*/
    public static string GetCPUManufacturer()
    {
        string cpuMan = String.Empty;
        ManagementClass mgmt = new ManagementClass("Win32_Processor");
        ManagementObjectCollection objCol = mgmt.GetInstances();
        foreach (ManagementObject obj in objCol)
        {
            if (cpuMan == String.Empty)
            {
                cpuMan = obj.Properties["Manufacturer"].Value.ToString();
            }
        }
        return cpuMan;
    }

    /*Retrieving the CPU's current clock speed.*/
    /*Call : hardware_info.GetCPUCurrentClockSpeed*/
    public static int GetCPUCurrentClockSpeed()
    {
        int cpuClockSpeed = 0;
        ManagementClass mgmt = new ManagementClass("Win32_Processor");
        ManagementObjectCollection objCol = mgmt.GetInstances();
        foreach (ManagementObject obj in objCol)
        {
            if (cpuClockSpeed == 0)
            {
                cpuClockSpeed = Convert.ToInt32(obj.Properties["CurrentClockSpeed"].Value.ToString());
            }
        }
        return cpuClockSpeed;
    }

    /*Retrieve the network adapters.*/
    /*Call : hardware_info.GetDefaultIPGateway*/
    public static string GetDefaultIPGateway()
    {
        ManagementClass mgmt = new ManagementClass("Win32_NetworkAdapterConfiguration");
        ManagementObjectCollection objCol = mgmt.GetInstances();
        string gateway = String.Empty;
        foreach (ManagementObject obj in objCol)
        {
            if (gateway == String.Empty)
            {
                if ((bool)obj["IPEnabled"] == true)
                {
                    gateway = obj["DefaultIPGateway"].ToString();
                }
            }
            obj.Dispose();
        }
        gateway = gateway.Replace(":", "");
        return gateway;
    }

    /*Retrieve CPU Speed.*/
    /*Call : hardware_info.GetCpuSpeedInGHz*/
    public static double? GetCpuSpeedInGHz()
    {
        double? GHz = null;
        using (ManagementClass mc = new ManagementClass("Win32_Processor"))
        {
            foreach (ManagementObject mo in mc.GetInstances())
            {
                GHz = 0.001 * (UInt32)mo.Properties["CurrentClockSpeed"].Value;
                break;
            }
        }
        return GHz;
    }

    /*Retrieving Current Language.*/
    /*Call : hardware_info.GetCurrentLanguage*/
    public static string GetCurrentLanguage()
    {
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");
        foreach (ManagementObject wmi in searcher.Get()){
            try
            {
                return wmi.GetPropertyValue("CurrentLanguage").ToString();
            }
            catch { }
        }
        return "BIOS Maker: Unknown";
    }

    /*Retrieving Current Language.*/
    /*Call : hardware_info.GetOSInformation*/
    public static string GetOSInformation()
    {
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
        foreach (ManagementObject wmi in searcher.Get()){
            try
            {
                return ((string)wmi["Caption"]).Trim() + ", " + (string)wmi["Version"] + ", " + (string)wmi["OSArchitecture"];
            }
            catch { }
        }
        return "BIOS Maker: Unknown";
    }

    /*Retrieving System MAC Address.*/
    /*Call : hardware_info.GetProcessorInformation*/
    public static String GetProcessorInformation()
    {
        ManagementClass mc = new ManagementClass("win32_processor");
        ManagementObjectCollection moc = mc.GetInstances();
        String info = String.Empty;
        foreach (ManagementObject mo in moc)
        {
            string name = (string)mo["Name"];
            name = name.Replace("(TM)", "™").Replace("(tm)", "™").Replace("(R)", "®").Replace("(r)", "®").Replace("(C)", "©").Replace("(c)", "©").Replace("    ", " ").Replace("  ", " ");

            info = name + ", " + (string)mo["Caption"] + ", " + (string)mo["SocketDesignation"];
        }
        return info;
    }

    /*Retrieving Computer Name.*/
    /*Call : hardware_info.GetComputerName*/
    public static String GetComputerName()
    {
        ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
        ManagementObjectCollection moc = mc.GetInstances();
        String info = String.Empty;
        foreach (ManagementObject mo in moc)
        {
            info = (string)mo["Name"];
        }
        return info;
    }

}
