using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Net;
using System.IO;
using System.Drawing;
using System.Web.Services;
using System.Web.Script.Services;
using System.Text.RegularExpressions;

namespace Software
{
    public partial class MyPage : System.Web.UI.Page
    {
        String connectionString;
        static string staticConnectionString;
        OleDbConnection conn;
        OleDbDataReader dr = null;
        String DrawingType;
        DataTable dtArtOrd = new DataTable();
        public String nothing = "00.00.00";
			
        List<string> uidlist = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; DATA SOURCE=D:\\Tablas.mdb";
            staticConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; DATA SOURCE=D:\\Tablas.mdb";
            conn = new OleDbConnection(connectionString);
            //locationtxt.BackColor = Color.White;
            //locationtxt.BorderWidth = 1;
            
            uidtxtbox.Focus();
            uidtxtbox.Attributes["onFocus"] = "this.select()";

            string file = @"W://Software/Sugam/WorkFlow Status/.starredUid/uid.txt";

            if (File.Exists(file) == true)
            {
                FileInfo info = new FileInfo(file);
                if (info.Length > 0)
                {
                    string[] uids = File.ReadAllLines(file);

                    foreach (string uiddata in uids)
                    {
                        string uid = uiddata.Split(' ').GetValue(0).ToString();
                        if (uid != "")
                        {
                            OleDbConnection con = new OleDbConnection(connectionString);
                            OleDbCommand cmd = new OleDbCommand("SELECT NumOrd, FinOrd, Location FROM [Ordenes de fabricación] WHERE FinOrd IS NULL AND NumOrd = " + uid, con);
                            con.Open();
                            OleDbDataReader dr = cmd.ExecuteReader();

                            if (dr.HasRows == true)
                            {
                                while (dr.Read())
                                {
                                    uidlist.Add(dr["NumOrd"].ToString() + " " + dr["Location"].ToString());
                                }
                            }
                            con.Close();
                        }
                    }

                    if (File.Exists(file) == true)
                        File.Delete(file);

                    if (File.Exists(file) == false)
                    {
                        var myFile = File.Create(file);
                        myFile.Close();
                        if (uidlist.Count() > 0)
                        {
                            foreach (String uidData in uidlist)
                            {
                                File.AppendAllText(@file, uidData + "\n");
                                var hideFile = new DirectoryInfo(file);
                                hideFile.Attributes = FileAttributes.Hidden;
                            }
                        }
                    }

                }
            }

            if (!IsPostBack)
            {
                randomlbl.Text = "0";
                //PcPbBtn.Text = "PC";
                if (Request.QueryString["workFlowUid"] != null)
                {
                    uidtxtbox.Text = Request.QueryString["workFlowUid"].ToString();
                    search(uidtxtbox.Text);
                }

            }		
			    
        }
        static int count = 0;
        public void gridView1Data(String id)
        {
            OleDbCommand command = new OleDbCommand("SELECT NumOrd, CodPie, ProPie, PlaPie, PieFin, PreAce from [Ordenes de fabricación (piezas)] where NumOrd=" + id);
            command.Connection = conn;
            conn.Open();
            OleDbDataAdapter olda = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            olda.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            conn.Close();          
            
        }


        public void gridView2Data(String id)            //Green Table
        {
            OleDbCommand command = new OleDbCommand("SELECT NumOrd, NumFas, CodPie, CanPie, FecAlbExt, CodProExt, PrePieExt, OrderDate from [Ordenes de fabricación (historia/exterior)] where NumOrd=" + id);
            command.Connection = conn;
            conn.Open();
            OleDbDataAdapter olda = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            olda.Fill(dt);
            GridView2.DataSource = dt;
            GridView2.DataBind();
            conn.Close();

        }
        

        public void gridView3Data(String id)
        {
            try
            {
                OleDbCommand command = new OleDbCommand("SELECT NumOrd, NumFas, HorIni, HorFin, CodPer, CodMaq, CanPie from [Ordenes de fabricación (historia/taller)] where NumOrd=" + id);
                command.Connection = conn;
                conn.Open();
                OleDbDataAdapter olda1 = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                olda1.Fill(dt);
                GridView3.DataSource = dt;
                GridView3.DataBind();
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());

                //string message = string.Format("Message: {0}\\n\\n", ex.Message);
                //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
                //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            }
        }

        public void gridView4Data(String id)
        {
            try
            {
                OleDbCommand command = new OleDbCommand("SELECT  [Pedidos a proveedor (líneas)].NumFas,[Pedidos a proveedor (líneas)].PrePie,[Pedidos a proveedor (líneas)].Codpie,[Pedidos a proveedor (líneas)].NumPed,[Pedidos a proveedor (cabeceras)].FecPed,[Pedidos a proveedor (cabeceras)].ProPed,[Proveedores].NomPro,[Pedidos a proveedor (líneas)].PlaPie,[Pedidos a proveedor (líneas)].PrePie,[Pedidos a proveedor (líneas)].PiePed,[Pedidos a proveedor (líneas)].PieRec FROM (( [Pedidos a proveedor (líneas)] INNER JOIN [Pedidos a proveedor (cabeceras)]   ON  [Pedidos a proveedor (líneas)].NumPed = [Pedidos a proveedor (cabeceras)].NumPed) INNER JOIN [Proveedores] ON [Pedidos a proveedor (cabeceras)].ProPed = [Proveedores].CodPro ) WHERE [Pedidos a proveedor (líneas)].NumOrd = " + id);
                command.Connection = conn;
                conn.Open();
                OleDbDataAdapter olda1 = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                olda1.Fill(dt);
                GridView4.DataSource = dt;
                GridView4.DataBind();
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());

                //string message = string.Format("Message: {0}\\n\\n", ex.Message);
                //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
                //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            }
        }

        public void gridView5Data(String id)        //orange table
        {
            try
            {
                conn.Close();
                OleDbCommand command = new OleDbCommand("SELECT CodPie, Nompie, MatPie, CalPie, DurPie, DiaExt, Longit, DiaInt, ModPie from [Artículos de clientes (piezas)] where CodArt='" + id + "'");
                command.Connection = conn;
                conn.Open();
                OleDbDataAdapter olda1 = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                olda1.Fill(dt);
                GridView5.DataSource = dt;
                GridView5.DataBind();
                conn.Close();
            }
            catch (Exception ex)
            {
                //Response.Write(ex.ToString());

                string message = string.Format("Message: {0}\\n\\n", ex.Message);
                message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
                message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            }
        }

               

        public string pdfpath { get; set; }


        /*--------------    Make Empty textbox Function End here   ----------------*/

