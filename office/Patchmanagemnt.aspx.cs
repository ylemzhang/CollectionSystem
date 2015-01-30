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

public partial class Patchmanagemnt : AdminPageBase
{

    protected string CompanyID
    {
        get
        {
            return Request["id"].ToString();
        }
    }

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


    protected void Page_Load(object sender, EventArgs e)
    {
        this.PagingControl1.PagingClick += new EventHandler(PagingControl1_PagingClick);
        if (!this.IsPostBack)
        {

           
            PagingControl1.ListRecordNumPerPage = base.ListRecordNumPerPage;
            BindGrid();

        }

    }


    private void BindGrid()
    {
        GetTotalRecords();
        bind();

    }
    private void GetTotalRecords()
    {

        string where = "CompanyID=" + CompanyID;
     
      
        TotalRecords = PatchBLL.GetPatchTotalItems(where).ToString();

        PagingControl1.TotalRecords = int.Parse(TotalRecords);

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

    private DataSet GetDataGridSource()
    {
        string where = "CompanyID=" + CompanyID;
    

        DataSet ds = PatchBLL.GetPatchPagingitems(this.PagingControl1.ListRecordNumPerPage, this.PagingControl1.CurrentPage, where);

        return ds;
    }

    void PagingControl1_PagingClick(object sender, EventArgs e)
    {

        bind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
          
        }
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != -1)
        {
            e.Row.Cells[1].Visible = false;
            string id = e.Row.Cells[1].Text;
           
            e.Row.Attributes.Add("ondblclick", "window.location.href='PatchEdit.aspx?id=" + id + "&CompanyID="+CompanyID+"'");

            e.Row.ToolTip = Common.StrTable.GetStr("dubbleClickToEdit");
             e.Row.Cells[3].Text = DateTime.Parse(e.Row.Cells[3].Text.Trim()).ToString("yyyy-MM-dd");
             e.Row.Cells[4].Text = DateTime.Parse(e.Row.Cells[4].Text.Trim()).ToString("yyyy-MM-dd");
            
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
        BLL.PatchBLL.DeletePatch(ids,CompanyID);
        BindGrid();

    }



}

