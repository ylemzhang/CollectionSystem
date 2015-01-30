using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///WebAccess 的摘要说明
/// </summary>
public class WebAccess
{
    private WebAccess()
    {

    }

    public static BLL.BLLEntity GetInstance()
    {
        return BLL.BLLEntity.GetInstance();
    }
}