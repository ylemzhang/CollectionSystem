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

public partial class ExportExcel : System.Web.UI.Page
{



    private void BindGrid()
    {
        DataSet ds = Session["ExportData"] as DataSet;


        this.GridView1.DataSource = ds.Tables[0];

        GridView1.DataBind();




    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            BindGrid();

            Response.Clear();
            Response.Buffer = true;//设置缓冲输出     
            Response.Charset = "GB2312";//设置输出流的HTTP字符集   

            Response.AppendHeader("Content-Disposition", "attachment;filename=Report.xls");
            Response.ContentEncoding = System.Text.Encoding.UTF7;

            Response.ContentType = "application/ms-excel";
            // Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");
            System.IO.StringWriter tw = new System.IO.StringWriter();//将信息写入字符串
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);//在WEB窗体页上写出一系列连续的HTML特定字符和文本。

            GridView1.RenderControl(hw);
            Response.Write(tw.ToString());



            Response.End();


            //Response.ContentType = "application/ms-excel";
            //Response.Charset = "GB2312";
            //string filename = "attachment;filename=list.xls";

            //Response.AppendHeader("Content-Disposition", filename);
            //Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");// 


            //System.IO.StringWriter tw = new System.IO.StringWriter();//将信息写入字符串
            //System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);//在WEB窗体页上写出一系列连续的HTML特定字符和文本。
            ////此类提供ASP.NET服务器控件在将HTML内容呈现给客户端时所使用的格式化功能
            ////获取control的HTML
            //GridView1.RenderControl(hw);		//将DATAGRID中的内容输出到HtmlTextWriter对象中
            //// 把HTML写回浏览器
            //Response.Write(tw.ToString());
            //Response.End();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Cells[i].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        }



    }



   

    protected void GridView1_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            for (int i = 0; i < e.Item.Cells.Count; i++)
            {

                e.Item.Cells[i].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            }

        }

    }
}
