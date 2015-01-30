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
using System.Text;

public partial class SystemMangement : AdminPageBase
{
   
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_Click(object sender, EventArgs e)
    {
        //BLL.AdminBLL.InitSystem();
        //string message = Common.StrTable.GetStr("initializationSuccess");
        //string script = "alert('{0}');window.top.location.href='login.aspx'";
        //script = string.Format(script, message);
        //ExceuteScript(script);
    }


    /// <summary>
    /// 得到权限菜单
    /// </summary>
    /// <returns></returns>
    public static string GetPage()
    {
        StringBuilder sbmenu = new StringBuilder();
        DataSet ds = WebAccess.GetInstance().GetUserChildeUrl("SystemManagementfrm", WebBeanUse.GetInstance().Account);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr;
            string htmLeft =
                "<li id='menuGroup99' style='cursor:pointer;'><a href=\"javascript:showUrl('{0}');\">[{1}]</a></li>";
            string url = string.Empty;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                url = dr["Url"].ToString();
                if (!string.Empty.Equals(dr["UrlParams"].ToString()))
                {
                    url += "?" + dr["UrlParams"].ToString();
                }
                sbmenu.AppendFormat(htmLeft, url, dr["UrlName"].ToString());
            }
        }
        return sbmenu.ToString();
    }

}
