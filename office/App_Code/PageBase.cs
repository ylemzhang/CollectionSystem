using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text;
using BLL;

/// <summary>
/// Summary description for PageBase
/// </summary>
public class PageBase : System.Web.UI.Page
{
    public PageBase()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    string copyScript = @"<script>
  function   Click(){   
           
            window.event.returnValue=false;   
            }   
            document.oncontextmenu=Click;  
              function   NoSelect(){   
         return false;
            }   
            document.oncontextmenu=Click; 
           document.onselectstart =NoSelect;
</script>";
    protected override void OnLoad(EventArgs e)
    {
        if (CurrentUser == null)
        {
            ExceuteScript("window.top.location.href='login.aspx'");
        }
        else
        {
            if (!AllowCopy)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "js02", copyScript);
            }
            base.OnLoad(e);
        }
    }

    protected void ExceuteScript(string script)
    {
        StringBuilder js = new StringBuilder();

        js.Append("<script language='javascript'>");

        js.Append(script);
        js.Append("</script>");

        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js01", js.ToString());
    }

    protected void Alert(string message)
    {
       
        string script = @"window.alert(""{0}"")";
        script = string.Format(script, Common.StrTable.GetStr(message));
        ExceuteScript(script);
    }

    protected void Alert(string para,string message)
    {
        string script = @"window.alert(""{1}  {0}"")";
        script = string.Format(script, Common.StrTable.GetStr(message),para);
        ExceuteScript(script);
    }

    protected void AlertMessage(string message)
    {
        message=message.Replace('"','\'');
        string script = @"window.alert(""{0}"")";
        script = string.Format(script, message);
        ExceuteScript(script);
    }


    protected string UserName
    {
        get
        {
            if (CurrentUser == null)
            {
                ExceuteScript("window.top.location.href='login.aspx'");
                return "";
            }
            else
            {
                return CurrentUser.UserName;
            }

        }
    }

    protected bool IsAdmin
    {
        get
        {
            if (CurrentUser == null)
            {
                ExceuteScript("window.top.location.href='login.aspx'");
                return false;
            }
            else
            {
                return CurrentUser.RoleID == "1";
            }

        }
    }

    protected bool IsSuperAdmin
    {
        get
        {
            if (CurrentUser == null)
            {
                ExceuteScript("window.top.location.href='login.aspx'");
                return false;
            }
            else
            {
                return CurrentUser.ID == "1" ;
            }

        }
    }

    protected bool IsLead
    {
        get
        {
            if (CurrentUser == null)
            {
                ExceuteScript("window.top.location.href='login.aspx'");
                return false;
            }
            else
            {
                return LeadGroupDS.Tables[0].Rows.Count > 0;

            }


        }
    }

    //protected bool IsLeadofTheCompany(string CompanyID)
    //{

    //    if (CurrentUser == null)
    //    {
    //        ExceuteScript("window.top.location.href='login.aspx'");
          
    //    }
    //    else
    //    {
    //        foreach (DataRow dr in LedCompanyDS.Tables[0].Rows)
    //        {
    //            if (dr[0].ToString() == CompanyID)
    //                return true;
    //        }

    //    }
    //    return false;
    //}

    //protected bool IsCompanyUser
    //{
    //    get
    //    {
    //        if (CurrentUser == null)
    //        {
    //            ExceuteScript("window.top.location.href='login.aspx'");
    //            return false;
    //        }
    //        else
    //        {
    //            return ReadCompanyDS.Tables[0].Rows.Count > 0;
    //        }


    //    }
    //}

    //protected bool IsCompanyUserofTheCompany(string CompanyID)
    //{

    //    if (CurrentUser == null)
    //    {
    //        ExceuteScript("window.top.location.href='login.aspx'");

    //    }
    //    else
    //    {
    //        foreach (DataRow dr in ReadCompanyDS.Tables[0].Rows)
    //        {
    //            if (dr[0].ToString() == CompanyID)
    //                return true;
    //        }

    //    }
    //    return false;
    //}

   

    protected int ListRecordNumPerPage
    {
        get
        {
            if (CurrentUser == null)
            {
                ExceuteScript("window.top.location.href='login.aspx'");
                return 0;
            }
            else
            {
                return CurrentUser.PageCount;
            }



        }
    }


    protected int CaseDisplayColumn
    {
        get
        {
            if (CurrentUser == null)
            {
                ExceuteScript("window.top.location.href='login.aspx'");
                return 0;
            }
            else
            {
                return CurrentUser.CaseDisplayColumn;
            }



        }
    }


    protected SystemUser CurrentUser
    {
        get
        {
            if (AdminBLL.CurrentSystemUser == null)
            {
                ExceuteScript("window.top.location.href='login.aspx'");
                return null;
            }
            else
            {
                return AdminBLL.CurrentSystemUser;
            }

        }
    }

  



    protected bool CheckFileExist(string fileName)
    {

        string fName = Server.MapPath(fileName);
        System.IO.FileInfo file = new System.IO.FileInfo(fName);
        if (file.Exists)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    protected string GetPath(string file)
    {
        return Server.MapPath(Common.Tools.AttachMentPath) + file;
    }
    protected string GetCompanyPath(string file)
    {

        return Server.MapPath(Common.Tools.CompanyInfoPath) + file;
    }
    protected void AlertTooLong()
    {
        Alert("内容超过4000,可能有格式在里面,请看源格式");
    }

    protected void GetDropDownListSeleted(DropDownList ddl, string value)
    {
        for (int i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Value .ToLower() == value.ToLower())
            {
                ddl.SelectedIndex = i;
                return;
            }
        }
    }

    protected void GetDropDownListSeletedByText(DropDownList ddl, string text)
    {
        for (int i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Text.ToLower() == text.ToLower())
            {
                ddl.SelectedIndex = i;
                return;
            }
        }

    }

    protected DataSet UserDS
    {
        get
        {
            if (Session["UserDS"] == null)
            {
                Session["UserDS"] = BLL.AdminBLL.GetAllUser("ID,UserName,RoleID,RealName");
            }
            return Session["UserDS"] as DataSet;
        }
    }


    protected DataSet LeadGroupDS
    {
        get
        {
            if (Session["LeadGroupDS"] == null)
            {
                Session["LeadGroupDS"] = BLL.AdminBLL.GetledGroups(CurrentUser.ID);
            }
            return Session["LeadGroupDS"] as DataSet;
        }
    }

    protected bool isGroupLead(string groupID)
    {

            string where="ID={0} and LeadID={1}";
            where =string.Format(where,groupID,CurrentUser.ID);
            DataSet ds=BLL.GroupBLL.GetGroupList(where);
            return (ds.Tables[0].Rows.Count > 0);
          
    }



    protected string GetDateString(string text)
    {
        if (text.Trim() != "" && text.Trim() != "&nbsp;")
        {
            return DateTime.Parse(text.Trim()).ToString("yyyy-MM-dd");
        }
        else
        {
            return text;
        }
    }


    protected string GetUserName(string userID)
    {
        foreach (DataRow dr in UserDS.Tables[0].Rows)
        {
            if (userID == dr[0].ToString())
                return dr[1].ToString();
        }
        return "";
    }

    protected DataSet GetCompanyUsers(string CompanyID)
    {
     
       
            string where = @" ID in 
(select distinct userID from companyuser where companyID={0} ) or id in (select distinct leadID from grouptable where  companyID={0}) ";
            where = string.Format(where, CompanyID);
            return BLL.AdminBLL.GetUserList("ID,UserName,RoleID,RealName", where);
      

    }

    protected DataSet GetALLCompanyUsers()
    {


        string where = @" ID in 
(select distinct userID from companyuser ) or id in (select distinct leadID from grouptable ) ";
       
        return BLL.AdminBLL.GetUserList("ID,UserName,RoleID,RealName", where);


    }

