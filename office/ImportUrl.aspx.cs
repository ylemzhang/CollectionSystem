using System;
using System.IO;
using System.Text;
using System.Web.UI;

public partial class ImportUrl : Page
{
    public StringBuilder DivContent { get; set; }
    public StringBuilder SqlValues { get; set; }
    /// <summary>
    /// 索引最大值
    /// </summary>
    public int MaxIndex { 
        get
        {
            return ViewState["MaxIndex"] == null ? 1 : Convert.ToInt32(ViewState["MaxIndex"].ToString());
        }
        set { ViewState["MaxIndex"] = value; }
    }
    /// <summary>
    /// 页面载入事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            MaxIndex = GetMaxIndex();
        }
    }
    /// <summary>
    /// 获取索引最大值
    /// </summary>
    /// <returns></returns>
    private int GetMaxIndex()
    {
        WebBean bean = WebBean.GetInstance();
        return bean.GetMaxUrlIndex()+1;
    }
    /// <summary>
    /// 递归遍历指定目录下的目录和文件生成树
    /// </summary>
    /// <param name="path">指定目录路径</param>
    private void CreateDirectoryFiles(string path)
    {
        DirectoryInfo directoryInfo = Directory.CreateDirectory(path);
        foreach (FileInfo fileInfo in directoryInfo.EnumerateFiles())
        {
            switch (fileInfo.Attributes)
            {
                case FileAttributes.Archive:
                case FileAttributes.Normal:
                    if (fileInfo.Extension.Equals(".aspx"))
                    {
                        string url = fileInfo.FullName.Replace(TextBoxWebDirectory.Text.Trim(), "~").Replace("\\", "/");
                        DivContent.Append(url);
                        AppendValuesString(url, fileInfo.Name.Replace(".aspx", ""));
                    }
                    break;
                default:
                    break;
            }
        }
        if (directoryInfo.GetDirectories().Length <= 0) return;
        foreach (DirectoryInfo directory in directoryInfo.EnumerateDirectories())
        {
            switch (directory.Attributes)
            {
                case FileAttributes.Archive:
                case FileAttributes.Normal:
                case FileAttributes.Directory:
                    CreateDirectoryFiles(directory.FullName);
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// 导入按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonImport_Click(object sender, EventArgs e)
    {
        WebBean bean = WebBean.GetInstance();
        try
        {
            if(RadioButtonCover.Checked)
                bean.DeleteAllUrl();
            DivContent = new StringBuilder();
            SqlValues = new StringBuilder();
            CreateDirectoryFiles(TextBoxImportDirectory.Text.Trim());
            bean.ImportUrl(SqlValues.ToString());
            //tempdiv.InnerText = divContent.ToString();
            Show(Page, "导入成功！");
        }
        catch (Exception exception)
        {
            WriteLog.WriteExceptionLog(exception.ToString());
            Show(Page, "导入失败！");
        }
    }
    /// <summary>
    /// 添加sql语句的Value部分
    /// </summary>
    /// <param name="url"></param>
    private void AppendValuesString(string url,string name)
    {
        if (!String.IsNullOrEmpty(SqlValues.ToString()))
        {
            SqlValues.Append(" union ");
        }

        SqlValues.Append(" select NEWID(),'" + url + "', '" + name + "', '', ''," + MaxIndex + "," + DropDownListAuthentication.SelectedValue + ", " + DropDownListShow.SelectedValue);
        MaxIndex++;
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