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

public partial class CompanyList : AdminPageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {

            translation();

            if (!IsSuperAdmin)
            {
                spanDelete.Style.Add("display", "none");
            }
            BindGrid();

        }

    }


    protected void translation()
    {
        LinkButton2.Text = Common.StrTable.GetStr("delete");

    }

    private void BindGrid()
    {

        DataSet ds = BLL.CompanyBLL.GetCompanyList();

        this.GridView1.DataSource = ds;
        this.GridView1.DataBind();

    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
            e.Row.Cells[2].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != -1)
        {
            string id = e.Row.Cells[1].Text.Trim();
            string hasbalance = e.Row.Cells[2].Text.Trim();
            if (hasbalance!="1")
            {
                hasbalance = "0";
            }
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Attributes.Add("currenRowID", "Row" + id);
       
            e.Row.Attributes.Add("ondblclick", "fillDetail(this,"+id+",'"+hasbalance+"')");

            e.Row.ToolTip = Common.StrTable.GetStr("dubbleClickToEdit");


        }
    }




    protected void LinkButton2_Click(object sender, EventArgs e) //delete
    {
        string idstr = "";
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            
            CheckBox chk = (CheckBox)this.GridView1.Rows[i].FindControl( "CheckBox1");
            if (chk.Checked)
            {
                idstr = idstr + this.GridView1.Rows[i].Cells[1].Text + ",";
            }
        }


        string ids = idstr.Substring(0, idstr.Length - 1);
        BLL.CompanyBLL.DeleteCompany(ids);
        BindGrid();
        this.txtID.Text = "";

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        BindGrid();

    }

}


