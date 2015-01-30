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

public partial class Left : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    /// <summary>
    /// 得到权限菜单
    /// </summary>
    /// <returns></returns>
    public static string GetPage()
    {
        StringBuilder sbmenu = new StringBuilder();
        DataSet ds = WebAccess.GetInstance().GetUserChildeUrl("myPage", WebBeanUse.GetInstance().Account);
        if (ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr;
            string htmLeft = "<li id='menuGroup{2}' style=’cursor: pointer;'>"
                            + "<img onclick='displayMenu({2})' id='groupimg{2}' src='images/t_list_09.jpg' style='cursor: pointer;' />"
                            +"<a href=\"javascript:showUrl('{0}');\">[{1}]</a></li>";
            string url = string.Empty;
            string urlParams = string.Empty;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                url = dr["Url"].ToString();
                string displayMenu = string.Empty;
                if (!string.Empty.Equals(dr["UrlParams"].ToString()))
                {
                    //取 &displayMenu=98
                    urlParams = dr["UrlParams"].ToString();
                    string[] arr = urlParams.Split('&');
                    foreach (var param in arr)
                    {
                        if (param.IndexOf("displayMenu") == 0)
                        {
                            displayMenu = param.Substring(param.IndexOf("=") + 1);
                            break;
                        }
                    }
                    url += "?" + dr["UrlParams"].ToString();
                }
                sbmenu.AppendFormat(htmLeft, url, dr["UrlName"].ToString(), displayMenu);           
            }
        }
        return sbmenu.ToString();
    }
        

}
