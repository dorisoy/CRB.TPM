
using CRB.TPM.Data.Abstractions.Annotations;

namespace CRB.TPM.Mod.MainData.Core.Domain.MTerminal
{
    public partial class MTerminalEntity
    {
        /// <summary>
        /// ����վ����
        /// </summary>
        [NotMappingColumn]
        public string StationOrgCD { get; set; }
    }
}
