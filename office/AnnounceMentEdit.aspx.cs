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

public partial class AnnounceMentEdit :PageBase 
{
     private string rID
    {
        get
        {
            if (this.ViewState["rID"] == null)
            {
                this.ViewState["rID"] = Request.QueryString["id"];
            }
            return this.ViewState["rID"].ToString();

        }

    }

 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            checkPermisssion();
            translation();
            FillData();
        }

    }
     void checkPermisssion()
     {
         if (!this.IsAdmin)
         {
             this.Response.Redirect("nopermission.htm");
         }
     }
    protected void translation()
    {
        btnSave.Text = Common.StrTable.GetStr("save");
      
        btnDelete.Text = Common.StrTable.GetStr("delete");


    }
    private void FillData()
    {
        if (rID == string.Empty)
        {
            this.btnDelete.Style.Add("display", "none");
            return;
            
        }

        spanAdd.Style.Add("display", "none");
        DataSet ds = BLL.AnnoumentBLL.GetAnnouncementByID(rID);

        DataRow dr = ds.Tables[0].Rows[0];

        this.txtTitle .Text = dr["Title"].ToString();
        this.txtBody.Value= dr["Body"].ToString();
        string date = dr["ExpireDate"].ToString();
        if (date != string.Empty)
        {
            this.txtDate.Text = ((DateTime)dr["ExpireDate"]).ToShortDateString();
        }

        this.txtAttachList.Text = dr["Attachment"].ToString();
        fillDiv();
    }

    private void fillDiv()
    {
        string allstr=this.txtAttachList.Text;
        if (allstr.Length <1)
        {
            return;
        }
        string [] strs=allstr.Split('|');
        string Attachmentlist = strs[0];
        string realnames = strs[1];
        if (Attachmentlist.Length > 1)
        {
            string template = @"<span  style ='color:Blue;cursor:hand' onclick=""javascript:window.open('UploadPath/attachment/{1}')"">{0}</span>&nbsp;&nbsp;";
            string s = Attachmentlist;
            string s1 = realnames;
          
            string[] attrs = s.Split(',');
            string[] realatttrs = s1.Split(',');

            string div = "";

            for (int i = 0; i < attrs.Length; i++)
            {
                if (attrs[i] != "")
                {

                    div += string.Format(template, attrs[i], realatttrs[i]);
                }
            }
            divAttachmentList.InnerHtml = div;
        }
        else
        {
           
            divAttachmentList.InnerHtml = "";

        }
    }

    private void Cancel()
    {
        string script = "window.close();opener.window.refreshPage();";
        base.ExceuteScript(script);
       // Server.Transfer("Announcementlist.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string date = this.txtDate.Text;
        DateTime expireDate=DateTime.MinValue;
        if (date!=string.Empty)
        {
            expireDate = DateTime.Parse(date);
        }
        if (this.txtBody.Value.Length > 4000)
        {
            AlertTooLong();
            return;
        }
        BLL.AnnoumentBLL.UpdateAnnouncement(rID, this.txtTitle.Text, this.txtBody.Value, expireDate, this.txtAttachList.Text, CurrentUser.UserName);
        Cancel();
      
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        BLL.AnnoumentBLL.DeleteAnnouncement(rID);
        Cancel();
    }

  


    //private string GetPath(string file)
    //{
    //    return Server.MapPath(Common.Tools.AttachMentPath) + file;
    //}
}
