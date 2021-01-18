using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;

namespace Software
{
    public partial class SeeOC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string OrderNo = Request.QueryString["OrderNo"];
            string pdfpath = "W:\\Software\\Orders\\" + OrderNo + "\\" + "OC." + OrderNo + ".pdf";
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
            else
            {
                //string script = "alert('PT'S Not Available!!!')";
                //System.Web.UI.ScriptManager.RegisterClientScriptBlock(,this.GetType(), "Test", script, true);
                //Response.Write("<script>alert('PT'S Not Available!!!');</script>");
                string path = "W:\\test\\Access\\Planos\\draft.pdf";
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