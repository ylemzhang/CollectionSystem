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

public partial class NoteEdit1 : PageBase
{
    private string NoteID
    {
        get
        {
            if (ViewState["NoteID"] == null)
            {
                return Request["id"];
            }
            else
            {
                return ViewState["NoteID"].ToString();
            }
        }
        set
        {
            ViewState["NoteID"] = value;
        }
    }

    protected string CaseID
    {
        get
        {
            return Request["caseID"].ToString();
        }
    }

    protected string CompanyID
    {
        get
        {
            return Request["companyID"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
           
                this.txtType.Text = "1"; //tele phone
                this.lblTitle.Text = "µç»°£º";
                string phone = Request["phone"];
                this.TextBox1.Text = phone;
                this.divTeleType.Style.Add("display", "block");
                BindTeleTypeDropdownList();
                BindnoteType();

        }
    }

    private void BindTeleTypeDropdownList()
    {
        string id = "2"; //case type
        DataSet ds = new BLL.TypeBLL().GetTypeDataListByTypeID(id);
        ddlTeleType.DataSource = ds;

        ddlTeleType.DataTextField = "FTypeValue";
        ddlTeleType.DataValueField = "ID";
        ddlTeleType.DataBind();



    }

    private void BindnoteType()
    {
        ddlcontractResult.DataSource = base.NoteContactResult;
        ddlcontractResult.DataBind();

        ddlcontactorType.DataSource = base.NoteContactType;
        ddlcontactorType.DataBind();

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
 
            DateTime dt = DateTime.Now;
            string phoneType = "";
            if (this.ddlTeleType.SelectedIndex > -1)
            {
                phoneType = this.ddlTeleType.SelectedItem.Text;
            }
            BLL.NoteBLL.InsertNote(txtNote.Text.Trim(), CurrentUser.ID, 1, int.Parse(CaseID), 0, this.TextBox1.Text.Trim(), phoneType, DateTime.MinValue, dt, int.Parse(CompanyID), this.txtContactor.Text.Trim(), this.ddlcontactorType.Text, this.ddlcontractResult.Text);

      

        base.ExceuteScript("window.opener.location.href=window.opener.location.href;window.close()");

    }
}
