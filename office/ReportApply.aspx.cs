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

public partial class ReportApply : PageBase
{
    protected string TotalRecord = "";
    protected string CompanyID
    {
        get
        {
            return ddlCompany.SelectedValue;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            bindDropDownList();
            int m = Convert.ToInt32(DateTime.Today.DayOfWeek);

            this.txtFrom.Text = DateTime.Now.AddDays(-m).ToShortDateString();
            this.txtTo.Text = DateTime.Now.AddDays(6 - m).ToShortDateString();
            BindGrid(this.txtFrom.Text, this.txtTo.Text);

        }

    }

    private void bindDropDownList()
    {
        ddlCompany.DataSource = BLL.CompanyBLL.GetCompanyList();
        ddlCompany.DataTextField = "CompanyName";
        ddlCompany.DataValueField = "ID";
        ddlCompany.DataBind();
        ddlCompany.SelectedIndex = 0;
       

    }




    private void BindGrid(string dateFrom, string dateTo)
    {


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
                this.txtFrom.Text = "";
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
                this.txtTo.Text = "";
            }

            dateTo = dateTo + " 23:59:59";
        }
        string app = System.Configuration.ConfigurationManager.AppSettings["Application"];
       
        string where = " sentOn between '{1}' and '{2}' and  body like '%companyid={0}''%' and (sender='{3}' or recipient ='{3}' ) and ({4}) ";
       
        string[] apps = app.Split('|');
        string titlecon="";
        foreach (string appstr in apps)
        {
            titlecon += " or title='" + appstr + "'";
        }
        titlecon = titlecon.Substring(3);
        where = string.Format(where, CompanyID, dateFrom, dateTo, CurrentUser.UserName, titlecon);


      string order= " order by sender,title";


      DataSet ds = BLL.MessageBLL.GetReportMessageList(where, order);
        TotalRecord = ds.Tables[0].Rows.Count.ToString();
        if (ds.Tables[0].Rows.Count > 0)
        {

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dr["body"]=transform(dr["body"].ToString());
            }

            this.GridView1.DataSource = ds;
            this.GridView1.DataBind();

            Session["ExportWhere"] = where;

            this.spanExcel.Visible = true;
        }
        else
        {
            this.GridView1.DataSource = null;
            this.GridView1.DataBind();
            this.spanExcel.Visible = false;
        }

    }

    public static string transform(string content)
    {
        content = content.Replace( "&amp;","&");
        content = content.Replace("&lt;", "<");
        content = content.Replace("&nbsp;", " ");
        content = content.Replace("&gt;", ">");
        content = content.Replace("&quot;", @"""");
        
        return content;
    }   
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[6].Visible = false; //如果想使第1列不可见,则将它的可见性设为false

        
        }


        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != -1)
        {
          
            string body = e.Row.Cells[6].Text;
            string mailID =  e.Row.Cells[7].Text;
            DataRow dr = GetCase(body);
            if (dr != null)
            {
                e.Row.Cells[4].Text = dr["tbName"].ToString();
                e.Row.Cells[5].Text = dr["tbKey"].ToString();
            }
           // e.Row.Cells[6].Text ="window.open('MessageDetail.aspx?type=send&id="+mailID+"')";;

            e.Row.Attributes.Add("ondblclick", "window.open('MessageShow.aspx?id=" + mailID + "')");
            e.Row.Cells[6].Visible = false;
           e.Row.Cells[7].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
        }
    }

    DataRow GetCase(string body)
       
    {
        try
        {
            body = transform(body);
            string start = "CaseDetail.aspx?id=";
            int begin = body.IndexOf(start) + start.Length;
            int end = body.IndexOf("&CompanyID=");
            string id = body.Substring(begin, end - begin);
            DataRow dr = new BLL.CaseBLL(int.Parse(CompanyID)).GetCaseByID(id).Tables[0].Rows[0];
            return dr;
        }
        catch
        {
            return null;
        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        string dateFrom = this.txtFrom.Text.Trim(); ;
        string dateTo = this.txtTo.Text.Trim();
        BindGrid(dateFrom, dateTo);
    }
   
   
}

