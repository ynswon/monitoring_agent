using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace funda_smartcon1
{
    class iniUtil
    {
        private string iniPath;

        public iniUtil(string path)
        {
            this.iniPath = path;  //INI 파일 위치를 생성할때 인자로 넘겨 받음
        }

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(    // GetIniValue 를 위해
            String section,
            String key,
            String def,
            StringBuilder retVal,
            int size,
            String filePath);



        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(  // SetIniValue를 위해
            String section,
            String key,
            String val,
            String filePath);


        // INI 값을 읽어 온다. 
        public String GetIniValue(String Section, String Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, iniPath);
            return temp.ToString();
        }

        // INI 값을 셋팅
        public void SetIniValue(String Section, String Key, String Value)
        {
            WritePrivateProfileString(Section, Key, Value, iniPath);
        }


        public void WriteRegistry(string key, string val)
        {
            //키 생성하기
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey("SoftWare").CreateSubKey("funda");

            //값 저장하기
            regKey.SetValue(key, val, RegistryValueKind.String);
        }



        public string ReadRegistry(string key)
        {
            RegistryKey reg = Registry.CurrentUser.CreateSubKey("SoftWare").CreateSubKey("funda");

            // TEST못 열면
            if (reg == null)
                return "";

            // 값 있으면
            if (null != reg.GetValue(key))
            {
                return Convert.ToString(reg.GetValue(key));
            }
            else
            {
                // 값 없으면
                return "";
            }
        }



        public void DeleteRegistry()
        {
            Registry.LocalMachine.DeleteSubKey("Software\\funda");
        }

    }
}
