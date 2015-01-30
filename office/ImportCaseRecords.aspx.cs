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

using System.IO;

public partial class ImportCaseRecords:PageBase
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
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (BLL.CompanyBLL.GetCacheFields(CompanyID, Common.Tools.CaseTableType) == null)
            {
                tbImport.Style.Add("display", "none");
                Response.Write(Common.StrTable.GetStr("ImportCaseNull"));
            }
        }

    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        string filename = this.FileUpload1.PostedFile.FileName.Trim();

        if (filename == string.Empty) return;


        int start = filename.LastIndexOf("\\");
       
        string saveName =Guid.NewGuid() + ".xls";


        string phicalPath = GetCompanyPath(saveName);
        FileUpload1.PostedFile.SaveAs(phicalPath);


        string message = BLL.CompanyBLL.ImportCompanyCaseRecords(phicalPath, CompanyID, this.txtPatch.Text.Trim(), this.txtDate.Text.Trim());

        System.IO.FileInfo file = new System.IO.FileInfo(phicalPath);
        if (file.Exists)
        {
            file.Delete();
        }
      
        if (message == "")
        {
            Alert("importSuccess");
          
        }
        else
        {

            base.AlertMessage(message);
        }

      

    }

   
}
