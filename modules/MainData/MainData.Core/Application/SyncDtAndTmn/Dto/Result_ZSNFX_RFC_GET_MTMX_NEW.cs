using System;

namespace CRB.TPM.Mod.MainData.Core.Application.SyncDtAndTmn.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class Result_ZSNFX_RFC_GET_MTMX_TPM
    {
        /// <summary>
        /// 经销商
        /// </summary> 
        public ET_CHANNELS[] ET_CHANNELS { get; set; }

        /// <summary>
        /// 终端
        /// </summary> 
        public ET_TERMINAL[] ET_TERMINAL { get; set; } 

        /// <summary>
        /// 关系
        /// </summary> 
        public ET_RELATION[] ET_RELATION { get; set; }
    }
}
