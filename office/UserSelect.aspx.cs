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

public partial class UserSelect : PageBase
{
   
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            translation();
            ddlGroup.DataSource = BLL.GroupBLL.GetGroupList(" Companyid in (select ID from companytable)");
            ddlGroup.DataTextField = "GroupName";
            ddlGroup.DataValueField = "ID";
            ddlGroup.DataBind();

            ddlGroup.Items.Insert(0, "");
            ddlGroup.SelectedIndex = 0;

            BindGrid();

        }

    }

    protected string returnType
    {
        get
        {
            if (Request["returnType"] != null)
                return "1";
            else
                return "0";
        }
    }

   


    protected void translation()
    {
       
    }

    private void BindGrid()
    {


        this.GridView1.DataSource = UserDS;
        this.GridView1.DataBind();

    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
            e.Row.Cells[2].Text = Common.StrTable.GetStr("userName");
           // e.Row.Cells[3].Text = Common.StrTable.GetStr("role");
            e.Row.Cells[3].Text = Common.StrTable.GetStr("realName");
           
        }

        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != -1)
        {
            e.Row.Cells[1].Visible = false;
            string id = e.Row.Cells[1].Text.ToString();
            e.Row.Attributes.Add("currenRowID", id);
            //e.Row.Cells[3].Text = GetRoleName(e.Row.Cells[3].Text);

        }
    }




    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGroup.SelectedIndex == 0)
        {
            BindGrid();
        }
        else
        {
            DataSet ds = BLL.AdminBLL.GetUserList("ID,UserName,realName", "ID in (select userID from companyuser where groupID=" + ddlGroup.SelectedItem.Value + ")");
            this.GridView1.DataSource = ds;
            this.GridView1.DataBind();
        }
    }
}

