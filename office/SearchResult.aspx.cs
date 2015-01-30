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

public partial class SearchResult : PageBase
{




    protected string CompanyID
    {
        get
        {
            return Request["companyID"];
        }
    }

    protected string CompanyName
    {
        get
        {
            return Request["companyName"];
        }
    }

    protected string SearchType
    {
        get
        {
            return Request["type"];
        }
    }

    protected string TotalRecords
    {
        get
        {

            return this.ViewState["TotalRecords"].ToString();

        }
        set
        {
            this.ViewState["TotalRecords"] = value;
        }
    }

    private string BasicFilter
    {
        get
        {
            if (IsAdmin )
            {
                return "";
            }
            else
            {
                string where = " and  (ownerid= {0} or  ID in (select CaseID from ReadCaseUsers where userid= {0} and CompanyID ={1}))";
                where = string.Format(where, CurrentUser.ID, CompanyID);
                return where;
            }
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.PagingControl1.PagingClick += new EventHandler(PagingControl1_PagingClick);
        PagingControl1.ListRecordNumPerPage = base.ListRecordNumPerPage;
        if (!this.IsPostBack)
        {
            if (IsAdmin)
            {
            }

            else
            {
                spanExcel.Style.Add("display", "none");
            }


            BindGrid();

        }

    }







    private void BindGrid()
    {


        GetTotalRecords();
        bind();

    }
    private void GetTotalRecords()
    {

        List<SearchRow> SearchRowsList = Session["SearchRowsList"] as List<SearchRow>;


        string where = "(" + new SearchBLL().GetSearchSql(SearchRowsList) + ")" + BasicFilter;

        if (SearchType == Common.Tools.CaseTableType)
        {
            this.lblTitle.Text = CompanyName + " : 案件记录";
            TotalRecords = new CaseBLL(int.Parse(CompanyID)).GetCaseTotalItems(where).ToString();
        }
        else if (SearchType == Common.Tools.BalanceTableType)
        {
            this.lblTitle.Text = CompanyName + " : 余额记录";
            TotalRecords = new BalanceBLL(int.Parse(CompanyID)).GetBalanceTotalItems(where).ToString();
        }

        else if (SearchType == Common.Tools.PaymentTableType)
        {
            this.lblTitle.Text = CompanyName + " : 每日还款记录";
            TotalRecords = new PaymentBLL(int.Parse(CompanyID)).GetPaymentTotalItems(where).ToString();
        }

        PagingControl1.TotalRecords = int.Parse(TotalRecords);

    }

    private void bind()
    {

        DataSet ds = GetDataGridSource();
        if (ds != null)
        {
            this.GridView1.DataSource = ds;
            this.GridView1.DataBind();

        }
    }

    private DataSet GetDataGridSource()
    {
        List<SearchRow> SearchRowsList = Session["SearchRowsList"] as List<SearchRow>;


        string where = "(" + new SearchBLL().GetSearchSql(SearchRowsList) + ")" + BasicFilter;
        DataSet ds = null;

        if (SearchType == Common.Tools.CaseTableType)
        {

            ds = new CaseBLL(int.Parse(CompanyID)).GetCasePagingitems(this.PagingControl1.ListRecordNumPerPage, this.PagingControl1.CurrentPage, where);
        }
        else if (SearchType == Common.Tools.BalanceTableType)
        {
            ds = new BalanceBLL(int.Parse(CompanyID)).GetBalancePagingitems(this.PagingControl1.ListRecordNumPerPage, this.PagingControl1.CurrentPage, where);
        }

        else if (SearchType == Common.Tools.PaymentTableType)
        {
            ds = new PaymentBLL(int.Parse(CompanyID)).GetPaymentPagingitems(this.PagingControl1.ListRecordNumPerPage, this.PagingControl1.CurrentPage, where);
        }

        if (ds == null || ds.Tables[0].Rows.Count == 0)
        {
            spanExcel.Style.Add("display", "none");
        }
        else
        {
            DataSet reportDs = ds.Copy();
            AdoptToExcelFormat(reportDs);
            ChangeColumnTitle(reportDs);
            Session["ExportData"] = reportDs;

            ChangeColumnTitle(ds);

        }
        return ds;



    }

    private void AdoptToExcelFormat(DataSet ds) //excel format
    {
        bool hasID = ds.Tables[0].Columns.Contains("tbIdentityNo");
        bool hasCard = ds.Tables[0].Columns.Contains("tbCardNo");

        foreach (DataRow dr in ds.Tables[0].Rows)//去除显示格式在excel中变成科学.
        {
            dr["tbKey"] = "'" + dr["tbKey"].ToString();
            if (hasID)
            {
                dr["tbIdentityNo"] = "'" + dr["tbIdentityNo"].ToString();
            }
            if (hasCard)
            {
                dr["tbCardNo"] = "'" + dr["tbCardNo"].ToString();
            }

        }



    }

    private void ChangeColumnTitle(DataSet ds)
    {
        DataSet fields = new SearchBLL().GetSearchFields(CompanyID, SearchType);
        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
        {
            string originName = ds.Tables[0].Columns[i].Caption;
            ds.Tables[0].Columns[i].ColumnName = GetDisplayName(fields, originName);

        }

    }
    private string GetDisplayName(DataSet FieldDS, string originName)
    {
        foreach (DataRow dr in FieldDS.Tables[0].Rows)
        {
            if (dr["FieldName"].ToString() == originName)
                return dr["FName"].ToString();
        }
        return originName;
    }

    void PagingControl1_PagingClick(object sender, EventArgs e)
    {

        bind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false; //如果想使第1列不可见,则将它的可见性设为false


            if (SearchType == Common.Tools.CaseTableType)
            {
                e.Row.Cells[2].Text = "分配给";
            }
            else if (SearchType == Common.Tools.BalanceTableType)
            {
                e.Row.Cells[2].Text = "最新余额时间";
            }

            else if (SearchType == Common.Tools.PaymentTableType)
            {
                e.Row.Cells[2].Text = "导入时间";
            }

        }
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != -1)
        {
            string id = e.Row.Cells[1].Text;
            e.Row.Cells[1].Visible = false;

            if (SearchType == Common.Tools.CaseTableType)
            {
               // e.Row.Attributes.Add("ondblclick", "window.open('CaseDetail.aspx?id=" + id + "&CompanyID=" + CompanyID + "')");
                e.Row.Attributes.Add("ondblclick", "window.open('CaseDetail.aspx?id=" + id + "&CompanyID=" + CompanyID + "','_blank')");

                e.Row.ToolTip = Common.StrTable.GetStr("dubbleClickToEdit");
                e.Row.Cells[2].Text = GetUserName(e.Row.Cells[1].Text);

            }



        }
    }





    protected void btnRefresh_Click(object sender, EventArgs e) //delete
    {
        bind();

    }

    //protected void btnExport_Click(object sender, EventArgs e) //delete
    //{
    //    ExportToExcel();

    //}




    //private void ExportToExcel()
    //{
    //    Response.Clear();
    //    Response.Buffer = true;//设置缓冲输出     
    //    //Response.Charset = "UTF-8";//设置输出流的HTTP字符集   

    //    Response.AppendHeader("Content-Disposition", "attachment;filename=Report.xls");


    //    Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");
    //    System.IO.StringWriter tw = new System.IO.StringWriter();//将信息写入字符串
    //    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);//在WEB窗体页上写出一系列连续的HTML特定字符和文本。

    //    GridView1.RenderControl(hw);
    //    Response.Write(tw.ToString());



    //    Response.End();

    //}


    protected void LinkButton2_Click(object sender, EventArgs e) //delete
    {
        string idstr = "";
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)this.GridView1.Rows[i].FindControl("CheckBox1");
            if (chk.Checked)
            {
                idstr = idstr + this.GridView1.Rows[i].Cells[1].Text + ",";
            }
        }


        string ids = idstr.Substring(0, idstr.Length - 1);

        if (SearchType == Common.Tools.CaseTableType)
        {
            new CaseBLL(int.Parse(CompanyID)).DeleteCase(ids);
        }
        else if (SearchType == Common.Tools.BalanceTableType)
        {
            new BalanceBLL(int.Parse(CompanyID)).DeleteBalance (ids);
        }

        else if (SearchType == Common.Tools.PaymentTableType)
        {
            new PaymentBLL(int.Parse(CompanyID)).DeletePayment (ids);
        }

        BindGrid();

    }


}

