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
using BLL;


using System.Collections.Generic;

public partial class advanceSearch :PageBase
{
    List<SearchRow> SearchRowsList = new List<SearchRow>();
    private DataSet CompanyDS;
  

    protected string CompanyID
    {
        get
        {
            return ddlCompany.SelectedValue;
        }
    }

    protected string CompanyName
    {
        get
        {
            return ddlCompany.SelectedItem.Text ;
        }
    }

    protected string TableType
    {
        get
        {
            return this.ViewState["TableType"].ToString();
        }
        set
        {
            this.ViewState["TableType"] = value;
        }
    }



    private DataSet SearchFields
    {
       
        get
        {
            string key = TableType+CompanyID + "SearchFields";
            if (this.ViewState[key] == null)
            {
                this.ViewState[key] = GetFields();
            }
            return this.ViewState[key] as DataSet;

        }
        set
        {
            string key = TableType + CompanyID + "SearchFields";
            this.ViewState[key] = value;
        }
    }

   

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {

            CompanyDS = BLL.CompanyBLL.GetCompanyList();



            if (CompanyDS.Tables[0].Rows.Count == 0)
            {

                this.GridView1.Visible = false;
                this.btnGo.Visible = false;
                return;

            }


            TableType = Common.Tools.CaseTableType;
            this.lblTitle.Text = "案件表";
            bindCompayList();

            SearchRowsList = new SearchBLL().GetEmptySearchRow(3);
            BindGrid();
            ShowBalanceLink();
        }

    }

    private void ShowBalanceLink()
    {

        string hasBalanceTable = BLL.CompanyBLL.GetIfHasBalanceTable(CompanyID);
        if (hasBalanceTable == "1" && this.HasbalanceTable(CompanyID) && IsAdmin)
        {
            this.LinkButton3.Visible = true;
        }
        else
        {
            this.LinkButton3.Visible = false;
        }

        if (this.HasPaymentTable(CompanyID) && IsAdmin)
        {
            this.LinkButton1 .Visible = true;
        }
        else
        {
            this.LinkButton1.Visible = false;
        }

        if (this.HasCaseTable(CompanyID) && IsAdmin)
        {
            this.LinkButton2.Visible = true;
        }
        else
        {
            this.LinkButton2.Visible = false;
        }
    }

    private void bindCompayList()
    {

        ddlCompany.DataSource = CompanyDS;
        ddlCompany.DataTextField = "CompanyName";
        ddlCompany.DataValueField = "ID";
        ddlCompany.DataBind();
        ddlCompany.SelectedIndex = 0;

    }



    private DataSet GetFields()
    {
        DataSet fileds = new SearchBLL().GetSearchFields(CompanyID, TableType);
        if (fileds == null)
            return null;
        DataSet ds = fileds.Copy();
        ds.Tables[0].Columns.Add("DatabaseFieldAndType");
        for (int i = 0; i < ds.Tables[0].Rows.Count;i++ )
        {
            DataRow dr = ds.Tables[0].Rows[i];
            dr["DatabaseFieldAndType"] = dr["FieldName"].ToString() + "|" + dr["FieldType"].ToString();
        }
        return  ds;


    }



    void BindGrid()
    {
        if (!HasCaseTable(CompanyID))
        {
            this.GridView1.Visible = false;
            this.btnGo.Visible = false;
            return;
        }
        else
        {
            this.btnGo.Visible = true;
            this.GridView1.Visible = true;
            this.GridView1.DataSource = SearchRowsList;
            GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != -1)
        {

          
            TextBox txtValue = (TextBox)e.Row.FindControl("txtValue");
            DropDownList ddlField = (DropDownList)e.Row.FindControl("ddlField");
          
            DropDownList ddloperator = (DropDownList)e.Row.FindControl("ddlOperator");

            DropDownList ddlAndOr = (DropDownList)e.Row.FindControl("ddlAndOr");
            string ddlid= ddloperator.ClientID;
            string script=string.Format("fieldChange(this,'{0}')",ddlid);
            ddlField.Attributes.Add("onchange", script);

            FillData(e.Row.RowIndex, ddlAndOr, ddlField, ddloperator, txtValue);


        }
    }

    void FillData(int index, DropDownList ddlAndOr, DropDownList ddl, DropDownList ddloperator, TextBox txtValue)
    {
        SearchRow row = SearchRowsList[index];
        ddlAndOr.DataSource = new string[] { "And", "Or" };

        ddlAndOr.DataBind();

        if (row.AndOr == "Or")
        {
            ddlAndOr.SelectedIndex = 1;
        }
        else
        {
            ddlAndOr.SelectedIndex = 0;
        }



        txtValue.Text = row.SearchValue;

        //fill ddlField
        ddl.DataSource = SearchFields;
        ddl.DataTextField = "FName";
        ddl.DataValueField = "DatabaseFieldAndType";

        ddl.DataBind();
        ddl.Items.Insert(0, "");

        //fill ddloperator

        string fieldName = row.Field;
        if (row.Field == string.Empty || row.Field == null)
        {
            return;
        }
        for (int i = 0; i < ddl.Items.Count; i++)
        {
            if (ddl.Items[i].Value .StartsWith(fieldName))
            {
                ddl.SelectedIndex = i;
                string fieldType = ddl.SelectedValue.Split(new char[]{'|'})[1];


                fillDropdownList(ddloperator, fieldType, row.SearchOperator);
                return;
            }
        }

    }




    private void getNewSearchRowsList()
    {
        SearchRowsList.Clear();
        string txtRows = this.txtSearchRows.Value;
        if (txtRows.Length > 0)
        {
            txtRows = txtRows.Substring(0, txtRows.Length - 1);
        }

        string[] rows = txtRows.Split(new char[] { ';' });

        for (int i = 0; i < rows.Length ; i++)
        {
            string [] row = rows[i].Split(new char[] { '|' });

            SearchRow srow = new SearchRow();
            srow.Field = row[0];
            srow.FieldType = row[1];
            srow.SearchOperator = row[2]; ;
            srow.SearchValue = row[3];
            srow.AndOr = row[4];
            SearchRowsList.Add(srow);

        }

    }

   

    
    private void fillDropdownList(DropDownList ddloperator, string fieldType, string value)
    {

        //if (fieldType == string.Empty)
        //{
        //    ddloperator.Items.Clear();
        //    return;
        //}
        string[] arrs = SearchBLL.strArr;
        if (fieldType == "money" || fieldType == "int" || fieldType == "datetime")
        {
            arrs = SearchBLL.numArr;//date
        }


        ddloperator.Items.Clear();
        ddloperator.DataSource = arrs;
        ddloperator.DataBind();
        if (value != string.Empty)
        {
            ddloperator.SelectedValue = value;
        }


    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        getNewSearchRowsList();
        if (SearchRowsList.Count == 0)
        {
            Alert("searchconditionnull");
            return;
        }
        else
        {

            if (CheckSearchList())
            {
                Session["SearchRowsList"] = SearchRowsList;

                string script = "window.location.href='SearchResult.aspx?companyID={0}&type={1}&companyName={2}'";
                script = string.Format(script, CompanyID, TableType, CompanyName);
             
              base.ExceuteScript(script);
            }
        }


    }

    private bool CheckSearchList()
    {
        foreach (SearchRow row in SearchRowsList)
        {

            string text = row.SearchValue;
            string fieldType= row.FieldType ;

            if (text.ToLower().Trim() == "null")
            {
                continue;
            }
            if (fieldType == "datetime" )
            {
                try
                {
                    DateTime.Parse(text.Trim());
                }
                catch
                {
                    Alert("timeformatincorrect");

                    return false;
                }
            }

            else if  (fieldType == "money" )
            {
                try
                {
                    decimal.Parse (text.Trim());
                
                }
                catch
                {
                    Alert("numberformatincorrect");

                    return false;
                }
            }
            else if (fieldType == "int")
            {
                try
                {
                    int.Parse(text.Trim());

                }
                catch
                {
                    Alert("numberformatincorrect");

                    return false;
                }
            }
        }
        return true;
    }

    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        getNewSearchRowsList();
        SearchRow srow = new SearchRow();
        srow.AndOr = "And";
        SearchRowsList.Add(srow);
        BindGrid();


    }

    private void Rebind()
    {
        SearchRowsList = new SearchBLL().GetEmptySearchRow(3);
        BindGrid();
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        TableType = Common.Tools.CaseTableType;
        if (HasCaseTable(CompanyID))
        {
            Rebind();
            ShowBalanceLink();
            this.btnGo.Visible = true;
        }
        else
        {
            this.GridView1.Visible = false;
            this.btnGo.Visible = false;



            this.LinkButton1.Visible = false;



            this.LinkButton2.Visible = false;
            this.LinkButton3.Visible = false;


        }
    }



    protected void LinkButton2_Click(object sender, EventArgs e)//case
    {
        TableType = Common.Tools.CaseTableType;
        this.lblTitle.Text = "案件记录";
        Rebind();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)//payment
    {
        TableType = Common.Tools.PaymentTableType;
        this.lblTitle.Text = "每日还款记录";
        Rebind();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)//balance
    {
        TableType = Common.Tools.BalanceTableType ;
        this.lblTitle.Text = "余额记录";
        Rebind();
    }
}