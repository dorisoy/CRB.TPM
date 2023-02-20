using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRB.TPM.Utils.Enums;

namespace CRB.TPM.Utils.File
{
    /// <summary>
    /// 文件对象
    /// </summary>
    public class FileObject
    {
        /// <summary>
        /// 模块编码
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 访问方式
        /// </summary>
        public FileAccessMode AccessMode { get; set; }

        /// <summary>
        /// 文件本地物理路径
        /// </summary>
        public string PhysicalPath { get; set; }

        /// <summary>
        /// 文件信息
        /// </summary>
        public FileDescriptor FileInfo { get; set; }
    }
}
