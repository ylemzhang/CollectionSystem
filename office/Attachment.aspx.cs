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

public partial class Attachment :PageBase
{
    

    protected string Attachmentlist
    {
        get
        {
            if (this.ViewState["Attachmentlist"] == null)
            {
                this.ViewState["Attachmentlist"] ="";
            }
            return this.ViewState["Attachmentlist"].ToString();

        }
        set
        {
            this.ViewState["Attachmentlist"] = value;
        }

    }

    protected string AttachmentlistRealName
    {
        get
        {
            if (this.ViewState["AttachmentlistRealName"] == null)
            {
                this.ViewState["AttachmentlistRealName"] = "";
            }
            return this.ViewState["AttachmentlistRealName"].ToString();

        }
        set
        {
            this.ViewState["AttachmentlistRealName"] = value;
        }

    }


 

    protected void Page_Load(object sender, EventArgs e)
    {
        this.btnImport.Text = Common.StrTable.GetStr("attach");
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string filename = this.FileUpload1.PostedFile.FileName.Trim();

        if (filename == string.Empty) return;


        int start = filename.LastIndexOf("\\");
        string saveName = filename.Substring(start + 1);

        start = saveName.LastIndexOf(".");
        string ExtName = saveName.Substring(start);

        string realname = Guid.NewGuid() + ExtName;

        string phicalPath = GetPath(realname);

        FileUpload1.PostedFile.SaveAs(phicalPath);

        Attachmentlist = Attachmentlist + "," + saveName;
        AttachmentlistRealName = AttachmentlistRealName + "," + realname;
        if (Attachmentlist.StartsWith(","))
        {
           Attachmentlist= Attachmentlist.Substring(1);
        }

        if (AttachmentlistRealName.StartsWith(","))
        {
            AttachmentlistRealName = AttachmentlistRealName.Substring(1);
        }
        fillDiv();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string filename = txtDeleteFile.Text;
        string realname = txtDeleteRealName.Text;
        if (filename == string.Empty) return;
        string phicalPath = GetPath(realname);
        System.IO.FileInfo file = new System.IO.FileInfo(phicalPath);
        if (file.Exists)
        {
            file.Delete();
        }


      
       Attachmentlist = Attachmentlist.Replace(filename , "");

       Attachmentlist = Attachmentlist.Replace(",,", ",");
       if (Attachmentlist.StartsWith(","))
       {
           Attachmentlist=Attachmentlist.Substring(1);
       }
       if (Attachmentlist.EndsWith(","))
       {
           Attachmentlist = Attachmentlist.Substring(0, Attachmentlist.Length - 1);
       }


       AttachmentlistRealName = AttachmentlistRealName.Replace(realname , "");
       AttachmentlistRealName = AttachmentlistRealName.Replace(",,", ",");
       if (AttachmentlistRealName.StartsWith(","))
       {
           AttachmentlistRealName=AttachmentlistRealName.Substring(1);
       }
       if (AttachmentlistRealName.EndsWith(","))
       {
           AttachmentlistRealName=AttachmentlistRealName.Substring(0, AttachmentlistRealName.Length - 1);
       }

      
        fillDiv();

      
    }

    

    private void fillDiv()
    {
        if (Attachmentlist.Length > 1)
        {
          
            string[] attrs = Attachmentlist.Split(',');
            string[] realattrs = AttachmentlistRealName.Split(',');
            string div = "";

            for (int i = 0; i < attrs.Length; i++)
            {
                if (attrs[i] != "")
                {
                    string template =@"<span  style ='color:Blue;cursor:hand' onclick=""javascript:window.open('UploadPath/attachment/{1}')"">{0}</span>&nbsp;&nbsp;<span style=""cursor:hand "" onclick=""deleteAttach('{0}','{1}')"">[Delete]</span> &nbsp;&nbsp;";
                    div += string.Format(template, attrs[i], realattrs[i]);
                }
            }
            divAttachmentList.InnerHtml = div;
        }
        else
        {
             divAttachmentList.InnerHtml = "";

        }
    }

    //private string GetPath(string file)
    //{


    //    return Server.MapPath(UploadFilePath) + file;
    //}
}
