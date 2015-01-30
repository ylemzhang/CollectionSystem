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

using BLL;

public partial class UserEdit:AdminPageBase
{
    private string returnUrl
    {
        get
        {
            return "Usermanagement.aspx";

        }

    }

    private string UserID
    {
        get
        {
            if (this.ViewState["UserID"] == null)
            {
                this.ViewState["UserID"] = Request.QueryString["id"];
            }
            return this.ViewState["UserID"].ToString();

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = string.Format("window.location.href='{0}';return false", returnUrl);
        btnCancel.Attributes.Add("onclick", script);
        

        if (!this.IsPostBack)
        {
            translation();
            bindData();
           
        }

    }

    protected void translation()
    {
        btnSave.Text = Common.StrTable.GetStr("save");
        btnCancel.Text = Common.StrTable.GetStr("cancel");


    }

    private void FillCompanyDiv()
    {
        string sb = "";
        string roleNmae = "只读人员";
        DataSet leadcompany = BLL.GroupBLL.GetGroupList("LeadID=" + UserID);
        if (leadcompany.Tables[0].Rows.Count >0)//lead
        {
            foreach (DataRow dr in leadcompany.Tables[0].Rows)
            {
                sb += GetCompanyNameByID(dr["CompanyID"].ToString()) + "<input type='checkbox' checked  disabled='disabled'/>";
            }
            roleNmae = "组长";
        }
        else //operater
        {
            DataSet readcompany = BLL.CompanyBLL.GetALLCompanysByUserID(UserID);
            if (readcompany.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in readcompany.Tables[0].Rows)
                {
                    sb += GetCompanyNameByID(dr["CompanyID"].ToString()) + "<input type='checkbox' checked  disabled='disabled' />";
                }
                roleNmae = "业务人员";
            }
          
        }
        lblRole.Text = roleNmae;
        divList.InnerHtml = sb;
        return;
    }

    private void bindData()
    {


        bindList();
        if (UserID != string.Empty)
        {
         
            BLL.SystemUser user = BLL.AdminBLL.GetUserByID(UserID);

            this.txtUserName.Text = user.UserName;
            this.txtPassword.Attributes.Add("value", user.Password);
            this.txtRealName.Text = user.RealName;
            this.txtPhone.Text = user.Phone;
            this.txtMobile.Text = user.Mobile;
            this.txtEmail.Text = user.Email;
            string roleid = user.RoleID;


            checkDropDownlistSelected(ddlGender, user.Gender);
            FillCompanyDiv();



        }

    }

  



    private void checkDropDownlistSelected(DropDownList ddl,string value)
    {
        foreach (ListItem ls in ddl.Items)
        {
            if (ls.Value == value)
            {
                ls.Selected = true;
            }
        }
    }
    private void bindList()
    {
        DataSet ds = BLL.AdminBLL.GetRoleList(); ;
   

        string[] genders = new string[] { "M", "F" };
        this.ddlGender.DataSource = genders;
        this.ddlGender.DataBind();

      
    }
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        BLL.SystemUser user = new BLL.SystemUser();
        user.ID = UserID;
        string usr=this.txtUserName.Text.Trim();
        user.UserName  = usr;
        user.Password = this.txtPassword.Text.Trim();
        user.RealName = this.txtRealName.Text.Trim();
        user.Phone = this.txtPhone.Text.Trim();
        user.Mobile = this.txtMobile.Text.Trim();
        user.Email = this.txtEmail.Text.Trim();
        if (usr == "Super1" || usr == "Super2" || usr == "Super3")
        {
            user.RoleID = "1"; 
        }
        else
        {
            user.RoleID = "3"; //业务人员
        }
        user.Gender = this.ddlGender.Text;

     
        int rtn = BLL.AdminBLL.UpdateUser(user);
        if (rtn == 1)
        {
           
            string script = string.Format("window.location.href='{0}';", returnUrl);
            base.ExceuteScript(script);


        }
        else
        {
            string script = @"window.alert(""{0} {1}"")";
            script = string.Format(script, Common.StrTable.GetStr("userName"),Common.StrTable.GetStr("exist"));
            base.ExceuteScript(script);
        }


    }
}
