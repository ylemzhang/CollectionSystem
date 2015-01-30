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

public partial class CaseCategoryEdit : PageBase
{
    private string returnUrl
    {
        get
        {
            return "CaseCategory.aspx";

        }

    }


    private string CaseCategoryID
    {
        get
        {
            if (this.ViewState["CaseCategoryID"] == null)
            {
                if (Request["id"] == null) return string.Empty;
                this.ViewState["CaseCategoryID"] = Request["id"];
            }
            return this.ViewState["CaseCategoryID"].ToString();

        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        string script = string.Format("window.location.href='{0}';return false", returnUrl);
        btnCancel.Attributes.Add("onclick", script);

        if (!this.IsPostBack)
        {


            if (CaseCategoryID != string.Empty)
            {

                FillData();
            }


        }

    }




    private void FillData()
    {
        DataSet ds = BLL.CaseTypeBLL.GetCaseTypeByID(CaseCategoryID);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            this.txtFName.Text = dr["CaseTypeName"].ToString();

            this.chkDisplay.Checked = (dr["IsDisplay"].ToString().Trim() != "False");


        }

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {


        string fname = this.txtFName.Text.Trim();

        string isdisplay = "1";


        if (!this.chkDisplay.Checked)
        {
            isdisplay = "0";
        }


        BLL.CaseTypeBLL.UpdateCaseType(CurrentUser.ID, fname, isdisplay, CaseCategoryID);


        string script = string.Format("window.location.href='{0}';", returnUrl);
        base.ExceuteScript(script);


    }


}
