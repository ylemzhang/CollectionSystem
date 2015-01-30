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
using BLL;

public partial class CaseDetail : PageBase
{
    private string groupTemplete = @" <tr><td colspan={1}  ><font  color='#cc6600'><b>{0}</b></font></td></tr>";
    private string tdTemplete = @" <td style='word-break:break-all;'><b>{0}:</b></td><td style='word-break:break-all;'>{1}</td>";
    private string trTemplete = @" <tr>{0}</tr>";

    private DataRow ItemRow;
    private DataTable TbFields;
    private ArrayList Groups;
    private StringBuilder SB;

    private int ColumnsCount;

    //private string NoteTemplate = @"by <strong>{1}</strong>  {2}  <input type=button onclick=""EditNote('{4}')""  value='修改' /><br/> {3} {0} <br />";

    private string NoteTemplate
    {
        get
        {
            //<input type=button onclick=""EditNote('{4}')""  value='修改' />
            if (this.ViewState["NoteTemplate"] == null)
              return  @"by <strong>{1}</strong>  {2}  <br/> {3} {0} <br />";

            return this.ViewState["NoteTemplate"].ToString();
        }
        set
        {
            this.ViewState["NoteTemplate"] = value;

        }
    }
    protected string AlertTypeCaseTypeID
    {
        get
        {
            if (this.ViewState["AlertTypeCaseTypeID"] == null)
                return "";

            return this.ViewState["AlertTypeCaseTypeID"].ToString();
        }
        set
        {
            this.ViewState["AlertTypeCaseTypeID"] = value;

        }
    }

    protected string AlertTypePromiseID
    {
        get
        {
            if (this.ViewState["AlertTypePromiseID"] == null)
                return "";

            return this.ViewState["AlertTypePromiseID"].ToString();
        }
        set
        {
            this.ViewState["AlertTypePromiseID"] = value;

        }
    }


    protected string AlertTypeFollowByID
    {
        get
        {
            if (this.ViewState["AlertTypeFollowByID"] == null)
                return "";

            return this.ViewState["AlertTypeFollowByID"].ToString();
        }
        set
        {
            this.ViewState["AlertTypeFollowByID"] = value;

        }
    }


    protected string AlertTypeCommentID
    {
        get
        {
            if (this.ViewState["AlertTypeCommentID"] == null)
                return "";

            return this.ViewState["AlertTypeCommentID"].ToString();
        }
        set
        {
            this.ViewState["AlertTypeCommentID"] = value;

        }
    }





    protected string CaseUserName
    {
        get
        {
            if (this.ViewState["CaseUserName"] == null)
                return "";

            return this.ViewState["CaseUserName"].ToString();
        }
        set
        {
            this.ViewState["CaseUserName"] = value;

        }
    }


    protected string HtmlUrl
    {
        get
        {
            if (this.ViewState["HtmlUrl"] == null)
                return "";

            return this.ViewState["HtmlUrl"].ToString();
        }
        set
        {
            this.ViewState["HtmlUrl"] = value;

        }
    }


    protected string PatchName
    {
        get
        {
            if (this.ViewState["PatchName"] == null)
                return "";
            return this.ViewState["PatchName"].ToString();
        }
        set
        {
            this.ViewState["PatchName"] = value;

        }
    }

    protected string Importtime
    {
        get
        {
            if (this.ViewState["Importtime"] == null)
                return "";
            return this.ViewState["Importtime"].ToString();
        }
        set
        {
            this.ViewState["Importtime"] = value;

        }
    }

    protected string Balance
    {
        get
        {
            if (this.ViewState["Balance"] == null)
                return "";
            return this.ViewState["Balance"].ToString();
        }
        set
        {
            this.ViewState["Balance"] = value;

        }
    }

    protected string Expiredate
    {
      
        get
        {
            if (this.ViewState["Expiredate"] == null)
                return "";
            return this.ViewState["Expiredate"].ToString();
        }
        set
        {
            this.ViewState["Expiredate"] = value;

        }
    }


    protected string CaseID
    {
        get
        {
            if (Request["id"] == null)
                return "";
            return Request["id"].ToString();
        }
    }

    protected string CompanyID
    {
        get
        {
            return Request["companyID"];
        }
    }

    protected string HtmlContent
    {
        get
        {
            if (this.ViewState["HtmlContent"] == null)
                return "";
            return this.ViewState["HtmlContent"].ToString();
        }
        set
        {
            this.ViewState["HtmlContent"] = value;

        }
    }

