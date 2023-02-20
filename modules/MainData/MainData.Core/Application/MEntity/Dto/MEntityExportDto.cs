using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Mod.MainData.Core.Domain.MEntity;
using CRB.TPM.Utils.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.MEntity.Dto
{
    public class MEntityExportDto : MEntityEntity
    {
        /// <summary>
        /// 状态
        /// </summary>
        public string EnabledNm { get { return Enabled == 1 ? "有效" : "无效";  } }

    }
}
