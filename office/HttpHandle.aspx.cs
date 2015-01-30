using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class HttpHandle :PageBase 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Response.Cache.SetNoStore();
            string actType = Request["actType"];
            if (this.CurrentUser  == null)
            {
                return;
            }
           
            if (actType == "1") //punch in
            {
                BLL.LeaveBLL.InsertLeave(int.Parse(CurrentUser.ID), CurrentUser.UserName, DateTime.Now);
               
                return;
            }
            if (actType == "2") //punch in
            {
                BLL.LeaveBLL.UpdateLeave (int.Parse(CurrentUser.ID), DateTime.Now);
                return;
            }
          
        }
    }
}
