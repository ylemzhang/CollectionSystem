using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// url权限配置实体。
    /// </summary>
    internal class UrlConfigEntity
    {
        public string GUID { get; set; }
        public string ParentGUID { get; set; }
        public string Url { get; set; }
        public string UrlCode { get; set; }
        public string UrlParams { get; set; }
        public string UrlName { get; set; }
        public int UrlIndex { get; set; }
        public bool Forbidden { get; set; }
        public int PriorityLevel { get; set; }
    }

    /// <summary>
    /// 权限配置唯一性筛选器。
    /// </summary>
    internal class UrlConfigEntityComparer : IEqualityComparer<UrlConfigEntity>
    {
        public bool Equals(UrlConfigEntity x, UrlConfigEntity y)
        {
            if (x.GUID.Equals(y.GUID) && x.Forbidden.Equals(y.Forbidden) && x.PriorityLevel.Equals(y.PriorityLevel))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(UrlConfigEntity obj)
        {
            return obj.GUID.GetHashCode();
        }
    }
}
