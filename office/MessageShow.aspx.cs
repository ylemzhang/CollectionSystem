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

public partial class MessageShow : System.Web.UI.Page
{
    protected string Title
    {
        get
        {

            return this.ViewState["Title"].ToString();

        }
        set
        {
            this.ViewState["Title"] = value;
        }
    }


    protected string SendOn
    {
        get
        {

            return this.ViewState["SendOn"].ToString();

        }
        set
        {
            this.ViewState["SendOn"] = value;
        }
    }

    protected string Recipient
    {
        get
        {

            return this.ViewState["Recipient"].ToString();

        }
        set
        {
            this.ViewState["Recipient"] = value;
        }
    }

    protected string Body
    {
        get
        {

            return this.ViewState["Body"].ToString();

        }
        set
        {
            this.ViewState["Body"] = value;
        }
    }

    protected string Sender
    {
        get
        {

            return this.ViewState["Sender"].ToString();

        }
        set
        {
            this.ViewState["Sender"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
          DataSet  ds = BLL.MessageBLL.GetMessageByID(Request["id"]);
          DataRow dr = ds.Tables[0].Rows[0];
          Recipient = dr["Recipient"].ToString();
          Body = dr["Body"].ToString();
          Sender = dr["Sender"].ToString();
          SendOn = dr["sentOn"].ToString();
          Title = dr["Title"].ToString();

        }
    }
}
