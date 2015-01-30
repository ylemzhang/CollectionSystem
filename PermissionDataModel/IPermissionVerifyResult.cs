using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel
{
    public interface IPermissionVerifyResult
    {
        /// <summary>
        /// 权限验证状态。
        /// </summary>
        Enum_PermissionVerifyState PermissionVerifyState { get; }

        /// <summary>
        /// 权限验证提示信息。
        /// </summary>
        string PermissionVerifyMessage { get; }
    }
}
