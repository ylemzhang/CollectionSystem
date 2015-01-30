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

public partial class FieldsDetail : PageBase
{


    protected string ImportType
    {
        get
        {
            return Request["tableType"];
        }
    }
    private string FieldID
    {
        get
        {
            if (this.ViewState["FieldID"] == null)
            {
                if (Request["id"] == null) return string.Empty;
                this.ViewState["FieldID"] = Request["id"];
            }
            return this.ViewState["FieldID"].ToString();

        }
    }

    private string CompanyID
    {
        get
        {
            return Request["companyID"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string script = "window.close();return false";
        btnCancel.Attributes.Add("onclick", script);
      
        if (!this.IsPostBack)
        {
           

            if (FieldID != string.Empty)
            {
                BindDropDownlist();
                FillData();
            }
            else
            {
                if (ImportType != Common.Tools.CaseTableType)
                {
                    this.trShow.Visible = false;
                }
                else
                {
                    BindDropDownlist();

                }
            }
          
        }

    }

    private ArrayList GetGroups(DataTable tb)
    {
        ArrayList ar = new ArrayList();
        foreach (DataRow dr in tb.Rows)
        {
            if (!ar.Contains(dr["Misk"].ToString()))
            {
                ar.Add(dr["Misk"].ToString());
            }
        }
        return ar;
    }

    void BindDropDownlist()
    {
        DataTable TbFields = BLL.CompanyBLL.GetCacheFields(CompanyID, ImportType).Tables[0];
       ArrayList Groups = GetGroups(TbFields);
       this.ddlGroup.DataSource = Groups;
       this.ddlGroup.DataBind();
     
     
    }


    private void FillData()
    {
        DataSet ds = BLL.FieldBLL.GetFieldByID(FieldID);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            this.txtFName.Text  = dr["FName"].ToString();
            string fieldlength = dr["FieldLength"].ToString();
            if (fieldlength == "")
            {
                fieldlength = "50";
            }
            this.txtLength.Text = fieldlength;


            string fieldType = dr["fieldType"].ToString();
            if (fieldType == "")
            {
                fieldType = "nvarchar";
            }
            this.txtType.Text = fieldType;

            this.ddlGroup.Text = dr["Misk"].ToString();
            this.chkDisplay.Checked = (dr["IsDisplay"].ToString().Trim() != "0");

            this.txtFieldName.Text = dr["FieldName"].ToString();
        }
        this.txtLength.Enabled = false;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
       

        string fname = this.txtFName.Text.Trim();
        string group = "";
        string isdisplay = "1";

        if (ImportType != Common.Tools.CaseTableType)
        {
          
        }
        else
        {
            group = this.ddlGroup.Text;
            if (!this.chkDisplay.Checked)
            {
                isdisplay = "0";
            }
        }

     
  

        if (FieldID != string.Empty) //update
        {
            BLL.FieldBLL.UpdateField(FieldID, fname, group, isdisplay, CompanyID, ImportType);
        }
        else  //insert
        {
             string FieldName = "tb_" + DateTime.Now.Ticks.ToString();
             BLL.FieldBLL.InsertField(FieldName, fname, CompanyID, ImportType,"", this.txtLength.Text, group,isdisplay );
        }

        string script = "window.close();opener.window.refreshPage();";
        base.ExceuteScript(script);
        

    }



    protected void btnDelete_Click(object sender, EventArgs e)
    {

        BLL.FieldBLL.DeleteField(CompanyID, FieldID,txtFieldName.Text,ImportType);
        string script = "window.close();opener.window.refreshPage();";
        base.ExceuteScript(script);

    }
}