       protected string OwnerID
    {
        get
        {
            if (this.ViewState["OwnerID"] == null)
                return "";
            return this.ViewState["OwnerID"].ToString();
        }
        set
        {
            this.ViewState["OwnerID"] = value;

        }
    }


    protected DataSet PhoneList
    {
        get
        {
            if (this.ViewState["PhoneList"] == null)
                return GetCalledPhoneList();

            return this.ViewState["PhoneList"] as DataSet;
        }
       
    }

    private DataSet paymentDS;

    private DataSet GetCalledPhoneList()
    {
        string where = "NoteType=1 and CompanyID={0} and CaseID={1}";
        where = string.Format(where, CompanyID, CaseID);
        return NoteBLL.GetNoteList(where);
    }

    private bool IsCalled(string phone)
    {
        if (phone.Trim().Length == 0)
            return false;
        foreach (DataRow dr in PhoneList.Tables[0].Rows)
        {
            if (dr["Str1"].ToString() == phone)
                return true;
        }
        return false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
         
            if (CompanyID == null)
            {
                this.divAll.Style.Add("display", "none");
                Response.Write("没有内容");
                return;
            }
          
            initData();


            ddlcontractResult.DataSource = base.NoteContactResult;
            ddlcontractResult.DataBind();

            ddlcontactorType.DataSource = base.NoteContactType;
            ddlcontactorType.DataBind();


            //MobilePhone = ItemRow["tbMobile"].ToString();
            if (base.IsAdmin)
            {
                FormContent();

            }
            else
            {
                //common user 

                string ownerID = ItemRow["OwnerID"].ToString();
               
                if (ownerID == CurrentUser.ID)
                {

                    FormContent();
                    spanSignUser.Style.Add("display", "none");
                    btnComment.Enabled = false;
                    return;
                }

                //read user;
               string where = "UserID={0} and CaseID={1} and CompanyID={2}";
                where = string.Format(where, CurrentUser.ID, CaseID,CompanyID);

                DataSet ds = BLL.ReadCaseUsersBLL.GetReadCaseUsersList(where);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    NoteTemplate = @"by <strong>{1}</strong>  {2}  <input type=button style='display:none' onclick=""EditNote('{4}')""  value='修改' /><br/> {3} {0} <br />";
                    FormContent();
                    spanSignUser.Style.Add("display", "none");
                    spanSignHelper.Style.Add("display", "none");
                    this.btnPromised .Enabled = false;
                    this.btnSaveNote.Enabled = false;
                    btnCaseType.Enabled = false;
                    btnFollow.Enabled = false;
                    return;
                }

                else 
                {
                    ds = BLL.OpenedCaseBLL.GetOpenedCaseList("ID", where);
                    if (ds.Tables[0].Rows.Count > 0) //help user
                    {
                         NoteTemplate = @"by <strong>{1}</strong>  {2}  <input type=button style='display:none' onclick=""EditNote('{4}')""  value='修改' /><br/> {3} {0} <br />";
                    FormContent();
                    spanSignUser.Style.Add("display", "none");
                    spanSignHelper.Style.Add("display", "none");
                    this.btnPromised .Enabled = false;
                    this.btnSaveNote.Enabled = false;
                    btnCaseType.Enabled = false;
                    btnFollow.Enabled = false;
                        btnComment.Enabled = false;
                    return;
                    }
                    else
                    {
                        Response.Redirect("Nopermission.htm");
                    }
                  
                }


            }
        }
    }

    private DataTable NoteHistortTable;
    private void FormContent()
    {
        GetNote();
        BindCaseTypeDropdownList();
        DrawHtml();
        BindPaymentList();
        //FillPromiseDate();
        GetNewBalanceInfo();
       
        bindKeyList();//same record
        GetKeyInfo();
        GetAlertInfo();

        InitNavagator();
        
    }

    private void GetAlertInfo()
    {
       
        string where = "CaseID={0} and CompanyID={1} ";
        where = string.Format(where, CaseID, CompanyID);
        DataSet AlertDS = BLL.AlertBLL.GetAlertList(where);
     

        foreach (DataRow dr in AlertDS.Tables[0].Rows)
        {
            int alertType=int.Parse(dr["AlertType"].ToString());
            string id=dr["ID"].ToString();
            switch(alertType)
            {
                case 3: //base.AlertTypeCaseType:
                    AlertTypeCaseTypeID = id;
                    base.GetDropDownListSeletedByText(this.ddlCaseType,dr["Str1"].ToString());
                    break;
                case 2: // base.AlertTypeComment:
                    AlertTypeCommentID = id;
                    this.txtComment.Text = dr["Str1"].ToString();
                    lblPerson.Text = "(" + dr["Person"].ToString() + "/" + dr["Date1"].ToString()+")";
                    break;
                case 4: //base.AlertTypeFollowBy:
                    AlertTypeFollowByID = id;
                   
                    string followDate = dr["Date1"].ToString();
                    if (followDate != "")
                    {
                        this.txtfollowDate.Text = DateTime.Parse(followDate).ToString("yyyy-MM-dd");
                    }

                    break;
                case 1: //base.AlertTypePromise:
                    AlertTypePromiseID  = id;
                    string PromisedDate = dr["Date1"].ToString();
                    if (PromisedDate != "")
                    {
                        this.txtDate.Text = DateTime.Parse(PromisedDate).ToString("yyyy-MM-dd");
                    }

                    this.txtPromisedPay.Text = dr["Num1"].ToString();
                    break;
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

    private void GetKeyInfo()
    {
        for (int i = 0; i < RadioButtonList1.Items.Count;i++ )
        {
            string key = RadioButtonList1.Items[i].Value;
            ViewState["Key_" + key] = ItemRow[key].ToString();
        }
    }

    private string GetJudgeCondition() //取得查找条件，根据唯一标识
    {

        return "tbKey='" + ItemRow["tbKey"].ToString() + "'";
         //DataRow[] drs=getJudgeFieldsDrs();
         //string con = "1=1 ";
         //foreach (DataRow dr in drs)
         //{
         //    string field = dr["FieldName"].ToString();
         //    con += " and "+field + " = '" + ItemRow[field].ToString() + "'";
          
         //}
         //return con;
    }

    private void GetNote()
    {
        DataSet ds = BLL.NoteBLL.GetNoteListByCaseID(CompanyID, CaseID);
        NoteHistortTable = ds.Tables[0];
        GetNoteHtmlByType0(NoteHistortTable);
        GetNoteHtmlByType1(ds);
        GetNoteHtmlByType2(NoteHistortTable);

        this.divHistory.InnerHtml = this.txtNote0.Text;
  
    }
   
   

    private void GetNoteHtmlByType0(DataTable dt)
    {
        DataRow[] rows = dt.Select("NoteType=0");
        StringBuilder sb = new StringBuilder();


        foreach (DataRow dr in rows)
        {
            string str = "";
            string temp = string.Format(NoteTemplate, dr["Body"].ToString() ,GetUserName( dr["CreateBy"].ToString()), dr["CreateOn"].ToString(), str, dr["ID"].ToString());
            sb.AppendLine(temp);
        }
        txtNote0.Text  = sb.ToString();
    }




    private void GetNoteHtmlByType1(DataSet ds)
    {
        if (null != ds && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow[] rows = ds.Tables[0].Select("NoteType=1");
            DataTable dt = ds.Tables[0].Clone();
            foreach (var et in rows)
            {
                dt.ImportRow(et);
            }
            string name = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["CreateBy"] = GetUserName(dt.Rows[i]["CreateBy"].ToString());
            }
            this.gvTel.DataSource = dt.DefaultView;
            this.gvTel.DataBind();
        }
        
        //StringBuilder sb = new StringBuilder();


        //foreach (DataRow dr in rows)
        //{

        //    string str = "电话：" + dr["Str1"].ToString() +"  联系人：" + dr["contactor"].ToString()+"  结果：" + dr["Str2"].ToString() ;            
        //    string temp = string.Format(NoteTemplate, dr["Body"].ToString(), GetUserName(dr["CreateBy"].ToString()), dr["CreateOn"].ToString(), str, dr["ID"].ToString());
        //    sb.AppendLine(temp);
        //}
        //txtNote1.Text = sb.ToString();
    }

    private void GetNoteHtmlByType2(DataTable dt)
    {
        DataRow[] rows = dt.Select("NoteType=2");
        StringBuilder sb = new StringBuilder();


        foreach (DataRow dr in rows)
        {
            string str = "路费：" + dr["Num1"].ToString() + "  联系人：" + dr["contactor"].ToString()  ;
            str += "  联系对象：" + dr["contactorType"].ToString() + "  是否可联：" + dr["contractResult"].ToString();

            string temp = string.Format(NoteTemplate, dr["Body"].ToString(), GetUserName(dr["CreateBy"].ToString()), dr["CreateOn"].ToString(), str, dr["ID"].ToString());
            sb.AppendLine(temp);
        }
        txtNote2.Text = sb.ToString();
    }


   private void   BindPaymentList()
     {
        
         string where = GetJudgeCondition();

       
             if (HasPaymentTable(CompanyID))
             {
                 paymentDS = new BLL.PaymentBLL(int.Parse(CompanyID)).GetPaymentList(where);

                 this.GridView1.DataSource = paymentDS;
                 this.GridView1.DataBind();
             }
        
     }

    //private void FillPromiseDate()
    //{
     
    //    string PromisedDate = ItemRow["PromisedDate"].ToString();
    //    if (PromisedDate != "")
    //    {
    //        this.txtDate.Text  = DateTime.Parse(PromisedDate).ToString("yyyy-MM-dd");
    //    }
    //}

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != -1)
        {
            e.Row.Cells[3].Text = GetDateString(e.Row.Cells[3].Text);
            e.Row.Cells[5].Text = GetDateString(e.Row.Cells[5].Text);

        }
    }

  

    private void initData()
    {

        //BLL.OpenedCaseBLL.InsertOpenedCase(CurrentUser.ID, CaseID, CompanyID);

       // new BLL.CaseBLL(int.Parse(CompanyID)).UpdateCaseOpened(1, int.Parse(CaseID));
        ItemRow = new BLL.CaseBLL(int.Parse(CompanyID)).GetCaseByID(CaseID).Tables[0].Rows[0];

        OwnerID = ItemRow["OwnerID"].ToString();
        if (OwnerID == "")
        {
            this.lblSignUser.Text = "未分配";
        }
        else
        {
            this.lblSignUser.Text = GetUserName(OwnerID);
        }

      


        TbFields = BLL.CompanyBLL.GetCacheFields(CompanyID, Common.Tools.CaseTableType).Tables[0];
        Groups = GetGroups(TbFields);
        ColumnsCount = base.CaseDisplayColumn;
        SB = new StringBuilder();

        GetPatchInfo();

        if (IsAdmin)
        {
            trapp.Style.Add("display", "none");
        }
        else
        {
            string app = System.Configuration.ConfigurationManager.AppSettings["Application"];
            string[]  apps = app.Split('|');
            ddlApp.DataSource =apps;
            ddlApp.DataBind();
            ddlApp.SelectedIndex = 0;
            
        }
       
    }

    private void GetPatchInfo()
    {
        string patchID = ItemRow["PatchID"].ToString();
        DataSet ds =BLL.PatchBLL.GetPatchByID(patchID);
        DataRow dr=ds.Tables[0].Rows[0];
        PatchName = dr["PatchName"].ToString();
        Expiredate = DateTime.Parse(dr["ExpireDate"].ToString()).ToString("yyyy-MM-dd");
        Importtime = DateTime.Parse(dr["ImportTime"].ToString()).ToString("yyyy-MM-dd");

    }

    private void GetNewBalanceInfo()
    {
        string hasBalanceTable =BLL.CompanyBLL.GetIfHasBalanceTable(CompanyID);
        if (hasBalanceTable=="1" && this.HasbalanceTable(CompanyID))
        {
            string where = GetJudgeCondition();
            DataSet ds = new BLL.BalanceBLL(int.Parse(CompanyID)).GetBalanceList(where);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                Balance = DateTime.Parse(dr[0].ToString()).ToString("yyyy-MM-dd") + "&nbsp;&nbsp;&nbsp;&nbsp;" + dr[1].ToString();
            }
            else
            {
                Balance = DateTime.Now.ToString("yyyy-MM-dd") + "&nbsp;&nbsp;&nbsp;&nbsp;" + ItemRow["tbBalance"].ToString();
            }
        }
        else
        {

            if (paymentDS!=null && paymentDS.Tables[0].Rows.Count > 0)
            {
                DataRow dr = paymentDS.Tables[0].Rows[paymentDS.Tables[0].Rows.Count - 1];
                Balance = DateTime.Parse(dr["tbPayDate"].ToString()).ToString("yyyy-MM-dd") + "&nbsp;&nbsp;&nbsp;&nbsp;" + dr["tbBalance"].ToString();
          
            }
            else
            {
                Balance = DateTime.Now.ToString("yyyy-MM-dd") + "&nbsp;&nbsp;&nbsp;&nbsp;" + ItemRow["tbBalance"].ToString();
            }
        }
        
    }

    private void DrawHtml()
    {
        for (int i = 0; i < Groups.Count; i++)
        {

            DrawGroup(Groups[i].ToString());
        }
        HtmlContent = SB.ToString();

    }

    private string getTeleType(string phone)
    {
        DataRow[] rows = NoteHistortTable.Select("NoteType=1");
       
        foreach (DataRow dr in rows)  //desc
        {
            if (dr["Str1"].ToString().IndexOf(phone) > -1)
            {
                return "("+dr["Str2"].ToString()+")";
            }
         
        }

        return "";
    }


    private void DrawGroup(string groupName)
    {
        string temp = string.Format(groupTemplete, groupName, 2*ColumnsCount);
        SB.AppendLine(temp);
        DataRow[] drs = getDrsFromGroup(groupName);
       
        int begin = 0;
        string lineContent = "";
     
        for (int i = 0; i < drs.Length; i++)
        {
            DataRow dr = drs[i];
            string title = dr["FName"].ToString();
            string field = dr["FieldName"].ToString();
            string content = ItemRow[field].ToString().Trim();
            string type = dr["FieldType"].ToString().ToLower();
            string IsDisplay = dr["IsDisplay"].ToString().ToLower();

            if (IsDisplay == "0")
                continue;

            if (field == "tbName")
            {
             
                CaseUserName = content;
                HtmlUrl = Common.Tools.HtmlUrl + CaseUserName + ".htm";
            }
            if (field == "tbBalance" || field == "tbCardNo" )
            {
               
                content = string.Format("<font color='red'>{0}</font>", content);
            }

           
          
            if (type == "datetime" && content.Length >1)
            {
                content = DateTime.Parse(content).ToString("yyyy-MM-dd");
            }
            else if (type == "telephone" && content != "")
            {
               
                string phoneType=getTeleType(content);
                if (phoneType != "")
                {

                    content = string.Format(@"<span name='spantelephone' style ='cursor:hand;color:red' ondblclick=""EditTelephone('{2}','{0}','{3}','{4}')"" >{0}{1}</span>", content, phoneType, "", CompanyID, CaseID);

                }
                else if (content != "")
                {
                    content = string.Format(@"<span name='spantelephone' style ='cursor:hand;color:Blue' ondblclick=""EditTelephone('{2}','{0}','{3}','{4}')"" >{0}{1}</span>", content, "", "", CompanyID, CaseID);
                }
               
                //string Phoned = ItemRow["Phoned"].ToString();
                //if (Phoned == "1" && IsCalled(content))
                //{
                   
                //        content = string.Format("<font color='red'>{0}(已打)</font>", content);
                 
                //}
                //else if (content!="")
                //{
                //    content = string.Format("<font color='blue'>{0}</font>", content);
                //}
            }

            if (content == "")
            {
                content = "&nbsp;";
            }

            if (begin < ColumnsCount)
            {
                lineContent += string.Format(tdTemplete, title, content);
               
                if (begin == ColumnsCount - 1) //last
                {
                    string thisRow = string.Format(trTemplete, lineContent);
                    SB.AppendLine(thisRow);
                }
                else
                {
                    if (i == drs.Length - 1)
                    {
                        string thisRow = string.Format(trTemplete, lineContent);
                        SB.AppendLine(thisRow);
                    }
                }
                begin++;
            }
            else
            {

                lineContent = string.Format(tdTemplete, title, content);
                begin = 1;
                if (i == drs.Length - 1)
                {
                    SB.AppendLine(lineContent);
                }
                
            }

        }

    }


    private DataRow[] getDrsFromGroup(string groupName)
    {
        return TbFields.Select("Misk='" + groupName + "'");
    }

    //private DataRow[] getJudgeFieldsDrs()
    //{
    //    return TbFields.Select("IsJudge='1'");
    //}


    private ArrayList GetGroups(DataTable tb)
    {
        ArrayList ar = new ArrayList();
        foreach (DataRow dr in tb.Rows)
        {
            if (!ar.Contains(dr["Misk"].ToString()))
            {
                ar.Add(dr["Misk"].ToString());
            }
        }
        return ar;
    }

    //protected void btn_Click(object sender, EventArgs e)
    //{
     
    //    DateTime dt;
    //    try
    //    {
    //        dt = DateTime.Parse(txtDate.Text);
    //    }
    //    catch
    //    {
    //        Alert("timeformatincorrect");
    //        return;

    //    }
    //    new BLL.CaseBLL(int.Parse(CompanyID)).UpdateCasePromiseDate(dt, int.Parse(CaseID));
    //    Alert("saveSuccess");
      
    //}

    protected void btnSaveNote_Click(object sender, EventArgs e)
    {
        decimal  num=0;
        string phone="";
        if(this.txtType.Text =="2" && this.TextBox1.Text.Trim()!="")
        {
          
            num=decimal.Parse(this.TextBox1.Text);
           

        }
        else if(this.txtType.Text =="1")
        {
            phone=TextBox1.Text.Trim();
           

        }
        DateTime dt = DateTime.Now;
       int noteID= BLL.NoteBLL.InsertNote(txtNote.Text.Trim(), CurrentUser.ID, int.Parse(txtType.Text), int.Parse(CaseID), num, phone, "", DateTime.MinValue, dt, int.Parse(CompanyID),this.txtContactor.Text.Trim(),this.ddlcontactorType.Text,this.ddlcontractResult.Text);
     



        if (this.txtType.Text == "1")
        {
            new BLL.CaseBLL(int.Parse(CompanyID)).UpdateCasePhoned(1, int.Parse(CaseID));

  
            //string temp = "<font color='red'>{0}(已打)</font>";
            //string Donecontent = string.Format(temp, phone);
            //if (!HtmlContent.Contains(Donecontent))
            //{
            //    temp = "<font color='blue'>{0}</font>";
            //    string contentNomark = string.Format(temp, phone);
            //    HtmlContent = HtmlContent.Replace(contentNomark, Donecontent);
            //}          

            //string str = "电话：" + phone + " 联系人：" + this.txtContactor.Text.Trim();
            //string temp1 = string.Format(NoteTemplate, txtNote.Text.Trim(), CurrentUser.UserName, dt,  str, noteID.ToString());
       
            //this.txtNote1.Text = temp1+this.txtNote1.Text;
            //this.divHistory.InnerHtml = this.txtNote1.Text;

            DataSet ds = BLL.NoteBLL.GetNoteListByCaseType(CompanyID, CaseID, 1);
            if (null != ds && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["CreateBy"] = GetUserName(ds.Tables[0].Rows[i]["CreateBy"].ToString());
                }
            }
            this.gvTel.DataSource = ds;
            this.gvTel.DataBind();
        }
        else if (this.txtType.Text == "2")
        {
            new BLL.CaseBLL(int.Parse(CompanyID)).UpdateCaseVisited(1, int.Parse(CaseID));

           

            string str = "路费：" + num.ToString() + " 联系人：" + this.txtContactor.Text.Trim() + "<br/>";
            str += " 联系对象：" + this.ddlcontactorType.Text + " 是否可联：" + this.ddlcontractResult.Text + "<br/>";

            string temp = string.Format(NoteTemplate, txtNote.Text.Trim(), CurrentUser.UserName, dt, str,noteID);

            this.txtNote2.Text = temp + this.txtNote2.Text;
            this.divHistory.InnerHtml = this.txtNote2.Text;
        }
        else if (this.txtType.Text == "0")
        {
            string str = "";
            string temp = string.Format(NoteTemplate, txtNote.Text.Trim(), CurrentUser.UserName, dt, str,noteID);

            this.txtNote0.Text = temp + this.txtNote0.Text;
            this.divHistory.InnerHtml  = this.txtNote0.Text;
        }

        displayText();
       
    }

    private void displayText()
    {
          


         tr1.Style.Add("display","block");
        if (this.txtType.Text == "1")
        {
            span0.InnerText ="电话访问记录";
            span1.InnerText ="电话";
           
         
        }
        else if (this.txtType.Text == "2")
        {
            span0.InnerText ="拜访记录";
            span1.InnerText ="路费";
        }

        this.txtNote.Text = "";
        this.TextBox1.Text = "";
    }


    protected void btnAssign_Click(object sender, EventArgs e) //delete
    {
    
        new CaseBLL(int.Parse(CompanyID)).UpdateCaseOwer(int.Parse(this.txtSignUserID.Text), CaseID);
        this.lblSignUser.Text = GetUserName(this.txtSignUserID.Text);
       


    }



    //same records
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
            e.Row.Cells[9].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != -1)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[9].Visible = false;
            string id = e.Row.Cells[0].Text;
            string companyID = e.Row.Cells[1].Text;


            //e.Row.Attributes.Add("ondblclick", "window.open('CaseDetail.aspx?id=" + id + "&CompanyID=" + companyID + "')");
            e.Row.Attributes.Add("ondblclick", "window.open('CaseDetail.aspx?id=" + id + "&CompanyID=" + companyID + "','_blank')");
            e.Row.ToolTip = Common.StrTable.GetStr("dubbleClickToEdit");

            e.Row.Cells[1].Text = getCompanyName(e.Row.Cells[1].Text.Trim());
            e.Row.Cells[2].Text = getPatchName(e.Row.Cells[2].Text);
            e.Row.Cells[6].Text = GetUserName(e.Row.Cells[6].Text);


        }
    }

    private DataSet CompanyDS;
    private DataSet ALLPatchsDS;

    
    protected void btnGoxxx_Click(object sender, EventArgs e)
    {
        //string where = "";
        string fields = "ID,OwnerID,tbName,tbKey,tbBalance,tbMobile,tbIdentityNo,PatchID";
        string key = this.RadioButtonList1.SelectedItem.Value;
        StringBuilder sb = new StringBuilder();

        string KeyValue = ViewState["Key_" + key].ToString();
        string template = " select {0} from companycase_{1} where {2} ='{3}'";


    
        CompanyDS = BLL.CompanyBLL.GetCompanyList();
        int count = CompanyDS.Tables[0].Rows.Count;

        for (int i = 0; i < count; i++)
        {

            DataRow dr = CompanyDS.Tables[0].Rows[i];
            string id = dr["ID"].ToString();

            if (HasCaseTable(id) && id != CompanyID)
            {
                string fieldsAddCompanyID = id + "  as CompanyID," + fields;
                string tempsql = string.Format(template, fieldsAddCompanyID, id, key, KeyValue);
                sb.AppendLine(tempsql);
                sb.AppendLine("Union All");


             

            }

        }

        string sql = sb.ToString();
     
        if (sql == "")
        {
            return;
        }
        else
        {

            sql = sql.Substring(0, sql.Length - "Union All".Length - 2);

            DataSet ds = ReportBLL.GetDataSet(sql);

        

            if (ds.Tables[0].Rows.Count>0)
            {
               
            ALLPatchsDS = BLL.PatchBLL.GetPatchList();

            }
         
            this.GridView2.DataSource = ds;
            this.GridView2.DataBind();
        }



    }
    private void bindKeyList()
    {


        RadioButtonList1.Items.Add(new ListItem("账号/合同号", "tbKey"));
        RadioButtonList1.Items.Add(new ListItem("身份证", "tbIdentityNo"));
        RadioButtonList1.Items.Add(new ListItem("手机号", "tbMobile"));
        RadioButtonList1.Items.Add(new ListItem("姓名", "tbName"));

        RadioButtonList1.Items[0].Selected = true;

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
        foreach (DataRow dr in this.ALLPatchsDS.Tables[0].Rows)
        {
            if (dr["ID"].ToString() == id)
            {
                return dr["PatchName"].ToString();
            }
        }
        return "";
    }