//    protected DataSet GetCompanyUsersList(string where)
//    {


//        string where = @" ID in 
//(select userID from companyuser where companyID={0} ) or id in (select leadID from grouptable where  companyID={0}) ";
//        where = string.Format(where, CompanyID);
//        return BLL.AdminBLL.GetUserList("ID,UserName,RoleID,RealName", where);


//    }




    protected bool HasCaseTable(string CompanyID)
    {
        return (BLL.CompanyBLL.GetCacheFields(CompanyID, Common.Tools.CaseTableType) != null);
    }

    protected bool HasPaymentTable(string CompanyID)
    {
        return (BLL.CompanyBLL.GetCacheFields(CompanyID, Common.Tools.PaymentTableType ) != null);
    }

    protected bool HasbalanceTable(string CompanyID)
    {
        return (BLL.CompanyBLL.GetCacheFields(CompanyID, Common.Tools.BalanceTableType ) != null);
    }

    private static bool _AllowCopy = true;
    /// <summary>
    /// 设置的copy值
    /// </summary>
    public static bool AllowCopy
    {
        get
        {
            return _AllowCopy;
        }
        set
        {
            _AllowCopy = value;
        }
    }


    protected string GetCompanyNameByID(string companyID)
    {
        DataSet ds = CompanyBLL.GetCompanyByID(companyID);
        if (ds.Tables[0].Rows.Count > 0)
            return ds.Tables[0].Rows[0]["CompanyName"].ToString();
        return "";
    }


    protected int AlertTypePromise = 1;
    protected int AlertTypeComment = 2;
    protected int AlertTypeCaseType = 3;

    protected int AlertTypeFollowBy = 4;


    protected DataSet GetMyCompany()
    {
        if (IsAdmin)
        {
            return BLL.CompanyBLL.GetCompanyList();
        }
        else
        {
            return BLL.CompanyBLL.GetALLCompanysIDandNameByUserID(CurrentUser.ID);
        }
    }


    protected static int  day1= int.Parse(System.Configuration.ConfigurationManager.AppSettings["day1"]);
    protected static int day2 = int.Parse(System.Configuration.ConfigurationManager.AppSettings["day2"]);
    protected static int day3 = int.Parse(System.Configuration.ConfigurationManager.AppSettings["day3"]);


    protected string[] NoteContactType = new string[] { "", "持卡人", "父母", "兄弟姐妹", "朋友", "其它" };
    protected string[] NoteContactResult = new string[] { "", "可联", "完全失联", "暂时失联",  "其它" };
    
}
