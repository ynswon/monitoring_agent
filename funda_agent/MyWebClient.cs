using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Collections;
using System.Collections.Specialized;

using System.Xml;

using System.Net;
using System.Diagnostics;


using System.Threading;

using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace funda_agent
{ 

    public class MyWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            w.Timeout = 20 * 1000;
            return w;
        }
    }

}
