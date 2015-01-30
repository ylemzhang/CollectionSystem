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

public partial class Top :PageBase
{

    /// <summary>
    /// 得到权限菜单
    /// </summary>
    /// <returns></returns>
    public static string GetPage()
    {
        StringBuilder sbmenu = new StringBuilder();
        
        DataSet ds = WebAccess.GetInstance().GetUserUrl(WebBeanUse.GetInstance().Account);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr;
            string tmpmenu = "<a href=\"javascript:showUrl('{0}');\">[{1}]</a>";
            string url = string.Empty;
            string urlParams = string.Empty;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];//UrlParams                
                url = dr["Url"].ToString();
                urlParams = dr["UrlParams"].ToString();
                if ("top".Equals(dr["UrlCode"].ToString().Trim().ToLower())
                    && !string.Empty.Equals(urlParams)
                    && "search".Equals(urlParams.Trim().ToLower()))
                {
                    continue;                    
                }
                if (!string.Empty.Equals(urlParams))
                {
                    url += "?" + dr["UrlParams"].ToString();
                }
                sbmenu.AppendFormat(tmpmenu, url, dr["UrlName"].ToString());
            }
        }
        return sbmenu.ToString();
    }

    /// <summary>
    /// 检查是否有搜索权限
    /// </summary>
    /// <returns></returns>
    public static bool CheckSearch()
    {
        bool isSearch = false;
        StringBuilder sbmenu = new StringBuilder();

        DataSet ds = WebAccess.GetInstance().GetUserUrl(WebBeanUse.GetInstance().Account);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr;
            string urlParams = string.Empty;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dr = ds.Tables[0].Rows[i];//UrlParams                
                urlParams = dr["UrlParams"].ToString();
                if ("top".Equals(dr["UrlCode"].ToString().Trim().ToLower()) && !string.Empty.Equals(urlParams))
                {
                    if ("search".Equals(urlParams.Trim().ToLower()))
                    {
                        isSearch = true;
                        break;
                    }
                }                
            }
        }
        return isSearch;
    }
    
    protected bool IsGroupLead
    {
      get
      {
        DataSet leadcompany = BLL.GroupBLL.GetGroupList("LeadID=" + CurrentUser.ID);
        return leadcompany.Tables[0].Rows.Count >0;
      }
    }
    protected  static bool grouplead = false;

    protected string IsAdminUser
    {
        get
        {
            if (IsAdmin) return "1";
            return "0";
        }
    }
    protected static int leaveHour = int.Parse(System.Configuration.ConfigurationManager.AppSettings["leaveHour"]);
    protected static int leaveMinute = int.Parse(System.Configuration.ConfigurationManager.AppSettings["leaveMinute"]);
  
    protected string Now
    {
        get
        {
            return DateTime.Now.DayOfWeek.ToString();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {        
        grouplead= this.IsGroupLead;
        if (!this.IsPostBack)
        {
           
            lblUser.Text = base.UserName;
            LeaveManage();

            //if (AllowCopy)
            //{
            //    this.LinkButton2.Text = "[禁止拷贝]";
            //}
            //else
            //{
            //    this.LinkButton2.Text = "[允许拷贝]";
            //}

            bindCompayList();
            
        }
    }   

    private void LeaveManage()
    {
        string id = CurrentUser.ID;
        string today = DateTime.Now.Date.ToString("yyyy-MM-dd");
        string tomorrow = DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd");
        string where = "userID={0} and PunchIn between '{1}' and '{2}'";
        where = string.Format(where, id, today, tomorrow);
        DataSet ds=BLL.LeaveBLL.GetLeaveList(where);
        if (ds.Tables[0].Rows.Count != 0)
        {
            this.spanleave.InnerText = Common.StrTable.GetStr("punchOut");
        }
       
        
    }
  
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        base.ExceuteScript("window.top.location.href='login.aspx'");
    }

    protected void btnIsCopy_Click(object sender, EventArgs e)
    {
        //this.AllowCopy = !this.AllowCopy;
        //if (AllowCopy)
        //{
        //    this.LinkButton2.Text = "[禁止拷贝]";
        //}
        //else
        //{
        //    this.LinkButton2.Text = "[允许拷贝]";
        //}
        //base.ExceuteScript("top.window.mainFra.document.location.href=top.window.mainFra.document.location.href;");
    }

    private void bindCompayList()
    {

        ddlCompany.DataSource = GetMyCompany();
        ddlCompany.DataTextField = "CompanyName";
        ddlCompany.DataValueField = "ID";
        ddlCompany.DataBind();
        ddlCompany.Items.Insert(0, "");

        ddlCompany.SelectedIndex = 0;

    }

    //protected DataSet GetMyCompany()
    //{
    //    if (IsAdmin)
    //    {
    //        return BLL.CompanyBLL.GetCompanyList();
    //    }
    //    else
    //    {
    //        return BLL.CompanyBLL.GetALLCompanysIDandNameByUserID(CurrentUser.ID);
    //    }
    //}
   
}
