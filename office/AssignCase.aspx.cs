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

public partial class AssignCase : PageBase
{

    private DataSet PatchsDS;
    private DataSet CompanyDS;
    private DataSet CompanyAllReadUserDS;
    //{
    //    get
    //    {
    //        if (ViewState["CompanyAllReadUserDS"] == null)
    //        {
    //            ViewState["CompanyAllReadUserDS"]=BLL.ReadCaseUsersBLL.GetReadCaseUsersList("CompanyID=" + CompanyID);
    //        }
    //        return ViewState["CompanyAllReadUserDS"] as DataSet;
    //    }
       
    //}
 
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

    private void GetTotalNum(DataSet ds)
    {
        double num = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            num += double.Parse(dr[4].ToString());
        }
        long abc = (long)num;
        this.spanToNum.InnerText = abc.ToString();

    }


      protected string SearchWhere
    {
        get
        {

            return this.ViewState["SearchWhere"].ToString();

        }
        set
        {
            this.ViewState["SearchWhere"] = value;
        }
    }


   

    protected void Page_Load(object sender, EventArgs e)
    {
        this.PagingControl1.PagingClick += new EventHandler(PagingControl1_PagingClick);
        if (!this.IsPostBack)
        {
          
            
            //if (base.IsAdmin)
            //{
            //    CompanyDS = BLL.CompanyBLL.GetCompanyList();
            //}
         
            // else
            //{
            //    Response.Redirect("Nopermission.htm");
            //}

            CompanyDS = BLL.CompanyBLL.GetCompanyList();
         

            PagingControl1.ListRecordNumPerPage = base.ListRecordNumPerPage;
       
            if (CompanyDS.Tables[0].Rows.Count == 0)
            {
                PagingControl1.TotalRecords = 0;
                
                this.btnGo.Enabled = false;
                return;
            }
            
           
            bindList();

           
            BindGrid();

        }

    }

  

    private void bindList()
    {
        bindCompayList();
        bindPatchList();
        bindCompayUserList();
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

        PatchsDS=BLL.PatchBLL.GetCompanyPatchListByCompanyID(CompanyID);
        ddlPatch.DataSource = PatchsDS;

       
        ddlPatch.DataTextField = "PatchName";
        ddlPatch.DataValueField = "ID";
        ddlPatch.DataBind();


        if (PatchsDS.Tables[0].Rows.Count != 0)
        {
            ddlPatch.SelectedIndex = 0;
        }
       

    }


    private void bindCompayUserList()
    {

        ddlCompanyUsers.DataSource = GetCompanyUsers(CompanyID);
        ddlCompanyUsers.DataTextField = "UserName";
        ddlCompanyUsers.DataValueField = "ID";
        ddlCompanyUsers.DataBind();
        ddlCompanyUsers.Items.Insert(0, "未分配");
        ddlCompanyUsers.SelectedIndex = 0;
        ddlCompanyUsers.Items.Insert(0, "");
        ddlCompanyUsers.SelectedIndex = 0;

    }





    private void BindGrid()
    {
        if (!HasCaseTable(CompanyID))
        {
            PagingControl1.TotalRecords = 0;
            
            this.btnGo.Enabled = false;
            return;
        }
       // string where = "PatchID=" + PatchID;
        SearchWhere= "PatchID=" + PatchID;
        GetTotalRecords(SearchWhere);
        bind();

    }
    private void GetTotalRecords(string where)
    {
        int total = new CaseBLL(int.Parse(CompanyID)).GetCaseTotalItems(where);

        spanTotal.InnerText = total.ToString();


  

        PagingControl1.TotalRecords = total;

    }

    private void bind()
    {

        DataSet ds = GetDataGridSource();
       
        if (ds != null)
        {
            CompanyAllReadUserDS =BLL.ReadCaseUsersBLL.GetReadCaseUsersList("CompanyID=" + CompanyID);
           
            this.GridView1.DataSource = ds;
            this.GridView1.DataBind();
            GetTotalNum(ds);

        }
    }

    //private void bind1()
    //{
    //    string where = "PatchID=" + PatchID;


    //    DataSet ds = new CaseBLL(int.Parse(CompanyID)).GetCasePagingitems(this.PagingControl1.ListRecordNumPerPage, this.PagingControl1.CurrentPage, where);
     

    //    if (ds != null)
    //    {
    //        CompanyAllReadUserDS = BLL.ReadCaseUsersBLL.GetReadCaseUsersList("CompanyID=" + CompanyID);

    //        this.GridView1.DataSource = ds;
    //        this.GridView1.DataBind();

    //    }
    //}

    private DataSet GetDataGridSource()
    {
       // string where = "PatchID=" + PatchID;


        DataSet ds = new CaseBLL(int.Parse(CompanyID)).GetCasePagingitems (this.PagingControl1.ListRecordNumPerPage, this.PagingControl1.CurrentPage, SearchWhere);

        return ds;
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

        }
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != -1)
        {
            e.Row.Cells[1].Visible = false;
            string id = e.Row.Cells[1].Text;

            CheckBox chk = (CheckBox)e.Row.FindControl("CheckBox1");
            chk.Attributes.Add("onclick", "checkSelect(this,'" + e.Row.Cells[4].Text.Trim() + "');");

            e.Row.Attributes.Add("ondblclick", "window.open('CaseDetail.aspx?id=" + id + "&CompanyID=" + CompanyID + "','_blank')");
            e.Row.ToolTip = Common.StrTable.GetStr("dubbleClickToEdit");
            e.Row.Cells[5].Text = GetUserName(e.Row.Cells[5].Text);
            e.Row.Cells[6].Text = GetReadUserNames(e.Row.Cells[6].Text);

        }
    }





    protected void btnRefresh_Click(object sender, EventArgs e) //delete
    {
        bind();

    }

    protected void btnAssign_Click(object sender, EventArgs e) //delete
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
        new CaseBLL(int.Parse(CompanyID)).UpdateCaseOwer(int.Parse(this.txtSignUserID.Text), ids);

        bind();

    }

    protected void btnAssignReadUser_Click(object sender, EventArgs e) //delete
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

        string txtreadIDs=this.txtSignUserID.Text;
        string [] readIDs=txtreadIDs.Split(new char [] {','});
        BLL.ReadCaseUsersBLL.InsertReadCaseUsers(readIDs,ids,CompanyID);


        bind();

    }



    protected void btnGoxxx_Click(object sender, EventArgs e)
    {
        if (txtTo.Text.Trim() != "")
        {
            try
            {
                decimal.Parse(txtTo.Text.Trim());
            }
            catch
            {

                this.txtTo.Text = "";
            }
        }

        if (txtFrom.Text.Trim() != "")
        {
            try
            {
                decimal.Parse(txtFrom.Text.Trim());
            }
            catch
            {

                this.txtFrom.Text = "";
            }
        }


        string where = "PatchID ={0}";

        where = string.Format(where, PatchID);



        if (txtFrom.Text.Trim() == "" && txtTo.Text.Trim() == "")
        {

        }
        else if (txtFrom.Text.Trim() == "")
        {
            where = where + " and  tbBalance<={0}";
            where = string.Format(where, txtTo.Text);
          
        }
        else if (txtTo.Text.Trim() == "")
        {
            where = where + " and  tbBalance>={0}";
            where = string.Format(where, txtFrom.Text);
        }
        else  //
        {
            where = where + " and tbBalance>={0} and tbBalance<={1}";
            where = string.Format(where, txtFrom.Text, txtTo.Text);
        }

        if (this.txtName.Text.Trim() != "")
        {
            where += " and tbName like N'%{0}%' ";
            where = string.Format(where, this.txtName.Text.Trim());
        }

        if (ddlCompanyUsers.SelectedIndex > 0)
        {
            if (ddlCompanyUsers.SelectedIndex ==1)
            {
                where += " and OwnerID is null ";
            }
            else
            {
                where += " and OwnerID = {0} ";
                where = string.Format(where, this.ddlCompanyUsers.SelectedValue);
            }
        }
        SearchWhere=where;
         GetTotalRecords( where);
        this.PagingControl1.CurrentPage = 1;


        DataSet ds = new CaseBLL(int.Parse(CompanyID)).GetCasePagingitems(this.PagingControl1.ListRecordNumPerPage, this.PagingControl1.CurrentPage, SearchWhere);
        CompanyAllReadUserDS =BLL.ReadCaseUsersBLL.GetReadCaseUsersList("CompanyID=" + CompanyID);
        this.GridView1.DataSource = ds;
        this.GridView1.DataBind();

        GetTotalNum(ds);
      




    }



    private void ReloadGridList()
    {
        string where = "PatchID=" + PatchID;
        GetTotalRecords(where);
        this.PagingControl1.CurrentPage = 1;
       
        DataSet ds = new CaseBLL(int.Parse(CompanyID)).GetCasePagingitems(this.PagingControl1.ListRecordNumPerPage, this.PagingControl1.CurrentPage, where);
        this.GridView1.DataSource = ds;
        this.GridView1.DataSource = ds;
       
        CompanyAllReadUserDS = BLL.ReadCaseUsersBLL.GetReadCaseUsersList("CompanyID=" + CompanyID);
        this.GridView1.DataBind();
        GetTotalNum(ds);
    }
    protected void ddlPatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ReloadGridList();
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindPatchList();
        bindCompayUserList();
        this.txtFrom.Text = "";
        this.txtName.Text = "";
        this.txtTo.Text = "";
        this.txtSignUserID.Text = "";
        this.ddlCompanyUsers.SelectedIndex = 0;
      
        if (PatchID == "-1")
        {
           

            PagingControl1.TotalRecords = 0;
            this.GridView1.DataSource = null;
            this.GridView1.DataBind();

            this.btnGo.Enabled = false;
        }
        else
        {
            this.btnGo.Enabled = true;
            ReloadGridList();
        }
      
    }

    private string GetReadUserNames(string caseID)
    {
        DataRow[] drs = CompanyAllReadUserDS.Tables[0].Select("CaseID=" + caseID);
        string rtn = "";
        foreach (DataRow dr in drs)
        {
            string userid = dr["UserID"].ToString();
            rtn += GetUserName(userid) + ";";
        }
        if (rtn.Length > 0)
        {
            rtn = rtn.Substring(0, rtn.Length - 1);
        }
        return rtn;
    }

}

