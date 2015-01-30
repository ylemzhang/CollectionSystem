using System;

namespace DataModel
{
    public class UrlDataModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParentGUID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UrlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UrlParams { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UrlName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int UrlIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Boolean? UserAuthentication { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Boolean? Show { get; set; }
    }
}