using CRB.TPM.Config.Abstractions;
using CRB.TPM.Utils.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Config.Core
{
    public class ConfigCollection : CollectionAbstract<ConfigDescriptor>, IConfigCollection
    {
    }
}
