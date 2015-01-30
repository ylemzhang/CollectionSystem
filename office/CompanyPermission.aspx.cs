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

public partial class CompanyPermission : AdminPageBase
{

    protected string CompanyID
    {
        get
        {
            if (this.ViewState["CompanyID"] == null)
            {
                this.ViewState["CompanyID"] = Request["id"];
            }
            return this.ViewState["CompanyID"].ToString();

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {

            BindGrid();

        }

    }



    private void BindGrid()
    {
        string where = "CompanyID=" + CompanyID;
        DataSet ds = BLL.GroupBLL.GetGroupList(where);

        this.GridView1.DataSource = ds;
        this.GridView1.DataBind();

    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
       
        }

        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != -1)
        {
            e.Row.Cells[1].Visible = false;
            string id = e.Row.Cells[1].Text;

            e.Row.Attributes.Add("ondblclick", "window.location.href='GroupEdit.aspx?CompanyID="+ this.CompanyID+"&id=" + id + "'");
            e.Row.Cells[3].Text = GetUserName(e.Row.Cells[3].Text);
            e.Row.ToolTip = Common.StrTable.GetStr("dubbleClickToEdit");


        }
    }



    protected void LinkButton2_Click(object sender, EventArgs e) //delete
    {
        string idstr = "";
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)this.GridView1.Rows[i].FindControl("CheckBox1");
            if (chk.Checked)
            {
                idstr = idstr + this.GridView1.Rows[i].Cells[1].Text + ",";
            }
        }


        string ids = idstr.Substring(0, idstr.Length - 1);
        BLL.GroupBLL.DeleteGroup(ids);
        BindGrid();

    }



}

