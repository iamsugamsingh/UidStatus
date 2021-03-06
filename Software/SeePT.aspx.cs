﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;

namespace Software
{
    public partial class SeePT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string OrderNo = Request.QueryString["OrderNo"];
            string pdfpath = "W:\\Technical Section\\Manish\\PT's\\" + OrderNo + ".pdf";
            if (File.Exists(pdfpath))
            {
                string path = pdfpath;
                WebClient client = new WebClient();
                Byte[] buffer = client.DownloadData(path);
                if (buffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                    Response.End();
                    Response.Flush();
                }
            }
        }
    }
}