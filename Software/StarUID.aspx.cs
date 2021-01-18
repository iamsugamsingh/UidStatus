using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

namespace Software
{
    public partial class StarUID : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string file = @"W://Software/Sugam/WorkFlow Status/.starredUid/uid.txt";

            DataTable staruidtable = new DataTable();
            staruidtable.Columns.Add("Uid");
            staruidtable.Columns.Add("Location");

            if (File.Exists(file) == true)
            {
                FileInfo info = new FileInfo(file);
                if (info.Length > 0)
                {
                    string[] uids = File.ReadAllLines(file);

                    foreach (string uiddata in uids)
                    {
                        string uid = uiddata.Split(' ').GetValue(0).ToString();
                        string locations = uiddata.Split(' ').GetValue(1).ToString();

                        string loc = "";

                        if (locations == "1")
                        {
                            loc = "AWS 1";
                        }
                        if (locations == "2")
                        {
                            loc = "AWS 2";
                        }
                        staruidtable.Rows.Add(uid, loc);
                    }

                    GridView1.DataSource = staruidtable;
                    GridView1.DataBind();

                }
            }
            else
            {
                Response.Write("No Starred Uid Available...!");
            }
            
        }

        protected void Uid_Command(Object sender, CommandEventArgs e)
        {
            String uid = e.CommandArgument.ToString();
            Response.Write("<script>window.open('http://10.0.0.5:8989/uidstatus/MyPage.aspx?workFlowUid=" + uid + "','_blank');</script>");
        }
    }
}