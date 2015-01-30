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

public partial class LeaveManagement : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            if (isMyPage)
            {
                this.LinkButton2.Visible = false;
            }
            int m = Convert.ToInt32(DateTime.Today.DayOfWeek);

           this.txtFrom.Text = DateTime.Now.AddDays(-m).ToShortDateString();
           this.txtTo.Text = DateTime.Now.AddDays(6-m).ToShortDateString();
           BindGrid(this.txtFrom.Text, this.txtTo.Text);

        }

    }

   


    protected bool isMyPage
    {
        get
        {
            return (Request["type"] == null);
        }
    }

    
    private void BindGrid(string dateFrom, string dateTo)
    {

        string where;
        if (dateFrom == "")
        {
            dateFrom = "1-1-1";
        }
        else
        {
            try
            {
                DateTime.Parse(dateFrom);
            }
            catch
            {
                dateFrom = "1-1-1";
                this.txtFrom.Text = "";
            }
            dateFrom = dateFrom + " 00:00:00";
        }
        if (dateTo == "")
        {
            

            dateTo = "3000-12-1";
        }
        else
        {
            try
            {
                DateTime.Parse(dateTo);
            }
            catch
            {
                dateTo = "3000-12-1";
                this.txtTo.Text = "";
            }

            dateTo = dateTo + " 23:59:59";
        }

        if (isMyPage)
        {
            where = "userID={0} and PunchIn between '{1}' and '{2}'";
            where = string.Format(where, CurrentUser.ID, dateFrom, dateTo);
           
        }
        else //system management
        {

            if (this.IsAdmin)
            {


                where = "PunchIn between '{0}' and '{1}' and UserID<>{2} order by UserID  ";
                where = string.Format(where, dateFrom, dateTo, CurrentUser.ID);


            }
            else
            {
                Response.Redirect("Nopermission.htm");
                return;
            }
           

        }

        this.GridView1.DataSource = BLL.LeaveBLL.GetLeaveList(where);
        this.GridView1.DataBind();

    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            if (isMyPage)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
            }
            else
            {
                e.Row.Cells[1].Visible = false;
               
            }
         
        }

        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != -1)
        {
           // e.Row.Cells[1].Visible = false;

            e.Row.Cells[3].Text = GetWeek(e.Row.Cells[3].Text.Trim());
            e.Row.Cells[4].Text = GetLateWeek(e.Row.Cells[4].Text.Trim());
            e.Row.Cells[5].Text = GetEarlyWeek(e.Row.Cells[5].Text.Trim());
            if (isMyPage)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
            }
            else
            {
                e.Row.Cells[1].Visible = false;
            }

            if (this.IsAdmin && (!isMyPage ))
            {
                string id= e.Row.Cells[1].Text.Trim();

                e.Row.Attributes.Add("ondblclick", "window.location.href='leaveEdit.aspx?id=" + id + "'");
                e.Row.ToolTip = Common.StrTable.GetStr("dubbleClickToEdit");
            }

        }
    }

    private string GetWeek(string date)
    {
        if (date == "" || date == "&nbsp;") return "";
        DateTime dt = DateTime.Parse(date);
       return dt.DayOfWeek.ToString();
     
    }
    private string GetLateWeek(string date)
    {
        if (date == "" || date == "&nbsp;") return "";
        DateTime dt = DateTime.Parse(date);
        int comeHour = int.Parse(System.Configuration.ConfigurationManager.AppSettings["comeHour"]);
        DateTime dstandard = new DateTime(dt.Year, dt.Month, dt.Day, comeHour, 0, 0);
        if (dstandard < dt)
            return "<font color='red'>" + date + "</font>";
        else
            return date;

    }
    private string GetEarlyWeek(string date)
    {
        if (date == "" || date == "&nbsp;") return "";

        int leaveHour = int.Parse(System.Configuration.ConfigurationManager.AppSettings["leaveHour"]);
        DateTime dt = DateTime.Parse(date);
        DateTime dstandard = new DateTime(dt.Year, dt.Month, dt.Day, leaveHour, 0, 0);
        if (dstandard > dt)
            return "<font color='red'>" + date + "</font>";
        else
            return date;


    }

    protected void btn_Click(object sender, EventArgs e)
    {
        string dateFrom = this.txtFrom.Text.Trim(); ;
        string dateTo = this.txtTo.Text.Trim();
        BindGrid(dateFrom, dateTo);
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
        BLL.LeaveBLL.DeleteLeave (ids);
        string dateFrom = this.txtFrom.Text.Trim(); ;
        string dateTo = this.txtTo.Text.Trim();
        BindGrid(dateFrom, dateTo);
    }

}

