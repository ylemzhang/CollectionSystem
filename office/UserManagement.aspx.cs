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

public partial class UserManagement :AdminPageBase
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!this.IsPostBack)
        {
            
            translation();
            BindGrid();

        }

    }


    protected void translation()
    {
        LinkButton2.Text = Common.StrTable.GetStr("delete");

    }

    private void BindGrid()
    {
      
        DataSet ds=  BLL.AdminBLL.GetUserListWithOutAdmin("ID,UserName,RealName");
      
        this.GridView1.DataSource = ds;
        this.GridView1.DataBind();

    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
            e.Row.Cells[2].Text = Common.StrTable.GetStr("userName");
          
            e.Row.Cells[3].Text = Common.StrTable.GetStr("realName");
        }

        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != -1)
        {
            e.Row.Cells[1].Visible = false;
            string id = e.Row.Cells[1].Text;
          
            e.Row.Attributes.Add("ondblclick", "window.location.href='UserEdit.aspx?id=" + id + "'");

            e.Row.ToolTip = Common.StrTable.GetStr("dubbleClickToEdit");


        }
    }

    //private string GetRoleName(string roleid)
    //{
    //    foreach (DataRow dr in roleTalbe.Tables[0].Rows)
    //    {
    //        if (roleid == dr[0].ToString())
    //            return dr[1].ToString();
    //    }
    //    return "";
    //}


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
        BLL.AdminBLL.DeleteUser(ids);
        BindGrid();

    }



}

