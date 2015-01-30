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


public partial class CompanyTree :PageBase
{
    protected string Menus="";

    private DataSet PatchsDS;
    private DataSet CompanyDS;

    //protected int CompanyID = 6;  //temperary
    //protected int PatchID = 6;

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
            return ddlPatch.SelectedValue;
        }
    }

    //public string  BasicFilter
    //{
    //    get
    //    {
    //        return this.ViewState["BasicFilter"].ToString();
    //    }
    //    set
    //    {
    //        this.ViewState["BasicFilter"] = value;
    //    }
    //}

    public string OrderStr
    {
        get
        {

            if (this.ViewState["OrderStr"] == null)
                return null;
          
            return this.ViewState["OrderStr"].ToString();
        }
        set
        {
            this.ViewState["OrderStr"] = value;
        }
    }


    public string OrderField
    {
        get
        {

            if (this.ViewState["OrderField"] == null)
                return null;

            return this.ViewState["OrderField"].ToString();
        }
        set
        {
            this.ViewState["OrderField"] = value;
        }
    }

    protected string PatchName
    {
        get
        {
            return ddlPatch.SelectedItem.Text ;
        }
    }

    private DataSet AllOpenedDataSet;

    private string GroupMenu = @" <li id='menuGroup{0}' class='selected' style='cursor:pointer;'><img onclick='displayMenu({0})' id='groupimg{0}' src='images/t_list_10.jpg' style='cursor:pointer;'/> {1}({2})</a></li>";
    private string itemMenu = @" <li pid='{0}' class='w1' style='cursor:hand ' onclick=""javascript:showUrl(this,'CaseDetail.aspx?id={2}&companyID={3}')"">{1}</li>";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            CompanyDS = BLL.CompanyBLL.GetCompanyList();
            if (CompanyDS.Tables[0].Rows.Count == 0)
            {
                return;
            }
           
          
        
           
            bindCompayList();
           // checkCookie();
            companyChange();
           

           
        }
    }

    private void checkCookie()
    {
         if (Request.Cookies["Mobile"] != null)
        {
            txtMobile.Text = Request.Cookies["Mobile"].Value;

        }

        if (Request.Cookies["CaseName"] != null)
        {
            txtName.Text = Request.Cookies["CaseName"].Value;

        }

        if (Request.Cookies["Note"] != null)
        {
            txtNote.Text = Request.Cookies["Note"].Value;

        }




        if (Request.Cookies["MoneyFrom"] != null)
        {
            txtFrom.Text = Request.Cookies["MoneyFrom"].Value;

        }

        if (Request.Cookies["MoneyTo"] != null)
        {
            txtTo.Text = Request.Cookies["MoneyTo"].Value;

        }


        if (Request.Cookies["OrderField"] != null)
        {
            OrderField = Request.Cookies["OrderField"].Value;

        }

        if (Request.Cookies["OrderStr"] != null)
        {
            OrderStr = Request.Cookies["OrderStr"].Value;

        }
     



    }

    private void companyChange()
    {
        GetPatchs();
        if (PatchsDS.Tables[0].Rows.Count == 0)
        {
            return;
        }
        bindPatchList();
         formSearchMenu();
    }
    private void bindCompayList()
    {
      
        ddlCompany.DataSource = CompanyDS;
        ddlCompany.DataTextField = "CompanyName";
        ddlCompany.DataValueField = "ID";
        ddlCompany.DataBind();

    
        
    }

    private void bindPatchList()
    {
      
        ddlPatch.DataSource = PatchsDS;
      
        ddlPatch.DataTextField = "PatchName";
        ddlPatch.DataValueField = "ID";
        ddlPatch.DataBind();

            ddlPatch.SelectedIndex = 0;
       


        ddlHasPayment.Items.Clear();
        ddlHasPayment.Items.Add("所有");
        ddlHasPayment.Items.Add("有还款记录");
        ddlHasPayment.Items.Add("无还款记录");

      
            ddlHasPayment.SelectedIndex = 0;
      

        this.ddlOpened.Items.Clear();
        ddlOpened.Items.Add("所有");
        ddlOpened.Items.Add("已打开");
        ddlOpened.Items.Add("未打开");

     
            ddlOpened.SelectedIndex = 0;
       

        this.ddlPhoned .Items.Clear();
        ddlPhoned.Items.Add("所有");
        ddlPhoned.Items.Add("已打电话");
        ddlPhoned.Items.Add("未打电话");

  
            ddlPhoned.SelectedIndex = 0;
     

        this.ddlVisit.Items.Clear();
        ddlVisit.Items.Add("所有");
        ddlVisit.Items.Add("已外访");
        ddlVisit.Items.Add("未外访");


            ddlVisit.SelectedIndex = 0;
      

        this.ddlPromisedDate.Items.Clear();
          ddlPromisedDate.Items.Add("所有");
        ddlPromisedDate.Items.Add("已承诺还款");
        ddlPromisedDate.Items.Add("未承诺还款");


            ddlPromisedDate.SelectedIndex = 0;
       

        this.ddlReadonlyCase.Items.Clear();
    
        ddlReadonlyCase.Items.Add("分配给我的");
        ddlReadonlyCase.Items.Add("我只读的");


            ddlReadonlyCase.SelectedIndex = 0;
      


        ddlOrder.Items.Clear();
        ddlOrder.Items.Add(new ListItem("姓名", "tbName"));
        ddlOrder.Items.Add(new ListItem("余额", "tbBalance"));
        

        ddlOrder.Items.Add(new ListItem("账号/合同号", "tbKey"));
        ddlOrder.Items.Add(new ListItem("身份证", "tbIdentityNo"));
        ddlOrder.Items.Add(new ListItem("手机号", "tbMobile"));


  
            ddlOrder.SelectedIndex = 0;
      


    }

    private void  GetPatchs()
    {
      PatchsDS= BLL.PatchBLL.GetCompanyPatchListByCompanyID(CompanyID);
    }

  

    //private DataSet GetCaseUserNamesByPatchID(string patchID)
    //{
    //    string where =" PatchID = " + PatchID;
    //    return new BLL.CaseBLL(int.Parse(CompanyID)).GetCaseList(where );
    //}

    private bool IsOpened(string CaseID)
    {
        DataTable dt = AllOpenedDataSet.Tables[0];
        return (dt.Select("CaseID=" + CaseID).Length > 0);
    }
    private void formSearchMenu()
    {
       
        string where = GetSearchCondition();
        StringBuilder sb = new StringBuilder();
        string group;
        string item;

        int i = 1;

        string order;
        string orderField;
        if (OrderStr == null)
        {
            order = "";
            orderField = "";
        }
        else
        {
            order = OrderStr;
            orderField = OrderField;
        }

        DataSet CaseUsersDS = new BLL.CaseBLL(int.Parse(CompanyID)).GetCaseList(where, orderField, order);
        AllOpenedDataSet = BLL.OpenedCaseBLL.GetOpenedCaseList("CaseID", string.Format("CompanyID={0} and UserID={1}", CompanyID, CurrentUser.ID));

        int count = CaseUsersDS.Tables[0].Rows.Count;

        group = string.Format(GroupMenu, i, PatchName, count);
        sb.AppendLine(group);

        for (int j = 0; j < CaseUsersDS.Tables[0].Rows.Count; j++)
        {
            DataRow drCase = CaseUsersDS.Tables[0].Rows[j];
            string displayName = drCase[1].ToString();
            string caseID = drCase[0].ToString();

            if (drCase["Repeated"].ToString() == "1")
            {
                displayName = string.Format("<font color='red'>{0}</font>", displayName);
            }

           
                if (drCase["Phoned"].ToString() == "1")
                {
                    displayName = string.Format("{0}(电)", displayName);
                }
                if (drCase["Visited"].ToString() == "1")
                {
                    displayName = string.Format("{0}(访)", displayName);
                }
                if (drCase["PromisedDate"].ToString() != "")
                {
                    displayName = string.Format("{0}(诺)", displayName);
                }


                if (IsOpened(caseID))
                {
                    displayName = string.Format("{0}(打开)", displayName);
                }
           
            item = string.Format(itemMenu, i, displayName, drCase[0].ToString(), CompanyID);

            sb.AppendLine(item);
        }


        Menus = sb.ToString();

    }

    private string GetSearchCondition()
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



        string where =  " PatchID = " + PatchID;



        if (txtFrom.Text.Trim() == "" && txtTo.Text.Trim() == "")
        {

        }
        else if (txtFrom.Text.Trim() == "")
        {
            where = where + " and  tbBalance>={0}";
            where = string.Format(where, txtFrom.Text);
        }
        else if (txtTo.Text.Trim() == "")
        {
            where = where + " and  tbBalance<={0}";
            where = string.Format(where, txtTo.Text);
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

        if (this.txtNote.Text.Trim() != "")
        {
            where += " and id in (select distinct caseID from notetable where  body like N'%{0}%' and companyID={1}) ";
            where = string.Format(where, this.txtNote.Text.Trim(), CompanyID);
        }


        if (this.txtMobile.Text.Trim() != "")
        {
            string con = getTelePhoneStr(this.txtMobile.Text.Trim());

            where += " and ({0}) ";
            where = string.Format(where, con);
        }

        if (this.ddlOpened.SelectedIndex == 1) //
        {
            where += string.Format(" and  ID in (select caseid from  openedcase where userID ={0} and companyid={1} )  ",CurrentUser.ID,CompanyID);
           
        }
        else if (this.ddlOpened.SelectedIndex == 2)
        {
            where += string.Format(" and  ID not in (select caseid from  openedcase where userID ={0} and companyid={1} )  ", CurrentUser.ID, CompanyID);
        }


        if (this.ddlPhoned.SelectedIndex == 1) //
        {
            where += " and  Phoned=1  ";

        }
        else if (this.ddlPhoned.SelectedIndex == 2)
        {
            where += " and  Phoned<>1 ";
        }

        if (this.ddlVisit.SelectedIndex == 1) //
        {
            where += " and  Visited=1  ";

        }
        else if (this.ddlVisit.SelectedIndex == 2)
        {
            where += " and  Visited<>1 ";
        }

        if (this.ddlPromisedDate .SelectedIndex == 1) //
        {
            where += " and  PromisedDate is not null  ";

        }
        else if (this.ddlPromisedDate.SelectedIndex == 2)
        {
            where += " and  PromisedDate is  null ";
        }


        if (this.ddlReadonlyCase.SelectedIndex == 0) //
        {
            if (!this.IsAdmin)
            {
                where += string.Format(" and  ownerid= {0}  ", CurrentUser.ID);
            }

        }
        else if (this.ddlReadonlyCase.SelectedIndex == 1)
        {

            where += string.Format(" and ID in (select CaseID from ReadCaseUsers where userid= {0} and CompanyID={1})", CurrentUser.ID, CompanyID);
            
           
        }
        else
        {
           
            if (!this.IsAdmin)
            {
               where += string.Format(" and (ownerid= {0} or  ID in (select CaseID from ReadCaseUsers where userid= {0} and CompanyID={1}))",CurrentUser.ID, CompanyID);
               
            }
        }




        if (ddlHasPayment.SelectedIndex == 1) //payed
        {
            where += " and  tbkey in (select tbkey from companypayment_{0}) ";
            where = string.Format(where, CompanyID);
        }
        else if (ddlHasPayment.SelectedIndex == 2) //not payed
        {
            where += " and  tbkey not in (select tbkey from companypayment_{0}) ";
            where = string.Format(where, CompanyID);
        }

    


        return where;
    }

    private string getTelePhoneStr(string wildcard)
    {
        DataSet ds = BLL.CompanyBLL.GetCacheFields(CompanyID, Common.Tools.CaseTableType);
        DataRow[] drs = ds.Tables[0].Select("FieldType='telephone'");
        string rtn = "";
        foreach (DataRow dr in drs)
        {
            rtn += " OR " + dr["FieldName"].ToString() + " like N'%" + wildcard + "%'";
        }
        if (rtn.Length > 0)
        {
            return rtn.Substring(3);
        }
        return "";
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {


       
        formSearchMenu();
      //  PatchsDS = BLL.PatchBLL.GetCompanyPatchListByCompanyID(CompanyID.ToString());

        AddCookie();

      

    }

    protected void btnDesc_Click(object sender, EventArgs e)
    {

        OrderStr = "Desc";
     
        OrderField = this.ddlOrder.SelectedItem.Value;
        formSearchMenu();

      
    }

    protected void btnAsc_Click(object sender, EventArgs e)
    {

        OrderStr = "asc";

        OrderField = this.ddlOrder.SelectedItem.Value;

        formSearchMenu();
       
    }

    private void AddCookie()
    {

        HttpCookie ck1 = new HttpCookie("Company", ddlCompany.SelectedItem.Value);
        ck1.Expires = DateTime.Now.AddDays(30);
        Response.Cookies.Add(ck1);

        ck1 = new HttpCookie("Patch", this.ddlPatch.SelectedItem.Value);
        ck1.Expires = DateTime.Now.AddDays(30);
        Response.Cookies.Add(ck1);

        ck1 = new HttpCookie("HasPayment", this.ddlHasPayment.SelectedItem.Value);
        ck1.Expires = DateTime.Now.AddDays(30);
        Response.Cookies.Add(ck1);

        ck1 = new HttpCookie("Visited", this.ddlVisit.SelectedItem.Value);
        ck1.Expires = DateTime.Now.AddDays(30);
        Response.Cookies.Add(ck1);

        ck1 = new HttpCookie("PromisedDate", this.ddlPromisedDate .SelectedItem.Value);
        ck1.Expires = DateTime.Now.AddDays(30);
        Response.Cookies.Add(ck1);

      

        ck1 = new HttpCookie("ReadonlyCase", this.ddlReadonlyCase.SelectedItem.Value);
        ck1.Expires = DateTime.Now.AddDays(30);
        Response.Cookies.Add(ck1);

        ck1 = new HttpCookie("MoneyFrom", this.txtFrom.Text.Trim());
        ck1.Expires = DateTime.Now.AddDays(30);
        Response.Cookies.Add(ck1);

        ck1 = new HttpCookie("MoneyTo", this.txtTo.Text.Trim());
        ck1.Expires = DateTime.Now.AddDays(30);
        Response.Cookies.Add(ck1);

        ck1 = new HttpCookie("Mobile", this.txtMobile .Text.Trim());
        ck1.Expires = DateTime.Now.AddDays(30);
        Response.Cookies.Add(ck1);

        ck1 = new HttpCookie("CaseName", this.txtName.Text.Trim());
        ck1.Expires = DateTime.Now.AddDays(30);
        Response.Cookies.Add(ck1);

        ck1 = new HttpCookie("Note", this.txtNote .Text.Trim());
        ck1.Expires = DateTime.Now.AddDays(30);
        Response.Cookies.Add(ck1);

        ck1 = new HttpCookie("Opened", this.ddlOpened.SelectedItem.Value);
        ck1.Expires = DateTime.Now.AddDays(30);
        Response.Cookies.Add(ck1);


        ck1 = new HttpCookie("Phoned", this.ddlPhoned.SelectedItem.Value);
        ck1.Expires = DateTime.Now.AddDays(30);
        Response.Cookies.Add(ck1);


        //ck1 = new HttpCookie("OrderField", this.ddlOrder.SelectedItem.Value);
        //ck1.Expires = DateTime.Now.AddDays(30);
        //Response.Cookies.Add(ck1);

        //ck1 = new HttpCookie("OrderStr", "Desc");
        //ck1.Expires = DateTime.Now.AddDays(30);
        //Response.Cookies.Add(ck1);



    }
    protected void ddlPatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        initSearchData();
     
        formSearchMenu();
      
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetPatchs();
        if (PatchsDS.Tables[0].Rows.Count == 0)
        {
            return;
        }
        bindPatchList();
        ddlPatch.SelectedIndex = 0;
      
        initSearchData();
        formSearchMenu();
       
    }

    private void initSearchData()
    {
        this.txtFrom.Text = "";
        this.txtTo.Text = "";
        this.txtNote.Text = "";
        this.txtName.Text = "";
        this.txtMobile.Text = "";
        this.ddlHasPayment.SelectedIndex = 0;
        this.ddlPromisedDate.SelectedIndex = 0;
        this.ddlReadonlyCase.SelectedIndex = 0;
        this.ddlPhoned.SelectedIndex = 0;
        this.ddlOpened.SelectedIndex = 0;
        this.ddlOrder.SelectedIndex = 0;
        this.ddlVisit.SelectedIndex = 0;

        this.OrderField = "tbName";
        this.OrderStr = "asc";

       // AddCookie();
    }
}
