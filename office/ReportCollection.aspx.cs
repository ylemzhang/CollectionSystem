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

public partial class ReportCollection : PageBase
{
    private string tableHtml = @"  <table width=600 border=1 bordercolor =black  cellspacing=0 align=center>
      <tr><td  bgcolor='#cccccc'align='center'  width=60px >催收人员</td><td bgcolor='#cccccc' align=center  width=120px> 电话访问次数</td>
   <td align='center' bgcolor='#cccccc' width=160px >拜访次数</td><td  align='center' bgcolor='#cccccc' width=210px >路费合计</td>
   </tr>
 
{0}
</table> 
  ";



    private string resutlTemplate = @" 
   <tr>
   <td  bgcolor='#cccccc'align='center'>{0}</td><td align='center'> {1}</td><td  align='center'> {2}</td>
 <td  align='center'>{3}</td>
   </tr>";


    private string resutlTotalTemplate = @" 
   <tr height=25  bgcolor='#cccccc' >
    <td  bgcolor='#cccccc'align='center'>{0}</td><td align='center'> {1}</td><td  align='center'> {2}</td>
 <td  align='center'>{3}</td>
   </tr>";


    private DataSet CompanyDS;
    private DataSet ResultDS;
    private DataSet ReportUserDS;
    protected string ResutlHtmlForPage
    {
        get
        {
            if (this.ViewState["ResutlHtmlForPage"] == null)
            {
                return "";
            }
            return ViewState["ResutlHtmlForPage"].ToString();
        }
        set
        {
            ViewState["ResutlHtmlForPage"] = value;
        }
    }



    protected string CompanyID
    {
        get
        {
            if (ddlCompany.SelectedValue == null || ddlCompany.SelectedValue == "")
            {
                return "-1";
            }
            return ddlCompany.SelectedValue;
        }
    }








    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            DateTime dt = DateTime.Now;
            this.txtFrom.Text = dt.ToString("yyyy-MM-dd");
            this.txtTo.Text = this.txtFrom.Text;


            CompanyDS = BLL.CompanyBLL.GetCompanyList();

            bindList();


        }
    }


    private void bindList()
    {
        bindCompayList();
      
    }

    private void bindCompayList()
    {

        ddlCompany.DataSource = CompanyDS;
        ddlCompany.DataTextField = "CompanyName";
        ddlCompany.DataValueField = "ID";
        ddlCompany.DataBind();

        ddlCompany.Items.Insert(0, "");
        ddlCompany.SelectedIndex = 0;
   

    }

  







    protected void btnSearch_Click(object sender, EventArgs e)//
    {

        string NoteUserIDS;
        string ReportUserIDs;
        string IDstr;
        string companyIDS;
        if (CompanyID == "-1")
        {
            companyIDS = "1=1";
        }
        else
        {
            companyIDS = " companyID=" + CompanyID;
        }

        IDstr = "({0} In (select userID from companyuser where {1} ) or {0} in (select leadID from grouptable where  {1}) )";
        ReportUserIDs = string.Format(IDstr, "ID", companyIDS);
        NoteUserIDS = string.Format(IDstr, "Createby", companyIDS);
        ReportUserDS = BLL.AdminBLL.GetUserList("ID,UserName", ReportUserIDs);


        //if (this.IsAdmin)
        //{
        //    IDstr = "({0} In (select userID from companyuser where {1} ) or {0} in (select leadID from grouptable where  {1}) )";
        //    ReportUserIDs = string.Format(IDstr, "ID", companyIDS);
        //    NoteUserIDS = string.Format(IDstr, "Createby", companyIDS);
        //    ReportUserDS = BLL.AdminBLL.GetUserList("ID,UserName", ReportUserIDs);
        //}
        //else
        //{
        //    IDstr = "( {0}={1} or {0}  in ( select distinct userID from companyuser where groupID in (select ID from grouptable where leadID={1} and {2}) ) )";
        //    ReportUserIDs = string.Format(IDstr, "ID", CurrentUser.ID, companyIDS);
        //    NoteUserIDS = string.Format(IDstr, "Createby", CurrentUser.ID, companyIDS);
        //    ReportUserDS = BLL.AdminBLL.GetUserList("ID,UserName", ReportUserIDs);

        //}

        string where = "NoteType<>0 ";

     
        string dateFrom = this.txtFrom.Text.Trim();
        string dateTo = this.txtTo.Text.Trim();
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


        if (CompanyID == "-1")
        {
            where += "and CreateOn between '{0}' and '{1}'";
            where = string.Format(where, dateFrom, dateTo);
        }
        else
        {
            where += "and CompanyID= {0} and CreateOn between '{1}' and '{2}'";
            where = string.Format(where, CompanyID, dateFrom, dateTo);
        }


        where += " And  " + NoteUserIDS;

      
       
        ResultDS = BLL.ReportBLL.GetTotalCollectonByUsers(where);
        if (ResultDS.Tables[0].Rows.Count == 0)
        {

            ResutlHtmlForPage = string.Format(tableHtml, "");

        }
        else
        {
            lnkExcel.Visible = true;
            FormHtml();
        }

    }

    private void FormHtml()
    {


   

        StringBuilder sb = new StringBuilder();
        int count = ReportUserDS.Tables[0].Rows.Count;
     
        for (int i = 0; i < count; i++)
        {

            sb.Append(FormRow(ReportUserDS.Tables[0].Rows[i][0].ToString(),GetUserName( ReportUserDS.Tables[0].Rows[i][0].ToString())));//id,name
      
        }

        if (count > 1)
        {


            string TotalTr = string.Format(resutlTotalTemplate, "Total:", Sum_telCount, Sum_visitCount, Sum_visitFee);
            sb.Append(TotalTr);

        }
        string trsHtml = sb.ToString();

    

        ResutlHtmlForPage = string.Format(tableHtml, trsHtml);
    }

   
    int Sum_telCount = 0;
    int Sum_visitCount = 0;
    decimal Sum_visitFee = 0;
 

    private string FormRow(string userID, string userName)
    {

        int telCount = 0;
        int visitCount = 0;
        decimal visitFee = 0;

        GetCollectionData(ref telCount, ref visitCount, ref visitFee, userID);

        string rtn = string.Format(resutlTemplate, userName, telCount, visitCount, visitFee);

        Sum_telCount += telCount;
        Sum_visitCount += visitCount;
        Sum_visitFee += visitFee;
      
        return rtn;
    }

    private void GetCollectionData(ref int telCount, ref int visitCount, ref decimal visitFee, string userID)
    {
        DataRow[] drs = GetFormResult("createby ='" + userID+"'");

        foreach (DataRow dr in drs)
        {
            if (dr[3].ToString() == "2")
            {
                visitFee = decimal.Parse(dr[2].ToString());
                visitCount = int.Parse(dr[1].ToString());
            }
            else if (dr[3].ToString() == "1")
            {
                telCount = int.Parse(dr[1].ToString());
            }
        }
    }
    private DataRow[] GetFormResult(string where)
    {
        return ResultDS.Tables[0].Select(where);
    }


  


    public void EduceExcel(string html)
    {
        Response.Clear();
        Response.Buffer = true;//设置缓冲输出     
        //Response.Charset = "UTF-8";//设置输出流的HTTP字符集   

        Response.AppendHeader("Content-Disposition", "attachment;filename=ReportCollection.xls");


        Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");


        Response.Write(html);

        Response.End();
    }



    protected void lnkExcel_Click(object sender, EventArgs e)
    {
        EduceExcel(ResutlHtmlForPage);
    }
}