protected void  btnCaseType_Click(object sender, EventArgs e)
{
    BLL.AlertBLL.UpdateAlert(AlertTypeCaseTypeID, base.AlertTypeCaseType, int.Parse(CaseID), 0, this.ddlCaseType.SelectedItem.Text, "", DateTime.MinValue, int.Parse(CompanyID), OwnerID);
    Alert("saveSuccess");

}
protected void  btnPromised_Click(object sender, EventArgs e)
{
    decimal num = 0;
    try
    {
        num = decimal.Parse(this.txtPromisedPay.Text.Trim());
    }
    catch
    {
    }

    DateTime dt;
    try
    {
        dt = DateTime.Parse(txtDate.Text.Trim());
    }
    catch
    {
        Alert("timeformatincorrect");
        return;

    }


    BLL.AlertBLL.UpdateAlert(AlertTypePromiseID, base.AlertTypePromise, int.Parse(CaseID), num, "", CurrentUser.UserName, dt, int.Parse(CompanyID), OwnerID);
    Alert("saveSuccess");
}
protected void  btnFollow_Click(object sender, EventArgs e)
{
    DateTime dt;
    try
    {
        dt = DateTime.Parse(this.txtfollowDate.Text.Trim());
    }
    catch
    {
        Alert("timeformatincorrect");
        return;

    }
    BLL.AlertBLL.UpdateAlert(AlertTypeFollowByID, base.AlertTypeFollowBy, int.Parse(CaseID), 0, "", CurrentUser.UserName, dt, int.Parse(CompanyID), OwnerID);
    Alert("saveSuccess");
}
protected void  btnComment_Click(object sender, EventArgs e)
{
    BLL.AlertBLL.UpdateAlert(AlertTypeCommentID, base.AlertTypeComment, int.Parse(CaseID), 0, this.txtComment.Text.Trim(), CurrentUser.UserName, DateTime.Now, int.Parse(CompanyID), OwnerID);
    lblPerson.Text = "(" + CurrentUser.UserName + "/" +DateTime.Now.ToString() + ")";
    Alert("saveSuccess");
}
    protected void btnAddClass_Click(object sender, EventArgs e)
    {
        ArrayList arr = new ArrayList();
        string categoryID = this.txtClassID.Text;
        string sql = "Delete CaseTypeData where CaseID={0} and CompanyID={1} and CaseTypeID={2}";
        string sql1 = "insert CaseTypeData values({0} ,{1} ,{2})";

        arr.Add(string.Format(sql, CaseID, CompanyID, categoryID));
        arr.Add(string.Format(sql1, CaseID, CompanyID, categoryID));
           



        BLL.CaseTypeDataBLL.UpdateCaseTypeData(arr);

    }

    private string Act
    {
        get
        {
            return Request["act"] as string;
        }
    }

    private void BindCaseListNavigator()
    {
        this.ddlCaseList.DataSource = Session["CaseListDataSet"] as DataSet;
        this.ddlCaseList.DataTextField = "tbName";
        this.ddlCaseList.DataValueField = "ID";
        this.ddlCaseList.DataBind();
        base.GetDropDownListSeleted(ddlCaseList, CaseID);
    }
    protected void ddlCaseList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("CaseDetail.aspx?act=" + Act + "&id=" + this.ddlCaseList.SelectedItem.Value + "&CompanyID=" + CompanyID);
    }
    protected void lnkPre_Click(object sender, EventArgs e)
    {
        DataSet ds = Session["CaseListDataSet"] as DataSet;
        int index = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["ID"].ToString() == CaseID)
            {
                index = i;
                break;
            }
        }
        string id = ds.Tables[0].Rows[index - 1]["ID"].ToString();
        Response.Redirect("CaseDetail.aspx?act="+Act+"&id=" + id + "&CompanyID=" + CompanyID);
    }
    protected void lnkNext_Click(object sender, EventArgs e)
    {
        DataSet ds = Session["CaseListDataSet"] as DataSet;
        int index = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["ID"].ToString() == CaseID)
            {
                index = i;
                break;
            }
        }
        string id = ds.Tables[0].Rows[index + 1]["ID"].ToString();
        Response.Redirect("CaseDetail.aspx?act="+Act+"&id=" + id + "&CompanyID=" + CompanyID);

    }

    private void InitNavagator()
    {

        spanName.InnerText = CaseUserName;
        if (Act != null && Act!="")
        {
            BindCaseListNavigator();
            InitPreandNext();
            spanName.Style.Add("display", "none");
        }
        else
        {
            this.spanNavagator.Style.Add("display", "none");
        }
    }

    private void InitPreandNext()
    {
        DataSet ds=Session["CaseListDataSet"] as DataSet;
        if (ds == null)
        {
            this.lnkPre.Enabled = false;
            this.lnkNext.Enabled = false;
            return;
        }
        int count=ds.Tables[0].Rows.Count;
        if (count < 2)
        {
            this.lnkPre.Enabled = false;
            this.lnkNext .Enabled = false;
            return;
        }
        int index = 0;
        for (int i = 0; i < count; i++)
        {
            if (ds.Tables[0].Rows[i]["ID"].ToString() == CaseID)
            {
                index = i;
                break;
            }
        }
        if (index == 0)
        {
            this.lnkPre.Enabled = false;
           
        }

        if (index == count-1)
        {
            this.lnkNext .Enabled = false;
        }

    }
}