        public void emptyTxtbox()
        {
            worles_POtxt.Text = "";
            ptnumtxt.Text = "";
            orderdatetxt.Text = "";
            deliverydatetxt.Text = "";
            startdatetxt.Text = "";
            enddatetxt.Text = "";
            quantitytxt.Text = "";
            deliveredquantitytxt.Text = "";
            articletxt.Text = "";
            datostxt.Text = "";
            pendingqtytxt.Text = "";
            dtotxt.Text = "";
            ratetxt.Text = "";
            pointstxt.Text = "";
            locationtxt.Text = "";
            custreftxt.Text = "";
            descriptiontxt.Text = "";
            refsteelcuttxt.Text = "";
            custdrawingtxt.Text = "";
            markingonpiecetxt.Text = "";
            observationtxt.Text = "";
        }

        /*--------------    Make Empty textbox Function End here   ----------------*/



        
        /*--------------    Uid Search Function Start here  ----------------*/

        public void search(String id)
        {
            if (uidtxtbox.Text != "")
            {
                try
                {                  
                    conn.Close();
                    uidtxtbox.Text = id;
                    gridView1Data(uidtxtbox.Text);
                    conn.Close();
                    gridView2Data(uidtxtbox.Text);
                    conn.Close();
                    gridView3Data(uidtxtbox.Text);
                    conn.Close();
                    gridView4Data(uidtxtbox.Text);
                    conn.Close();
                    conn.Open();
                    OleDbCommand command = conn.CreateCommand();
                    if (CheckBox1.Checked==true)
                    {
                        command = new OleDbCommand("SELECT [Ordenes de fabricación].NumOrd, [Ordenes de fabricación].PinOrd, [Ordenes de fabricación].EntOrd, [Ordenes de fabricación].LanOrd, [Ordenes de fabricación].FinOrd, [Ordenes de fabricación].PieOrd, [Ordenes de fabricación].EntCli, [Ordenes de fabricación].ArtOrd, [Ordenes de fabricación].Datos, [Ordenes de fabricación].DtoOrd, [Ordenes de fabricación].PreOrd, [Ordenes de fabricación].MarPie, [Ordenes de fabricación].RefCorte, [Ordenes de fabricación].PlaOrd, [Ordenes de fabricación].Observaciones, [Ordenes de fabricación].Location, [Pedidos de clientes].FecPed, [Pedidos de clientes].PedPed, [Pedidos de clientes].WorlesPo, [Pedidos de clientes].OcDate, [Artículos de clientes].NomArt FROM (([Ordenes de fabricación] INNER JOIN [Pedidos de clientes] ON [Ordenes de fabricación].PinOrd = [Pedidos de clientes].NumPed) INNER JOIN [Artículos de clientes] ON [Ordenes de fabricación].ArtOrd = [Artículos de clientes].CodArt)  WHERE ([Ordenes de fabricación].NumOrd = " + uidtxtbox.Text + " and [Ordenes de fabricación].ArtOrd = '" + articletxt.Text + "')", conn);
                    }
                    else
                    {
                        command = new OleDbCommand("SELECT [Ordenes de fabricación].NumOrd, [Ordenes de fabricación].PinOrd, [Ordenes de fabricación].EntOrd, [Ordenes de fabricación].LanOrd, [Ordenes de fabricación].FinOrd, [Ordenes de fabricación].PieOrd, [Ordenes de fabricación].EntCli, [Ordenes de fabricación].ArtOrd, [Ordenes de fabricación].Datos, [Ordenes de fabricación].DtoOrd, [Ordenes de fabricación].PreOrd, [Ordenes de fabricación].MarPie, [Ordenes de fabricación].RefCorte, [Ordenes de fabricación].PlaOrd, [Ordenes de fabricación].Observaciones, [Ordenes de fabricación].Location, [Pedidos de clientes].FecPed, [Pedidos de clientes].PedPed, [Pedidos de clientes].WorlesPo, [Pedidos de clientes].OcDate, [Artículos de clientes].NomArt FROM (([Ordenes de fabricación] INNER JOIN [Pedidos de clientes] ON [Ordenes de fabricación].PinOrd = [Pedidos de clientes].NumPed) INNER JOIN [Artículos de clientes] ON [Ordenes de fabricación].ArtOrd = [Artículos de clientes].CodArt)  WHERE [Ordenes de fabricación].NumOrd = " + uidtxtbox.Text + "", conn);
                    }

                    //OleDbCommand command = new OleDbCommand("SELECT [Ordenes de fabricación].NumOrd,[Ordenes de fabricación].PinOrd, [Pedidos de clientes].WorlesPo, [Pedidos de clientes].FecPed, [Pedidos de clientes].OcDate, [Ordenes de fabricación].EntOrd, [Ordenes de fabricación].LanOrd, [Ordenes de fabricación].FinOrd, [Ordenes de fabricación].PieOrd, [Ordenes de fabricación].EntCli, [Ordenes de fabricación].ArtOrd, [Ordenes de fabricación].Datos, [Ordenes de fabricación].DtoOrd, [Ordenes de fabricación].PreOrd, [Ordenes de fabricación].Location, [Pedidos de clientes].PedPed, [Artículos de clientes].NomArt, [Ordenes de fabricación].RefCorte, [Ordenes de fabricación].PlaOrd, [Ordenes de fabricación].Observaciones FROM [Pedidos de clientes] INNER JOIN ([Artículos de clientes] INNER JOIN [Ordenes de fabricación] ON [Artículos de clientes].[CodArt] = [Ordenes de fabricación].[ArtOrd]) ON [Pedidos de clientes].[NumPed] = [Ordenes de fabricación].[PinOrd] where NumOrd=" + id);
                    command.Connection = conn;

                    dr = command.ExecuteReader();
                    emptyTxtbox();
                    if (dr.Read() == true)
                    {

                        DateTime orderdate = Convert.ToDateTime((dr["FecPed"].ToString()));
                        orderdatetxt.Text = (orderdate.ToString("dd-MM-yyyy"));

                        worles_POtxt.Text = (dr["WorlesPo"].ToString());

                        if (dr["OcDate"].ToString() != "")
                        {
                            ptnumtxt.BackColor = Color.Lime; //green
                            ptnumtxt.BorderWidth = 1;
                        }
                        else
                        {
                            ptnumtxt.BackColor = Color.Red;
                            ptnumtxt.BorderWidth = 1;
                        }

                        if (dr["OcDate"].ToString() != "")
                        {
                            ptnumtxt.Text = (dr["PinOrd"].ToString()) + " (" + Convert.ToDateTime(dr["OcDate"]).ToString("dd-MM-yyyy") + ")";
                        }
                        else
                        {
                            ptnumtxt.Text = dr["PinOrd"].ToString();
                        }

                        DateTime deliverydate = Convert.ToDateTime((dr["EntOrd"].ToString()));
                        deliverydatetxt.Text = (deliverydate.ToString("dd-MM-yyyy"));

                        if (id != "1")
                        {
                            if (dr["LanOrd"].ToString() != "")
                            {
                                DateTime startdate = Convert.ToDateTime((dr["LanOrd"].ToString()));
                                startdatetxt.Text = (startdate.ToString("dd-MM-yyyy"));
                            }
                            else
                            {
                                startdatetxt.Text = "";
                            }
                        }
                        else
                        {
                            startdatetxt.Text = "";
                        }
                        if (dr["FinOrd"].ToString() != "")
                        {
                            DateTime enddate = Convert.ToDateTime((dr["FinOrd"].ToString()));
                            enddatetxt.Text = (enddate.ToString("dd-MM-yyyy"));
                        }
                        quantitytxt.Text = (dr["PieOrd"].ToString());
                        deliveredquantitytxt.Text = (dr["EntCli"].ToString());
                        articletxt.Text = (dr["ArtOrd"].ToString());
                        datostxt.Text = (dr["Datos"].ToString());
                        long Qty = Convert.ToInt64((dr["PieOrd"].ToString()));
                        long DeliQty = Convert.ToInt64((dr["EntCli"].ToString()));

                        if ((Qty - DeliQty) < 0)
                        {
                            pendingqtytxt.Text = "0";
                        }
                        else
                        {
                            pendingqtytxt.Text = (Qty - DeliQty).ToString();
                        }

                        if (dr["DtoOrd"].ToString() != "")
                        {
                            dtotxt.Text = (dr["DtoOrd"].ToString() + "%");
                        }

                        ratetxt.Text = (dr["PreOrd"].ToString());

                        Double rate = Convert.ToDouble(ratetxt.Text);
                        Double dto=0.0;
                        if (dr["DtoOrd"].ToString() != "")
                        {
                            dto = Convert.ToDouble((dr["DtoOrd"].ToString()));
                        }
                        else
                        {
                            dtotxt.Text = dto.ToString();
                        }
                        Double temp = rate * DeliQty;
                        Double r = (temp * dto) / 100;
                        Double points = temp - r;
                        pointstxt.Text = points.ToString("0.00");

                        

                        if ((dr["Location"].ToString()) != "")
                        {
                            if ((dr["Location"].ToString()) == "1")
                            {
                                locationtxt.BackColor = Color.Purple;
                                locationtxt.Text = "AWS1";
                                locationtxt.ForeColor = Color.White;
                                PageBody.Attributes.Add("bgcolor", "#D2691E");
                                form1.Style["BackGround-Color"] = "#D2691E";
                            }
                            if ((dr["Location"].ToString()) == "2")
                            {
                                locationtxt.BackColor = Color.Orange;
                                locationtxt.Text = "AWS2";
                                locationtxt.ForeColor = Color.White;
                                PageBody.Attributes.Add("bgcolor", "#A52A2A");
                                form1.Style["BackGround-Color"] = "#A52A2A";
                            }
                        }
                        else
                        {
                            locationtxt.BackColor = Color.Red;
                            locationtxt.Text = "N/A";
                            locationtxt.ForeColor = Color.White;
                        }
                        custreftxt.Text = (dr["PedPed"].ToString());
                        descriptiontxt.Text = (dr["NomArt"].ToString());
                        refsteelcuttxt.Text = (dr["RefCorte"].ToString());
                        custdrawingtxt.Text = (dr["PlaOrd"].ToString());
                        markingonpiecetxt.Text = (dr["PlaOrd"].ToString()) + " W " + id;
                        observationtxt.Text = (dr["Observaciones"].ToString());

                        

                        //if (articletxt.Text != "")
                        //{
                        //    drawingtypetxt.Text = ".PC";
                        //    myframe.Attributes.Add("src", "DrawingInPopup.aspx?UID=" + uidtxtbox.Text + "&OrderNo=" + ptnumtxt.Text + "&Article=" + articletxt.Text + "&DrawingType=" + drawingtypetxt.Text + "&testdrawing= kkk");
                        //}

                        gridView5Data(articletxt.Text.ToString());
                        conn.Close();

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error:- UID NUMBER NOT FOUND!...');", true);

                        //emptyTxtbox();
                    }
                    if (ptnumtxt.Text != "")
                    {
                        CheckPreviousDrawing();
                        //btnPC_Click(null, null);
                        //ptbtn_Click(null, null);
                        //form1.Attributes.Add("bgcolor", "yellow");
                        if (datostxt.Text.Contains("?"))
                        {
                            form1.Style["BackGround-Color"] = "#FFD700";
                        }
                        else
                        {
                            form1.Attributes.Remove("style");
                        }
                        string pdfpath = "W:\\Technical Section\\Casing\\" + refsteelcuttxt.Text + ".pdf";
                        if (!File.Exists(pdfpath))
                        {
                            //lnkbtnRefStillCut.Text = System.Drawing.Color.Red;
                            RefSteelCutLink.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ff0040");
                        }
                        else
                        {
                            RefSteelCutLink.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0000FF");
                        }
						
						foreach (string uidvalue in uidlist)
						{
							string uid = uidvalue.Split(' ').GetValue(0).ToString();

							if (uidtxtbox.Text == uid)
							{
								LinkButton1.Style.Add("Color", "yellow");
                                break;
							}
							else
							{
								LinkButton1.Style.Add("Color", "white");
							}
						}
                    }

                    PcPbBtn_Click(null, null);
                }
                catch (Exception ex)
                {
                    string message = string.Format("Message: {0}\\n\\n", ex.Message);
                    message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                    message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
                    message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);            
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error:- UID NUMBER NOT FOUND!...');", true);                
            }
        }                  

