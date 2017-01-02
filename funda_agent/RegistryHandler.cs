using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace funda_agent
{
    public class RegistryHandler
    {
        public static void SetModeAsDevelopment()
        {
            RegistryHandler.WriteRegistry("Mode", "Development");
        }
        public static void SetModeAsNormal()
        {
            RegistryHandler.WriteRegistry("Mode", "");
        }
        public static void SetDBFromWepass(String store_id)
        {
            RegistryHandler.WriteRegistry("use_wepass", store_id);
        }
        public static void SetDBFromFunda()
        {
            RegistryHandler.WriteRegistry("use_wepass", "");
        }
        public static void WriteRegistry(string key, string value)
        {
            //키 생성하기
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey("FundaAgent", RegistryKeyPermissionCheck.ReadWriteSubTree);

            //값 저장하기
            regKey.SetValue(key, value, RegistryValueKind.String);
        }
        public static void WriteRegistry(string key, Int32 value)
        {
            //키 생성하기
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey("FundaAgent", RegistryKeyPermissionCheck.ReadWriteSubTree);

            //값 저장하기
            regKey.SetValue(key, (Int32) value, RegistryValueKind.DWord);
        }


        public static bool IsDBFromFunda()
        {
            if (ReadRegistryAsString("use_wepass") == "") return true;
            return false;
        }
        public static String GetWepassStoreID()
        {
            return ReadRegistryAsString("use_wepass");
        }

        public static string ReadRegistryAsString(string regVal)
        {
            RegistryKey reg = Registry.CurrentUser;
            reg = reg.OpenSubKey("FundaAgent", true);

            // TEST못 열면
            if (reg == null)
                return "";

            // 값 있으면
            if (null != reg.GetValue(regVal))
            {
                return Convert.ToString(reg.GetValue(regVal));
            }
            else
            {
                // 값 없으면
                return "";
            }
        }

        public static Int32 ReadRegistryAsInt32(string regVal)
        {
            RegistryKey reg = Registry.LocalMachine;
            reg = reg.OpenSubKey("FundaAgent", true);

            // TEST못 열면
            if (reg == null)
                return 0;

            // 값 있으면
            if (reg.GetValueKind(regVal) == RegistryValueKind.DWord)
            {
                if (null != reg.GetValue(regVal))
                {
                    return Int32.Parse(reg.GetValue(regVal).ToString());
                }
                else
                {
                    // 값 없으면
                    return 0;
                }
            }
            return 0;
        }



        public static void DeleteRegistry()
        {
            Registry.LocalMachine.DeleteSubKey("FundaAgent");
        }

    }
}
