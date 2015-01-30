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

public partial class NoteEdit:PageBase
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
            if (NoteID != "")
            {
                DataSet ds = BLL.NoteBLL.GetNoteByID(NoteID);

                string NoteType = ds.Tables[0].Rows[0]["NoteType"].ToString();
                this.txtType.Text = NoteType;

                if (NoteType == "1")
                {
                    this.lblTitle.Text = "电话：";
                    this.TextBox1.Text = ds.Tables[0].Rows[0]["Str1"].ToString();
                    this.divTeleType.Style.Add("display", "block");
                    BindTeleTypeDropdownList();
                    BindnoteType();

                    string phoneType = ds.Tables[0].Rows[0]["Str2"].ToString();
                    GetDropDownListSeletedByText(ddlTeleType, phoneType);

                    this.txtContactor.Text = ds.Tables[0].Rows[0]["contactor"].ToString();
                    this.ddlcontactorType.Text = ds.Tables[0].Rows[0]["contactorType"].ToString();
                    this.ddlcontractResult.Text = ds.Tables[0].Rows[0]["contractResult"].ToString();



                }
                else if (NoteType == "2")
                {
                    BindnoteType();
                    this.lblTitle.Text = "路费：";
                    this.TextBox1.Text = ds.Tables[0].Rows[0]["Num1"].ToString();
                    this.txtContactor.Text = ds.Tables[0].Rows[0]["contactor"].ToString();
                    this.ddlcontactorType.Text = ds.Tables[0].Rows[0]["contactorType"].ToString();
                    this.ddlcontractResult.Text = ds.Tables[0].Rows[0]["contractResult"].ToString();
                }
                else
                {
                    moreInfo.Style.Add("display", "none");
                }

                this.txtNote.Text = ds.Tables[0].Rows[0]["Body"].ToString();
            }

            //else //double click the phone
            //{
            //    this.txtType.Text = "1"; //tele phone
            //    this.lblTitle.Text = "电话：";
            //    string phone = Request["phone"];
            //    this.TextBox1.Text = phone;
            //    this.divTeleType.Style.Add("display", "block");
            //    BindTeleTypeDropdownList();
            //    BindnoteType();
            //    if (phone != "")
            //    {
            //        string where = "companyid={0} and caseID={1} and NoteType=1 and Str1='{2}' ";
            //        where = string.Format(where, CompanyID, CaseID, phone);
            //        DataSet ds = BLL.NoteBLL.GetNoteList(where);
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            NoteID = ds.Tables[0].Rows[0]["ID"].ToString();
            //            string phoneType = ds.Tables[0].Rows[0]["Str2"].ToString();
            //            GetDropDownListSeletedByText(ddlTeleType, phoneType);

            //            this.txtContactor.Text = ds.Tables[0].Rows[0]["contactor"].ToString();
            //            this.ddlcontactorType.Text = ds.Tables[0].Rows[0]["contactorType"].ToString();
            //            this.ddlcontractResult.Text = ds.Tables[0].Rows[0]["contractResult"].ToString();
            //            this.txtNote.Text = ds.Tables[0].Rows[0]["Body"].ToString();

            //        }

            //    }
               
            //}
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
        if (NoteID != "")
        {
            string NoteType = this.txtType.Text;

            if (NoteType == "1")
            {
               string  phoneType="";
                if (this.ddlTeleType.SelectedIndex > -1)
                {
                    phoneType = this.ddlTeleType.SelectedItem.Text;
                }
                BLL.NoteBLL.UpdateNote(NoteID, this.txtNote.Text, this.TextBox1.Text.Trim(), phoneType,this.txtContactor.Text.Trim(), this.ddlcontactorType.Text, this.ddlcontractResult.Text);
            }
            else if (NoteType == "2")
            {
                decimal num = 0;
                if (this.TextBox1.Text.Trim() != "")
                {
                    try
                    {
                        num = decimal.Parse(this.TextBox1.Text.Trim());
                    }
                    catch
                    {
                    }
                }
                BLL.NoteBLL.UpdateVisitNote(NoteID, this.txtNote.Text, num,this.txtContactor.Text.Trim(), this.ddlcontactorType.Text, this.ddlcontractResult.Text);

            }
            else
            {
                BLL.NoteBLL.UpdateNote(NoteID, this.txtNote.Text);
            }
        }

        //else
        //{
        //    DateTime dt = DateTime.Now;
        //    string phoneType = "";
        //    if (this.ddlTeleType.SelectedIndex > -1)
        //    {
        //        phoneType = this.ddlTeleType.SelectedItem.Text;
        //    }
        //    BLL.NoteBLL.InsertNote(txtNote.Text.Trim(), CurrentUser.ID, 1, int.Parse(CaseID), 0, this.TextBox1.Text.Trim(), phoneType, DateTime.MinValue, dt, int.Parse(CompanyID),this.txtContactor.Text.Trim(),this.ddlcontactorType.Text,this.ddlcontractResult.Text);
     
        //}

        
        base.ExceuteScript("window.opener.location.href=window.opener.location.href;window.close()");

    }
}
