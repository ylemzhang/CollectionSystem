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
using System.Text;


public partial class SameRecordSearch:AdminPageBase
{

    private DataSet PatchsDS;
    private DataSet CompanyDS;

    private DataSet ALLPatchsDS;
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

    protected string TotalRecords
    {
        get
        {
            if (null != this.ViewState["TotalRecords"])
            {
                return this.ViewState["TotalRecords"].ToString();
            }
            return null;
        }
        set
        {
            this.ViewState["TotalRecords"] = value;
        }
    }




    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            TotalRecords = "0";
            //if (base.IsAdmin)
            //{
                CompanyDS = BLL.CompanyBLL.GetCompanyList();
            //}

            //else
            //{
            //    Response.Redirect("Nopermission.htm");
            //}


            if (CompanyDS.Tables[0].Rows.Count == 0)
            {

                TotalRecords = "0";
                this.btnGo.Enabled = false;
               // this.btnMark.Enabled = false;
                return;
            }


            bindList();

            //this.btnMark.Enabled = false;


        }

    }



    private void bindList()
    {
        bindCompayList();
        bindPatchList();
        bindKeyList();

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

        PatchsDS = BLL.PatchBLL.GetCompanyPatchListByCompanyID(CompanyID);
        ddlPatch.DataSource = PatchsDS;


        ddlPatch.DataTextField = "PatchName";
        ddlPatch.DataValueField = "ID";
        ddlPatch.DataBind();


        ddlPatch.Items.Insert(0, "");
        ddlPatch.SelectedIndex = 0;
    }

    private void bindKeyList()
    {


        RadioButtonList1.Items.Add(new ListItem("账号/合同号", "tbKey"));
        RadioButtonList1.Items.Add(new ListItem("身份证", "tbIdentityNo"));
        RadioButtonList1.Items.Add(new ListItem("手机号", "tbMobile"));
        RadioButtonList1.Items.Add(new ListItem("姓名", "tbName"));

        RadioButtonList1.Items[0].Selected = true;

    }









    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false; 
        }
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != -1)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false; 
            string id = e.Row.Cells[0].Text;
            string first = e.Row.Cells[10].Text;
            if (first == "1")
            {
                e.Row.Attributes.Add("bgColor", "#F7F7F7");
            }
            string companyID = e.Row.Cells[1].Text;


            //e.Row.Attributes.Add("ondblclick", "window.open('CaseDetail.aspx?id=" + id + "&CompanyID=" + companyID + "')");
            e.Row.Attributes.Add("ondblclick", "window.open('CaseDetail.aspx?id=" + id + "&CompanyID=" + companyID + "','_blank')");

            e.Row.ToolTip = Common.StrTable.GetStr("dubbleClickToEdit");

            e.Row.Cells[1].Text = getCompanyName(e.Row.Cells[1].Text.Trim());
            e.Row.Cells[2].Text = getPatchName(e.Row.Cells[2].Text);
            e.Row.Cells[6].Text = GetUserName(e.Row.Cells[6].Text);


        }
    }




    //protected void btnMark_Click(object sender, EventArgs e)
    //{
    //    //MarkGridView2();
    //    //MarkGridView1();
    //    //Alert("saveSuccess");
    //}
    //private void MarkGridView2()//orignal
    //{
    //    if (GridView2.Rows.Count == 0)
    //        return;
    //    string idstr = "";

    //    for (int i = 0; i < this.GridView2.Rows.Count; i++)
    //    {
           
    //        idstr = idstr + this.GridView2.Rows[i].Cells[0].Text + ",";
    //    }


    //    string ids = idstr.Substring(0, idstr.Length - 1);
    //    new BLL.CaseBLL(int.Parse(CompanyID)).MarkRepeatedCase(1, ids);
      

    //}

    //private void MarkGridView1()//result
    //{
    //    if (GridView1.Rows.Count == 0)
    //        return;
      
    //    Hashtable companysandids = new Hashtable();
    //    for (int i = 0; i < this.GridView1.Rows.Count; i++)
    //    {
    //        string companyid = this.GridView1.Rows[i].Cells[8].Text;
    //        string caseID=this.GridView1.Rows[i].Cells[0].Text;
    //        if (!companysandids.Contains(companyid))
    //        {
    //            companysandids.Add(companyid, caseID+ ",");
    //        }
    //        else
    //        {
    //            companysandids[companyid] +=companysandids[companyid].ToString()+caseID+ ",";
    //        }
        
    //    }

    //    foreach (DictionaryEntry de in companysandids)
    //    {
    //       string  comapnyID=de.Key.ToString();
    //       string idstr = de.Value.ToString();
    //       idstr = idstr.Substring(0, idstr.Length - 1);
    //       new BLL.CaseBLL(int.Parse(comapnyID)).MarkRepeatedCase(1, idstr);
    //    }
      
    //}


    //protected void btnGoxxx_Click(object sender, EventArgs e)
    //{
    //    string where = "";
    //    string fields = "ID,OwnerID,tbName,tbKey,tbBalance,tbMobile,tbIdentityNo,PatchID";
    //    string key = this.RadioButtonList1.SelectedItem.Value;
    //    StringBuilder sb=new StringBuilder();

    //    StringBuilder sb2 = new StringBuilder();

    //    if (PatchID != "-1")
    //    {
    //        where = " where PatchID=" + PatchID;
    //    }

    //    string subSql = "(select distinct {1} from companycase_{0} {2})";
    //    subSql = string.Format(subSql, CompanyID, key, where);

    //    string template = " select {0} from companycase_{1} where {2} in {3}";


    //    string template2 = " select {0} from companycase_{1} where {2} in ({3})";

        

    //    CompanyDS = BLL.CompanyBLL.GetCompanyList();
    //    int count = CompanyDS.Tables[0].Rows.Count;

    //    for (int i = 0; i < count; i++)
    //    {

    //        DataRow dr = CompanyDS.Tables[0].Rows[i];
    //        string id = dr["ID"].ToString();

    //        if (HasCaseTable(id) && id!=CompanyID )
    //        {
    //            string fieldsAddCompanyID=id+"  as CompanyID," +fields;
    //            string tempsql = string.Format(template, fieldsAddCompanyID, id, key, subSql);
    //            sb.AppendLine(tempsql);
    //            sb.AppendLine("Union All");


    //            string tempsql2 = string.Format(template, key, id, key, subSql);
    //            sb2.AppendLine(tempsql2);
    //            sb2.AppendLine("Union All");


    //        }

    //    }

    //    string sql=sb.ToString();
    //    string sql2 = sb2.ToString();

    //    if (sql == "")
    //    {
    //        return;
    //    }
    //    else
    //    {

    //        sql = sql.Substring(0, sql.Length - "Union All".Length - 2);

    //        DataSet ds = ReportBLL.GetDataSet(sql);
          
    //        TotalRecords = ds.Tables[0].Rows.Count.ToString();

    //        if (TotalRecords == "0")
    //        {
    //           // this.btnMark.Enabled = false;
    //            return;
    //        }
           
    //            //this.btnMark.Enabled = true;

    //            ALLPatchsDS = BLL.PatchBLL.GetPatchList();

    //        this.GridView1.DataSource = ds;
    //        this.GridView1.DataBind();
           
    //        sql2 = sql2.Substring(0, sql2.Length - "Union All".Length - 2);

    //        string fieldsAddCompanyID = CompanyID  + "  as CompanyID," + fields;

    //        sql2 = string.Format(template2, fieldsAddCompanyID, CompanyID, key, sql2);

    //       ds = ReportBLL.GetDataSet(sql2);
    //        this.GridView2.DataSource = ds;
    //        this.GridView2.DataBind();
    //    }



    //}


    protected void btnGoxxx_Click(object sender, EventArgs e)
    {
        string where = "";
    
        string key = this.RadioButtonList1.SelectedItem.Value;

   

        if (PatchID != "-1")
        {
            where = " where PatchID=" + PatchID;
        }

        
       string  Sql2where =string.Format( "{2} in (select distinct {2} from companycase_{0} {1} )",CompanyID,where,key);

        StringBuilder subsb = new StringBuilder();

        StringBuilder sb2 = new StringBuilder();

        string subSql;

        string template2 = @"select {0}  as CompanyID,ID,OwnerID,tbName,tbKey,tbBalance,tbMobile,tbIdentityNo,PatchID,2 as ccc
 from companycase_{0} 
where  " + Sql2where;

        string Subtemplate = @"select {1}
 from companycase_{0} where {1} in (select distinct {1} from companycase_{2} {3} )";
//where  " + where;

//      select tbKey from companycase_38 where tbKey in (select distinct tbKey from companycase_40  where PatchID=33)


        CompanyDS = BLL.CompanyBLL.GetCompanyList();
        int count = CompanyDS.Tables[0].Rows.Count;

        for (int i = 0; i < count; i++)
        {

            DataRow dr = CompanyDS.Tables[0].Rows[i];
            string id = dr["ID"].ToString();

            if (HasCaseTable(id) && id != CompanyID)
            {

                string temp2 = string.Format(template2, id);
                sb2.AppendLine(temp2);
                sb2.AppendLine("Union All");


                string sub = string.Format(Subtemplate, id, key, CompanyID, where);
                subsb.AppendLine(sub);
                subsb.AppendLine("Union All");


            }

        }

      
        string sql2 = sb2.ToString();

        if (sql2 == "")
        {
            return;
        }
        else
        {
            ALLPatchsDS = BLL.PatchBLL.GetPatchList();
            sql2 = sql2.Substring(0, sql2.Length - "Union All".Length - 2);


            subSql = subsb.ToString();
          subSql = subSql.Substring(0, subSql.Length - "Union All".Length - 2);

            string sql1 = @"select {0}  as CompanyID,ID,OwnerID,tbName,tbkey,tbBalance,tbMobile,tbIdentityNo,PatchID,1 as ccc
from companycase_{0}
 where {2} in ({1}) {3} ";

            if (PatchID != "-1")
            {
                where = " and PatchID=" + PatchID;
            }
            else
            {
                where = "";
            }

            sql1 = string.Format(sql1, CompanyID, subSql, key,where);

           

            string searchSql = @"select * from ({0} union all
{1} ) as aaa order by {2},ccc,companyID";
            searchSql = string.Format(searchSql, sql1, sql2,key);


            DataSet ds = ReportBLL.GetDataSet(searchSql);

            this.GridView1.DataSource = ds;
            this.GridView1.DataBind();
        }



    }



    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindPatchList();
        TotalRecords = "0";


        this.GridView1.DataSource = null;
        this.GridView1.DataBind();

        //this.GridView2.DataSource = null;
        //this.GridView2.DataBind();



    }
   


    private string getCompanyName(string id)
    {
        if (id == "")
        {
            return "";
        }
        foreach (DataRow dr in CompanyDS.Tables[0].Rows)
        {
            if (dr["ID"].ToString() == id)
            {
                return dr["CompanyName"].ToString();
            }
        }
        return "";
    }


    private string getPatchName(string id)
    {
        if (id == "")
        {
            return "";
        }
        foreach (DataRow dr in  this.ALLPatchsDS .Tables[0].Rows)
        {
            if (dr["ID"].ToString() == id)
            {
                return dr["PatchName"].ToString();
            }
        }
        return "";
    }

}

