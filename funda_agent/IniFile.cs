﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace funda_agent
{
    public class IniFile
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <param name="INIPath"></param>
        public IniFile(string INIPath)
        {
            path = INIPath;
        }
        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <param name="Section"></param>
        /// Section name
        /// <param name="Key"></param>
        /// Key Name
        /// <param name="Value"></param>
        /// Value Name
        public void IniWriteValue(string Section, string Key, string Value)
        {
            String stacktrace = Environment.StackTrace;

            //fileWriterLog(DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond + "--" +stacktrace+ "Update config:", "log_debug.txt");
            //Log.WriteLine(DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond + "--" + stacktrace + "Update config:", "log_debug.txt");
            //LogWriter.WriteLine("Update config " + Section + " " + Key + " " + Value + ".\r\n");
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Path"></param>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key, string default_value = "")
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, default_value, temp, 255, this.path);
            return temp.ToString();

        }
    }
}
