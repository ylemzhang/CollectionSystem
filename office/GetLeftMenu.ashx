<%@ WebHandler Language="C#" Class="GetLeftMenu" %>

using System;
using System.Web;
using System.Data;

/// <summary>
/// 主页面--左菜单--三级菜单
/// </summary>
public class GetLeftMenu : IHttpHandler {

    string res = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string action = context.Request.Form["action"];
        res = string.Empty;
        if (!string.IsNullOrEmpty(action))
        {
            if (action.Equals("getThreeMenu"))
            {
                //GetThreeMenu("","");
                context.Response.Write(res);
                return;
            }
        }
        context.Response.Flush();
    }

    ///// <summary>
    ///// 获取用户菜单-三级菜单
    ///// </summary>
    ///// <param name="secondCode"></param>
    ///// <param name="threeCode"></param>
    ///// <returns></returns>
    //private static DataSet GetUserThreeMenu(string secondCode, string threeCode)
    //{
    //    DataSet ds = WebAccess.GetInstance().GetUserChildeUrl(secondCode, WebBeanUse.GetInstance().Account);//得到二级菜单
    //    DataSet dsChildren = new DataSet();
    //    DataRow dr;
    //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    {
    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            dr = ds.Tables[0].Rows[i];
    //            if (threeCode.Equals(dr["UrlCode"].ToString().Trim()))//三级菜单
    //            {
    //                dsChildren = dr["UrlChildren"] as DataSet;
    //                break;
    //            }
    //        }
    //    }
    //    return dsChildren;
    //}

    ///// <summary>
    ///// 得到三级权限菜单
    ///// </summary>
    ///// <returns></returns>
    //public static string GetThreeMenu(string secondCode, string threeCode)
    //{
    //    System.Text.StringBuilder sbmenu = new System.Text.StringBuilder();
    //    DataSet ds = GetUserThreeMenu(secondCode, threeCode);//得到三级菜单
    //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    {
    //        System.Data.DataRow dr;
    //        string htmLeft = " <li pid='{0}' class='w2' name='menuForum{1}' id='menuForum{1}' style='display: none;'>"
    //                            + "<a  href='javascript:showUrl('{2}')'>{3}</a></li>";//pid menuForumid url 内容
    //        string url = string.Empty;
    //        string urlParams = string.Empty;
    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            dr = ds.Tables[0].Rows[i];
    //            url = dr["Url"].ToString();
    //            string displayMenu = string.Empty;
    //            if (!string.Empty.Equals(dr["UrlParams"].ToString()))
    //            {
    //                //取 &displayMenu=98
    //                urlParams = dr["UrlParams"].ToString();
    //                string[] arr = urlParams.Split('&');
    //                foreach (var param in arr)
    //                {
    //                    if (param.IndexOf("displayMenu") == 0)
    //                    {
    //                        displayMenu = param.Substring(param.IndexOf("=") + 1);
    //                        break;
    //                    }
    //                }
    //                url += "?" + dr["UrlParams"].ToString();
    //            }
    //            sbmenu.AppendFormat(htmLeft, url, dr["UrlName"].ToString(), displayMenu);
    //        }
    //    }
    //    return sbmenu.ToString();
    //}
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}