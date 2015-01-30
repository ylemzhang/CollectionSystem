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


public partial class TypeDetail : AdminPageBase
{
   

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {

              string id = Request["id"];
              if (id == null && id == "0" || id == "")
              {
                  this.txtTypeID.Text = string.Empty;
              }
              else
              {
                  this.txtTypeID.Text = id;
              }
            BindGrid();

        }
       
    }


    private void BindGrid()
    {
        string id = this.txtTypeID.Text;
        if (id == string.Empty) return;
        DataSet ds = new BLL.TypeBLL().GetTypeDataListByTypeID(id);
        this.GridView1.DataSource = ds;
        this.GridView1.DataBind();

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
            string fdisplay = e.Row.Cells[2].Text.Replace("&nbsp;", string.Empty); ;
         
            string description = e.Row.Cells[3].Text.Replace("&nbsp;", string.Empty); ;

            string script = string.Format("fillDetail('{0}','{1}','{2}')", fdisplay,  description,id);
            e.Row.Attributes.Add("ondblclick", script);
           
            e.Row.ToolTip = "Double click to view the Type data detail";

            
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

      
        string ID = txtID.Text;
      string TypeID = this.txtTypeID.Text;
       string FTypeValue = this.txtFDisplay.Text.Trim();

        string Description = this.txtDiscription.Text.Trim();


        if (this.txtID.Text != string.Empty)
        {

            new BLL.TypeBLL().UpdateTypeData(Description, FTypeValue, TypeID, ID);


        }
        else
        {

            new BLL.TypeBLL().InsertTypeData(Description, FTypeValue, TypeID);

        }

        BindGrid();
        initEditFrom();
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
        new BLL.TypeBLL().DeleteTypeData(ids);
        BindGrid();
        initEditFrom();
    }


    private void initEditFrom()
    {
        this.txtFDisplay.Text = string.Empty;
     
        this.txtDiscription.Text = string.Empty;
        this.txtID.Text =string.Empty;
        this.btnSave.Text = "新增";
         
    }
}

