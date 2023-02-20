using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Mod.MainData.Core.Domain.MProductProperty;
using CRB.TPM.Mod.MainData.Core.Infrastructure.Enums;
using CRB.TPM.Utils.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.MProductProperty.Vo
{
    [ObjectMap(typeof(MProductPropertyEntity), true)]
    public class MProductPropertyQueryVo
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int ProductPropertiesType { get; set; }

        /// <summary>
        /// 产品类型名称
        /// </summary>
        public string ProductPropertiesTypeName
        {
            get
            {
               return Enum.IsDefined(typeof(ProductPropertiesTypeEnum), this.ProductPropertiesType) ? 
                 ((ProductPropertiesTypeEnum)this.ProductPropertiesType).ToDescription() : String.Empty;
            }
        }

        /// <summary>
        /// 产品属性编码
        /// </summary>
        public string ProductPropertiesCode { get; set; } = string.Empty;

        /// <summary>
        /// 产品属性名称
        /// </summary>
        public string ProductPropertiesName { get; set; } = string.Empty;

        /// <summary>
        ///  排序
        /// </summary>
        public int Sort { get; set; }
    }
}
