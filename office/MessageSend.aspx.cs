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

public partial class MessageSend : PageBase
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            translation();
            if (Request["id"] != null )
            {
                fillData();
            }

            if (Request["appType"] != null) //applicaton
            {
                fillApp();
            }
          
        }

    }

    private void fillApp()
    {
        int appType = int.Parse(Request["appType"].ToString());
        string companyID = Request["companyID"];
        string caseID = Request["caseID"]; 

        string app = System.Configuration.ConfigurationManager.AppSettings["Application"];
            string[]  apps = app.Split('|');
        string url=string.Format("CaseDetail.aspx?id={0}&CompanyID={1}",caseID,companyID);
       string txt = apps[appType] + "---" + @" <a href='#' onclick=""OpenWindow('{0}',1000,800)"" >案件</a>";
       this.txtTitle.Text = apps[appType];
       this.txtBody.Value = string.Format(txt,url);

       this.txtRecipient.Text = getLead(companyID);
    }

    private string getLead(string companyID)
    {
        DataSet ds = GroupBLL.GetLeadID(CurrentUser.ID, companyID);
        if (ds.Tables[0].Rows.Count == 0)
        {
            return "Admin";

        }
        else
        {
            string leadid = ds.Tables[0].Rows[0][0].ToString();
            if(leadid ==CurrentUser.ID)
            {
                return "Admin";
            }
            SystemUser user = AdminBLL.GetUserByID(leadid);
            return user.UserName;

        }
    }

    private void fillData()
    {
        string id = Request["id"].ToString();
        string act = Request["act"].ToString();
        string type = Request["type"].ToString();

        DataSet ds;
        if (type == "send")
        {
            ds = MessageBLL.GetMessageByID(id);
        }
        else
        {
            ds = MessageBLL.GetReceivedMessageByID(id);
        }
       
        DataRow dr = ds.Tables[0].Rows[0];
       this.txtTitle.Text= "Re: "+dr["Title"].ToString();
       this.txtBody.Value = FillBody(dr);
       //this.txtBody.Value = dr["Body"].ToString();
       string sender = dr["Sender"].ToString();
       string reciver = dr["Recipient"].ToString();
       if (act == "0")
       {
           txtRecipient.Text = sender;
       }
       if (act == "1")
       {
           txtRecipient.Text = sender + ";" + reciver;
       }
       if (act == "2")
       {
           this.txtAttachList.Text = dr["Attachment"].ToString();
           this.divAttachmentList.InnerHtml = fillAttatchment(this.txtAttachList.Text);
       }
     


    }

    private string FillBody(DataRow dr)
    {
        string body = "<br/><br/><hr/>";
        body += "From:" + dr["Sender"].ToString() + "<br/>";
        body += "Sent:" + dr["SentOn"].ToString() + "<br/>";
        body += "To:" + dr["Recipient"].ToString() + "<br/>";
        body += Common.StrTable.GetStr("title") +":" + dr["Title"].ToString() + "<br/>";
        body += dr["Body"].ToString();
        return body;
    }

    private string fillAttatchment(string Attachmentlist)
    {
        if (Attachmentlist == "")
            return "<hr/>";
        string template = @"<span  style ='color:Blue;cursor:hand' onclick=""javascript:window.open('UploadPath/attachment/{1}')"">{0}</span>&nbsp;&nbsp;";
        string[] strs = Attachmentlist.Split('|');
        if (strs.Length < 1) return "<hr/>";
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
    protected void translation()
    {
        btnSave.Text = Common.StrTable.GetStr("send");


    }


    private string checkEmailUser()
    {
        string userStrs = txtRecipient.Text.Trim();
        string newrecp="";
        if (userStrs.Length <1)
            return "";
        string[] users = userStrs.Split(';');
        foreach (string user in users)
        {
            if (newrecp.Contains(user))
            {
                continue;
            }
            else
            {
                newrecp += user + ";";
            }
            string temp =user.Trim();
            if (!validUser(temp))
            {
                Alert(temp, "invaliduser");
                return "";
            }
        }
        newrecp = newrecp.Substring(0, newrecp.Length-1);
        return newrecp;
    }
    private bool validUser(string user)
    {
        if (user == "") return true;
        foreach (DataRow dr in UserDS.Tables[0].Rows)
        {
            if (dr["UserName"].ToString().ToLower() == user.ToLower())
            {
                return true;
            }
        }
        return false;
    }

   

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string newrecp = checkEmailUser();
        if (newrecp!="")
        {
            if (this.txtBody.Value.Length > 4000)
            {
                AlertTooLong();
                return;
            }
            BLL.MessageBLL.SendMessage(this.txtTitle.Text, this.txtBody.Value, this.CurrentUser.UserName, newrecp, "0", this.txtAttachList.Text, DateTime.Now);
            Refresh();
        }
      
    }

    private void Refresh()
    {
        string script = "window.close();opener.window.refreshPage();";
        base.ExceuteScript(script);
    }
    

   
    //private string GetPath(string file)
    //{
    //    return Server.MapPath(UploadFilePath) + file;
    //}
}
