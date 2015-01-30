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

public partial class MessageList : PageBase
{
    protected string TotalRecords
    {
        get
        {

            return this.ViewState["TotalRecords"].ToString();

        }
        set
        {
            this.ViewState["TotalRecords"] = value;
        }
    }

    private string type
    {
        get
        {
            return Request["type"];
        }

    }

    private string WildCard
    {
        get
        {
            if (this.ViewState["WildCard"] == null)
            {
                this.ViewState["WildCard"] = this.txtSearch.Text.Trim();
            }
            return this.ViewState["WildCard"].ToString();

        }
        set
        {
            this.ViewState["WildCard"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (type == "send")
        {
            Response.Redirect("messagesendlist.aspx?type=send");
        }
        this.PagingControl1.PagingClick += new EventHandler(PagingControl1_PagingClick);
        translation();
        if (!this.IsPostBack)
        {
            PagingControl1.ListRecordNumPerPage = base.ListRecordNumPerPage;

            BindGrid();


        }

    }



    void PagingControl1_PagingClick(object sender, EventArgs e)
    {

        bind();
    }


    protected void translation()
    {
        LinkButton2.Text = Common.StrTable.GetStr("delete");

    }

    private void bind()
    {
        DataSet ds = GetDataGridSource();
        if (ds != null)
        {
            this.GridView1.DataSource = ds;
            this.GridView1.DataBind();

        }
    }
    private void BindGrid()
    {
        GetTotalRecords();
        bind();

    }
    private void GetTotalRecords()
    {

        string where = "Owner=N'{0}'";
        where = string.Format(where, CurrentUser.UserName);
        if (this.WildCard != "")
        {
            where = where + string.Format(" And (Title like N'%{0}%' or Body like N'%{0}%' or Sender  like N'%{0}%')", WildCard);
        }
        TotalRecords = MessageBLL.GetReceivedMessageTotalItems (where).ToString();

        PagingControl1.TotalRecords = int.Parse(TotalRecords);

    }

    private DataSet GetDataGridSource()
    {
        string where = "Owner=N'{0}'";
        where = string.Format(where, CurrentUser.UserName);
        if (this.WildCard != "")
        {
            where = where + string.Format(" And (Title like N'%{0}%' or Body like N'%{0}%' or Sender  like N'%{0}%')", WildCard);
        }

        DataSet ds = MessageBLL.GetReceivedMessagePagingitems (this.PagingControl1.ListRecordNumPerPage, this.PagingControl1.CurrentPage, where);

        return ds;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
          
            e.Row.Cells[4].Text = Common.StrTable.GetStr("sender");
            e.Row.Cells[5].Text = Common.StrTable.GetStr("title");
            e.Row.Cells[6].Text = Common.StrTable.GetStr("senton");
        }

        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != -1)
        {
            e.Row.Cells[1].Visible = false;
           
            string status=e.Row.Cells[2].Text ;
            if (status == "0")
            {
                e.Row.Cells[2].Text = "<img src='images/email.gif'/>";
            }
            else
            {
                e.Row.Cells[2].Text = "";
            }

            if (e.Row.Cells[3].Text.Trim() != "" && e.Row.Cells[3].Text.Trim() != "&nbsp;")
            {
                e.Row.Cells[3].Text = "<img src='images/attachment.gif'/>";
            }

            string id = e.Row.Cells[1].Text;
            string script = "edit('{0}','{1}','{2}',this)";
            script = string.Format(script, type, id, status);
            e.Row.Attributes.Add("ondblclick", script);

            e.Row.ToolTip = Common.StrTable.GetStr("dubbleClickToEdit");
        }
    }



    protected void LinkButton2_Click(object sender, EventArgs e) //delete
    {
        string idstr = "";
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)this.GridView1.Rows[i].FindControl("CheckBox1");
            if (chk.Checked)
            {
                idstr = idstr + this.GridView1.Rows[i].Cells[1].Text + ",";
            }
        }


        string ids = idstr.Substring(0, idstr.Length - 1);
        MessageBLL.DeleteReceivedMessage(ids);
        BindGrid();

    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.WildCard = txtSearch.Text.Trim();
        BindGrid();
    }
}

