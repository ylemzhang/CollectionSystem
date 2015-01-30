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

public partial class ImportsRecords : PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {

         
            BindGrid();

        }

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
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            string hasbalance = e.Row.Cells[2].Text.Trim();
            if (hasbalance != "1")
            {
                hasbalance = "0";
            }

            e.Row.Attributes.Add("currenRowID", "Row" + id);
            e.Row.Attributes.Add("ondblclick", "this.style.backgroundColor='gray';fillDetail(" + id + ",'"+hasbalance +"')");

            e.Row.ToolTip = Common.StrTable.GetStr("dubbleClickToEdit");


        }
    }






    //protected void btnSave_Click(object sender, EventArgs e)
    //{

    //    BindGrid();

    //}

}


