/*
 * Serial/Parallel/USBPrinter Port Monitoring 
 * 
 * with Bus Hound APIs
 * 
 * */
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Collections; 
using System.Web;
using System.Net;
using System.Net.Cache;


namespace funda_interface
{
    public class PortMonitor
    {
        public delegate void MonitorEventHandler(object sender, PortListenerEventArgs e);
        public event MonitorEventHandler OnMonitorEventHandler;
        
        public IniFile ini;

        public virtual void NotifyMonitorEvent(object sender, PortListenerEventArgs e)
        {
            if (OnMonitorEventHandler != null) OnMonitorEventHandler(sender, e);
        }
        public void initPortListener(bool select_all =true, int selected_com_port = -1)
        {
            PortListener.BHCaptureOff(); 

            if (select_all)
            {
                PortListener.BHSelectAllDevice();
            }
            else
            {
                // 특정 포트만 listening 하는지 보도록
                PortListener.BHDeselectAllDevice();
                PortListener.BHSelectComport(selected_com_port);

            }
            PortListener.BHCaptureOn(16384, 512 * 1024);
        }


        public delegate void MonitorDumpEventHandler(object sender, PortListenerEventArgs e);
        public event MonitorDumpEventHandler OnMonitorDumpEventHandler;
        public virtual void NotifyDumpEvent(object sender, PortListenerEventArgs e)
        {
            if (OnMonitorDumpEventHandler != null) OnMonitorDumpEventHandler(sender, e);
        }


        // capture 중인지 아닌지 표시
        public bool isStart = false;

        // monitoring 주기
        public const Int32 mPeriod = 200;
         
        public System.Timers.Timer MonitoringTimer = new System.Timers.Timer();

        public bool initMonitoring()
        {
            if (PortListener.BHIsReady())
            {
                MonitoringTimer.Interval = mPeriod;
                MonitoringTimer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);

                return true;
            }

            return false;
        }

        //        delegate void TimerEventFiredDelegate();
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CaptureData(sender, e);
        }

        public void startMonitoring()
        {
            // 모니터링 시작
            // 타이머로 200ms마다 캡처된 데이터가 있는지 체크 시작
            MonitoringTimer.Start();
            MonitoringTimer.Enabled = true;
            this.isStart = true;
        }

        public void stopMonitoring()
        {
            // 모니터링 중지
            // 타이머 off
            MonitoringTimer.Stop();
            this.isStart = false;
        }
        // dump된 data 가 있으면 event를 발생시켜서 parsingBinaryData() 호출
        public int tt = 0;
        public void CaptureData(object sender, System.EventArgs e)
        {
            while (true)
            {
                try
                {
                    Array capturedData = null;
                    // 모니터링 중지
                    MonitoringTimer.Stop();

                    // data 받기
                    try
                    {
                        if (PortListener.BHCaptureData(0, ref capturedData))
                        {
                            // generate event
                            NotifyMonitorEvent(this, new PortListenerEventArgs(capturedData));
                            System.Console.WriteLine("-->" + tt);
                            tt++;
                        }
                    }
                    catch (Exception ee)
                    {

                    }
                    // 이부분에서 comport 데이터 모니터링 하지 않으니
                    // comport로 print 하기
                    MonitoringTimer.Start();
                }
                catch (Exception eee)
                {
                    // 모니터링 재시작
                    MonitoringTimer.Start();
                }
            }
        }

        /*
        public void setOperationPort(OperationPort op)
        {
            _operationPort = op;
        }
        */

        #region ServerResponse Check
        //public Timer ServerCommunicationCheckTimer = new Timer();
        public System.Timers.Timer ServerCommunicationCheckTimer = new System.Timers.Timer();

        public void initServerCheck()
        {
            ServerCommunicationCheckTimer.Interval = 1000 * 60 * 20; // every 20 mins.
            ServerCommunicationCheckTimer.Elapsed += new System.Timers.ElapsedEventHandler(ServerCommunicationCheckTimer_Elapsed);
            //ServerCommunicationCheckTimer.Tick += new EventHandler(ServerCommunicationCheckTimer_Tick);
            ////MonitoringTimer.Interval = mPeriod;
            ////MonitoringTimer.Tick += new EventHandler(CaptureData);
            //MonitoringTimer.Interval = mPeriod;
            //MonitoringTimer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
        }

        void ServerCommunicationCheckTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //throw new NotImplementedException();
            stopServerCheckTimer();
            //String url = "http://dev.9flava.com/webapp/proc/WepassPOSConnection.asp";
            //String ret = StaticHttpAPIs.GetSvrResponse(new Uri(url));


            startServerCheckTimer();
            //_operationPort.resetBHSetting();
        }

        public void startServerCheckTimer()
        {
            ServerCommunicationCheckTimer.Start();
        }

        public void stopServerCheckTimer()
        {
            ServerCommunicationCheckTimer.Stop();
        }

        //public void ServerCommunicationCheckTimer_Tick(object sender, System.EventArgs e)
        //{
        //    stopServerCheckTimer();
        //    String url = "http://dev.9flava.com/webapp/proc/WepassPOSConnection.asp";
        //    String ret = StaticHttpAPIs.GetSvrResponse(new Uri(url));

        //    //if (ret != "200,ok")
        //    //{
        //    //    MessageBox.Show("인터넷 연결을 확인하세요!!!");
        //    //    return;
        //    //}

        //    //Log.WriteLine(DateTime.Now.ToShortTimeString() + " " + ret);

        //    startServerCheckTimer();
        //}
        #endregion

    }
}
