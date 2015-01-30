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

public partial class login :Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        translation();
    }
  
    protected void translation()
    {
        btnLogin.Text = Common.StrTable.GetStr("login");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

       
        string userName = txtUser.Text.Trim();
        string password = txtPass.Text.Trim();
        if(BLL.AdminBLL.Login(userName,password))
        {
            BLL.SystemUser user = Session["CurrentUser"] as BLL.SystemUser;
            var bean = WebBeanUse.GetInstance();
            bean.Account = user.ID;  
            FormsAuthentication.SetAuthCookie(userName, false);
            Response.Redirect("default.aspx");
        // FormsAuthentication.RedirectFromLoginPage("userName",false);
        }
        else
        {
            this.Msg.InnerHtml = Common.StrTable.GetStr("loginerror");
         
        }
       
    }

   
}
