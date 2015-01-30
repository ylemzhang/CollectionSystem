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

public partial class CompanyEdit : AdminPageBase
{
   

    protected string CompanyID
    {
        get
        {
            if (this.ViewState["CompanyID"] == null)
            {
                this.ViewState["CompanyID"] = Request.QueryString["id"];
            }
            return this.ViewState["CompanyID"].ToString();

        }
        set
        {
            ViewState["CompanyID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
     
        if (!this.IsPostBack)
        {
            translation();
            bindData();

        }

    }

    protected void translation()
    {
        btnSave.Text = Common.StrTable.GetStr("save");
       

    }


    private void bindData()
    {

        if (CompanyID != "")
        {
            DataSet ds = BLL.CompanyBLL.GetCompanyByID(CompanyID);
            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                this.txtCompanyName.Text = dr["CompanyName"].ToString();
                this.txtDescription.Text = dr["Description"].ToString();
                if (dr["HasBalanceTable"].ToString() == "1")
                {
                    this.chkBalance.Checked = true;
                }
                //this.chkBalance.Enabled = false;
            }

           
        }
      

    }

   

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string script;
        string hasbalancetable="";
        if ( this.chkBalance.Checked )
        {
            hasbalancetable="1";
        }
        if (CompanyID == "")
        {
            int rtn = BLL.CompanyBLL.InsertCompany(this.txtCompanyName.Text.Trim(), this.txtDescription.Text.Trim(), hasbalancetable);
           CompanyID = rtn.ToString();

            script = string.Format("parent.window.head.window.refreshPage({1},0);", Common.StrTable.GetStr("saveSuccess"),CompanyID);
        }
        else
        {
            BLL.CompanyBLL.UpdateCompany(CompanyID, this.txtCompanyName.Text.Trim(), this.txtDescription.Text.Trim(), hasbalancetable);

            script = string.Format("parent.window.head.window.refreshPage({1},1);", Common.StrTable.GetStr("saveSuccess"), CompanyID);
        }
        
        base.ExceuteScript(script);


    }


   
}

