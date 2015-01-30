using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AssignPermission : Page
{
    /// <summary>
    /// 权限组GUID
    /// </summary>
    private string GroupGuid
    {
        get { return ViewState["GroupGuid"] == null ? string.Empty : ViewState["GroupGuid"].ToString(); }
        set { ViewState["GroupGuid"] = value; }
    }
    /// <summary>
    /// 父节点GUID
    /// </summary>
    private string ParentUrlGuid
    {
        get { return ViewState["ParentUrlGuid"] == null ? null : ViewState["ParentUrlGuid"].ToString(); }
        set { ViewState["ParentUrlGuid"] = value; }
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
            if (Request.QueryString["guid"] != null)
            {
                GroupGuid = Request.QueryString["guid"];
                HiddenFieldGroupGUID.Value = GroupGuid;
            }
            InitUserGroupInfo();
            InitPages();
        }
    }
    /// <summary>
    /// 页面初始化
    /// </summary>
    private void InitPages()
    {
        InitUrlDataList();
        foreach (RepeaterItem item in Repeater1.Items)
        {
            var temp = item.FindControl("LinkButtonPrev") as LinkButton;
            temp.Visible = !String.IsNullOrEmpty(divSiteNav.InnerText.Trim());
        }
    }
    /// <summary>
    /// 初始化权限组信息
    /// </summary>
    private void InitUserGroupInfo()
    {
        WebBean bean = WebBean.GetInstance();
        DataSet dataSet = bean.GetUserGroupList(String.Empty, string.Empty);
        if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0) return;
        foreach (DataRow row in dataSet.Tables[0].Rows)
        {
            DropDownListGroupName.Items.Add(new ListItem(row["UserGroupName"].ToString(), row["GUID"].ToString()));
        }
        if (!string.IsNullOrEmpty(GroupGuid))
        {
            DropDownListGroupName.Items.FindByValue(GroupGuid).Selected = true;
        }
    }
    /// <summary>
    /// 初始化权限数据列表
    /// </summary>
    private void InitUrlDataList()
    {
        WebBean bean = WebBean.GetInstance();
        DataSet dataSet = bean.GetUrlAndUserGroupLink(GroupGuid, string.Empty, ParentUrlGuid,
                                                      DropDownListStatus.SelectedValue);
        Repeater1.DataSource = dataSet;
        Repeater1.DataBind();

        //if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0) return;
        //BindTreeView(dataSet.Tables[0], TreeView1.Nodes[0]);
    }
    /// <summary>
    /// 行绑定事件
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string arg = e.CommandArgument.ToString();
        int index = 0;
        var labelName = new Label();
        switch (e.CommandName)
        {
            case "next":
                ParentUrlGuid = arg;
                index = e.Item.ItemIndex;
                labelName = Repeater1.Items[index].FindControl("LabelUrlName") as Label;
                if (labelName != null)
                {
                    divSiteNav.InnerText += "-->" + labelName.Text;
                }
                InitUrlDataList();
                break;
            case "prev":
                ParentUrlGuid = GetParentUrlGuid(ParentUrlGuid);
                InitUrlDataList();
                index = e.Item.ItemIndex;
                labelName = Repeater1.Items[index].FindControl("LabelUrlName") as Label;
                if (labelName != null)
                {
                    int start = divSiteNav.InnerText.LastIndexOf("-->");
                    if (start >= 0)
                    {
                        divSiteNav.InnerText = divSiteNav.InnerText.Substring(0, start);
                    }
                }
                break;
        }
    }
    /// <summary>
    /// 根据子页面地址获取父页面地址
    /// </summary>
    /// <param name="childUrlGuid"></param>
    /// <returns></returns>
    private string GetParentUrlGuid(string childUrlGuid)
    {
        WebBean bean = WebBean.GetInstance();
        DataSet dataSet = bean.Select_Url_Data(childUrlGuid, string.Empty);
        if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0) return string.Empty;
        foreach (DataRow row in dataSet.Tables[0].Rows)
        {
            if (String.IsNullOrEmpty(row["ParentGUID"].ToString()))
            {
                return null;
            }
            return row["ParentGUID"].ToString();
        }
        return string.Empty;
    }
    /// <summary>
    /// 改变禁止允许状态
    /// </summary>
    /// <param name="forbidden"></param>
    /// <returns></returns>
    public string ChangeForbiddenToString(string forbidden)
    {
        if (String.IsNullOrEmpty(forbidden))
            return "未分配";
        return Convert.ToBoolean(forbidden) ? "禁止" : "允许";
    }
    /// <summary>
    /// 下拉框选择变化事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DropDownListGroupName_SelectedIndexChanged(object sender, EventArgs e)
    {
        GroupGuid = DropDownListGroupName.SelectedValue;
        InitUrlDataList();
    }
    /// <summary>
    /// 列表数据绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            var hiddenField = e.Item.FindControl("HiddenFieldGUID") as HiddenField;
            var prev = e.Item.FindControl("LinkButtonPrev") as LinkButton;
            var next = e.Item.FindControl("LinkButtonNext") as LinkButton;
            prev.Visible = !String.IsNullOrEmpty(ParentUrlGuid);
            next.Visible = IsHasChild(hiddenField.Value.Trim());
        }
    }
    /// <summary>
    /// 父页面是否存在子页面
    /// </summary>
    /// <param name="parentGuid"></param>
    /// <returns></returns>
    private bool IsHasChild(string parentGuid)
    {
        WebBean bean = WebBean.GetInstance();
        DataSet dataSet = bean.Select_Url_Data(parentGuid: parentGuid);
        if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0) return false;
        return true;
    }

    /// <summary>
    /// 保存按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        InitUrlDataList();
    }
}