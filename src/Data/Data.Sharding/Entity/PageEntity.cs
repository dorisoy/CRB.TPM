using System.Collections.Generic;

namespace CRB.TPM.Data.Sharding
{
    public class PageEntity<T>
    {
        public long Count { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
