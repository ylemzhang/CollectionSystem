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

public partial class ImportFields : AdminPageBase
{
    protected string TotalRecord = "";
    private string returnUrl
    {
        get
        {
            return "Companymanagement.aspx";

        }

    }

    

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

    protected string ImportType
    {
        get
        {
            string importType = Request["type"];
            switch (importType)
            {
                case "1": return  Common.Tools.CaseTableType;
                case "2": return Common.Tools.PaymentTableType;
                case "3": return Common.Tools.BalanceTableType;

                default: return Common.Tools.CaseTableType;
            }
          

        }
    }

    protected string ImportTitle
    {
        get
        {
            if (this.ViewState["ImportTitle"] == null)
            {
                string importType = Request["type"];
                switch (importType)
                {
                    case "1": return Common.StrTable.GetStr("ImportCase");
                    case "2": return Common.StrTable.GetStr("ImportPayment");
                    case "3": return Common.StrTable.GetStr("ImportBalance");

                    default: return "";
                }
            }
            return this.ViewState["ImportTitle"].ToString();
        }
        set
        {
            this.ViewState["ImportTitle"] = value;

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
       

    }


    private void bindData()
    {

        if (CompanyID != "")
        {


            if (BLL.CompanyBLL.GetCacheFields(CompanyID, ImportType) == null)
            {
                divImportCase.Style.Add("display", "block");
                this.divCaseFieldsList.Style.Add("display", "none");
            }
            else
            {

                BindDropdownList();
                ShowFieldList();

            }
        }
      



    }

    private void BindDropdownList()
    {
       DataSet ds= BLL.CompanyBLL.GetCacheFields(CompanyID, ImportType);
       this.ddlField.DataSource = ds;
        this.ddlField.DataTextField = "FName";
        this.ddlField.DataValueField = "FieldName";
        this.ddlField.DataBind();
        for (int i = 0; i < ddlField.Items.Count; i++)
        {
            if (ddlField.Items[i].Value.ToLower() =="tbkey")
            {
                ddlField.SelectedIndex = i;
                return;
            }
        }
        this.ddlField.Items.Insert(0, "请选择主键");

    }

    private void ShowFieldList()
    {
        divImportCase.Style.Add("display", "none");
        this.divCaseFieldsList.Style.Add("display", "block");
        DataSet ds = BLL.CompanyBLL.GetCacheFields(CompanyID, ImportType);
        this.GridCase.DataSource = ds;

        GridCase.DataBind();
        TotalRecord = ds.Tables[0].Rows.Count.ToString();

    }

   


    protected void btnImport_Click(object sender, EventArgs e)
    {
        string filename = this.FileUpload1.PostedFile.FileName.Trim();

        if (filename == string.Empty) return;


        int start = filename.LastIndexOf("\\");
        string saveName = Common.Tools.GetCashName(CompanyID,ImportType)+ ".xls";


        string phicalPath = GetCompanyPath(saveName);
        FileUpload1.PostedFile.SaveAs(phicalPath);


        string message = BLL.CompanyBLL.ImportFieldsTable(phicalPath, CompanyID, ImportType);

        System.IO.FileInfo file = new System.IO.FileInfo(phicalPath);
        if (file.Exists)
        {
            file.Delete();
        }


        if (message == "")
        {
            string script = @"window.alert(""{0}"");";
            script = string.Format(script, Common.StrTable.GetStr("importSuccess"));
            base.ExceuteScript(script);


            ShowFieldList();

        }
        else
        {


            string script = @"window.alert(""{0}"");";
            script = string.Format(script, message);
            base.ExceuteScript(script);
        }




    }






    protected void GridCase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
        }

        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != -1)
        {
            e.Row.Cells[1].Visible = false;
            string id = e.Row.Cells[1].Text;
            string isdisplay = e.Row.Cells[5].Text.Trim();
            if (isdisplay == "0")
            {
                e.Row.Cells[5].Text = "false";
            }
            else
            {
                e.Row.Cells[5].Text = "true";
            }
            if (ImportType== Common.Tools.CaseTableType)
            {
                string script = string.Format("OpenWindow('FieldsDetail.aspx?id={0}&companyID={1}&tableType={2}',600,300)", id, CompanyID,ImportType );
            e.Row.Attributes.Add("ondblclick", script);

            e.Row.ToolTip = Common.StrTable.GetStr("dubbleClickToEdit");
            }


        }
    }
    protected void btnUpdateKey_Click(object sender, EventArgs e)
    {
        if (ddlField.SelectedItem.Text == "请选择主键")
        {
            base.AlertMessage("请选择你要设的字段为主键");
            return;

        }
        string value = ddlField.SelectedItem.Value;

        BLL.CompanyBLL.UpdateTbKey(CompanyID, ImportType, value);
        Alert("saveSuccess");
    }
}

