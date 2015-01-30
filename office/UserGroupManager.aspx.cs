using System;
using System.Data;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserGroupManager : Page
{
    /// <summary>
    /// 页面载入事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
        }
    }
    /// <summary>
    /// 初始化页面
    /// </summary>
    public void InitPage()
    {
        WebBean bean = WebBean.GetInstance();
        DataSet dataSet = bean.GetUserGroupList("",TextBoxUserGroupName.Text.Trim());

        Repeater1.DataSource = dataSet;
        Repeater1.DataBind();
    }

    

    /// <summary>
    /// 行点击事件
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string guid = e.CommandArgument.ToString();
        switch (e.CommandName)
        {
            case "delete":
                WebBean bean = WebBean.GetInstance();
                DataSet dataSet = bean.SaveUserGroupData(guid, string.Empty, "delete");
                Repeater1.DataSource = dataSet;
                Repeater1.DataBind();
                break;
            case "assign":
                Response.Redirect("AssignPermission.aspx?guid="+guid);
                break;
            case "usermanager":
                Response.Redirect("ManagerUser.aspx?guid=" + guid);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 查询按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        InitPage();
    }
}