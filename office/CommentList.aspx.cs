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

public partial class CommentList : PageBase
{
    protected string TotalRecord = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            string id = Request["caseId"];
            DataSet ds = BLL.AlertBLL.GetAlertList("CaseID=" + id + " and AlertType=" + base.AlertTypeComment);
            TotalRecord = ds.Tables[0].Rows.Count.ToString();
            this.GridView1.DataSource = ds;
            this.GridView1.DataBind();
          
        }
    }

   
}
