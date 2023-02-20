using CRB.TPM.Excel.Abstractions.Export;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminal.Dto
{
    public class MTerminalQueryExportDto : MTerminalQueryDto
    {
        #region ExcelExport 导出信息

        /// <summary>
        /// 导出信息
        /// </summary>
        public ExcelExportEntityModel<MTerminalEntity> ExportModel { get; set; } = new ExcelExportEntityModel<MTerminalEntity>();

        /// <summary>
        /// 导出数量
        /// </summary>
        public long ExportCount { get; set; }

        /// <summary>
        /// 导出数量限制
        /// </summary>
        public virtual int ExportCountLimit => 50000;

        /// <summary>
        /// 是否超出导出数量限制
        /// </summary>
        public bool IsOutOfExportCountLimit => ExportCount > ExportCountLimit;


        /// <summary>
        /// 是否是导出操作
        /// </summary>
        public bool IsExport => ExportModel != null;

        /// <summary>
        /// 查询数量
        /// </summary>
        public bool QueryCount { get; set; } = true;

        #endregion
    }
}
