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

using System.Text;

public partial class AlertPromisedDate : PageBase
{

    private DataSet companyDS
    {
        get
        {
            return ViewState["companyDS"] as DataSet;
        }
        set
        {
            ViewState["companyDS"] = value;
        }
    }
    protected string TotalRecord = "";


 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
             string   where = " alerttype=1 and DATEDIFF(day, getdate(),date1 )  between 0 and {0} ";
                where = string.Format(where, CurrentUser.AlertDays);
            string Companywhere=where;
            if (Act != null && Act != "")
            {
                divSearch.Style.Add("display", "block");
                 Companywhere = " alerttype=1  ";
                 this.txtDateFrom.Text = DateTime.Now.ToString("yyyy-MM-dd");
                 this.txtDateTo.Text = DateTime.Now.AddDays(CurrentUser.AlertDays).ToString("yyyy-MM-dd");
            }
           

            
            GetCompanies(Companywhere);


            if (companyDS == null || companyDS.Tables[0].Rows.Count == 0)
            {
                LinkButton1.Enabled = false;
                return;
            }

            bindCompayList();
           
            string sql = GetSql(where);
            BindList(sql);
        }
    }

    private string Act
    {
        get
        {
            return Request["act"] as string;
        }
    }

    private void GetCompanies(string where)
    {
        //where = " p.tbPayDate =  '2008-11-03'";
    
        string sql = "Select distinct CompanyID, c.companyName from AlertTable a inner join  companytable c on companyID =c.ID where " + where;
        companyDS = BLL.AlertBLL.GetAlertCompanys(sql);
       
    }

    private string GetSql(string where)
    {
        string sqlTempate = @"select {0} as CompanyID,c.ID,p.PatchName, c.OwnerID, c.tbName, c.tbKey,  c.tbBalance, a.date1,a.Num1 from companycase_{0} c 
inner join  patch  p on c.PatchID=p.id 
inner  join (select *  from alertTable   
where  {1}  and companyID={0}  ) a on c.ID = a.CaseID where p.ExpireDate>getdate()";
        if (!base.IsAdmin)
        {
            sqlTempate = sqlTempate + " and c.OwnerID = " + CurrentUser.ID;
        }



        StringBuilder sb = new StringBuilder();


        int count = companyDS.Tables[0].Rows.Count;

        for (int i = 0; i < count; i++)
        {
            DataRow dr = companyDS.Tables[0].Rows[i];
            string id = dr[0].ToString();
            if (HasCaseTable(id))
            {
                string tempsql = string.Format(sqlTempate, id, where);
                sb.AppendLine(tempsql);

                sb.AppendLine("Union All");

            }

        }

        string sql= sb.ToString();
        if (sql == "")
        {
            return "";
        }
        else
        {
            return  sql.Substring(0, sql.Length - "Union All".Length - 2);// /r/n
        }

    }

    private void BindList(string sql)
    {


        if (sql == "")
        {
            return;
        }


        DataSet ds = BLL.CaseBLL.GetSearchCaseList(sql);
        TotalRecord = ds.Tables[0].Rows.Count.ToString();
        if (ds.Tables[0].Rows.Count == 0)
        {
            DataRow dr = ds.Tables[0].NewRow();
            ds.Tables[0].Rows.Add(dr);

        }
        else
        {

            if (Act != null && Act != "")
            {
                Session["CaseListDataSet"] = ds;
            }
        }
        this.GridView1.DataSource = ds;
        this.GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != -1)
        {
            string companyID = e.Row.Cells[1].Text;
            string caseID = e.Row.Cells[8].Text;
            e.Row.Cells[0].Text = GetUserName(e.Row.Cells[0].Text.Trim());
            e.Row.Cells[1].Text = getCompanyName(companyID);
          
            e.Row.Cells[6].Text = GetDateString(e.Row.Cells[6].Text);
            e.Row.Attributes.Add("ondblclick", "window.open('CaseDetail.aspx?act=" + Act + "&id=" + caseID + "&CompanyID=" + companyID + "','_blank')");
            e.Row.ToolTip = Common.StrTable.GetStr("dubbleClickToEdit");
            e.Row.Cells[8].Visible = false;

        }
        else
        {
            e.Row.Cells[8].Visible = false;
        }
    }

    private string getCompanyName(string id)
    {
        if (id == "")
        {
            return "";
        }
        foreach (DataRow dr in companyDS.Tables[0].Rows)
        {
            if (dr[0].ToString() == id)
            {
                return dr[1].ToString();
            }
        }
        return "";
    }


    private void bindCompayList()
    {

        ddlCompany.DataSource = companyDS;
        ddlCompany.DataTextField = "companyName";
        ddlCompany.DataValueField = "CompanyID";
        ddlCompany.DataBind();
        ddlCompany.Items.Insert(0, "");

        ddlCompany.SelectedIndex = 0;

    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
      

        string companyID = ddlCompany.SelectedItem.Value;
        bindPatchList(companyID);

        string where = GetSearchWhere();
        string sql = GetSingleSql(where);
        BindList(sql);
    

    }
    private void bindPatchList(string companyID)
    {
        if (companyID == "")
        {
            ddlPatch.Items.Clear();
            return;
        }
        DataSet PatchsDS = BLL.PatchBLL.GetCompanyPatchListByCompanyID(companyID);
        ddlPatch.DataSource = PatchsDS;

        ddlPatch.DataTextField = "PatchName";
        ddlPatch.DataValueField = "ID";
        ddlPatch.DataBind();
        ddlPatch.Items.Insert(0, "");
        ddlPatch.SelectedIndex = 0;
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        //GetCompanies(" alerttype=1  ");
        string where = GetSearchWhere();
        string sql = GetSingleSql(where);
        BindList(sql);
    }

    private string GetSearchWhere()
    {
        string dateFrom = this.txtDateFrom.Text.Trim();
        string dateTo = this.txtDateTo.Text.Trim();
        string where = " alerttype={2} and date1 between '{0}' and '{1}' ";
        if (dateFrom == "")
        {
            dateFrom = "1-1-1";
        }
        else
        {
            try
            {
                DateTime.Parse(dateFrom);
            }
            catch
            {
                dateFrom = "1-1-1";
                this.txtDateFrom.Text = "";
            }
            dateFrom = dateFrom + " 00:00:00";
        }
        if (dateTo == "")
        {


            dateTo = "3000-12-1";
        }
        else
        {
            try
            {
                DateTime.Parse(dateTo);
            }
            catch
            {
                dateTo = "3000-12-1";
                this.txtDateTo.Text = "";
            }

            dateTo = dateTo + " 23:59:59";
        }


        where = string.Format(where, dateFrom, dateTo, base.AlertTypePromise);

        string sumFrom = txtFrom.Text.Trim();
        string sumTo = txtTo.Text.Trim();

        if (sumFrom != "")
        {
            try
            {

                where += " and Num1>=" + decimal.Parse(sumFrom);
            }
            catch
            {
              
                this.txtFrom.Text = "";
            }
        }

        if (sumTo != "")
        {
            try
            {

                where += " and Num1<=" + decimal.Parse(sumTo);
            }
            catch
            {

                this.txtTo.Text = "";
            }
        }

        return where;
    }

    private string GetSingleSql(string where)
    {
        string sqlTempate = @"select {0} as CompanyID,c.ID,p.PatchName, c.OwnerID, c.tbName, c.tbKey,  c.tbBalance, a.date1,a.Num1 from companycase_{0} c 
inner join  patch  p on c.PatchID=p.id 
inner  join (select *  from alertTable   
where  {1}  and companyID={0}  ) a on c.ID = a.CaseID where p.ExpireDate>getdate()";
        if (!base.IsAdmin)
        {
            sqlTempate = sqlTempate + " and c.OwnerID = " + CurrentUser.ID;
        }

        if (ddlCompany.SelectedIndex > 0)
        {
            if (ddlPatch.SelectedIndex > 0)
            {
                sqlTempate += " and P.ID=" + ddlPatch.SelectedItem.Value;

            }

            return string.Format(sqlTempate, ddlCompany.SelectedItem.Value, where);
        }
        else
        {
            return GetSql(where);
          

        }
      

       

    }
}
