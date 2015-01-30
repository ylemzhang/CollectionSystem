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

public partial class ExcepFields : PageBase
{
    string CompanyID
    {
        get
        {
            return Request["CompanyID"].ToString();
        }
    }

    string ReportType
    {
        get
        {
            return Request["type"].ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            DataSet ds = BLL.CompanyBLL.GetCacheFields(CompanyID, Common.Tools.CaseTableType);

            lstOrg.DataSource = ds.Tables[0];
            lstOrg.DataTextField = "FName";
            lstOrg.DataValueField = "FieldName";
            lstOrg.DataBind();
            if (ReportType == "0")
            {
                lstOrg.Items.Insert(0, new ListItem("业务员", "u.UserName"));
                lstOrg.Items.Insert(0, new ListItem("批号", "p.patchName"));
                lstOrg.Items.Insert(0, new ListItem("催收方式", "(case n.noteType when 1 then N'电催' else N'拜访' end)"));
                lstOrg.Items.Insert(0, new ListItem("备注", "n.body"));
                lstOrg.Items.Insert(0, new ListItem("联系电话", "n.Str1"));
                lstOrg.Items.Insert(0, new ListItem("联系对象", "n.contactorType"));
                lstOrg.Items.Insert(0, new ListItem("联系对象姓名", "n.contactor"));
                lstOrg.Items.Insert(0, new ListItem("是否可联", "n.contractResult"));

                lstOrg.Items.Insert(0, new ListItem("路费", "n.Num1"));
                lstOrg.Items.Insert(0, new ListItem("日期", "n.createon"));
            }
            else
            {
                lstOrg.Items.Insert(0, new ListItem("申请者", "Sender"));
                lstOrg.Items.Insert(0, new ListItem("接受申请者", "Recipient"));
                lstOrg.Items.Insert(0, new ListItem("申请类型", "Title"));

                lstOrg.Items.Insert(0, new ListItem("申请日期", "SentOn"));

             
            }

         
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet ds;
        string nameStr = this.txtFieldsName.Text;
        string valueStr = this.txtFieldsValue.Text;

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string[] names = nameStr.Split('|');
        string[] values = valueStr.Split('|');

        if (ReportType == "0")
        {
            for(int i=0;i<values.Length;i++)
            {
                string value = values[i];
                if (value.IndexOf(".") == -1)
                {
                    value = "c." + value;
                }
                sb.Append(value + " as '" + names[i] + "',");
            }
            string fs = sb.ToString();
            fs = fs.Substring(0, fs.Length - 1);
            ds = BLL.ReportBLL.GetCollectonDetail(CompanyID, Session["ExportWhere"].ToString(), fs);
        }


        else
        {



            getAll() ;
            ds = dsSource;
        }
        Session["ExportData"] = ds;
        Response.Redirect("ExportExcel.aspx");

    }

    public static string transform(string content)
    {
        content = content.Replace("&amp;", "&");
        content = content.Replace("&lt;", "<");
        content = content.Replace("&nbsp;", " ");
        content = content.Replace("&gt;", ">");
        content = content.Replace("&quot;", @"""");

        return content;
    }   
   string  ids;

    string GetCase(string body)
    {
        try
        {
            body = transform(body);
            string start = "CaseDetail.aspx?id=";
            int begin = body.IndexOf(start) + start.Length;
            int end = body.IndexOf("&CompanyID=");
            string id= body.Substring(begin, end - begin);
            int temp = int.Parse(id);
            return id;
         
        }
        catch
        {
            return "0";
        }
    }
    DataSet dsMail;
    DataSet dsCase;
    DataSet dsSource;
    private void getAll()
    {
        getEmptyDataSet();
        string order = " order by sender,title";
         dsMail = BLL.MessageBLL.GetReportMessageList(Session["ExportWhere"].ToString(), order);

        foreach(DataRow dr in  dsMail.Tables[0].Rows)
        {
            string id = GetCase(dr["Body"].ToString());
            dr["ID"] = id;
            ids += ","+id;

        }
        ids ="("+ ids.Substring(1)+")";
        dsCase = new BLL.CaseBLL(int.Parse(CompanyID)).GetReportCaseList("ID in" + ids);


        string nameStr = this.txtFieldsName.Text;
        string[] names = nameStr.Split('|');
        string valueStr = this.txtFieldsValue.Text;
        string[] values = valueStr.Split('|');

        foreach (DataRow dr in dsMail.Tables[0].Rows)
        {
            DataRow drSource = dsSource.Tables[0].NewRow();
            string id = dr["ID"].ToString();
          
            DataRow[] drCases = dsCase.Tables[0].Select("ID=" + id);
           
            if (drCases.Length == 0)
            {
                continue;
            }
            DataRow drCase = drCases[0];;
            for (int i = 0; i < names.Length; i++)
            {
               string name = names[i];
                 string value =values[i];
                DataRow ddtemp;

                if(dr.Table.Columns.Contains(value))
                {
                    ddtemp=dr;
                }
                else{
                    ddtemp=drCase;
                }

                if (ddtemp != null)
                {
                    drSource[name] = ddtemp[value].ToString();
                }
            }

            dsSource.Tables[0].Rows.Add(drSource);


        }
    }


    private void getEmptyDataSet()
    {
        dsSource = new DataSet();
        DataTable dt = new DataTable();

        string nameStr = this.txtFieldsName.Text;
        string[] names = nameStr.Split('|');

        for (int i = 0; i < names.Length; i++)
            {
                string name = names[i];
                DataColumn dc = new DataColumn(name);
             
                dt.Columns.Add(dc);

            }

        dsSource.Tables.Add(dt);
      
    }
}
