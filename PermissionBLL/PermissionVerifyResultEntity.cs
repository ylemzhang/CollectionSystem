using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel;

namespace BLL
{
    /// <summary>
    /// 权限验证结果模型。
    /// </summary>
    internal class PermissionVerifyResultEntity : IPermissionVerifyResult
    {
        /// <summary>
        /// 权限验证状态。
        /// </summary>
        private Enum_PermissionVerifyState _permissionVerifyState = Enum_PermissionVerifyState.初始化状态;

        /// <summary>
        /// 权限验证状态。
        /// </summary>
        public Enum_PermissionVerifyState PermissionVerifyState
        {
            get
            {
                return _permissionVerifyState;
            }
            set
            {
                _permissionVerifyState = value;
            }
        }

        /// <summary>
        /// 权限验证提示信息。
        /// </summary>
        private string _permissionVerifyMessage = string.Empty;

        /// <summary>
        /// 权限验证提示信息。
        /// </summary>
        public string PermissionVerifyMessage
        {
            get
            {
                return _permissionVerifyMessage;
            }
            set
            {
                _permissionVerifyMessage = value;
            }
        }
    }
}
