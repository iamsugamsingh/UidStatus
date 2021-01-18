using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Software
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int x = 10;
                int y = 0;
                int r=x/y;
            }
            catch(Exception ex)
            {
                string message = string.Format("Message: {0}\\n\\n", ex.Message);
                message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
                message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            }
        }
    }
}