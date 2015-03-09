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

public partial class AlertPayment : PageBase
{

    protected string TotalRecord = "";

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


    private DataSet PatchDS
    {
        get
        {
            return ViewState["PatchDS"] as DataSet;
        }
        set
        {
            ViewState["PatchDS"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            GetCompanies();
            if (companyDS == null || companyDS.Tables[0].Rows.Count == 0)
            {
                LinkButton1.Enabled = false;
                return;
            }

            if (Act != null && Act != "")
            {
                divSearch.Style.Add("display", "block");
         
                this.txtDateFrom.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.txtDateTo.Text = this.txtDateFrom.Text;
                bindCompayList();
            }
            string where = " p.ImportDate between '{0} 00:00:00' and   '{0} 23:59:59'";
            where =string.Format(where,txtDateFrom.Text);
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


    private void bindCompayList()
    {

        ddlCompany.DataSource = companyDS;
        ddlCompany.DataTextField = "CompanyName";
        ddlCompany.DataValueField = "ID";
        ddlCompany.DataBind();
        ddlCompany.Items.Insert(0, "");

        ddlCompany.SelectedIndex = 0;

    }

    
    private void GetCompanies()
    {
        companyDS = GetMyCompany();

        PatchDS = BLL.PatchBLL.GetPatchList();
    }

    private string GetSql(string where)
    {
        string sqlTempate = "select {0} as CompanyID, c.ID,c.OwnerID,p.tbName,p.tbKey, p.tbPayment ,p.tbPayDate ,p.tbBalance, p.ImportDate, c.PatchID  from companypayment_{0} p inner join  companycase_{0}  c on p.tbkey=c.tbkey  where  {1} ";

        StringBuilder sb = new StringBuilder();


        int count = companyDS.Tables[0].Rows.Count;
        //string shouBieField = ",'0' as shouBie";
        //显示手别
        //string shouBieSql = string.Empty;
        //shouBieSql = " select * from Field where (isdisplay is null or isdisplay=1)  and fname  like '%手别%' ";
        //var dsShouBie = BLL.PaymentBLL.GetAlertPaymentList(shouBieSql);
        //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //{
        //    shouBieField = string.Format(",c.{0} as shouBie ", ds.Tables[0].Rows[0][0]);
        //}
        
        for (int i = 0; i < count; i++)
        {

            DataRow dr = companyDS.Tables[0].Rows[i];
            string id = dr["ID"].ToString();
            if (HasCaseTable(id) && HasPaymentTable(id))
            {
                //if (dsShouBie.Tables.Count > 0 && dsShouBie.Tables[0].Rows.Count > 0)
                //{
                //    for (int j = 0; j < dsShouBie.Tables[0].Rows.Count;j++ )
                //        if (dsShouBie.Tables[0].Rows[j]["companyid"].ToString().Equals(id))
                //        {
                //            shouBieField = string.Format(",c.{0} as shouBie ", dsShouBie.Tables[0].Rows[j]["FieldName"]);
                //            break;
                //        }                    
                //}
                string tempsql = string.Format(sqlTempate, id, where);
                //shouBieField = ",'0' as shouBie";
                sb.AppendLine(tempsql);
                sb.AppendLine("Union All");
            }


        }
        string sql = sb.ToString();
        if (sql == "")
        {
            return "";
        }
        else
        {
            return sql.Substring(0, sql.Length - "Union All".Length - 2);// /r/n
        }

    }

    private string GetSingleSql(string where)
    {
        string sqlTempate = "select {0} as CompanyID, c.ID,c.OwnerID,p.tbName,p.tbKey, p.tbPayment ,p.tbPayDate ,p.tbBalance, p.ImportDate,c.PatchID from companypayment_{0} p inner join  companycase_{0}  c on p.tbkey=c.tbkey  where  {1} ";

        if (!base.IsAdmin)
        {
            sqlTempate = sqlTempate + " and c.OwnerID = " + CurrentUser.ID;
        }

        //string shouBieField = ",'0' as shouBie";

        if (ddlCompany.SelectedIndex > 0)
        {
            ////显示手别
            //string shouBieSql = string.Empty;
            //shouBieSql = string.Format(" select FieldName from Field where (isdisplay is null or isdisplay=1) and companyid ={0} and fname  like '%手别%' "
            //                        ,ddlCompany.SelectedItem.Value);
            //var ds= BLL.PaymentBLL.GetAlertPaymentList(shouBieSql);
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    shouBieField = string.Format(",c.{0} as shouBie ", ds.Tables[0].Rows[0][0]);
            //}
            return string.Format(sqlTempate, ddlCompany.SelectedItem.Value, where);
        }
        else
        {
            
            return GetSql(where);


        }
    }


    private void BindList(string sql)
    {

       

        if (sql == "")
        {
            return;
        }        
       
        DataSet ds = BLL.PaymentBLL.GetAlertPaymentList(sql);
        TotalRecord = ds.Tables[0].Rows.Count.ToString();
        if (ds.Tables[0].Rows.Count == 0)
        {
            DataRow dr = ds.Tables[0].NewRow();
            ds.Tables[0].Rows.Add(dr);

        }
        else
        {
            if(Act != null && Act!="")
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
            string caseID = e.Row.Cells[9].Text;
            e.Row.Cells[0].Text = GetUserName(e.Row.Cells[0].Text.Trim());
            e.Row.Cells[1].Text = getCompanyName(e.Row.Cells[1].Text.Trim());
            e.Row.Cells[3].Text = GetPatchName(e.Row.Cells[3].Text.Trim());
            e.Row.Cells[7].Text = GetDateString(e.Row.Cells[6].Text);
            e.Row.Cells[9].Text = GetDateString(e.Row.Cells[8].Text);
            e.Row.Attributes.Add("ondblclick", "window.open('CaseDetail.aspx?act=" + Act + "&id=" + caseID + "&CompanyID=" + companyID + "','_blank')");
            e.Row.ToolTip = Common.StrTable.GetStr("dubbleClickToEdit");
            e.Row.Cells[9].Visible = false;

        }
        else
        {
            e.Row.Cells[8].Visible = false;
        }


      
    }

    private string GetPatchName(string id)
    {
        if (id == "")
        {
            return "";
        }
        foreach (DataRow dr in PatchDS.Tables[0].Rows)
        {
            if (dr["ID"].ToString() == id)
            {
                return dr["PatchName"].ToString();
            }
        }
        return "";
    }


    private string getCompanyName(string id)
    {
        if (id == "")
        {
            return "";
        }
        foreach (DataRow dr in companyDS.Tables[0].Rows)
        {
            if (dr["ID"].ToString() == id)
            {
                return dr["CompanyName"].ToString();
            }
        }
        return "";
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {

       
        string companyID = ddlCompany.SelectedItem.Value;
        bindPatchList(companyID);
        string where = GetSearchWhere();
        string sql = GetSingleSql(where);
        BindList(sql);


    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

      

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


    private string GetSearchWhere()
    {
        string dateFrom = this.txtDateFrom.Text.Trim();
        string dateTo = this.txtDateTo.Text.Trim();
 
        string where = " p.ImportDate between '{0}' and '{1}' ";

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
        string sumFrom = txtFrom.Text.Trim();
        string sumTo = txtTo.Text.Trim();

        if (sumFrom != "")
        {
            try
            {

                where += " and tbPayment>=" + decimal.Parse(sumFrom);
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

                where += " and tbPayment<=" + decimal.Parse(sumTo);
            }
            catch
            {

                this.txtTo.Text = "";
            }
        }


        if (ddlCompany.SelectedIndex > 0)
        {
            if (ddlPatch.SelectedIndex > 0)
            {
                where += " and C.PatchID=" + ddlPatch.SelectedItem.Value;

            }

          
        }


        where = string.Format(where, dateFrom, dateTo);


        return where;
    }
}
