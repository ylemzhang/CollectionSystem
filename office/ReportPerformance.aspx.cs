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

public partial class ReportPerformance : PageBase
{
    private string tableHtml = @"  <table width=100% border=1 bordercolor =black  cellspacing=0>
      <tr><td  bgcolor='#cccccc'align='center'  rowspan =2 width=60px >催收人员</td><td bgcolor='#cccccc' align=center colspan =2 width=120px> 在案业务量</td>
   <td rowspan =2 align='center' bgcolor='#cccccc' width=160px >逾期时间179天以下还款金额</td><td rowspan =2 align='center' bgcolor='#cccccc' width=210px >逾期时间逾期180-359天以下还款金额</td><td rowspan =2 align='center' bgcolor='#cccccc' width=160px >逾期时间360天以上还款金额</td>
    <td rowspan =2 align='center' bgcolor='#cccccc' width=100px >累计还款总金额 </td><td rowspan =2 align='center' bgcolor='#cccccc' width=100px >累计还款比率</td><td rowspan =2 align='center' bgcolor='#cccccc' width=100px >累计还款率排名</td>
   </tr>
  <tr><td align='center'  width=60px>户数</td><td align='center'  width=60px>金额</td></tr> 
{0}
</table> 
  ";



    private string resutlTemplate = @" 
   <tr>
   <td  bgcolor='#cccccc'align='center'><span style='color:blue;cursor:hand'  onclick=""window.open('PaymentReportDetail.aspx?companyID={9}&userID={10}')"">{0}</span></td><td align='center'> {1}</td><td  align='center'> {2}</td>
 <td  align='center'>{3}</td><td  align='center'> {4}</td><td align='center'> {5}</td>
 <td  align='center'>{6}</td><td  align='center'> {7}</td><td  align='center'> {8}</td>
   </tr>";


    private string resutlTotalTemplate = @" 
   <tr height=25  bgcolor='#cccccc' >
   <td  bgcolor='#cccccc'align='center'>{0}</td><td align='center'> {1}</td><td  align='center'> {2}</td>
 <td  align='center'>{3}</td><td  align='center'> {4}</td><td align='center'> {5}</td>
 <td  align='center'>{6}</td><td  align='center'> {7}</td><td  align='center'> {8}</td>
   </tr>";

