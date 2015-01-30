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

public partial class PaymentReportDetail : PageBase
{
    protected string CompanyID
    {
        get
        {
            return Request["companyID"];
        }
    }

    protected string UserID
    {
        get
        {
            return Request["userID"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            string where = Session["ReportWhere"].ToString();

            string sql = "select p.*,c.ID  as CaseID from companypayment_{0} p inner join  companycase_{0}  c on p.tbkey=c.tbkey  where {1} And  OwnerID ={2}";
            sql = string.Format(sql, CompanyID, where, UserID);
            this.GridView1.DataSource =  BLL.ReportBLL.GetDataSet(sql);
            this.GridView1.DataBind();
        }

    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != -1)
        {
            e.Row.Cells[3].Text = GetDateString(e.Row.Cells[3].Text);
            e.Row.Cells[5].Text = GetDateString(e.Row.Cells[5].Text);
            string script = "window.open('CaseDetail.aspx?id=" + e.Row.Cells[6].Text + "&CompanyID=" + CompanyID + "','casedetail','')";
           

            string txt=@"<span style='color:blue;cursor:hand' onclick=""{0}"">查看案件信息<span>";
            e.Row.Cells[6].Text = string.Format(txt, script);

        }
    }

}
