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

public partial class ReportCollectionDetail : PageBase
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
        string companyID=ddlCompany.SelectedItem.Value;

        bindPatchList(companyID);

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

        string where = " n.createon between '{1}' and '{2}' and n.companyID={0} ";
        where = string.Format(where,CompanyID, dateFrom, dateTo);

        if (ddlPatch.SelectedIndex > 0)
        {
            where += " and p.ID=" + ddlPatch.SelectedItem.Value;

        }

        string SearchKey = this.txtSearch.Text.Trim();
        if (SearchKey.Contains(","))
        {
            string rtn = "";
            string[] keys = SearchKey.Split(',');
            foreach (string key in keys)
            {
                if (key != "")
                {

                    rtn += " OR  c.tbName like N'%" + key + "%' or c.tbKey  like N'%" + key + "%'";

                }
            }
            rtn = rtn.Substring(3);
            where += " and ( "+rtn + ")";
           

        }
        else if (SearchKey!="")
        {
            where +=  " and ( c.tbName like N'%" + SearchKey + "%' or c.tbKey  like N'%" + SearchKey + "%')";
        }



        DataSet ds = BLL.ReportBLL.GetCollectonDetail(CompanyID, where);
        TotalRecord = ds.Tables[0].Rows.Count.ToString();
        if (ds.Tables[0].Rows.Count > 0)
        {
         
         
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


  

   
    protected void btn_Click(object sender, EventArgs e)
    {
        string dateFrom = this.txtFrom.Text.Trim(); ;
        string dateTo = this.txtTo.Text.Trim();
        BindGrid(dateFrom, dateTo);
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

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        string companyID = ddlCompany.SelectedItem.Value;

        bindPatchList(companyID);

        this.GridView1.DataSource = null;

        this.GridView1.DataBind();
    }
}

