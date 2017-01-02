using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Web;
using System.Web.Util;
using System.IO;
using System.Net; 


namespace funda_agent
{
    public class SqliteWriter
    {
        public string sunhotest = "";
        public enum DataStatus : int { Default = 0, UploadOK, UploadFailed };

        public SQLiteConnection m_dbConnection = null;
        private bool CreateDataTable()
        {

            string sql = "CREATE TABLE if not exists POS_DATA( " +
            "IDX INTEGER PRIMARY KEY   AUTOINCREMENT, " +
            "HEXDUMP TEXT NOT NULL, " +
            "STATUS INT NOT NULL, " +
            "MEMO TEXT NOT NULL, " +
            "LAST_UPDATE DATETIME NOT NULL " +
            ")";


            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ee)
            {
                System.Console.WriteLine("CreateDataTable 생성 버그");
                return true;
            }
            return false;
        }
        public  bool UpdateToServer(ref string printString)
        {
            string sql = "select idx from pos_data where status = 0 or status = 2 order by idx desc limit 1";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            int mainIdx = -100100;
            while (reader.Read())
            {
                try
                {
                    mainIdx = int.Parse(reader["idx"].ToString());
                }
                catch (Exception ee)
                {
                    break;
                }
            }
            if (mainIdx != -100100)
            {
                sql = "select * from pos_data where status = 0 or status = 2 order by idx asc";
                SQLiteCommand command2 = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader2 = command2.ExecuteReader();

                printString = "";
                
                while (reader2.Read())
                {
                    int presentIdx = -1;
                    try
                    {
                        presentIdx = int.Parse(reader2["idx"].ToString());

                        bool flagForMainIndex = false;
                        if (presentIdx == mainIdx)
                        {
                            flagForMainIndex = true;
                        }



                        try
                        {
                            MyWebClient WClient = new MyWebClient();
                            string raw_data_for_get;
                            string orig_data_from_db;
                            orig_data_from_db = reader2["HEXDUMP"].ToString();
                            raw_data_for_get = System.Web.HttpUtility.UrlEncode(orig_data_from_db);


                            String callUrl = "http://14.63.215.51:880/api_fundareport/updatePosDataV2.php";

                            /*
                            String postData = String.Format("raw_data={0}&store_code={1}&api_key={2}",
                                raw_data_for_get,
                                Program.mPortMonitor.ini.IniReadValue("STORE", "CODE"),
                                Program.mPortMonitor.ini.IniReadValue("STORE", "APIKEY"));
                            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create   (callUrl);
                            // 인코딩 UTF-8
                            byte[] sendData = UTF8Encoding.UTF8.GetBytes(postData);
                            httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                            httpWebRequest.Method = "POST";
                            httpWebRequest.ContentLength = sendData.Length;
                            Stream requestStream = httpWebRequest.GetRequestStream();
                            requestStream.Write(sendData, 0, sendData.Length);
                            requestStream.Close();
                            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                            string myreturn = streamReader.ReadToEnd();
                            streamReader.Close();
                            httpWebResponse.Close();

                            System.Console.Write("return: " + myreturn);

                            String dataFromServer = ""; 
                            if (flagForMainIndex)
                            {
                                printString = dataFromServer;
                            }
                            UpdateStatusFlag(presentIdx, SqliteWriter.DataStatus.UploadOK);
                            */
                        }
                        catch (Exception eee)
                        {
                            UpdateStatusFlag(presentIdx, SqliteWriter.DataStatus.UploadFailed);
                        }


                        // SQL TABLE의 mainIdx 
                        
                    }
                    catch (Exception ee)
                    {
                        UpdateStatusFlag(presentIdx, SqliteWriter.DataStatus.UploadFailed); 
                    }
                }
            }
            mainIdx = mainIdx;
            return false;
            //    Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);

        }
        public bool UpdateStatusFlag(int index, DataStatus status)
        {
            string sql = "update POS_DATA set STATUS = @STATUS where idx = @INDEX";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            try
            {
                command.Parameters.AddWithValue("@INDEX", (int)index);
                command.Parameters.AddWithValue("@STATUS", (int)status); 
                command.ExecuteNonQuery();
            }
            catch (Exception ee)
            {
                System.Console.WriteLine("PutData 생성 버그");
                return true;
            }
            return false;

        }
        public bool PutData(string hexdump, DataStatus status, string memo = "")
        {
//             string sql = "insert into POS_DATA (HEXDUMP,STATUS,MEMO,LAST_UPDATE) values (" +
//             "'" + hexdump       + "'," +
//             "'" + (int)status   + "'," +
//             "'" + memo          + "'," +
//             " datetime('now', 'localtime')) ";


            string sql = "insert into POS_DATA (HEXDUMP,STATUS,MEMO,LAST_UPDATE) values (@HEXDUMP,@STATUS,@MEMO,datetime('now','localtime'))";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

            try
            {
                command.Parameters.AddWithValue("@HEXDUMP", hexdump);
                command.Parameters.AddWithValue("@STATUS", (int)status);
                command.Parameters.AddWithValue("@MEMO", memo);  

                command.ExecuteNonQuery();
            }
            catch (Exception ee)
            {
                System.Console.WriteLine("PutData 생성 버그");
                return true;
            }
            return false;
        }
        public void CreateConnection()
        {
            m_dbConnection = new SQLiteConnection("Data Source=FundaPosdata.db3;Version=3;");
            m_dbConnection.Open();


            string sql = "CREATE TABLE if not exists test_table (name VARCHAR(20), score INT)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ee)
            {
                System.Console.WriteLine("table 생성 버그");
            }
            CreateDataTable();
        }
        public void CloseConnection()
        {
            if (m_dbConnection == null) return;
            m_dbConnection.Close();
            m_dbConnection = null;
        }
        public void Run()
        {
            // http://blog.tigrangasparian.com/2012/02/09/getting-started-with-sqlite-in-c-part-one/
            // 
            // ### Create the database
            // SQLiteConnection.CreateFile("MyDatabase.sqlite");

            // ### Connect to the database

            // ### Create a table
            //string sql = "CREATE TABLE if not exist highscores (name VARCHAR(20), score INT)";
            //SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            //command.ExecuteNonQuery();

            // ### Add some data to the table
            // string sql = "insert into highscores (name, score) values ('Me', 3000)";
            // SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            // command.ExecuteNonQuery();
            // sql = "insert into highscores (name, score) values ('Myself', 6000)";
            // command = new SQLiteCommand(sql, m_dbConnection);
            // command.ExecuteNonQuery();
            // sql = "insert into highscores (name, score) values ('And I', 9001)";
            // command = new SQLiteCommand(sql, m_dbConnection);
            // command.ExecuteNonQuery();

            // ### select the data
            string sql = "select * from highscores order by score desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);

            Console.ReadKey();
        }
    }
}
