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

public partial class MessageDetail : System.Web.UI.Page
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


    protected string Attachment
    {
        get
        {

            return this.ViewState["Attachment"].ToString();

        }
        set
        {
            this.ViewState["Attachment"] = value;
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

    protected string type
    {
        get
        {
            return Request["type"];
        }

    }

    protected int noid
    {
        get
        {
            if (Request["id"] == null || Request["id"] == null)
                return 1;
            return 0;
        }
    }

    protected string messageID
    {
        get
        {
            if (Request["id"]!=null)
            return Request["id"].ToString();
        return "";
        }
    }
    string titleFormat = " <strong><font color =blue size=3pt>{0} </font> </strong>  ";
    string senderFormat = "<font size=3pt>{3}&nbsp;{2}&nbsp;{0}  </font> &nbsp;&nbsp;&nbsp;{1} ";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
           
            if (noid == 1)
            {
                Title = "";
                Attachment = "";
                Body = "";
                Sender = "";


            }
            else
            {

                if (type == "inbox" && Request["isfirst"] == "0")
                {
                    UpdateStatus();
                }
                fillData();

            }
        }

    }
    private void UpdateStatus() //unread to read
    {
        MessageBLL.UpdateReceivedMessageStatus("1", "ID=" + Request["id"]);
    }

    private void fillData()
    {
        DataSet ds;
        if (type == "send" )
        {
            ds = MessageBLL.GetMessageByID(Request["id"]);
        }
        else
        {
            ds = MessageBLL.GetReceivedMessageByID(Request["id"]);
        }
        if (ds.Tables[0].Rows.Count == 0)
        {
            Title = "No Record";
            Attachment = "";
            Body = "";
            Sender = "";
            return;
        }
        DataRow dr = ds.Tables[0].Rows[0];
        Title = string.Format(titleFormat, dr["Title"].ToString());
        if (type == "inbox")
        {
            string sss = "From &nbsp" + dr["Sender"].ToString();

            Sender = string.Format(senderFormat, dr["Recipient"].ToString(), dr["SentOn"].ToString(), "To", sss);
        }
        else  //sent box
        {
            Sender = string.Format(senderFormat, dr["Recipient"].ToString(), dr["SentOn"].ToString(), "To", "");
        }

        Body = dr["Body"].ToString();
        Attachment = fillAttatchment(dr["Attachment"].ToString());
    }

    private string fillAttatchment(string Attachmentlist)
    {
        if (Attachmentlist == "")
            return "<hr/>";
        string template = @"<span  style ='color:Blue;cursor:hand' onclick=""javascript:window.open('UploadPath/attachment/{1}')"">{0}</span>&nbsp;&nbsp;";
        string[] strs = Attachmentlist.Split('|');
        if (strs.Length < 2) return "<hr/>";
        Attachmentlist = strs[0];
        string realnames = strs[1];

        if (Attachmentlist.Length > 1)
        {
            string s = Attachmentlist;
         
            string[] attrs = s.Split(',');
            string[] realatttrs = realnames.Split(',');
            string div = "&nbsp;&nbsp;<img src='images/attachment.gif'/>";

            for (int i = 0; i < attrs.Length; i++)
            {
                if (attrs[i] != "")
                {

                    div += string.Format(template, attrs[i], realatttrs[i]);
                }
            }
            return div + "<hr/>";
        }
        return "<hr/>";

    }
}
