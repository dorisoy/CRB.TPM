using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Host.Web.Options
{
    /// <summary>
    /// OSSConfig
    /// </summary>
    public class OSSOptions
    {
        /// <summary>
        /// OSS提供器
        /// </summary>
        public OSSProvider Provider { get; set; } = OSSProvider.Local;

        /// <summary>
        /// 雪花云配置
        /// </summary>
        public CRBConfig CRB { get; set; } = new CRBConfig();

        /// <summary>
        /// 华润云配置
        /// </summary>
        public CRCConfig CRC { get; set; } = new CRCConfig();

    }


    /// <summary>
    /// OSS提供器
    /// </summary>
    public enum OSSProvider
    {
        [Description("本地存储")]
        Local,
        /// <summary>
        /// 雪花云
        /// </summary>
        [Description("雪花云")]
        CRB,
        /// <summary>
        /// 华润云
        /// </summary>
        [Description("华润云")]
        CRC
    }


    /// <summary>
    /// 雪花云配置
    /// </summary>
    public class CRBConfig
    {
        /// <summary>
        /// 域名
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// 访问令牌ID
        /// </summary>
        public string AccessKeyId { get; set; }

        /// <summary>
        /// 访问令牌密钥
        /// </summary>
        public string AccessKeySecret { get; set; }

        /// <summary>
        /// 存储空间名称
        /// </summary>
        public string BucketName { get; set; }

        /// <summary>
        /// 自定义域名
        /// </summary>
        public string Domain { get; set; }
    }


    /// <summary>
    /// 华润云配置
    /// </summary>
    public class CRCConfig
    {
        /// <summary>
        /// 域名
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// 访问令牌ID
        /// </summary>
        public string AccessKeyId { get; set; }

        /// <summary>
        /// 访问令牌密钥
        /// </summary>
        public string AccessKeySecret { get; set; }

        /// <summary>
        /// 存储空间名称
        /// </summary>
        public string BucketName { get; set; }

        /// <summary>
        /// 自定义域名
        /// </summary>
        public string Domain { get; set; }
    }
}


