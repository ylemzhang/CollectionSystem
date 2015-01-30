<%@ WebHandler Language="C#" Class="SetCopy" %>

using System;
using System.Web;

public class SetCopy : IHttpHandler {

    string res = string.Empty;
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string action = context.Request.Form["action"];
        res = string.Empty;
        if (!string.IsNullOrEmpty(action))
        {
            if (action.Equals("setCopy"))
            {
                SetPageCopy();
                context.Response.Write(res);
                return;
            }                        
        }
        context.Response.Flush(); 
    }

    /// <summary>
    /// 设置copy
    /// </summary>
    public void SetPageCopy()
    {
        PageBase.AllowCopy = !PageBase.AllowCopy;
        res = "ok";
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}