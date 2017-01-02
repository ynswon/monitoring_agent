using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data; 
using System.Text; 

using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

using System.Web; 


namespace funda_interface
{

    public class PortListenerEventArgs : EventArgs
    {

        private readonly string message;
        public Array binaryData;
        public PortListenerEventArgs(string message)
        {
            this.message = message;
        }
        public PortListenerEventArgs(Array arr)
        {
            this.binaryData = arr;
        }

        public string Message
        {
            get { return this.message; }
        }
    }

    public class MessagePassEventArgs : EventArgs
    {
        private readonly string message;
        public MessagePassEventArgs(string message)
        {
            this.message = message;
        }

        public string Message
        {
            get { return this.message; }
        }
    }


    static class PortListener
    {
        public static FundaInterface fifi = null;
        private static string update_target_url = "";
        public static void SetUpdateUrl(String url)
        {
            update_target_url = url;
        }
        private static Object streamLock = new Object();
        struct COMDEV
        {
            public int DevId;
            public string portNum;
        };

        struct LPTDEV
        {
            public int DevId;
            public string portNum;
        };

        struct USBPRTDEV
        {
            public int DevId;
            public int ParentId;
            public string deviceName;
        };

        public struct BHDEVsimple
        {
            public int DevId;
            public int ParentId;
            public int Periph;
            public int BusType;
            public int Selected; // 0 : off / 1 : on
            public int BusSpeed;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string Name;
        };

