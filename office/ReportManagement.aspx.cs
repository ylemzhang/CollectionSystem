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

public partial class ReportManagement:PageBase
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
        DataSet ds = WebAccess.GetInstance().GetUserChildeUrl("ReportManagement", WebBeanUse.GetInstance().Account);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr;
            string htmLeft = "<tr><td align ='center'>" 
                        + "<a href='{0}'>"
                        +"<img src='Images/{1}.jpg' />"
                        +"<br />{2}"
                        +"</a></td></tr>";
            string url = string.Empty;
            string urlParams = string.Empty;            
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];
                url = dr["Url"].ToString();
                string img = string.Empty;
                if (!string.Empty.Equals(dr["UrlParams"].ToString()))
                {
                    //取图片文件名 &img=aaa
                    urlParams = dr["UrlParams"].ToString();
                    string[] arr = urlParams.Split('&');
                    foreach (var param in arr)
                    {
                        if (param.IndexOf("img") == 0)
                        {
                            img = param.Substring(param.IndexOf("=") + 1);
                            break;
                        }
                    }
                    url += "?" + dr["UrlParams"].ToString();
                }
                sbmenu.AppendFormat(htmLeft, url,img, dr["UrlName"].ToString());
            }
        }
        return sbmenu.ToString();
    }

}
