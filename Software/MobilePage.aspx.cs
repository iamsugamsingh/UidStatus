using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Software
{
    public partial class MobilePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Visible = false;
            if (!IsPostBack)
            {
                LoadDrawing();
            }

        }
        void LoadDrawing()
        {
            try
            {
                string Article = Request.QueryString["Article"];
                string DrawingType = Request.QueryString["DrawingType"];

                //string Article = "34340100553";
                //string DrawingType = ".PC";
                if (Article.Length > 6)
                {
                    string pdfpath = "W:\\test\\Access\\Planos\\" + Article.Substring(0, 6) + "\\" + Article + DrawingType + ".pdf";
                    Label1.Text = pdfpath;
                    if (File.Exists(pdfpath))
                    {
                        PdftoIMG(pdfpath);
                        //string outputpath = Server.MapPath("~/Images/UIDimage/New.jpg");
                        //string ghostScriptPath = Server.MapPath("~/Images/gs904w32.exe");
                        //PdfToJpg(ghostScriptPath,pdfpath, outputpath);
                        Image1.ImageUrl = "~/images/UIDimage/New.jpg";
                        //Iframe.Attributes.Add("src", "Access/Planos/" + TextBoxArticle.Text.Substring(0, 6) + "/" + TextBoxArticle.Text + ".PC.pdf");
                    }
                    else
                    {
                        Image1.ImageUrl = "~/images/draft.jpg";
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
        void PdftoIMG(string pdfpath)
        {
            var filePath = Server.MapPath("~/images/UIDimage/New.jpg");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            Spire.Pdf.PdfDocument pdfdocument = new Spire.Pdf.PdfDocument();
            pdfdocument.LoadFromFile(pdfpath);
            //for (int i = 0; i < pdfdocument.Pages.Count; i++)
            //{
            System.Drawing.Image image = pdfdocument.SaveAsImage(0, 96, 96);
            image.Save(string.Format(Server.MapPath("~/images/UIDimage/New.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg));
            //}  
        }
    }
}