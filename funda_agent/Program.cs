using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using funda_interface;
namespace funda_agent
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static FundaInterface my_funda = null;
        [STAThread]
        static void Main(string[] args)
        {
            bool flagMutex;
            bool _isRestart = false;
            int i;
            int _restartProcessId = -1;
            for (i = 0; i < args.Length-1; i++)
            {
                if (args[i] == "/restart")
                {
                    int.TryParse(args[i + 1], out _restartProcessId);
                }
                MessageBox.Show("program restart");
            }
            if (_isRestart)
            { 
                try
                {
                    // get old process and wait UP TO 5 secs then give up!
                    Process oldProcess = Process.GetProcessById(_restartProcessId);
                    oldProcess.WaitForExit(5000);
                }
                catch (Exception ex)
                {
                    // the process did not exist - probably already closed!
                    //TODO: --> LOG
                } 
            }

         
            Mutex m_hMutex = new Mutex(true, "MyFundaAgent", out flagMutex);
            if (flagMutex)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainDevForm()); 
                m_hMutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("프로그램이 이미 실행중입니다!", "Error!");
            }



        }


        //public static SqliteWriter mSqliteWriter;
        //public static PortMonitor   mPortMonitor;
        public static FundaReport   mFundaReport;
        public static Config mConfig;
        public static FundaReportShortcut mFundaReportShortcut;
        //public static SmartconShortcut mSmartconShortcut;
    }


}