        public struct BHData
        {
            public int Length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16384)]
            public byte[] Dumpdata;
        };

        public struct BHval
        {
            public Int64 PhaseOut;
            public Int64 PhaseIn;
            public Int32 DevId;
            public UInt32 BaudRate;
        };

        public struct BHbaudrate
        {
            public int baudRate;
            //public int portNum;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public String portName;
        };


        [DllImport("MonitoringAPIs.dll")]
        public static extern bool _BHGetBaudrate(ref BHbaudrate val);
        [DllImport("MonitoringAPIs.dll")]
        public static extern bool _BHGetDevice(ref BHDEVsimple dev);
        [DllImport("MonitoringAPIs.dll")]
        public static extern bool _BHSelectDevice(ref BHDEVsimple dev);
        [DllImport("MonitoringAPIs.dll")]
        public static extern bool _BHDeselectDevice(ref BHDEVsimple dev);
        [DllImport("MonitoringAPIs.dll")]
        public static extern bool _BHCaptureOn(Int32 MaxPhaseVal, Int32 CBytes);
        [DllImport("MonitoringAPIs.dll")]
        public static extern bool _BHCaptureOff();
        [DllImport("MonitoringAPIs.dll")]
        public static extern bool _BHDumpData(ref BHData Data, ref BHval val);
        [DllImport("MonitoringAPIs.dll")]
        public static extern bool _BHIsReady();
        [DllImport("MonitoringAPIs.dll")]
        public static extern bool _BHIsModeRun();

        public static String[] portNameArray = new String[256];
        public static IniFile config;
        public static int[] allowed_baud_rate = { 1200, 2400, 4800, 9600, 14400, 19200, 28800, 38400, 57600, 76800, 115200, 230400 };
        public static bool BHIsReady()
        {
            return _BHIsReady();
        }

        // Find Device ID by DeviceName
        public static Int32 BHGetDeviceIDbyName(String DeviceName)
        {
            for (int DevId = 0; ; DevId++)
            {
                BHDEVsimple dev = new BHDEVsimple();
                dev.DevId = DevId;

                if (!PortListener._BHGetDevice(ref dev)) break;

                String DevName = dev.Name;
                DevName = Regex.Replace(DevName, @"\s", "");

                if (DevName.Equals(DeviceName))
                {
                    return DevId;
                }
            }
            return -1;
        }

        // Find Device Name by DeviceID
        public static String BHGetDeviceNameByID(Int32 DevId)
        {
            BHDEVsimple dev = new BHDEVsimple();
            dev.DevId = DevId;
            PortListener._BHGetDevice(ref dev);
            String mDeviceName = Regex.Replace(dev.Name, @"\s", "");
            return mDeviceName;
        }

        public static bool BHSelectComport(int comport)
        {
            int DevId;
            for (DevId = 0; ; DevId++)
            {
                BHDEVsimple dev = new BHDEVsimple();
                dev.DevId = DevId;

                if (!_BHGetDevice(ref dev)) break;

                BHDeselectDevice(DevId);

                if (dev.Periph == -7) //#define PERIPH_PORTS     (-7)     // serial/parallel port
                {
                    COMDEV comdev = new COMDEV();
                    Match matchC = Regex.Match(dev.Name.ToString(), @"COM" + @comport);
                    if (matchC.Success)
                    {
                        BHSelectDevice(DevId);

                        portNameArray[DevId] = matchC.Value;
                        return true;
                    }
                }

            }
            return false;
        }
        // Select Device
        public static bool BHSelectDevice(int DeviceID)
        {
            BHDEVsimple dev = new BHDEVsimple();
            dev.DevId = DeviceID;
            return _BHSelectDevice(ref dev);
        }

        public static void BHSelectAllDevice()
        {
            int DevId;
            for (DevId = 0; ; DevId++)
            {
                BHDEVsimple dev = new BHDEVsimple();
                dev.DevId = DevId;

                if (!_BHGetDevice(ref dev)) break;

                BHDeselectDevice(DevId);

                if (dev.Periph == -7) //#define PERIPH_PORTS     (-7)     // serial/parallel port
                {
                    COMDEV comdev = new COMDEV();
                    Match matchC = Regex.Match(dev.Name.ToString(), @"COM[0-9]");
                    if (matchC.Success)
                    {
                        BHSelectDevice(DevId);

                        portNameArray[DevId] = matchC.Value;

                    }
                    matchC = Regex.Match(dev.Name.ToString(), @"Serial Port");
                    if (matchC.Success)
                    {
                        BHSelectDevice(DevId);
                    }

                    Match matchL = Regex.Match(dev.Name.ToString(), @"LPT[0-9]");
                    if (matchL.Success)
                    {
                        BHSelectDevice(DevId);
                    }
                }

                if (dev.Periph == 2)
                {
                    USBPRTDEV usbdev = new USBPRTDEV();

                    BHSelectDevice(dev.ParentId);
                    BHSelectDevice(dev.DevId);
                }
            }
        }
        public static void BHDeselectAllDevice()
        {
            int DevId;
            for (DevId = 0; ; DevId++)
            {
                BHDEVsimple dev = new BHDEVsimple();
                dev.DevId = DevId;

                if (!_BHGetDevice(ref dev)) break;

                BHDeselectDevice(DevId);
            }
        }


        // Deselect Device
        public static bool BHDeselectDevice(int DeviceID)
        {
            BHDEVsimple dev = new BHDEVsimple();
            dev.DevId = DeviceID;
            return _BHDeselectDevice(ref dev);
        }

        public static bool BHCaptureOn(int MaxPhaseVal = 16384, int CBytes = 524288)
        {
            return _BHCaptureOn(MaxPhaseVal, CBytes);
            //return _BHCaptureOn(16384, 512 * 1024);
        }

        public static bool BHCaptureOff()
        {
            return _BHCaptureOff();
        }

        public static bool BHIsSelected(int DeviceID)
        {
            BHDEVsimple dev = new BHDEVsimple();
            dev.DevId = DeviceID;
            if (_BHGetDevice(ref dev))
            {
                if (dev.Selected == 1)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public static int message_idx;
        public static Dictionary<int, string> message_data = new Dictionary<int,string>(); 
        public static void updateToServer()
        {
            // DataStatus.Default, failed(0, 2)인 녀석만 업로드
            // 이 중에서 마지막 인덱스이면서 0인 녀석만 response를 서버에 올리기
        }

        public static void splitStreamData()
        {
            bool hasEnd = FundaParser.hasEndData(strStreamData);

            if (hasEnd)
            {
                string[] arrrr = FundaParser.cutterStr.Split('/');
                foreach (string s in arrrr)
                {
                    int tt;
                    tt = 0;
                    String[] splitData;
                    try
                    {
                        splitData = Regex.Split(strStreamData, s);
                    }
                    catch (Exception eee)
                    {
                        continue;
                    }
                    if (splitData.Length <= 1) continue;

                    int i;
                    for (i = 0; i < splitData.Length - 1; i++)
                    {
                        if (splitData[i].Length <= 1) continue;
                        message_data[message_idx] = splitData[i];
                        message_idx++;
                    }
                    // 151117 andy disable sqlite
                    /*
                    Program.mSqliteWriter.sunhotest = message_data[message_idx - 1];
                    */
                    strStreamData = splitData[splitData.Length - 1];
                }
            }
        }


        public static void UpdateToServerNoUseSqlite()
        {
            while (true)
            {
                if (message_data.Count <= 0) break;

                try
                {
                    int target_key = -1010;
                    String target_value = "";
                    foreach (KeyValuePair<int, String> data in message_data)
                    {
                        target_key = data.Key;
                        target_value = data.Value;
                        break;
                    }
                    if (target_key == -1010) continue;
                    target_value.Trim();
                    message_data.Remove(target_key);
                    if (target_value == "") continue;
                    fifi.PublishRawData(target_value);
                }
                catch (Exception ee)
                {
                    System.Console.WriteLine("서버에 업로드 하는데 문제가 생겼습니다.");
                }
//                     try
//                     {
//                         bool flagForMainIndex = false;
//                         try
//                         {
//                             string raw_data_for_get;
// 
//                             string orig_data_from_db;
//                             orig_data_from_db = data.Value;
//                             fifi.PublishRawData(orig_data_from_db);
// 
//                         }
//                         catch (Exception eee)
//                         {
//                         }
// 
// 
//                         // SQL TABLE의 mainIdx 
// 
//                     }
//                     catch (Exception ee)
//                     {
//                         //UpdateStatusFlag(presentIdx, SqliteWriter.DataStatus.UploadFailed); 
//                     }
//                 }
            }
        }


        public static bool BHCaptureData(int DeviceID, ref Array capturedData)
        {
            BHData Data = new BHData();
            BHval Val = new BHval();
            bool retVal = _BHDumpData(ref Data, ref Val);


            if (retVal & Data.Length > 0)
            {
                BHbaudrate baudrate = new BHbaudrate();
                baudrate.portName = portNameArray[Val.DevId];
                _BHGetBaudrate(ref baudrate); // 가끔 이상한 값 올라옴 andy 

                // config에 기록한다.

                
                
                
                /*
                foreach (int eachbaud in allowed_baud_rate)
                {
                    if (eachbaud == baudrate.baudRate)
                    {
                        if (Program.mPortMonitor.ini  == null) break;
                        Program.mPortMonitor.ini.IniWriteValue("SYSTEM", "assumed_comport", baudrate.portName);
                        Program.mPortMonitor.ini.IniWriteValue("SYSTEM", "assumed_baudrate", baudrate.baudRate.ToString());
                        break;
                    }
                }
                 */
                

                assumed_com_port =  baudrate.portName;
                assumed_baud_rate =      baudrate.baudRate;
                if (assumed_com_port.Contains("COM"))
                {
                    capturedData = new byte[Data.Length];
                    
                    Array.Copy(Data.Dumpdata, capturedData, Data.Length);

                    lock (streamLock)
                    {
                        String outputForPrinter = "";
                        
                        strStreamData += System.Text.Encoding.Default.GetString(Data.Dumpdata, 0, Data.Length);
                        splitStreamData();
                        UpdateToServerNoUseSqlite();
                        //saveLocalDB();
                        
                        // 나원준
                        // 여기서 서버에 올려야함~~

                        //Program.mSqliteWriter.UpdateToServer(ref outputForPrinter);
                        System.Console.WriteLine(outputForPrinter);
                    }

                    if (display_sample_data.Length + Data.Length > 1000)
                    {
                        try
                        {
                            display_sample_data = display_sample_data.Substring(display_sample_data.Length + Data.Length - 1000);
                        }
                        catch (Exception ee)
                        {
                            display_sample_data = "";
                        }
                    }

                    // 화면 디스플레이용
                    for (int i = 0; i < Data.Length; i++)
                    {
                        if (Data.Dumpdata[i] >= 32 && Data.Dumpdata[i] <= 127)
                        {
                            display_sample_data += ((char)Data.Dumpdata[i]).ToString();
                        }
                    }
                }
            }
            else
            {
                retVal = false;
            }

            return retVal;
        }

        public static bool BHIsMonitoring()
        {
            return _BHIsModeRun();
        }
        public static String display_sample_data = "";
        public static String assumed_com_port = "";
        public static int assumed_baud_rate = -1;
        public static String strStreamData = "";
    }
}
