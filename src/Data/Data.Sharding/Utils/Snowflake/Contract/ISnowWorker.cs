using System;

namespace CRB.TPM.Data.Sharding
{
    internal interface ISnowWorker
    {
        Action<OverCostActionArg> GenAction { get; set; }

        long NextId();
    }
}
