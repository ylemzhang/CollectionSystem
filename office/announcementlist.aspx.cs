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

public partial class announcementlist : PageBase
{
    protected int isadmin
    {
        get
        {
            if (this.IsAdmin)
                return 1;
            return 0;
        }
    }
    private string total;
    protected string TotalRecords
    {
        get
        {

            return total;

        }
        set
        {
            total = value;
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            translation();
            BindList();

        }

    }

    protected void translation()
    {


    }
    private void BindList()
    {
        string where;
        if (Request["type"] == "outofdate")
        {
            where = " ExpireDate < '{0}'";
            where = string.Format(where, DateTime.Now.ToShortDateString());
        }
        else
        {
            where = "ExpireDate is null or ExpireDate > '{0}'";
            where = string.Format(where, DateTime.Now.ToShortDateString());
        }
        DataSet ds = BLL.AnnoumentBLL.GetAnnouncementList(where);

        TotalRecords = ds.Tables[0].Rows.Count.ToString();
        if (ds.Tables[0].Rows.Count == 0)
        {
            //this.tbNew.Style.Add("display", "block");
            //this.divlist .Style.Add("display", "none");
        }
        else
        {

            this.ItemsList.DataSource = ds;
            this.ItemsList.DataBind();
        }
    }

    protected string GetAttachment(string attachment)
    {
        return attachment;
    }

    protected void ItemsList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
       System.Web.UI.HtmlControls.HtmlGenericControl ct= (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divAttachlist");
       if (ct != null)
       {

         
           fillDiv(ct);
       }
       
    }

    private void fillDiv(System.Web.UI.HtmlControls.HtmlGenericControl ct)
    {
       
        string template = @"<span  style ='color:Blue;cursor:hand' onclick=""javascript:window.open('UploadPath/attachment/{1}')"">{0}</span>&nbsp;&nbsp;";
        string Attachmentlist = ct.InnerHtml.Trim();
        string[] strs = Attachmentlist.Split('|');
        if (strs.Length < 2) return;
       Attachmentlist = strs[0];
        string realnames = strs[1];

        if (Attachmentlist.Length > 1)
        {
           
            string[] attrs = Attachmentlist.Split(',');
            string[] realatttrs = realnames.Split(',');
            string div = "&nbsp;&nbsp;<img src='images/attachment.gif'/>";

            for (int i = 0; i < attrs.Length; i++)
            {
                if (attrs[i] != "")
                {

                    div += string.Format(template, attrs[i], realatttrs[i]);
                }
            }
            ct.InnerHtml = div;
        }
        else
        {

            ct.InnerHtml = "";

        }
    }

}
