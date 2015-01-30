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

public partial class ChangePass :PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        translation();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
      
        string password = txtPass.Text.Trim();
        string oldpass = txtOldPass.Text.Trim();
        if (BLL.AdminBLL.ChangePassword(base.UserName, oldpass,password))
        {
            this.Msg.InnerText = Common.StrTable.GetStr("changepasswordsuccessfully");
            this.Msg.Style.Add("color", "black");
        }
        else
        {
            this.Msg.InnerText = Common.StrTable.GetStr("oldpassworderror");
        }
    }

    protected void translation()
    {
        Button1.Text = Common.StrTable.GetStr("save");
       

    }
}
