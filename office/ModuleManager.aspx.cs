using System;
using System.Data;
using System.Web.UI;
using DataModel;

public partial class ModuleManager : Page
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
    private void InitPage()
    {
        DataBind();
    }
    /// <summary>
    /// 数据绑定到列表
    /// </summary>
    private void DataBind()
    {
        WebBean bean = WebBean.GetInstance();

        bool? authentication = null;
        switch (DropDownListAuthentication.SelectedValue)
        {
            case "1":
                authentication = true;
                break;
            case "2":
                authentication = false;
                break;
        }
        bool? show = null;
        switch (DropDownListShow.SelectedValue)
        {
            case "1":
                show = true;
                break;
            case "2":
                show = false;
                break;
        }
        UrlDataModel model=new UrlDataModel
                               {
                                   GUID = string.Empty,
                                   Url=TextBoxUrl.Text.Trim(),
                                   UrlName = TextBoxUrlName.Text.Trim(),
                                   UserAuthentication = authentication,
                                   Show = show
                               };
        DataSet dataSet = bean.SelectUrlData(model);
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
    /// 行点击事件
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void Repeater1_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
    {
        if (e.CommandName != "delete") return;
        try
        {
            if (e.CommandArgument == null) return;
            string guid = e.CommandArgument.ToString();
            WebBean bean = WebBean.GetInstance();
            bean.DeleteUrl(" where GUID='" + guid + "'");
            DataBind();
        }
        catch (Exception ex)
        {

        }
    }
}