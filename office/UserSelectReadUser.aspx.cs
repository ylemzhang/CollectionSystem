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

public partial class UserSelectReadUser : PageBase
{

    protected string CompanyID
    {
        get
        {
            return Request["companyID"].ToString();
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


        this.GridView1.DataSource = UserDS;
        this.GridView1.DataBind();

    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
            e.Row.Cells[2].Text = Common.StrTable.GetStr("userName");

            e.Row.Cells[3].Text = Common.StrTable.GetStr("realName");

        }

        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != -1)
        {
          
            string id = e.Row.Cells[1].Text.ToString();
            e.Row.Cells[1].Visible = false;
         e.Row.Attributes.Add("currenRowID", id);

           // string srcipt = string.Format("setRadio(this,'{0}')", id);
            //((RadioButton)e.Row.FindControl("RadioButton1")).Attributes.Add("onclick", srcipt);

        }
    }





}

