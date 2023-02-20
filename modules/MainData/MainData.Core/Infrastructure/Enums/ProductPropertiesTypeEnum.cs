using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Enums
{
    /// <summary>
    /// 产品属性
    /// </summary>
    public enum ProductPropertiesTypeEnum
    {
        [Description("档次")]
        Grade = 1,
        [Description("系列")]
        Series = 2,
        [Description("内包装")]
        InnerPackaging = 3,
        [Description("外包装")]
        OuterPackaging = 4,
    }
}