        /*--------------    Uid Search Function End   ----------------    */
        void CheckPreviousDrawing()
        {
            try
            {
                string OrderNo;
                if (ptnumtxt.Text.Contains("("))
                {
                    OrderNo = ptnumtxt.Text.Substring(0, ptnumtxt.Text.LastIndexOf(" "));
                }
                else
                {
                    OrderNo = ptnumtxt.Text;
                }
                string Article = articletxt.Text;
                if (Article.Length > 6)
                {
                    string pdfpath = "W:\\Software\\Orders\\" + OrderNo + "\\PrevDrawings\\" + Article + ".PC.pdf";
                    checkpathpdf.Text = pdfpath;
                    if (File.Exists(pdfpath))
                    {
                        pdwgbtn.Visible = true;
                    }
                    else
                    {
                        pdwgbtn.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                //Response.Write(ex.ToString());

                string message = string.Format("Message: {0}\\n\\n", ex.Message);
                message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
                message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            }
        }


        /*--------------    Article Number Search Function Start   ----------------    */


        public void articlesearch(string artnum)
        {
            
            if (articletxt.Text != "")
            {
                try
                {
                    OleDbCommand command = new OleDbCommand("SELECT Top 1 [Ordenes de fabricación].NumOrd FROM [Artículos de clientes] INNER JOIN [Ordenes de fabricación] ON [Artículos de clientes].[CodArt] = [Ordenes de fabricación].[ArtOrd] where CodArt='" + articletxt.Text + "' ORDER BY [Ordenes de fabricación].NumOrd DESC");
                    command.Connection = conn;
                    conn.Open();
                    dr = command.ExecuteReader();
                    if (dr.Read() == true)
                    {
                        uidtxtbox.Text = (dr["NumOrd"].ToString());
                        search(uidtxtbox.Text);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error:- UID Number Not Found!...');", true);
                        emptyTxtbox();
                        articletxt.Text = (Convert.ToInt64(artnum) + 1).ToString();

                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error:- Article Number Not Found!...');", true);

                    //string message = string.Format("Message: {0}\\n\\n", ex.Message);
                    //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                    //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
                    //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error:- Article Number Not Found!...');", true);
            }
            
        }

        /*--------------    Article Number Search Function End   ----------------    */


        public void custRefSearch(String s)
        {
            if (custreftxt.Text != "")
                {
                    try
                    {
                        OleDbCommand command = new OleDbCommand("SELECT Top 1 [Ordenes de fabricación].NumOrd FROM [Pedidos de clientes] INNER JOIN [Ordenes de fabricación] ON [Pedidos de clientes].[NumPed] = [Ordenes de fabricación].[PinOrd] where PedPed='"+custreftxt.Text+"' Order By NumOrd asc;");
                        command.Connection = conn;
                        conn.Open();
                        dr = command.ExecuteReader();
                        if (dr.Read() == true)
                        {
                            uidtxtbox.Text = (dr["NumOrd"].ToString());
                            search(uidtxtbox.Text);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error:- UID Number Not Found!...');", true);
                            emptyTxtbox();
                            //articletxt.Text = (Convert.ToInt64(artnum) + 1).ToString();

                        }
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error:- Cust. Reference Number Not Found!...');", true);

                        //string message = string.Format("Message: {0}\\n\\n", ex.Message);
                        //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                        //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
                        //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error:- Cust. Reference Number Not Found!...');", true);
                }
            
            }

        protected void btnP_DWG_Click(object sender, EventArgs e)
        {
            try
            {
                string OrderNo;
                if (ptnumtxt.Text.Contains("("))
                {
                    OrderNo = ptnumtxt.Text.Substring(0, ptnumtxt.Text.LastIndexOf(" "));
                }
                else
                {
                    OrderNo = ptnumtxt.Text;
                }
                //string ORDERNO = "17326";
                string Article = articletxt.Text;
                //string Article = "48480100307";
                if (Article.Length > 6)
                {
                    string pdfpath = "W:\\Software\\Orders\\" + OrderNo + "\\PrevDrawings\\" + Article + ".PC.pdf";
                    checkpathpdf.Text = pdfpath;
                    //Label1.Text = pdfpath;
                    //Label1.Text = pdfpath;
                    drawingtypetxt.Text = ".pdwg";
                    myframe.Attributes.Add("src", "DrawingInPopup.aspx?UID=" + uidtxtbox.Text + "&OrderNo=" + ptnumtxt.Text + "&Article=" + articletxt.Text + "&DrawingType=" + drawingtypetxt.Text + "&testdrawing= kkk");
                    //if (File.Exists(pdfpath))
                    //{
                    //    PdftoIMG(pdfpath);
                    //    ImageButton1.ImageUrl = "~/Images/UIDimage/New.jpg";
                    //}
                    //else
                    //{
                    //    ImageButton1.ImageUrl = "~/Images/draft.jpg";

                    //}
                    
                }
            }
            catch (Exception ex)
            {
                //Response.Write(ex.ToString());

                string message = string.Format("Message: {0}\\n\\n", ex.Message);
                message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
                message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            }
        }

        public void custDrwaingSearch(String s)
        {
            if (custdrawingtxt.Text != "")
            {
                try
                {
                    OleDbCommand command = new OleDbCommand("SELECT Top 1 NumOrd FROM  [Ordenes de fabricación] where PlaOrd='"+custdrawingtxt.Text+"' Order By NumOrd DESC;");
                    command.Connection = conn;
                    conn.Open();
                    dr = command.ExecuteReader();
                    if (dr.Read() == true)
                    {
                        uidtxtbox.Text = (dr["NumOrd"].ToString());
                        search(uidtxtbox.Text);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error:- UID Number Not Found!...');", true);
                        emptyTxtbox();
                        //articletxt.Text = (Convert.ToInt64(artnum) + 1).ToString();

                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error:- Cust. Drawing Number Not Found!...');", true);

                    //string message = string.Format("Message: {0}\\n\\n", ex.Message);
                    //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                    //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
                    //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error:- Cust. Drawing Number Not Found!...');", true);
            }

        }





        /*--------------    Uid Search button Function Start   ----------------    */        

        protected void searchbtn_Click(object sender, EventArgs e)
        { 
           search(uidtxtbox.Text);
           
        }

        /*--------------    Uid Search button Function End   ----------------    */




        /*--------------    Uid Previous button Function Start   ----------------    */

        protected void prevbtn_Click(object sender, EventArgs e)
        {
            if (uidtxtbox.Text != "")
            {
                if (CheckBox1.Checked == true)
                {
                    checkbox();
                    count--;
                    if (count <= dtArtOrd.Rows.Count - 1 && count != -1)
                    {
                        uidtxtbox.Text = dtArtOrd.Rows[count][0].ToString();
                        ////HiddenField1.Value = (Convert.ToInt32(HiddenField1.Value) + 1).ToString();
                        search(uidtxtbox.Text);
                        ////HiddenField2.Value = "0";
                    }
                    else
                    {
                        string script = "alert('There is No More UID For this Article!!!')";
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(prevbtn, this.GetType(), "Test1", script, true);
                        count++;
                    }

                }
                else
                {
                    long uid = Convert.ToInt64(uidtxtbox.Text);
                    uid = uid - 1;
                    search(uid.ToString());
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error:- PLEASE ENTER UID NUMBER!...');", true);
            }
        }
        /*--------------    Uid Previous button Function End   ----------------    */

        void checkbox()
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=W:\test\access\Tablas.mdb");
            OleDbCommand command = new OleDbCommand("SELECT NumOrd FROM [Ordenes de fabricación] WHERE ArtOrd = '" + articletxt.Text + "' ORDER BY [Ordenes de fabricación].NumOrd Desc", con);
            OleDbDataAdapter oleda = new OleDbDataAdapter(command);
            oleda.Fill(dtArtOrd);
        }


        /*--------------    Uid Next button Function Start   ----------------    */

        protected void nextbtn_Click(object sender, EventArgs e)
        {
            if (uidtxtbox.Text != "")
            {
                if (CheckBox1.Checked == true)
                {  
                    checkbox();
                    count++;
                    if (count <= dtArtOrd.Rows.Count - 1)
                    {
                        uidtxtbox.Text = dtArtOrd.Rows[count][0].ToString();
                        ////HiddenField1.Value = (Convert.ToInt32(HiddenField1.Value) + 1).ToString();
                        search(uidtxtbox.Text);
                        ////HiddenField2.Value = "0";
                    }
                    else
                    {
                        string script = "alert('There is No More UID For this Article!!!')";
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(nextbtn, this.GetType(), "Test", script, true);
                        count--;
                    }

                }
                else 
                {
                    long uid = Convert.ToInt64(uidtxtbox.Text);
                    uid = uid + 1;
                    search(uid.ToString());
                }
                
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error:- PLEASE ENTER UID NUMBER!...');", true);
            }           
        }
        /*--------------    Uid Next button Function End   ----------------    */



        /*--------------    PC Pdf Loading button Function Start   ----------------    */


        protected void PcPbBtn_Click(object sender, EventArgs e)
        {
            if (PcPbBtn.Text == "PC")
            {
                PcPbBtn.Text = "PT";

                drawingtypetxt.Text = ".PC";
                string userAgent = Request.ServerVariables["HTTP_USER_AGENT"];
                Regex OS = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                Regex device = new System.Text.RegularExpressions.Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                string device_info = string.Empty;
                if (OS.IsMatch(userAgent))
                {
                    device_info = OS.Match(userAgent).Groups[0].Value;
                }
                if (device.IsMatch(userAgent.Substring(0, 4)))
                {
                    device_info += device.Match(userAgent).Groups[0].Value;
                }
                if (!string.IsNullOrEmpty(device_info))
                {
                    //Response.Redirect("Mobile.aspx?device_info=" + device_info);
                    drawingtypetxt.Text = ".PC";
                    //Label7.Text = DrawingType;
                    myframe.Attributes.Add("src", "MobilePage.aspx?UID=" + uidtxtbox.Text + "&Article=" + articletxt.Text + "&DrawingType=" + drawingtypetxt.Text + "&testdrawing= kkk");
                }
                else if (articletxt.Text != "")
                {
                    string article = articletxt.Text;

                    myframe.Attributes.Add("src", "DrawingInPopup.aspx?UID=" + uidtxtbox.Text + "&OrderNo=" + ptnumtxt.Text + "&Article=" + articletxt.Text + "&DrawingType=" + drawingtypetxt.Text + "&testdrawing= kkk");
                    //pdfpath = "/333312/"+articletxt.Text + ".PC" + ".pdf";
                    checkpathpdf.Text = "W:\\test\\Access\\Planos\\" + article.Substring(0, 6) + "\\" + article + ".PC.pdf"; ;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error:- PLEASE ENTER UID NUMBER!...');", true);
                }
            }
            else
            {
                PcPbBtn.Text = "PC";

                if (articletxt.Text != "")
                {
                    string article = articletxt.Text;
                    drawingtypetxt.Text = ".PT";
                    myframe.Attributes.Add("src", "DrawingInPopup.aspx?UID=" + uidtxtbox.Text + "&OrderNo=" + ptnumtxt.Text + "&Article=" + articletxt.Text + "&DrawingType=" + drawingtypetxt.Text + "&testdrawing= kkk");
                    checkpathpdf.Text = "W:\\test\\Access\\Planos\\" + article.Substring(0, 6) + "\\" + article + ".PT.pdf"; ;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error:- PLEASE ENTER UID NUMBER!...');", true);
                }
            }
        }


        /*--------------    PC Pdf Loading button Function End   ----------------    */



        /*--------------    PT Pdf Loading button Function Start   ----------------    */  

        //protected void ptbtn_Click(object sender, EventArgs e)
        //{
            
        //}

        /*--------------    PT Pdf Loading button Function End   ----------------    */


        protected void download_click(object sender, EventArgs e)
        {
            //if (articletxt.Text != "")
            //{
            //    string Drawing = TextBox19.Text + " W " + TextBox8.Text;
            //    string pdfpath = "W:\\test\\Access\\Planos\\" + articletxt.Text.Substring(0, 6) + "\\" + articletxt.Text + ".PC.pdf";
                
            //    if (File.Exists(pdfpath))
            //    {
            //        Response.ContentType = "application/pdf";
            //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + custdrawingtxt.Text + ".pdf");
            //        Response.TransmitFile(pdfpath);
            //        Response.End();
            //    }
            //}
            string Drawing = custdrawingtxt.Text + " W " + uidtxtbox.Text;
            //if(Drawing.Contains())
            try
            {

                string Article = articletxt.Text;
                if (Article.Length > 6)
                {

                    //string pdfpath = "W:\\test\\Access\\Planos\\" + Article.Substring(0, 6) + "\\" + Article + ".PC.pdf";
                    string pdfpath = checkpathpdf.Text;
                    if (File.Exists(pdfpath))
                    {
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Drawing + ".pdf");
                        Response.TransmitFile(pdfpath);
                        Response.End();
                    }

                    else
                    {
                        // Image1.ImageUrl = "~/Images/draft.jpg";

                    }
                }
            }
            catch (Exception ex)
            {
                //Response.Write(ex.ToString());

                string message = string.Format("Message: {0}\\n\\n", ex.Message);
                message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
                message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            }
        }


        //protected void worles_POtxt_TextChanged(object sender, EventArgs e)
        //{
        //    if (TextBox22.Text == "")
        //    {
        //        string script = "alert('Please Enter UID!!!')";
        //        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Button3, this.GetType(), "Test", script, true);
        //    }
        //    else
        //    {
        //        DataTable dt = new DataTable();
        //        //currency_check(dt);
        //        System.Data.OleDb.OleDbDataAdapter da;
        //        OleDbCommand oleDbCmd = con.CreateCommand();
        //        oleDbCmd = new OleDbCommand("SELECT TOP 1 [Ordenes de fabricación].NumOrd, [Ordenes de fabricación].PinOrd, [Ordenes de fabricación].EntOrd, [Ordenes de fabricación].LanOrd, [Ordenes de fabricación].FinOrd, [Ordenes de fabricación].PieOrd, [Ordenes de fabricación].EntCli, [Ordenes de fabricación].ArtOrd, [Ordenes de fabricación].Datos, [Ordenes de fabricación].DtoOrd, [Ordenes de fabricación].PreOrd, [Ordenes de fabricación].MarPie, [Ordenes de fabricación].RefCorte, [Ordenes de fabricación].PlaOrd, [Ordenes de fabricación].Observaciones, [Ordenes de fabricación].Location, [Pedidos de clientes].FecPed, [Pedidos de clientes].PedPed, [Pedidos de clientes].WorlesPo, [Artículos de clientes].NomArt FROM (([Ordenes de fabricación] INNER JOIN [Pedidos de clientes] ON [Ordenes de fabricación].PinOrd = [Pedidos de clientes].NumPed) INNER JOIN [Artículos de clientes] ON [Ordenes de fabricación].ArtOrd = [Artículos de clientes].CodArt)  WHERE (([Pedidos de clientes].WorlesPo) = '" + TextBox22.Text + "') ORDER BY [Ordenes de fabricación].NumOrd Asc", con);
        //        //OleDbDataReader reader;
        //        con.Open();
        //        reader = oleDbCmd.ExecuteReader();
        //        UIDDetails();
        //        con.Close();
        //    }
        //}




        protected void seeocbtn_click(object sender, EventArgs e)
        {
            string OrderNo;
            if (ptnumtxt.Text.Contains("("))
            {
                OrderNo = ptnumtxt.Text.Substring(0, ptnumtxt.Text.LastIndexOf(" "));
            }
            else
            {
                OrderNo = ptnumtxt.Text;
            }
            string Drawing = custdrawingtxt.Text;
            string Article = articletxt.Text;
            string pdfpath = "W:\\Software\\Orders\\" + OrderNo + "\\" + "OC." + OrderNo + ".pdf";
            if (File.Exists(pdfpath))
            {
                string strPopup = "<script language='javascript' ID='script1'>"

                     // Passing intId to popup window.
                     + "window.open('SeeOC.aspx?UID=" + Drawing + "&OrderNo=" + OrderNo + "&testdrawing= kkk" + "data=" + HttpUtility.UrlEncode("UID=" + Drawing + "&Article=" + Article + "&testdrawing= kkk")

                     + "','new window', 'top=70, left=250, width=470, height=590, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

                     + "</script>";

                ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
            }
            else
            {
                string script = "alert('OC Not Available!!!')";
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(seeocbtn, this.GetType(), "Test", script, true);
                //Response.Write("<script>alert('PT'S Not Available!!!');</script>");
            }
        }

        
        protected void newTab_click(object sender, EventArgs e)
        {
            string Drawing = custdrawingtxt.Text;
            string Article = articletxt.Text;
            string OrderNo = ptnumtxt.Text;
            //Label7.Text = DrawingType;

            int rand = Convert.ToInt16(randomlbl.Text);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('DrawingInPopup.aspx?UID=" + Drawing + "&Article=" + Article + "&DrawingType=" + drawingtypetxt.Text + "&OrderNo=" + OrderNo + "','[" + rand + "]');", true);

            randomlbl.Text = (rand + 1).ToString();
            
            
        }

        





        /*--------------    PT_Num button Function Start   ----------------    */  

        protected void ptnumtxt_TextChanged(object sender, EventArgs e)
        {
            if (ptnumtxt.Text != "")
            {
                try
                {
                    long ptnum = Convert.ToInt64(ptnumtxt.Text);
                    OleDbCommand command = new OleDbCommand("SELECT TOP 1 [Ordenes de fabricación].NumOrd from [Ordenes de fabricación] INNER JOIN [Pedidos de clientes] ON [Pedidos de clientes].NumPed=[Ordenes de fabricación].PinOrd where NumPed=" + ptnum + " Order By [Ordenes de fabricación].NumOrd asc");
                    command.Connection = conn;
                    conn.Open();
                    dr = command.ExecuteReader();
                    if (dr.Read() == true)
                    {
                        uidtxtbox.Text = (dr["NumOrd"].ToString());
                        search(uidtxtbox.Text);
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error:- PT Number Not Found!...');", true);

                    //string message = string.Format("Message: {0}\\n\\n", ex.Message);
                    //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                    //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
                    //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        /*--------------    PT_Num button Function End   ----------------    */




        /*--------------    Article_Number Function Start   ----------------    */  
        
        protected void articletxt_TextChanged(object sender, EventArgs e)
        {
            articlesearch(articletxt.Text);
        }

        /*--------------    Article_Number Function End   ----------------    */




        /*--------------    Article Back Number Function Start   ----------------    */  


        protected void back_Click(object sender, EventArgs e)
        {
            if (articletxt.Text != "")
            {
                long artnum = Convert.ToInt64(articletxt.Text);
                artnum = artnum - 1;                
                articletxt.Text = artnum.ToString();
                articlesearch(articletxt.Text);                
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error:- Article Number Not Found!...');", true);
            }
        }


        /*--------------    Article Back Number Function End   ----------------    */




        /*--------------    Article Next Number Function Start   ----------------    */  


        protected void forth_Click(object sender, EventArgs e)
        {
            if (articletxt.Text != "")
            {
                long artnum = Convert.ToInt64(articletxt.Text);
                artnum = artnum + 1;
                articletxt.Text = artnum.ToString();
                articlesearch(articletxt.Text);
            }   
        }

        /*--------------    Article Next Number Function End   ----------------    */


        protected void pbtnbtn_Click(object sender, EventArgs e)
        {
                GridView1.Visible = true;
                GridView5.Visible = true;
                gridView1Data(uidtxtbox.Text);
                gridView5Data(articletxt.Text);
                GridView2.Visible = false;
                GridView3.Visible = false;
                GridView4.Visible = false;
        }
        protected void esbtnbtn_Click(object sender, EventArgs e)
        {
            GridView2.Visible = true;
            gridView2Data(uidtxtbox.Text);
            GridView1.Visible = false;
            GridView3.Visible = false;
            GridView4.Visible = false;
            GridView5.Visible = false;
        }

        protected void pendingbtn_Click(object sender, EventArgs e)
        {
            GridView4.Visible = true;
            gridView4Data(uidtxtbox.Text);
            GridView1.Visible = false;
            GridView3.Visible = false;
            GridView2.Visible = false;
            GridView5.Visible = false;
        }

        protected void internalstepsbtn_Click(object sender, EventArgs e)
        {
            GridView3.Visible = true;
            gridView3Data(uidtxtbox.Text);
            GridView1.Visible = false;
            GridView4.Visible = false;
            GridView2.Visible = false;
            GridView5.Visible = false;
        }
        protected void viewall_Click(object sender, EventArgs e)
        {
            GridView3.Visible = true;
            GridView1.Visible = true;
            GridView4.Visible = true;
            GridView2.Visible = true;
            GridView5.Visible = true;
            search(uidtxtbox.Text);
        }

        protected void custreftxt_TextChanged(object sender, EventArgs e)
        {
            custRefSearch(custreftxt.Text);
        }

        protected void custdrawingtxt_TextChanged(object sender, EventArgs e)
        {
            custDrwaingSearch(custdrawingtxt.Text);
        }

        protected void worles_POLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://mail.google.com/mail/u/0/#search/PO" + worles_POtxt.Text);
        }

        protected void custreflink_Click(object sender, EventArgs e)
        {
            var s = custreftxt.Text;
            var firstSpaceIndex = s.IndexOf(" ");
            var firstString = s.Substring(0, firstSpaceIndex); // INAGX4
            string secondString = s.Substring(firstSpaceIndex);
            Response.Redirect("https://mail.google.com/mail/u/0/#search/" + firstString);
        }

        protected void ptnumlink_click(object sender, EventArgs e)
        {
            string OrderNo;
            if (ptnumtxt.Text.Contains("("))
            {
                OrderNo = ptnumtxt.Text.Substring(0, ptnumtxt.Text.LastIndexOf(" "));
            }
            else
            {
                OrderNo = ptnumtxt.Text;
            }
            string Drawing = uidtxtbox.Text;
            string Article = articletxt.Text;
            //string OrderNo = Request.QueryString["OrderNo"];
            string pdfpath = "W:\\Technical Section\\Manish\\PT's\\" + OrderNo + ".pdf";
            if (File.Exists(pdfpath))
            {
                string strPopup = "<script language='javascript' ID='script1'>"

                     // Passing intId to popup window.
                     + "window.open('SeePT.aspx?UID=" + Drawing + "&OrderNo=" + OrderNo + "&testdrawing= kkk" + "data=" + HttpUtility.UrlEncode("UID=" + Drawing + "&Article=" + Article + "&testdrawing= kkk")

                     + "','new window', 'top=70, left=250, width=470, height=590, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

                     + "</script>";

                ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
            }
            else
            {
                string script = "alert('PT'S Not Available!!!')";
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(ptnumlink, this.GetType(), "Test", script, true);
                //Response.Write("<script>alert('PT'S Not Available!!!');</script>");
            }
        }


        protected void lnkbtnRefStillCut_Click(object sender, EventArgs e)
        {
            string RefStillCut = refsteelcuttxt.Text;
            string Drawing = uidtxtbox.Text;
            string Article = articletxt.Text;
            //string OrderNo = Request.QueryString["OrderNo"];
            string pdfpath = "W:\\Technical Section\\Casing\\" + RefStillCut + ".pdf";
            if (File.Exists(pdfpath))
            {
                string strPopup = "<script language='javascript' ID='script1'>"

                     // Passing intId to popup window.
                     + "window.open('RefSteelCut.aspx?UID=" + Drawing + "&RefStillCut=" + RefStillCut + "&testdrawing= kkk" + "data=" + HttpUtility.UrlEncode("UID=" + Drawing + "&Article=" + Article + "&testdrawing= kkk")

                     + "','new window', 'top=70, left=250, width=470, height=590, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

                     + "</script>";

                ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
            }
            else
            {
                string script = "alert('RefStillCut Drawing Not Available!!!')";
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(RefSteelCutLink, this.GetType(), "Test", script, true);
                //Response.Write("<script>alert('PT'S Not Available!!!');</script>");
            }
        }




        [WebMethod]
        public static List<ListItem> GetCustRef(string prefix)
        {
            List<ListItem> custref = new List<ListItem>();
            
            OleDbConnection con = new OleDbConnection(staticConnectionString);
            OleDbCommand cmd = new OleDbCommand("SELECT top 10 PedPed FROM [Pedidos de clientes] WHERE PedPed Like  + @custref + '%' Order By PedPed Desc", con);
            cmd.Parameters.AddWithValue("@custref", prefix);
            con.Open();
            OleDbDataReader odr = cmd.ExecuteReader();
            while (odr.Read())
            {
                custref.Add(new ListItem { Text = odr["PedPed"].ToString().Trim() });
            }
            con.Close();

            return custref;
        }

        [WebMethod]
        public static List<ListItem> GetCustDrawing(string prefix)
        {
            List<ListItem> custdraw = new List<ListItem>();
            //string str = "Provider=Microsoft.ACE.OLEDB.12.0;DATA SOURCE=W:\\test\\Access\\Tablas.mdb;Persist Security Info = False; ";
            OleDbConnection con = new OleDbConnection(staticConnectionString);
            OleDbCommand cmd = new OleDbCommand("SELECT top 10 PlaOrd FROM [Ordenes de fabricación] WHERE PlaOrd Like + @custdraw + '%' Order By PlaOrd Desc", con);
            cmd.Parameters.AddWithValue("@custdraw", prefix);
            con.Open();
            OleDbDataReader odr = cmd.ExecuteReader();
            while (odr.Read())
            {
                custdraw.Add(new ListItem { Text = odr["PlaOrd"].ToString().Trim() });
            }
            con.Close();

            return custdraw;
        }

        [WebMethod]
        public static List<ListItem> GetPtNum(string prefix)
        {
            List<ListItem> PtNum = new List<ListItem>();
            //string str = "Provider=Microsoft.ACE.OLEDB.12.0;DATA SOURCE=W:\\test\\Access\\Tablas.mdb;Persist Security Info = False; ";
            OleDbConnection con = new OleDbConnection(staticConnectionString);
            OleDbCommand cmd = new OleDbCommand("SELECT top 10 NumPed FROM [Pedidos de clientes] WHERE PedPed Like + @PtNum + '%' Order By NumPed Desc", con);
            cmd.Parameters.AddWithValue("@PtNum", prefix);
            con.Open();
            OleDbDataReader odr = cmd.ExecuteReader();
            while (odr.Read())
            {
                PtNum.Add(new ListItem { Text = odr["NumPed"].ToString().Trim() });
            }
            con.Close();

            return PtNum;
        }

        [WebMethod]
        public static List<ListItem> ArticleNumber(string prefix)
        {
            List<ListItem> ArticleNum = new List<ListItem>();
            //string str = "Provider=Microsoft.ACE.OLEDB.12.0;DATA SOURCE=W:\\test\\Access\\Tablas.mdb;Persist Security Info = False; ";
            OleDbConnection con = new OleDbConnection(staticConnectionString);
            OleDbCommand cmd = new OleDbCommand("SELECT top 10 CodArt FROM [Artículos de clientes] WHERE CodArt Like + @ArticleNum + '%' Order By CodArt Desc", con);
            cmd.Parameters.AddWithValue("@ArticleNum", prefix);
            con.Open();
            OleDbDataReader odr = cmd.ExecuteReader();
            while (odr.Read())
            {
                ArticleNum.Add(new ListItem { Text = odr["CodArt"].ToString().Trim() });
            }
            con.Close();

            return ArticleNum;
        }

        [WebMethod]
        public static List<ListItem> GetUidTxt(string prefix)
        {
            List<ListItem> UidNum = new List<ListItem>();
            //string str = "Provider=Microsoft.ACE.OLEDB.12.0;DATA SOURCE=W:\\test\\Access\\Tablas.mdb;Persist Security Info = False; ";
            OleDbConnection con = new OleDbConnection(staticConnectionString);
            OleDbCommand cmd = new OleDbCommand("SELECT top 10 NumOrd FROM [Ordenes de fabricación] WHERE NumOrd Like + @UidNum + '%' Order By NumOrd  Desc", con);
            cmd.Parameters.AddWithValue("@UidNum", prefix);
            con.Open();
            OleDbDataReader odr = cmd.ExecuteReader();
            while (odr.Read())
            {
                UidNum.Add(new ListItem { Text = odr["NumOrd"].ToString().Trim() });
            }
            con.Close();

            return UidNum;
        }

        [WebMethod]
        public static List<ListItem> GetWorlesPoTxt(string prefix)
        {
            List<ListItem> WorlesPoNum = new List<ListItem>();
            //string str = "Provider=Microsoft.ACE.OLEDB.12.0;DATA SOURCE=W:\\test\\Access\\Tablas.mdb;Persist Security Info = False; ";
            OleDbConnection con = new OleDbConnection(staticConnectionString);
            OleDbCommand cmd = new OleDbCommand("select top 10 WorlesPo from [Pedidos de clientes] where WorlesPo Like + @worlesPo + '%' Order By WorlesPo Desc", con);
            cmd.Parameters.AddWithValue("@worlesPo", prefix);
            con.Open();
            OleDbDataReader odr = cmd.ExecuteReader();
            while (odr.Read())
            {
                WorlesPoNum.Add(new ListItem { Text = odr["WorlesPo"].ToString().Trim() });
            }
            con.Close();

            return WorlesPoNum;
        }

        protected void worles_POtxt_TextChanged(object sender, EventArgs e)
        {
            if (worles_POtxt.Text!= "")
            {
                try
                {
                    OleDbCommand command = new OleDbCommand("SELECT TOP 1 [Ordenes de fabricación].NumOrd, [Ordenes de fabricación].PinOrd, [Ordenes de fabricación].EntOrd, [Ordenes de fabricación].LanOrd, [Ordenes de fabricación].FinOrd, [Ordenes de fabricación].PieOrd, [Ordenes de fabricación].EntCli, [Ordenes de fabricación].ArtOrd, [Ordenes de fabricación].Datos, [Ordenes de fabricación].DtoOrd, [Ordenes de fabricación].PreOrd, [Ordenes de fabricación].MarPie, [Ordenes de fabricación].RefCorte, [Ordenes de fabricación].PlaOrd, [Ordenes de fabricación].Observaciones, [Ordenes de fabricación].Location, [Pedidos de clientes].FecPed, [Pedidos de clientes].PedPed, [Pedidos de clientes].WorlesPo, [Artículos de clientes].NomArt FROM (([Ordenes de fabricación] INNER JOIN [Pedidos de clientes] ON [Ordenes de fabricación].PinOrd = [Pedidos de clientes].NumPed) INNER JOIN [Artículos de clientes] ON [Ordenes de fabricación].ArtOrd = [Artículos de clientes].CodArt)  WHERE [Pedidos de clientes].WorlesPo ='" + worles_POtxt.Text + "' ORDER BY [Ordenes de fabricación].NumOrd Asc");
                    command.Connection = conn;
                    conn.Open();
                    dr = command.ExecuteReader();
                    if (dr.Read() == true)
                    {
                        uidtxtbox.Text = (dr["NumOrd"].ToString());
                        search(uidtxtbox.Text);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error:- UID Number Not Found!...');", true);
                        emptyTxtbox();
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error:- Worles Po Not Found!...');", true);

                    //string message = string.Format("Message: {0}\\n\\n", ex.Message);
                    //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                    //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
                    //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Error:- Worles Po Not Found!...');", true);
            }
            
        }

        protected void MarkAsStar_Click(Object sender, EventArgs e)
        {            
            string filepath = @"W://Software/Sugam/WorkFlow Status/.starredUid/uid.txt";
            Boolean isUidExist = false;

            if (uidtxtbox.Text != "")
            {
                if (File.Exists(filepath) == false)
                {
                    var file = File.Create(filepath);
                    file.Close();
                }

                if (File.Exists(filepath))
                {
                    FileInfo info = new FileInfo(filepath);
                    string loc = "";
                    if (info.Length > 0)
                    {
                        if (uidtxtbox.Text != "")
                        {
                            string[] starreduid = File.ReadAllLines(filepath);

                            foreach (string uidvalue in starreduid)
                            {
                                string[] uids = uidvalue.Split(' ');
                                if (uids[0] == uidtxtbox.Text)
                                {
                                    isUidExist = true;
                                }
                            }

                            if (isUidExist == false)
                            {

                                if (locationtxt.Text == "AWS1")
                                {
                                    loc = "1";
                                }
                                if (locationtxt.Text == "AWS2")
                                {
                                    loc = "2";
                                }

                                File.AppendAllText(@filepath, "\n"+uidtxtbox.Text + " " + loc + "\n");
                                var hideFile = new DirectoryInfo(filepath);
                                hideFile.Attributes = FileAttributes.Hidden;


                                //Response.Write("<script>alert('" + uid[0] + " marked as star')</script>");
                            }
                        }
                    }
                    else
                    {
                        if (locationtxt.Text == "AWS1")
                        {
                            loc = "1";
                        }
                        if (locationtxt.Text == "AWS2")
                        {
                            loc = "2";
                        }
                        File.AppendAllText(@filepath, "\n"+uidtxtbox.Text + " " + loc + "\n");
                        var hideFile = new DirectoryInfo(filepath);
                        hideFile.Attributes = FileAttributes.Hidden;
                        //Response.Write("<script>alert('" + uid[0] + " marked as star')</script>");                    
                    }
                }

                LinkButton1.Style.Add("Color", "yellow");

            }
            else
            {
                Response.Write("<script>alert('Please Enter UID Number...!');</script>");
            }
        }

        protected void seeStarUids_Click(Object sender, EventArgs e)
        {
            string strPopup = "<script language='javascript' ID='script1'>" + "window.open('StarUID.aspx','new window', 'top=70, left=250, width=470, height=590, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')" + "</script>";

                ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
            
            

        }
    }
}