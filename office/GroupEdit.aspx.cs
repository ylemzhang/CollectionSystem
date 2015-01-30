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

public partial class GroupEdit : AdminPageBase
{
    protected string CompanyID
    {
        get
        {
            return Request["CompanyID"];

        }
    }

    protected string GroupID
    {
        get
        {
          return  Request["id"];
           
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            BindDropdownlist();

            fillData();
        }
    }

    private void fillData()
    {
        if (GroupID!="")
        {

            DataSet dsGroup = BLL.GroupBLL.GetGroupByID(GroupID);
            this.txtGroupName.Text = dsGroup.Tables[0].Rows[0]["GroupName"].ToString();
            txtOldGroupName.Text = this.txtGroupName.Text;
            if (dsGroup.Tables[0].Rows.Count > 0)
            {
                txtOldOwner.Text = dsGroup.Tables[0].Rows[0]["LeadID"].ToString();
                GetDropDownListSeleted(ddlUser, txtOldOwner.Text);
            }
            BindList();
        }
    }
    private void BindDropdownlist()
    {
       
        this.ddlUser.DataSource = UserDS;
        ddlUser.DataTextField = "UserName";
        ddlUser.DataValueField = "ID";
        ddlUser.DataBind();
        //ddlUser.Items.RemoveAt(0);
        //ddlUser.Items.Insert(0, "");

       


    }

    private void BindList()
    {
        DataSet ds = BLL.CompanyBLL.GetGroupUsersByID(GroupID);
        this.lstUser.Items.Clear();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string id = dr[0].ToString();
            string userName = GetUserName(id);

            this.lstUser.Items.Add(new ListItem(userName, id));
            this.txtOldUserAlls.Text += id + ",";
        }
        if (this.txtOldUserAlls.Text.Length > 0)
        {
            this.txtOldUserAlls.Text = this.txtOldUserAlls.Text.Substring(0, this.txtOldUserAlls.Text.Length - 1);
        }

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string ids = this.txtUserAll.Text;
        string[] useriDs = this.txtUserAll.Text.Split(new char[] { ',' });
       
        int leadid=int.Parse(this.ddlUser.SelectedItem.Value);

        if (GroupID == "")
        {
            int groupid = BLL.GroupBLL.InsertGroup(this.txtGroupName.Text, leadid, int.Parse(CompanyID));
            BLL.CompanyBLL.InsertGroupUsers(useriDs,groupid.ToString(), CompanyID);
        }
        else
        {

            if (txtOldOwner.Text != ddlUser.Text || txtOldGroupName.Text != this.txtGroupName.Text)
            {
                BLL.GroupBLL.UpdateGroup(GroupID, this.txtGroupName.Text, leadid);
           
               
            }

            if (this.txtUserAll.Text != txtOldUserAlls.Text)
            {

                BLL.CompanyBLL.InsertGroupUsers(useriDs, this.GroupID, CompanyID);


            }

           
            
           
        }
      
        string script = "window.location.href='CompanyPermission.aspx?id={0}'";
        script = string.Format(script, CompanyID);
        base.ExceuteScript(script);

    }
}
