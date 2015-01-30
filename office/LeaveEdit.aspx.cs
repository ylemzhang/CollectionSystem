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

public partial class LeaveEdit : AdminPageBase
{
    private string returnUrl
    {
        get
        {
            return "leavemanagement.aspx?type=leaveManagement";

        }

    }

    private string LeaveID
    {
        get
        {
            if (this.ViewState["LeaveID"] == null)
            {
                this.ViewState["LeaveID"] = Request.QueryString["id"];
            }
            return this.ViewState["LeaveID"].ToString();

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = string.Format("window.location.href='{0}';return false", returnUrl);
        btnCancel.Attributes.Add("onclick", script);


        if (!this.IsPostBack)
        {
            translation();
            bindData();
           
        }

    }

    protected void translation()
    {
        btnSave.Text = Common.StrTable.GetStr("save");
        btnCancel.Text = Common.StrTable.GetStr("cancel");


    }


    private void bindData()
    {

   
            DataSet ds = BLL.LeaveBLL.GetLeaveByID(LeaveID);
            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                this.txtUserName.Text = dr["UserName"].ToString();
                this.txtIn.Text = dr["PunchIn"].ToString();
                this.txtOut.Text = dr["PunchOut"].ToString();
            }
      
     

    }
   

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DateTime dtIn = DateTime.MinValue;
        DateTime dtOut = DateTime.MinValue;
        if (txtIn.Text.Trim() != "")
        {
            try
            {
                dtIn = DateTime.Parse(txtIn.Text.Trim());
            }
            catch
            {
                Alert("timeformatincorrect");
            }
        }
        if (txtOut.Text.Trim() != "")
        {
            try
            {
                dtOut = DateTime.Parse(txtOut.Text.Trim());
            }
            catch
            {
                Alert("timeformatincorrect");
            }
        }

        BLL.LeaveBLL.UpdateLeave(int.Parse(LeaveID), dtIn, dtOut);
        string script = string.Format("window.location.href='{0}';", returnUrl);
        base.ExceuteScript(script);

      
    }
}
