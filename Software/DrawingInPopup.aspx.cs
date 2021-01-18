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
    public partial class DrawingInPopup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HiddenField1.Value = ".PC";
            string DrawingType = Request.QueryString["DrawingType"];
            string Article = Request.QueryString["Article"];
            string OrderNo = Request.QueryString["OrderNo"];

            if (OrderNo.Contains("("))
            {
                OrderNo = OrderNo.Substring(0, OrderNo.LastIndexOf(" "));
            }
            //else
            //{
            //    OrderNo = OrderNo;
            //}

            //DrawingType = ".PC";
            if (Article.Length > 6)
            {
                string pdfpath="";
                if (DrawingType == ".PC" || DrawingType == ".PT")
                {
                    pdfpath = "W:\\test\\Access\\Planos\\" + Article.Substring(0, 6) + "\\" + Article + DrawingType + ".pdf";
                }
                if (DrawingType == ".pdwg")
                {
                    pdfpath = "W:\\Software\\Orders\\" + OrderNo + "\\PrevDrawings\\" + Article + ".PC.pdf";
                }
                string path = pdfpath;
                if (File.Exists(path))
                {
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
                    //path = "~/Images/UIDimage/New.pdf";
                    path = "W:\\test\\Access\\Planos\\draft.pdf";
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
}