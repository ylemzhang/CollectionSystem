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

public partial class SearchCaseList : PageBase
{

    protected string SearchTitle
    {
        get
        {
            string where = "";
            switch (SearchType)
            {
                case "1": where = "查找条件(电话)：" + SearchKey; break;
                case "2": where = "查找条件(姓名或帐号)：" + SearchKey; break;
                case "3": where = "查找条件(全部)：" + SearchKey; break;
            }
            return where;
        }

    }
    protected string SearchType
    {
        get
        {
            return Request["type"];
        }
    }

    protected string SearchKey
    {
        get
        {
            return Request["key"].Trim().Replace("'","''");
        }
    }

    protected string companyID
    {
        get
        {
            return Request["companyID"];
        }
    }

    DataSet companyDS;
    protected string TotalRecord = "";


  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
          
            GetCompanies();
            if (companyDS == null)
            {

                return;
            }
            BindList();
        }
    }

    private void GetCompanies()
    {
       
        companyDS = BLL.CompanyBLL.GetCompanyList();
       
    }

    private string GetSql()
    {
        string sqlTempate = @"select top 500 {0} as CompanyID,ID,OwnerID, tbName, tbKey,  tbBalance from companycase_{0}  where  {1} ";


       

        StringBuilder sb = new StringBuilder();


        int count = companyDS.Tables[0].Rows.Count;

        if (companyID == "")
        {

            for (int i = 0; i < count; i++)
            {
                DataRow dr = companyDS.Tables[0].Rows[i];
                string id = dr[0].ToString();
                if (HasCaseTable(id))
                {
                    string where = "";
                    switch (SearchType)
                    {
                        case "1": where = GetTelephoneSql(id); break;
                        case "2": where = GetNameandTbKeySql(id); break;
                        case "3": where = GetAllSql(id); break;
                    }

                    if (where != "")
                    {
                        string tempsql = string.Format(sqlTempate, id, where);
                        sb.AppendLine(tempsql);

                        sb.AppendLine("Union All");
                    }

                }

            }
        }
        else
        {

            if (HasCaseTable(companyID))
            {
                string where = "";
                switch (SearchType)
                {
                    case "1": where = GetTelephoneSql(companyID); break;
                    case "2": where = GetNameandTbKeySql(companyID); break;
                    case "3": where = GetAllSql(companyID); break;
                }

                if (where != "")
                {
                    string tempsql = string.Format(sqlTempate, companyID, where);
                    sb.AppendLine(tempsql);

                    sb.AppendLine("Union All");
                }

            }
        }

        return sb.ToString();
    }

    private string GetTelephoneSql(string CompanyID)
    {
        DataSet ds = BLL.CompanyBLL.GetCacheFields(CompanyID, Common.Tools.CaseTableType);
        DataRow[] drs = ds.Tables[0].Select("FieldType='telephone'");
        string rtn = "";

        if (SearchKey.Contains(","))
        {
            string [] keys = SearchKey.Split(',');
            foreach (string key in keys)
            {
                if (key != "")
                {
                    foreach (DataRow dr in drs)
                    {
                        rtn += " OR " + dr["FieldName"].ToString() + " like N'%" + key + "%'";
                    }
                }
            }
                
        }
        else
        {
            foreach (DataRow dr in drs)
            {
                rtn += " OR " + dr["FieldName"].ToString() + " like N'%" + SearchKey + "%'";
            }
        }
        if (rtn == "")
        {
            return "";
        }
        else
        {
            return rtn.Substring(3);
        }
       
    }

    private string GetNameandTbKeySql(string CompanyID)
    {
        string rtn = "";

        if (SearchKey.Contains(","))
        {
            string[] keys = SearchKey.Split(',');
            foreach (string key in keys)
            {
                if (key != "")
                {
                  
                        rtn += " OR  tbName like N'%" + key + "%' or tbKey  like N'%" + key + "%'";
                   
                }
            }

            return rtn.Substring(3);

        }
        else
        {
            return " tbName like N'%" + SearchKey + "%' or tbKey  like N'%" + SearchKey + "%'";
        }

      

      
      
    }

    private string GetAllSql(string CompanyID)
    {
        DataSet ds = BLL.CompanyBLL.GetCacheFields(CompanyID, Common.Tools.CaseTableType);

        string rtn = "";
        string fieldtype;
       
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            fieldtype = dr["FieldType"].ToString().ToLower();
            if (fieldtype == "datetime" || fieldtype == "money")
            {
                continue;
            }

            if (SearchKey.Contains(","))
            {
                string[] keys = SearchKey.Split(',');
                foreach (string key in keys)
                {
                    if (key != "")
                    {
                        rtn += " OR " + dr["FieldName"].ToString() + " like N'%" + key + "%'";
                    }
                }
            }
            else
            {
                rtn += " OR " + dr["FieldName"].ToString() + " like N'%" + SearchKey + "%'";
            }

          
        }

        return rtn.Substring(3);

    }


    private string GetNoteAndCommentSql()
    {
        string where = "Select CaseID from NoteTable where CompanyID={0} and ( body like N'%" + SearchKey + "%' or Str2 like N'%" + SearchKey + "%') union all Select CaseID from AlertTable where CompanyID={0} and (Str1 like N'%" + SearchKey + "%') ";
        string sqlTempate = @"select top 50 {0} as CompanyID,ID,OwnerID, tbName, tbKey,  tbBalance from companycase_{0}  where  ID in ({1} ) ";
        StringBuilder sb = new StringBuilder();


        int count = companyDS.Tables[0].Rows.Count;

        for (int i = 0; i < count; i++)
        {
            DataRow dr = companyDS.Tables[0].Rows[i];
            string id = dr[0].ToString();
            if (HasCaseTable(id))
            {
                string tempWhere = string.Format(where, id);
                string tempsql = string.Format(sqlTempate, id, tempWhere);
                sb.AppendLine(tempsql);

                sb.AppendLine("Union All");

            }

        }
        return sb.ToString();
    }

    private void BindList()
    {

        string sql = GetSql();
        if (sql == "")
        {
            return;
        }
        else
        {

            sql = sql.Substring(0, sql.Length - "Union All".Length - 2);// /r/n
            DataSet ds = BLL.CaseBLL.GetSearchCaseList(sql);
            TotalRecord = ds.Tables[0].Rows.Count.ToString();
            if (TotalRecord== "0")
            {
                if (SearchType == "3")
                {
                    sql = GetNoteAndCommentSql();
                    sql = sql.Substring(0, sql.Length - "Union All".Length - 2);// /r/n
                     ds = BLL.CaseBLL.GetSearchCaseList(sql);
                    TotalRecord = ds.Tables[0].Rows.Count.ToString();

                    if (TotalRecord == "0")
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        ds.Tables[0].Rows.Add(dr);
                    }

                }
                else
                {
                    DataRow dr1 = ds.Tables[0].NewRow();
                    ds.Tables[0].Rows.Add(dr1);
                }

            }
            this.GridView1.DataSource = ds;
            this.GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != -1)
        {
            string companyID = e.Row.Cells[1].Text;
            string caseID = e.Row.Cells[5].Text;
            e.Row.Cells[0].Text = GetUserName(e.Row.Cells[0].Text.Trim());
            e.Row.Cells[1].Text = getCompanyName(companyID);

        
            //e.Row.Attributes.Add("ondblclick", "window.open('CaseDetail.aspx?id=" + caseID + "&CompanyID=" + companyID + "')");
            e.Row.Attributes.Add("ondblclick", "window.open('CaseDetail.aspx?id=" + caseID + "&CompanyID=" + companyID + "','_blank')");
            e.Row.ToolTip = Common.StrTable.GetStr("dubbleClickToEdit");
            e.Row.Cells[5].Visible = false;

        }
        else
        {
            e.Row.Cells[5].Visible = false;
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

}
