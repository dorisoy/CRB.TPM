using System;

namespace CRB.TPM.Data.Sharding
{
    internal interface IIdGenerator
    {
        /// <summary>
        /// 生成过程中产生的事件
        /// </summary>
        Action<OverCostActionArg> GenIdActionAsync { get; set; }

        /// <summary>
        /// 生成新的long型Id
        /// </summary>
        /// <returns></returns>
        long NewLong();

    }
}
