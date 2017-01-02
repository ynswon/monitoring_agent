using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
//using funda_smartcon1;
using funda_interface;
namespace funda_agent
{
    public partial class MainDevForm : Form
    {
        public MainDevForm()
        {
            InitializeComponent();


            Program.my_funda = new FundaInterface();
            Program.my_funda.Initilize(
                System.Environment.CurrentDirectory + @"\funda_interface"
                );
            Program.my_funda.Start();
        }

        
        /*

        public void mPortMonitor_OnMonitorEventHandler(object sender, PortListenerEventArgs e)
        {
            System.Console.WriteLine(e.ToString());

        }

        public void mPortMonitor_OnMonitorDumpEventHandler(object sender, PortListenerEventArgs e)
        {
            //operationPort.fileWriterLog(e.Message + "\r\n", "log_dump.txt");
            // 덤프
        }
        */
        private String display_mode;
        private String display_mode_desc;
        private Boolean stealth_mode;
        private void mutex()
        { 
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;

            //Program.mPortMonitor = new PortMonitor();
            //Machine.ini =
            Machine.ini = new IniFile(System.Environment.CurrentDirectory + @"\data_pos_agent.ini");

            String code = Machine.ini.IniReadValue("STORE", "CODE");
            String apikey = Machine.ini.IniReadValue("STORE", "APIKEY");
            String str_display;
            String str_display_desc;
            bool stelth_mode;
            Console.WriteLine(code + " " + apikey);
            while (true)
            { 
                    // funda agent
                if (Program.my_funda.IsValidApiKey(code,apikey,out str_display,out str_display_desc, out stelth_mode))
                {
                    break;
                } 
                LoginForm lf = new LoginForm();
                

                lf.ShowDialog(this);

                if (lf.DialogResult == DialogResult.OK)
                {
                    if (lf.GetOperationMode() == "1")
                    {
                        Machine.ini.IniWriteValue("OPERATION", "MODE", "1");
                        Machine.ini.IniWriteValue("STORE", "CODE", lf.GetStoreCode());
                        Machine.ini.IniWriteValue("STORE", "PASSWORD", lf.GetPassword());
                    }
                    else
                    {
                        Machine.ini.IniWriteValue("OPERATION", "MODE", "0");
                        Machine.ini.IniWriteValue("STORE", "CODE", lf.GetStoreCode());
                        Machine.ini.IniWriteValue("STORE", "APIKEY", lf.GetApiKey());
                    }
                    //continue;
                    break;
                }
                else
                {                    
                    Application.Exit();                   
                    break; 
                }                
                
            }
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;


            Program.mFundaReportShortcut = new FundaReportShortcut();

            //Program.mFundaReportShortcut.UpdateOpacity(Program.mPortMonitor.ini.IniReadValue("STORE", "LOGO_TRANS", "30%"));
            Program.mFundaReport = new FundaReport();
            Program.mConfig = new Config();

            Program.mFundaReportShortcut.Show();



            if (RegistryHandler.ReadRegistryAsString("Mode") == "Development")
            {
                
            }
            else
            {
                this.ShowInTaskbar = false;
                this.WindowState = FormWindowState.Minimized;
            }
            String load_count;
            int load_count_int;

            // 151117 andy disable sqlite
            //Program.mSqliteWriter = new SqliteWriter();
            //Program.mSqliteWriter.CreateConnection();


            //load_count = Program.mPortMonitor.ini.IniReadValue("SYSTEM", "LOAD_COUNT", "0");


            //FundaParser.updateXML();

            /*
             * POS PROG -> 출력COM
             * SYSTEM의 POSPROGCOM
             * 0: AUTO
             * 1~32: COM포트 번호
             * 
             * */
//            Int16.TryParse
            try
            {
                /*
                load_count_int = int.Parse(load_count);
                 */
            }
            catch (Exception eee)
            
            {
                load_count_int = 0;
            }
            /*
            Program.mPortMonitor.ini.IniWriteValue("SYSTEM", "LOAD_COUNT", (load_count_int + 1).ToString());

            Program.mPortMonitor.initPortListener();
            Program.mPortMonitor.OnMonitorEventHandler += new PortMonitor.MonitorEventHandler(mPortMonitor_OnMonitorEventHandler);
            Program.mPortMonitor.OnMonitorDumpEventHandler += new PortMonitor.MonitorDumpEventHandler(mPortMonitor_OnMonitorDumpEventHandler);
            Program.mPortMonitor.initMonitoring();
            Program.mPortMonitor.startMonitoring();
             */
            //Hide();
        }

        /*
        public virtual void NotifyMonitorEvent(object sender, PortListenerEventArgs e)
        {
        }
         * 
         */
        private void timer1_Tick(object sender, EventArgs e)
        {
            /*
            String tt;
            tt = PortListener.strStreamData;
            tt = tt;    
             */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.mConfig.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FundaReport fr = new FundaReport();
            fr.ShowDialog();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("종료하시겠습니까?", "My Application",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    e.Cancel = false;
                    // 151117 andy disable sqlite
                    // Program.mSqliteWriter.CloseConnection();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = false;
                // 151117 andy disable sqlite
                // Program.mSqliteWriter.CloseConnection();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 151117 andy disable sqlite
            /*
            Program.mSqliteWriter.PutData("hex1", SqliteWriter.DataStatus.Default, "");
            Program.mSqliteWriter.PutData("hex2", SqliteWriter.DataStatus.UploadOK, "");
            Program.mSqliteWriter.PutData("hex3", SqliteWriter.DataStatus.UploadFailed, "");
            Program.mSqliteWriter.PutData("hex4", SqliteWriter.DataStatus.Default, "");
             */
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            String tt;
            tt = "!@#!@#!@#!@#";
            byte[] dddd = GetBytes(tt);

            //Byte[] d = FundaParser.StringToByteArray(tt);
            String dddddd = FundaParser.ByteArrayToString(dddd);
            tt = tt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String bye = "";
            // 151117 andy disable sqlite
            // Program.mSqliteWriter.UpdateToServer(ref bye);
            System.Console.WriteLine(bye);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RegistryHandler.SetModeAsDevelopment();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RegistryHandler.SetModeAsNormal();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            System.Console.WriteLine(RegistryHandler.ReadRegistryAsString("Mode"));
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void 창열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Config()).Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
