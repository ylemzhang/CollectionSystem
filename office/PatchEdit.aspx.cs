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

public partial class PatchEdit : AdminPageBase
{


    protected string PatchID
    {

        get
        {
            return Request["id"].ToString();
        }
    }

    protected string CompanyID
    {

        get
        {
            return Request["CompanyID"].ToString();
        }
    }


    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {

            bindData();

        }

    }




    private void bindData()
    {

        if (PatchID != "")
        {
            DataSet ds = BLL.PatchBLL.GetPatchByID(PatchID);
            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                this.txtPatchName.Text = dr["PatchName"].ToString();

                string ExpireDate = dr["ExpireDate"].ToString();
                this.txtDate.Text = DateTime.Parse(ExpireDate).ToString("yyyy-MM-dd");

                string ImportTime = dr["ImportTime"].ToString();
                this.txtImportDate.Text = DateTime.Parse(ImportTime).ToString("yyyy-MM-dd");


            }


        }




    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DateTime date;
        try
        {
            date = DateTime.Parse(this.txtDate.Text.Trim());
        }
        catch
        {
            base.Alert("timeformatincorrect");
            return;
        }

        string url = "Patchmanagemnt.aspx?id=" + CompanyID;
        BLL.PatchBLL.UpdatePatch(PatchID, this.txtPatchName.Text.Trim(), date);

        string script = string.Format("  window.location.href='{0}';", url);

        base.ExceuteScript(script);
    }
}



