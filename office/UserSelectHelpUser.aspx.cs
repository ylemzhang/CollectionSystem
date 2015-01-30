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

public partial class UserSelectHelpUser : PageBase
{

    protected string CompanyID
    {
        get
        {
            return Request["companyID"].ToString();
        }
    }


    protected string CaseID
    {
        get
        {
            return Request["caseID"].ToString();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {

            BindGrid();

        }

    }



private DataTable  HelpuserTable;


    private void BindGrid()
    {
        string where = " companyID={0} and caseID={1}";
        where = string.Format(where, CompanyID, CaseID);
        HelpuserTable = BLL.OpenedCaseBLL.GetOpenedCaseList("UserID",where).Tables[0];
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
            if (isChecked(id))
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("CheckBox1");
             chk.Checked = true;
            }
            //e.Row.Attributes.Add("currenRowID", id);

            // string srcipt = string.Format("setRadio(this,'{0}')", id);
            //((RadioButton)e.Row.FindControl("RadioButton1")).Attributes.Add("onclick", srcipt);

        }
    }



    private bool isChecked(string opencaseID)
    {
        DataRow[] drs = HelpuserTable.Select("UserID=" + opencaseID);
        if (drs == null || drs.Length == 0)
            return false;
        else
            return true;
    }

    protected void Button1_ServerClick(object sender, EventArgs e)
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
        string ids = "";
        if (idstr.Length > 0)
        {
            ids = idstr.Substring(0, idstr.Length - 1);
        }
        
        BLL.OpenedCaseBLL.UpdateOpenedCase(ids, CaseID, CompanyID);
      
        base.ExceuteScript("window.close()");
    }
}

