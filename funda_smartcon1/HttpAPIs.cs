using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Text.RegularExpressions;


namespace funda_smartcon1
{
    class PostParams
    {
        public PostParams(String argument, String data)
        {
            _argument = argument;
            _data = data;
        }
    
        private String _argument;
        public String Argument
        {
            get { return _argument; }
            set { _argument = value; }
        }
        private String _data;
        public String Data
        {
            get { return _data; }
            set { _data = value; }
        }
    }

    class HttpAPIs
    {
        public String PostServerResponse(String url, String postParams)
        {
            Uri address = new Uri(url); // real

            try
            {
                //
                postParams = "data=" + postParams;
                Encoding encoding = Encoding.UTF8;
                byte[] data = encoding.GetBytes(postParams);

                //Create the web request
                HttpWebRequest _request = WebRequest.Create(address) as HttpWebRequest;

                _request.UserAgent = "Wepass POS Client";
                _request.Method = "POST";
                _request.ContentType = "application/x-www-form-urlencoded";
                _request.ContentLength = data.Length;

                Stream requestStream = _request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();

                HttpWebResponse myHttpWebResponse = (HttpWebResponse)_request.GetResponse();
                Stream responseStream = myHttpWebResponse.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);

                string result = myStreamReader.ReadToEnd();

                myStreamReader.Close();
                responseStream.Close();

                myHttpWebResponse.Close();

                return result;
            }
            catch (Exception e)
            {
                //LogWriter.WriteLine(e.StackTrace.ToString() + url + postParams);
            }
            finally
            {

            }
            return null;
        }

        public String PostServerResponse(String url, ArrayList postParamsArray)
        {
            Uri address = new Uri(url); // real

            try
            {
                String postParamsVal = String.Empty;
                String postParamsValGen = String.Empty;
                foreach (PostParams pParam in postParamsArray)
                {
                    postParamsValGen += pParam.Argument + "=" + pParam.Data + "&";
                }
                postParamsValGen = postParamsValGen.Substring(0, postParamsValGen.Length - 1);
                postParamsVal = Regex.Replace(postParamsValGen, " ", "%20", RegexOptions.Compiled);

                //Encoding encoding = Encoding.UTF8;
                int euckrCodepage = 51949;
                System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
                System.Text.Encoding euckr = System.Text.Encoding.GetEncoding(euckrCodepage);
 
                //byte[] data = euckr.GetBytes(postParamsVal);
                byte[] data = utf8.GetBytes(postParamsVal);

                //Create the web request
                HttpWebRequest _request = WebRequest.Create(address) as HttpWebRequest;

                _request.UserAgent = "Wepass POS Client";
                _request.Method = "POST";
                _request.ContentType = "application/x-www-form-urlencoded";
                _request.ContentLength = data.Length;

                Stream requestStream = _request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();

                HttpWebResponse myHttpWebResponse = (HttpWebResponse)_request.GetResponse();
                Stream responseStream = myHttpWebResponse.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(responseStream, Encoding.UTF8);

                string result = myStreamReader.ReadToEnd();
                
                myStreamReader.Close();
                responseStream.Close();

                myHttpWebResponse.Close();

                return result;
            }
            catch (Exception e)
            {
                //LogWriter.WriteLine(e.StackTrace.ToString() + "msg" + e.Message.ToString() );
            }
            finally
            {

            }
            return null;
        }

        public String GetServerResponse(Uri address)
        {
            string result = "";

            try
            {
                //Create the web request
                HttpWebRequest _request = WebRequest.Create(address) as HttpWebRequest;

                //Cache 정책
                //HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                //_request.CachePolicy = noCachePolicy;

                /*테스트
                byte[] byteArray = Encoding.UTF8.GetBytes(param);
                Stream dataStream = _request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();*/

                // Get response  
                using (HttpWebResponse response = _request.GetResponse() as HttpWebResponse)
                {
                    // Get the response stream  
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

                    // Read the whole contents and return as a string  
                    result = reader.ReadToEnd();

                    response.Close();
                    reader.Close();
                }

            }
            catch (Exception e)
            {

                //LogWriter.WriteLine(e.StackTrace.ToString()+"msg"+e.Message.ToString());
            }
            finally
            {

            }

            return result;
        }

        
    }

    static class StaticHttpAPIs
    {
        public static String GetSvrResponse(Uri address)
        {
            String result = "";

            try
            {
                //Create the web request
                HttpWebRequest _request = WebRequest.Create(address) as HttpWebRequest;

                // Get response  
                using (HttpWebResponse response = _request.GetResponse() as HttpWebResponse)
                {
                    // Get the response stream  
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

                    // Read the whole contents and return as a string  
                    result = reader.ReadToEnd();

                    response.Close();
                    reader.Close();
                }

            }
            catch (Exception e)
            {
                //LogWriter.WriteLine(e.StackTrace.ToString());
            }
            finally
            {

            }

            return result;
        }
    }

    public class UploadFile
    {
        public UploadFile()
        {
            ContentType = "application/octet-stream";
        }
        public string Name { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public Stream Stream { get; set; }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[32768];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        public static byte[] UploadFiles(string address, IEnumerable<UploadFile> files, NameValueCollection values)
        {
            var request = WebRequest.Create(address);
            request.Method = "POST";
            var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            boundary = "--" + boundary;

            try
            {
                using (var requestStream = request.GetRequestStream())
                {
                    // Write the values
                    foreach (string name in values.Keys)
                    {
                        var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                        requestStream.Write(buffer, 0, buffer.Length);
                        buffer = Encoding.ASCII.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", name, Environment.NewLine));
                        requestStream.Write(buffer, 0, buffer.Length);
                        buffer = Encoding.UTF8.GetBytes(values[name] + Environment.NewLine);
                        requestStream.Write(buffer, 0, buffer.Length);
                    }

                    // Write the files
                    foreach (var file in files)
                    {
                        var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                        requestStream.Write(buffer, 0, buffer.Length);
                        buffer = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", file.Name, file.Filename, Environment.NewLine));
                        requestStream.Write(buffer, 0, buffer.Length);
                        buffer = Encoding.ASCII.GetBytes(string.Format("Content-Type: {0}{1}{1}", file.ContentType, Environment.NewLine));
                        requestStream.Write(buffer, 0, buffer.Length);
                        //file.Stream.CopyTo(requestStream);
                        CopyStream(file.Stream, requestStream);
                        buffer = Encoding.ASCII.GetBytes(Environment.NewLine);
                        requestStream.Write(buffer, 0, buffer.Length);
                    }

                    var boundaryBuffer = Encoding.ASCII.GetBytes(boundary + "--");
                    requestStream.Write(boundaryBuffer, 0, boundaryBuffer.Length);
                }

                using (var response = request.GetResponse())
                using (var responseStream = response.GetResponseStream())
                using (var stream = new MemoryStream())
                {
                    //responseStream.CopyTo(stream);
                    CopyStream(responseStream, stream);
                    return stream.ToArray();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
