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

using System.Drawing;

using System.Text;
using BLL;

public partial class CaseList : PageBase
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

    // protected string TotalRecord = "";

    private void GetTotalNum(DataSet ds)
    {
        double num = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            num += double.Parse(dr[6].ToString());
        }
        long abc = (long)num;
        this.spanToNum.InnerText = abc.ToString();

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
            bindCompayList();

           // BindList();
            if (Act != "1")  //basic
            {
                bindUsers();
            }
            else
            {
                ddlUser.Enabled = false;
            }

            if (Act == "4")
            {
                this.btnRemoveFromClass.Style.Add("display", "block");
            }

            if (Act == "3")
            {
                this.linkDeleteHelp.Style.Add("display", "block");
            }

            if (!IsAdmin) //apply 
            {
                if (Act == "1" || Act == "5")
                {
                    this.spanApply.Style.Add("display", "block");
                    string app = System.Configuration.ConfigurationManager.AppSettings["Application"];
                    string[] apps = app.Split('|');
                    ddlApp.DataSource = apps;
                    ddlApp.DataBind();
                    ddlApp.SelectedIndex = 1;
                }

            }

            if (Act == "1" || Act == "5")
            {
                spanCaseType.Style.Add("display", "block");
                BindCaseTypeDropdownList();
            }


        }
    }

    private void BindCaseTypeDropdownList()
    {
        string id = "1"; //case type
        DataSet ds = new BLL.TypeBLL().GetTypeDataListByTypeID(id);
        ddlCaseType.DataSource = ds;

        ddlCaseType.DataTextField = "FTypeValue";
        ddlCaseType.DataValueField = "ID";
        ddlCaseType.DataBind();

        ddlCaseType.SelectedIndex = 0;
    }

    private void bindUsers()
    {
        ddlUser.DataSource = UserDS;
        ddlUser.DataTextField = "UserName";
        ddlUser.DataValueField = "ID";
        ddlUser.DataBind();

        ddlUser.Items.Insert(0, "");
        ddlUser.SelectedIndex = 0;
    }
    private string Act
    {
        get
        {
            return Request["act"] as string;
        }
    }

    private void GetCompanies()
    {
        if (Act == "1")
        {
            companyDS = GetMyCompany();
        }
        else if (Act == "2")
        {
            string sql = "select * from companytable where id in (select distinct companyid  from readcaseusers where userID={0})";
            companyDS = BLL.ReportBLL.GetDataSet(string.Format(sql, CurrentUser.ID));
        }
        else if (Act == "3")
        {
            string sql = "select * from companytable where id in (select distinct companyid  from OpenedCase where userID={0})";
            companyDS = BLL.ReportBLL.GetDataSet(string.Format(sql, CurrentUser.ID));
        }
        else if (Act == "4")
        {
            string sql = "select * from companytable where id in (select d.companyID from caseType c , CaseTypeData d where c.id=d.CasetypeID and userID={0} )";
            companyDS = BLL.ReportBLL.GetDataSet(string.Format(sql, CurrentUser.ID));


        }
        else if (Act == "5")
        {
            if (IsAdmin)
            {
                companyDS = GetMyCompany();
            }
            else
            {
                string sql = "select * from companytable where id in (select distinct CompanyID from companyuser where groupID in (select id from grouptable where leadid={0})  )";
                companyDS = BLL.ReportBLL.GetDataSet(string.Format(sql, CurrentUser.ID));
            }


        }

    }


    string sqlWhere
    {
        get
        {
            if (ViewState["sqlWhere"] == null)
            {
                ViewState["sqlWhere"] = GetSearchWhere();

            }
            return ViewState["sqlWhere"].ToString();

        }
        set
        {
            ViewState["sqlWhere"] = value;
        }
    }

    private string GetSql()
    {
        string where = sqlWhere;


        string sqlTempate = @"select {0} as CompanyID,c.ID,p.PatchName, c.OwnerID, c.tbName, c.tbKey,  c.tbBalance, -1 as notetime, p.importtime,c.tbIdentityNo from companycase_{0} c 
inner join  patch  p on c.PatchID=p.id 
   where  {1}  and companyID={0}";

        if (Act == "1")  //basic
        {

            sqlTempate += " and c.OwnerID = " + CurrentUser.ID;


        }
        else if (Act == "2")  //readonly
        {
            if (this.ddlUser.SelectedIndex > 0)
            {
                sqlTempate += " and c.OwnerID = " + ddlUser.SelectedItem.Value;
            }
            sqlTempate += " and c.ID in (select CaseID from ReadCaseUsers where userid= " + CurrentUser.ID + " and CompanyID={0})";
        }
        else if (Act == "3")  //assited case
        {
            if (this.ddlUser.SelectedIndex > 0)
            {
                sqlTempate += " and c.OwnerID = " + ddlUser.SelectedItem.Value;
            }
            sqlTempate += " and c.ID in (select CaseID from OpenedCase where userid= " + CurrentUser.ID + " and CompanyID={0})";
        }
        else if (Act == "4")  //classified cases
        {
            if (this.ddlUser.SelectedIndex > 0)
            {
                sqlTempate += " and c.OwnerID = " + ddlUser.SelectedItem.Value;
            }
            string categoryID = Request["category"];
            sqlTempate += " and c.ID in (select CaseID from CaseTypeData where CaseTypeID= " + categoryID + " and CompanyID={0})";
        }


        if (Act == "5")  //basic
        {
            if (this.ddlUser.SelectedIndex > 0)
            {
                sqlTempate += " and c.OwnerID = " + ddlUser.SelectedItem.Value;
            }
            if (IsAdmin)
            {
                sqlTempate += " and c.OwnerID <> " + CurrentUser.ID;
            }
            else //lead
            {
                string sss = " and c.OwnerID <> {0} and c.OwnerID <> 1 and c.OwnerID in (select distinct userID from companyuser where groupID in (select id from grouptable where leadid={0})) ";
                sqlTempate += string.Format(sss, CurrentUser.ID);

            }


        }


        if (ddlCaseType.SelectedIndex > 0)
        {
            sqlTempate += " and c.ID in (select caseid from alerttable where alerttype=3 and str1='" + ddlCaseType.SelectedItem.Text + "' and CompanyID={0})";
        }


        if (ddlCompany.SelectedIndex > 0 || (ddlCompany.SelectedIndex == 0 && (Act == "1" || Act == "5")))
        {
            if (ddlPatch.SelectedIndex > 0)
            {
                sqlTempate += " and P.ID=" + ddlPatch.SelectedItem.Value;

            }
            return string.Format(sqlTempate, ddlCompany.SelectedItem.Value, where);


        }

        else
        {
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
    }

    DataSet ColorDS(DataSet ds)
    {
        string where = sqlWhere;


        string sqlTempate = @"select c.ID  from companycase_{0} c 
inner join  patch  p on c.PatchID=p.id 
   where  {1}  and companyID={0}";

        if (Act == "1")  //basic
        {

            sqlTempate += " and c.OwnerID = " + CurrentUser.ID;


        }
        else //5
        {
            if (IsAdmin)
            {
                sqlTempate += " and c.OwnerID <> " + CurrentUser.ID;
            }
            else //lead
            {
                string sss = " and c.OwnerID <> {0} and c.OwnerID <> 1 and c.OwnerID in (select distinct userID from companyuser where groupID in (select id from grouptable where leadid={0})) ";
                sqlTempate += string.Format(sss, CurrentUser.ID);


            }
        }

        if (ddlPatch.SelectedIndex > 0)
        {
            sqlTempate += " and P.ID=" + ddlPatch.SelectedItem.Value;

        }



        string idSql = string.Format(sqlTempate, ddlCompany.SelectedItem.Value, where);

        string sql = "select caseID,max(createon)  from notetable where companyid={1} and caseID in ({0}) group by caseID";
        sql = string.Format(sql, idSql, ddlCompany.SelectedItem.Value);

        DataSet dsNoteTime = BLL.ReportBLL.GetDataSet(sql);



        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string id = dr["ID"].ToString();
            DataRow[] drs = dsNoteTime.Tables[0].Select("caseID=" + id);
            DateTime beginTime;
            if (drs.Length > 0)
            {
                beginTime = DateTime.Parse(drs[0][1].ToString());
            }
            else
            {
                beginTime = DateTime.Parse(dr["ImportTime"].ToString());
            }

            DateTime now = DateTime.Now;
            TimeSpan ts = now - beginTime;
            if (ts.Days > day3)
            {
                if (Act == "1")
                {
                    if (IsAdmin || IsLead)  //主管和admin,自己不消失
                    {
                        dr["notetime"] = day3;
                    }
                    else
                    {
                        dr.Delete();
                    }
                }
                else //5
                {
                    dr["notetime"] = day3;
                }

            }
            else if (ts.Days > day2)
            {
                dr["notetime"] = day2;
            }
            else if (ts.Days > day1)
            {
                dr["notetime"] = day1;
            }

        }

        ds.Tables[0].AcceptChanges();
        return ds;


    }

    private void BindList()
    {

        string sql = GetSql();
        if (sql == "")
        {
            return;
        }


        DataSet ds = BLL.CaseBLL.GetSearchCaseList(sql);


        if ((Act == "1" || Act == "5") && ds.Tables[0].Rows.Count > 0)
        {
            ds = ColorDS(ds);
        }
        spanTotal.InnerText = ds.Tables[0].Rows.Count.ToString();
        GetTotalNum(ds);
        //if (ds.Tables[0].Rows.Count == 0)
        //{
        //    DataRow dr = ds.Tables[0].NewRow();
        //    ds.Tables[0].Rows.Add(dr);

        //}
        this.GridView1.DataSource = ds;
        if (Act != null && Act != "")
        {
            Session["CaseListDataSet"] = ds;
        }
        this.GridView1.DataBind();
    }

    protected void btnSort_Click(object sender, EventArgs e)
    {
        if (ViewState["order"] == null)
        {
            ViewState["order"] = "ASC";
        }
        else
        {
            if (ViewState["order"].ToString() == "ASC")
            {
                ViewState["order"] = "DESC";
            }
            else
            {
                ViewState["order"] = "ASC";
            }
        }

        string sql = GetSql();
        if (sql == "")
        {
            return;
        }


        DataSet ds = BLL.CaseBLL.GetSearchCaseList(sql);


        if ((Act == "1" || Act == "5") && ds.Tables[0].Rows.Count > 0)
        {
            ds = ColorDS(ds);
        }
        spanTotal.InnerText = ds.Tables[0].Rows.Count.ToString();
        GetTotalNum(ds);
        //if (ds.Tables[0].Rows.Count == 0)
        //{
        //    DataRow dr = ds.Tables[0].NewRow();
        //    ds.Tables[0].Rows.Add(dr);

        //}
        // this.GridView1.DataSource = ds;
        if (Act != null && Act != "")
        {
            Session["CaseListDataSet"] = ds;
        }

        string sort = txtSort.Text;
        ds.Tables[0].DefaultView.Sort = sort + " " + ViewState["order"];
        this.GridView1.DataSource = ds.Tables[0];

        this.GridView1.DataBind();



        ////DataSet ds = GetDataGridSource();
        ////string sort = txtSort.Text;
        ////ds.Tables[0].DefaultView.Sort = sort + " " + ViewState["order"];
        ////this.GridView1.DataSource = ds.Tables[0];
        ////GridView1.DataBind();
        ////initEditPage();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != -1)
        {
            string companyID = e.Row.Cells[2].Text;
            string caseID = e.Row.Cells[7].Text;

            CheckBox chk = (CheckBox)e.Row.FindControl("CheckBox1");
            chk.Attributes.Add("onclick", "checkSelect(this,'" + e.Row.Cells[6].Text.Trim() + "');");

            e.Row.Cells[1].Text = GetUserName(e.Row.Cells[1].Text.Trim());
            //e.Row.Cells[1].Style.Add("Color","blue");
            //e.Row.Cells[1].Attributes.Add("ondblclick", "window.alert('ha ha')");
           
            e.Row.Cells[2].Text = getCompanyName(companyID);

            e.Row.Attributes.Add("ondblclick", "window.open('CaseDetail.aspx?act=" + Act + "&id=" + caseID + "&CompanyID=" + companyID + "','_blank')");
            e.Row.ToolTip = Common.StrTable.GetStr("dubbleClickToEdit");
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            string day = e.Row.Cells[9].Text;
            if ((Act == "1" || Act == "5") && day != "-1")
            {
                e.Row.ForeColor = color(e.Row.Cells[9].Text);
                ////e.Row.Cells[6].Text = @"<a href=""javascript:sortList('tbBalance')"">"+e.Row.Cells[6].Text+"</a>";
                ////e.Row.Cells[10].Text = @"<a href=""javascript:sortList('tbCardNo')"">"+e.Row.Cells[6].Text+"</a>";

            }

        }
        else
        {
            if (Act == "1" || Act == "5")
            {

                e.Row.Cells[1].Text = @"<a href=""javascript:sortList('OwnerID')"">业务员</a>";
                e.Row.Cells[2].Text = @"<a href=""javascript:sortList('CompanyID')"">公司</a>";
                e.Row.Cells[3].Text = @"<a href=""javascript:sortList('tbName')"">姓名</a>";
                e.Row.Cells[4].Text = @"<a href=""javascript:sortList('PatchName')"">批号</a>";
                e.Row.Cells[5].Text = @"<a href=""javascript:sortList('tbKey')"">帐号</a>";
                e.Row.Cells[6].Text = @"<a href=""javascript:sortList('tbBalance')"">金额</a>";
                e.Row.Cells[10].Text = @"<a href=""javascript:sortList('tbIdentityNo')"">身份证</a>";
            }
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
        }
    }

    private Color color(string day)
    {

        if (day == day1.ToString())
        {
            return Color.DarkViolet;
        }
        else if (day == day2.ToString())
        {
            return Color.Red;
        }
        else  //day3
        {
            return Color.Gray;
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
        ddlCompany.DataTextField = "CompanyName";
        ddlCompany.DataValueField = "ID";
        ddlCompany.DataBind();

        if (Act == "1" || Act == "5")  //basic
        {
            ddlCompany.SelectedIndex = 0;
            string companyID = ddlCompany.SelectedItem.Value;

            bindPatchList(companyID);
        }
        else
        {
            ddlCompany.Items.Insert(0, "");
            ddlCompany.SelectedIndex = 0;
        }



    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {


        string companyID = ddlCompany.SelectedItem.Value;

        bindPatchList(companyID);

        this.txtFrom.Text = "";
        this.txtTo.Text = "";
        this.ddlUser.Text = "";

        //sqlWhere = null;
        //BindList();

        //this.GridView1.DataSource = null;

        //this.GridView1.DataBind();




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

        sqlWhere = null;
        BindList();
    }

    private string GetSearchWhere()
    {

        string where = "  p.ExpireDate>getdate()  ";


        string sumFrom = txtFrom.Text.Trim();
        string sumTo = txtTo.Text.Trim();

        if (sumFrom != "")
        {
            try
            {

                where += " and tbBalance>=" + decimal.Parse(sumFrom);
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

                where += " and tbBalance<=" + decimal.Parse(sumTo);
            }
            catch
            {

                this.txtTo.Text = "";
            }
        }



        return where;
    }


    protected void btnAddClass_Click(object sender, EventArgs e)
    {
        ArrayList arr = new ArrayList();
        string categoryID = this.txtClassID.Text;
        string sql = "Delete CaseTypeData where CaseID={0} and CompanyID={1} and CaseTypeID={2}";
        string sql1 = "insert CaseTypeData values({0} ,{1} ,{2})";
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)this.GridView1.Rows[i].FindControl("CheckBox1");
            if (chk.Checked)
            {
                string companyID = this.GridView1.Rows[i].Cells[8].Text;
                string caseID = this.GridView1.Rows[i].Cells[7].Text;
                arr.Add(string.Format(sql, caseID, companyID, categoryID));
                arr.Add(string.Format(sql1, caseID, companyID, categoryID));
            }

        }



        BLL.CaseTypeDataBLL.UpdateCaseTypeData(arr);

        BindList();

    }
    protected void RemoveFromClass_Click(object sender, EventArgs e)
    {

        ArrayList arr = new ArrayList();
        string categoryID = Request["category"];
        string sql = "Delete CaseTypeData where CaseID={0} and CompanyID={1} and CaseTypeID={2}";
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)this.GridView1.Rows[i].FindControl("CheckBox1");
            if (chk.Checked)
            {
                string companyID = this.GridView1.Rows[i].Cells[8].Text;
                string caseID = this.GridView1.Rows[i].Cells[7].Text;
                arr.Add(string.Format(sql, caseID, companyID, categoryID));
            }

        }



        BLL.CaseTypeDataBLL.UpdateCaseTypeData(arr);

        BindList();


    }
    protected void DeleteHelpuser_Click(object sender, EventArgs e)
    {
        ArrayList arr = new ArrayList();
        string categoryID = Request["category"];
        string sql = "Delete OpenedCase where CaseID={0} and CompanyID={1} and UserID={2}";
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)this.GridView1.Rows[i].FindControl("CheckBox1");
            if (chk.Checked)
            {
                string companyID = this.GridView1.Rows[i].Cells[8].Text;
                string caseID = this.GridView1.Rows[i].Cells[7].Text;
                arr.Add(string.Format(sql, caseID, companyID, CurrentUser.ID));
            }

        }



        BLL.CaseTypeDataBLL.UpdateCaseTypeData(arr);

        BindList();

    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        string applyText = ddlApp.SelectedItem.Text;

        string companyID = ddlCompany.SelectedItem.Value;
        string company = ddlCompany.SelectedItem.Text;
        string lead = getLead(companyID);

        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)this.GridView1.Rows[i].FindControl("CheckBox1");
            if (chk.Checked)
            {

                string caseID = this.GridView1.Rows[i].Cells[7].Text;

                string url = string.Format("CaseDetail.aspx?id={0}&CompanyID={1}", caseID, companyID);
                string title = string.Format("{0}--{1}--{2}", applyText, company, this.GridView1.Rows[i].Cells[3].Text);

                string txt = applyText + "---" + @" <a href='#' onclick=""OpenWindow('{0}',1000,800)"" >{1}</a> <br/>";
                string body = string.Format(txt, url, title);
                BLL.MessageBLL.SendMessage(title, body, this.CurrentUser.UserName, lead, "0", "", DateTime.Now);

            }

        }

        //string script = "allUnSelect();";
        //base.ExceuteScript(script);
        //BindList();







    }

    private string getLead(string companyID)
    {

        DataSet ds = GroupBLL.GetLeadID(CurrentUser.ID, companyID);
        if (ds.Tables[0].Rows.Count == 0)
        {
            return "Admin";

        }
        else
        {
            string leadid = ds.Tables[0].Rows[0][0].ToString();
            if (leadid == CurrentUser.ID)
            {
                return "Admin";
            }
            SystemUser user = AdminBLL.GetUserByID(leadid);
            return user.UserName;

        }
    }

    protected void ddlCaseType_SelectedIndexChanged(object sender, EventArgs e)
    {
        sqlWhere = null;
        BindList();
    }
    protected void btnShowTime_Click(object sender, EventArgs e)
    {

        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            Label lbl1 = (Label)this.GridView1.Rows[i].FindControl("labelTime");

            string companyID = this.GridView1.Rows[i].Cells[8].Text;
            string caseID = this.GridView1.Rows[i].Cells[7].Text;

            lbl1.Text = GetTime(companyID, caseID);


        }

    }

    private string GetTime(string companyID, string caseID)
    {
        return NoteBLL.GetTime(companyID, caseID);
    }
}