    private DataSet PatchsDS;
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
            return ddlCompany.SelectedValue;
        }
    }

    protected string PatchID
    {
        get
        {
            if (ddlPatch.SelectedValue == null || ddlPatch.SelectedValue == "")
            {
                return "-1";
            }
            return ddlPatch.SelectedValue;
        }
    }

    protected string GroupID
    {
        get
        {
            if (ddlGroups.SelectedValue == null || ddlGroups.SelectedValue == "")
            {
                return "-1";
            }
            return ddlGroups.SelectedValue;
        }
    }

    private DataSet TotalDS;







    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            DateTime dt = DateTime.Now;
            this.txtFrom.Text = dt.ToString("yyyy-MM-dd");
            this.txtTo.Text = this.txtFrom.Text;



            CompanyDS = BLL.CompanyBLL.GetCompanyList();


            bindList();
            if ((!HasCaseTable(CompanyID)) || (!HasPaymentTable(CompanyID)))
            {
                this.btn.Visible = false;
            }

        }
    }


    private void bindList()
    {
        bindCompayList();
        bindPatchList();
        bindGroupList();

    }

    private void bindCompayList()
    {

        ddlCompany.DataSource = CompanyDS;
        ddlCompany.DataTextField = "CompanyName";
        ddlCompany.DataValueField = "ID";
        ddlCompany.DataBind();
        ddlCompany.SelectedIndex = 0;

    }

    private void bindPatchList()
    {
        if (CompanyID == "")
            return;
        PatchsDS = BLL.PatchBLL.GetCompanyPatchListByCompanyID(CompanyID);
        ddlPatch.DataSource = PatchsDS;


        ddlPatch.DataTextField = "PatchName";
        ddlPatch.DataValueField = "ID";
        ddlPatch.DataBind();

        ddlPatch.Items.Insert(0, "");
        ddlPatch.SelectedIndex = 0;




    }


    private void bindGroupList()
    {
        if (CompanyID == "")
            return;
        DataSet ds = BLL.GroupBLL.GetGroupList("CompanyID=" + CompanyID);
        ddlGroups.DataSource = ds;


        ddlGroups.DataTextField = "GroupName";
        ddlGroups.DataValueField = "ID";
        ddlGroups.DataBind();

        ddlGroups.Items.Insert(0, "");
        ddlGroups.SelectedIndex = 0;




    }







    protected void btnSearch_Click(object sender, EventArgs e)//
    {
        if (CompanyID == "")
        {
            this.lnkExcel.Visible = false;
            ResutlHtmlForPage = string.Format(tableHtml, "");
            return;
        }

        string ReportUserIDs;
        string CaseUserIDS;
        string IDstr;
        //get all the relative users
        if (this.GroupID == "-1")
        {
            IDstr = "({0} In (select userID from companyuser where companyID={1} ) or {0} in (select leadID from grouptable where  companyID={1}) )";
            ReportUserIDs = string.Format(IDstr, "ID", CompanyID);
            CaseUserIDS = string.Format(IDstr, "OwnerID", CompanyID);
            ReportUserDS = BLL.AdminBLL.GetUserList("ID,UserName", ReportUserIDs);
            //if (this.IsAdmin)
            //{
            //    IDstr = "({0} In (select userID from companyuser where companyID={1} ) or {0} in (select leadID from grouptable where  companyID={1}) )";
            //    ReportUserIDs = string.Format(IDstr, "ID", CompanyID);
            //    CaseUserIDS = string.Format(IDstr, "OwnerID", CompanyID);
            //    ReportUserDS = BLL.AdminBLL.GetUserList("ID,UserName", ReportUserIDs);
            //}
            //else
            //{
            //    IDstr = "( {0}={1} or {0}  in ( select distinct userID from companyuser where groupID in (select ID from grouptable where leadID={1} and CompanyID={2}) ) )";
            //    ReportUserIDs = string.Format(IDstr, "ID", CurrentUser.ID, CompanyID);
            //    CaseUserIDS = string.Format(IDstr, "OwnerID", CurrentUser.ID, CompanyID);
            //    ReportUserDS = BLL.AdminBLL.GetUserList("ID,UserName", ReportUserIDs);

            //}
        }
        else
        {
            IDstr = "({0} In (select distinct userID from companyuser where companyID={1} and GroupID={2})  )";
            ReportUserIDs = string.Format(IDstr, "ID", CompanyID, GroupID);
            CaseUserIDS = string.Format(IDstr, "OwnerID", CompanyID, GroupID);
            ReportUserDS = BLL.AdminBLL.GetUserList("ID,UserName", ReportUserIDs);

            //if (this.IsAdmin)
            //{
            //    IDstr = "({0} In (select distinct userID from companyuser where companyID={1} and GroupID={2})  )";
            //    ReportUserIDs = string.Format(IDstr, "ID", CompanyID, GroupID);
            //    CaseUserIDS = string.Format(IDstr, "OwnerID", CompanyID, GroupID);
            //    ReportUserDS = BLL.AdminBLL.GetUserList("ID,UserName", ReportUserIDs);
            //}
            //else
            //{
            //    IDstr = "( {0}={1} or {0}  in ( select distinct userID from companyuser where groupID in (select ID from grouptable where leadID={1} and CompanyID={2} and ID={3}) ) )";
            //    ReportUserIDs = string.Format(IDstr, "ID", CurrentUser.ID, CompanyID,GroupID);
            //    CaseUserIDS = string.Format(IDstr, "OwnerID", CurrentUser.ID, CompanyID, GroupID);
            //    ReportUserDS = BLL.AdminBLL.GetUserList("ID,UserName", ReportUserIDs);

            //}
        }

        //end 



        string where = "";

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

        string guo = " and PatchID in (select ID  from patch where CompanyID={2} and expiredate>getdate()) ";
        if (PatchID == "-1")
        {
            where += "  tbPayDate between '{0}' and '{1}'" + guo;
            where = string.Format(where, dateFrom, dateTo, CompanyID);
        }
        else
        {
            where += "PatchID= {0} and tbPayDate between '{1}' and '{2}'" + guo;
            where = string.Format(where, PatchID, dateFrom, dateTo);
        }

        where += " And  " + CaseUserIDS;



        TotalDS = new BLL.ReportBLL(CompanyID).GetTotalCaseNumOfUsers(PatchID);

        ResultDS = new BLL.ReportBLL(CompanyID).GetTotalPaymentAllUsers(where);

        if (ResultDS.Tables[0].Rows.Count == 0)
        {

            ResutlHtmlForPage = string.Format(tableHtml, "");

        }
        else
        {
            //过滤admin
            if (!IsAdmin)
            {
                if (null != ReportUserDS && ReportUserDS.Tables.Count > 0 && ReportUserDS.Tables[0].Rows.Count > 0)
                {
                    int rowsCount = ReportUserDS.Tables[0].Rows.Count;
                    for (int i = 0; i < rowsCount; i++)
                    {
                        if (ReportUserDS.Tables[0].Rows[i]["UserName"].ToString().Trim().ToLower().Equals("admin"))
                        {
                            ReportUserDS.Tables[0].Rows.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
            lnkExcel.Visible = true;
            FormHtml();
            Session["ReportWhere"] = string.Format("  tbPayDate between '{0}' and '{1}'", dateFrom, dateTo);
        }

    }


    private void FormHtml()
    {



        decimal totalRateDec = 0;


        StringBuilder sb = new StringBuilder();
        int count = ReportUserDS.Tables[0].Rows.Count;
        decimal[] ranks = new decimal[count];
        decimal[] oldranks = new decimal[count];
        for (int i = 0; i < count; i++)
        {
            totalRateDec = 0;
            sb.Append(FormRow(ReportUserDS.Tables[0].Rows[i][0].ToString(), ReportUserDS.Tables[0].Rows[i][1].ToString(), ref totalRateDec));//id,name
            ranks[i] = totalRateDec;
        }

        if (count > 1)
        {
            decimal ttt = 0;
            if (Sum_totoalBalance != 0)
            {
                ttt = (Sum_totalWithinTime / Sum_totoalBalance) * 100;
            }

            ttt = Math.Round(ttt, 2);
            string percentage = ttt.ToString() + "%";

            string TotalTr = string.Format(resutlTotalTemplate, "Total:", Sum_accountNumber, Sum_totoalBalance, Sum_exp1, Sum_exp2, Sum_exp3, Sum_totalWithinTime, percentage, "");
            sb.Append(TotalTr);

        }
        string trsHtml = sb.ToString();

        Array.Copy(ranks, oldranks, count);
        Array.Sort(ranks);
        for (int i = 0; i < count; i++)
        {
            int rank = count - (Array.IndexOf(ranks, oldranks[i]));
            trsHtml = trsHtml.Replace("RANK" + ReportUserDS.Tables[0].Rows[i][0].ToString(), rank.ToString());
        }

        ResutlHtmlForPage = string.Format(tableHtml, trsHtml);
    }

    int Sum_accountNumber = 0;
    decimal Sum_totoalBalance = 0;
    decimal Sum_exp1 = 0;
    decimal Sum_exp2 = 0;
    decimal Sum_exp3 = 0;
    decimal Sum_totalWithinTime = 0;

    private string FormRow(string userID, string userName, ref  decimal totalRateDec)
    {


        int accountNumber = 0;
        decimal totoalBalance = 0;
        decimal exp1 = 0;
        decimal exp2 = 0;
        decimal exp3 = 0;
        decimal totalWithinTime = 0;
        string percentage = "";
        string rank = "RANK" + userID;
        DataRow[] drs = GetFormAcountandBalanceResult("ownerID =" + userID);
        if (drs.Length > 0)
        {
            accountNumber = int.Parse(drs[0][1].ToString());
            totoalBalance = decimal.Parse(drs[0][2].ToString());
        }

        GetExpiredDataSum(ref  exp1, ref  exp2, ref  exp3, userID);
        totalWithinTime = exp1 + exp2 + exp3;

        if (totoalBalance != 0)
        {
            totalRateDec = (totalWithinTime / totoalBalance) * 100;
        }

        decimal ttt = Math.Round(totalRateDec, 2);
        percentage = ttt.ToString() + "%";
        string rtn = string.Format(resutlTemplate, userName, accountNumber, totoalBalance, exp1, exp2, exp3, totalWithinTime, percentage, rank, CompanyID, userID);

        Sum_accountNumber += accountNumber;
        Sum_totoalBalance += totoalBalance;
        Sum_exp1 += exp1;
        Sum_exp2 += exp2;
        Sum_exp3 += exp3;
        Sum_totalWithinTime += totalWithinTime;
        return rtn;
    }

    private DataRow[] GetFormResult(string where)
    {
        return ResultDS.Tables[0].Select(where);
    }

    private DataRow[] GetFormAcountandBalanceResult(string where)
    {
        return TotalDS.Tables[0].Select(where);
    }

    private void GetExpiredDataSum(ref decimal exp1, ref decimal exp2, ref decimal exp3, string userID)
    {
        DataRow[] drs = GetFormResult("ownerID =" + userID);

        int start = 0;
        foreach (DataRow dr in drs)
        {
            string expiredstatus = dr[1].ToString().Trim();
            decimal sum = decimal.Parse(dr[2].ToString());
            if (expiredstatus == "")
            {
                exp1 += sum;
                continue;
            }
            else if (expiredstatus.Contains("未逾期"))
            {

                exp1 += sum;
                continue;



            }
            else
            {
                if (expiredstatus.Contains("-"))
                {
                    expiredstatus = expiredstatus.Replace("逾期", "").Replace("天", "");
                    try
                    {
                        string startStr = expiredstatus.Split(new char[] { '-' })[0];

                        start = int.Parse(startStr);
                    }
                    catch
                    {
                        start = 0;
                    }
                }
                else if (expiredstatus.Contains("以上"))
                {
                    expiredstatus = expiredstatus.Replace("逾期", "").Replace("天", "").Replace("以上", "");
                    try
                    {


                        start = int.Parse(expiredstatus) + 1;
                    }
                    catch
                    {
                        start = 0;
                    }
                }
                else if (expiredstatus.Contains("以下"))
                {
                    expiredstatus = expiredstatus.Replace("逾期", "").Replace("天", "").Replace("以下", "");
                    try
                    {


                        start = int.Parse(expiredstatus) - 1;
                    }
                    catch
                    {
                        start = 0;
                    }

                }

            }

            if (start < 180)
            {
                exp1 += sum;
            }
            else if (start < 360)
            {
                exp2 += sum;
            }
            else
            {
                exp3 += sum;
            }

        }
    }



    public void EduceExcel(string html)
    {
        Response.Clear();
        Response.Buffer = true;//设置缓冲输出     
        //Response.Charset = "UTF-8";//设置输出流的HTTP字符集   

        Response.AppendHeader("Content-Disposition", "attachment;filename=ReportPerformance.xls");


        Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");


        Response.Write(html);

        Response.End();
    }


    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = ddlCompany.SelectedValue;
        if (HasCaseTable(id) && HasPaymentTable(id))
        {
            bindPatchList();
            bindGroupList();

            btn.Visible = true;
        }
        else
        {
            ddlPatch.Items.Clear();
            ddlGroups.Items.Clear();
            btn.Visible = false;
        }
        this.lnkExcel.Visible = false;
        ResutlHtmlForPage = string.Format(tableHtml, "");
    }
    protected void lnkExcel_Click(object sender, EventArgs e)
    {
        EduceExcel(ResutlHtmlForPage);
    }
}
