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

public partial class CaseListLeft: PageBase
{
       private string GroupMenu = @" <li id='menuGroup{0}'' style='cursor:pointer;'><img onclick='displayMenu({0})' id='groupimg{0}' src='images/t_list_09.jpg' style='cursor:pointer;'/> <a href=""javascript:showUrl('caselist.aspx?category={1}&act=4')"">{2}</a></li>";
    //private string itemMenu = @" <li pid='{0}' class='w1' style='cursor:hand ' onclick=""javascript:showUrl(this,'CaseDetail.aspx?id={2}&companyID={3}')"">{1}</li>";
    protected string Menus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FormMenu();
        }

    }

    /// <summary>
    /// 得到权限菜单
    /// </summary>
    /// <returns></returns>
    public static string GetPage()
    {
        StringBuilder sbmenu = new StringBuilder();
        DataSet ds = WebAccess.GetInstance().GetUserChildeUrl("CaseFrm", WebBeanUse.GetInstance().Account);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr;
            string htmLeft = 
                "<li style='cursor:pointer;'><img src='images/t_list_09.jpg' style='cursor:pointer;'/> <a href=\"javascript:showUrl('{0}');\">[{1}]</a></li></li>";
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

    private void FormMenu()
    {
        DataSet casetypeDs = BLL.CaseTypeBLL.GetCaseTypeList("ID,CaseTypeName", "UserID=" + base.CurrentUser.ID + " and isDisplay=1");

        StringBuilder sb = new StringBuilder();
        foreach (DataRow dr in casetypeDs.Tables[0].Rows)
        {
            string id=dr[0].ToString();
            int a=int.Parse(id)+20;
            sb.AppendLine(string.Format(GroupMenu, a,id, dr[1].ToString()));
        }
        Menus =sb.ToString();
    }
}
