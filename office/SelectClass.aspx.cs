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

public partial class SelectClass : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            DataSet ds = BLL.CaseTypeBLL.GetCaseTypeList("*", "UserID=" + CurrentUser.ID);
            this.ddlClass.DataSource = ds;
            this.ddlClass.DataTextField = "CaseTypeName";
            this.ddlClass.DataValueField = "ID";
            this.ddlClass.DataBind();
        }
    }
}