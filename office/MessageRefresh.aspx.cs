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

public partial class MessageRefresh : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Response.Cache.SetNoStore();
            if (BLL.AdminBLL.CurrentSystemUser == null)
            {
                return;
            }
            string emailCount = BLL.MessageBLL.GetNewEmailCount(BLL.AdminBLL.CurrentSystemUser.UserName);
            DateTime dt = DateTime.Now.AddSeconds(-20);
          
            string sql = "select count(*) from alerttable where alertType=2 and Date1>'{0}' and caseownerid={1}";
            sql = string.Format(sql, dt.ToString(), CurrentUser.ID);
            string commentCount = BLL.MessageBLL.GetAlertCommentCount(sql);
            Response.Write(emailCount+"|"+commentCount);
            Response.End();
        }
    }
}
