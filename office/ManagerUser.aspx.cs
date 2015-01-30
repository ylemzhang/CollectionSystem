using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ManagerUser : Page
{
    /// <summary>
    /// 权限组GUID
    /// </summary>
    public String GroupGuid
    {
        get { return ViewState["GroupGuid"] != null ? ViewState["GroupGuid"].ToString() : string.Empty; }
        set { ViewState["GroupGuid"] = value; }
    }
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
    private void InitPage()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["guid"]))
        {
            GroupGuid = Request.QueryString["guid"];
        }
        InitDropDownListGroup();

        DataBind();
    }
    /// <summary>
    /// 初始化下拉框
    /// </summary>
    private void InitDropDownListGroup()
    {
        DropDownListGroup.Items.Clear();
        WebBean bean = WebBean.GetInstance();
        DataSet dataSet = bean.GetUserGroupList("", "");
        if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0) return;
        DropDownListGroup.Items.Add(new ListItem("请选择", "0"));
        foreach (DataRow row in dataSet.Tables[0].Rows)
        {
            DropDownListGroup.Items.Add(new ListItem(row["UserGroupName"].ToString(), row["GUID"].ToString()));
        }
        if (!string.IsNullOrEmpty(GroupGuid))
        {
            DropDownListGroup.Items.FindByValue(GroupGuid).Selected = true;
        }
    }
    /// <summary>
    /// 数据绑定到列表
    /// </summary>
    private void DataBind()
    {
        if (DropDownListGroup.SelectedValue == "0")
        {
            //提示请选择用户组
            GroupGuid = string.Empty;
        }
        WebBean bean = WebBean.GetInstance();
        bool? ban = null;
        switch (DropDownListBan.SelectedValue)
        {
            case "1":
                ban = false;
                break;
            case "2":
                ban = true;
                break;
            default:
                break;
        }
        DataSet dataSet = bean.SelectGroupUsersData(GroupGuid, TextBoxAccount.Text.Trim(),
                                                    TextBoxUserName.Text.Trim(), ban,
                                                    RadioButtonIsTrue.Checked);
        //提示消息"权限组信息保存成功！"
        Repeater1.DataSource = dataSet;
        Repeater1.DataBind();
    }
    /// <summary>
    /// 查询按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    /// <summary>
    /// 下拉框选择变化事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DropDownListGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        GroupGuid = DropDownListGroup.SelectedValue == "0" ? string.Empty : DropDownListGroup.SelectedValue;
        DataBind();
    }
    /// <summary>
    /// 行点击事件
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        WebBean bean = WebBean.GetInstance();
        if (e.CommandName == "action")
        {
            List<String> args = e.CommandArgument.ToString().Split(',').ToList();
            if (args.Count <= 0)
            {
                return;
            }
            bean.ChangeUserState(args[0], Convert.ToBoolean(args[1]));
            DataBind();
        }
        if(e.CommandName=="delete")
        {
            string arg = e.CommandArgument.ToString().Trim();
            bean.AddUserToUserGroup(arg, string.Empty, false);
            bean.DeleteUserData(arg);
            DataBind();
        }
    }
    /// <summary>
    /// 添加至权限组按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonAddToGroup_Click(object sender, EventArgs e)
    {
        if (DropDownListGroup.SelectedValue == "0")
        {
            //提示请选择要添加的用户组
            Show(Page, "请选择要添加到的权限组");
            return;
        }
        WebBean bean = WebBean.GetInstance();
        int count = 0;
        foreach (RepeaterItem item in Repeater1.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                var chk = (CheckBox) item.FindControl("CheckBoxItemID");
                if (chk.Checked)
                {
                    count++;
                    var hf = (HiddenField) item.FindControl("HiddenFieldGUID");
                    bean.AddUserToUserGroup(hf.Value, DropDownListGroup.SelectedValue, !RadioButtonIsTrue.Checked);
                }
            }
        }
        if (count > 0)
        {
            Show(Page, "添加成功！");
            DataBind();
        }
        else
        {
            //提示请选择要添加的用户
            Show(Page, "请选择要添加的用户");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioButton_OnCheckedChanged(object sender, EventArgs e)
    {
        ButtonAddToGroup.Text = !RadioButtonIsTrue.Checked ? "添加至权限组" : "从权限组中删除";
        DataBind();
    }

    /// <summary>
    /// 显示消息提示对话框
    /// </summary>
    /// <param name="page">当前页面指针，一般为this</param>
    /// <param name="msg">提示信息</param>
    public static void Show(Page page, string msg)
    {
        if (!page.ClientScript.IsStartupScriptRegistered("message"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message",
                                                    "<script language='javascript'>alert('" + msg.Replace("'", "‘") +
                                                    "');</script>");
        }
    }
}