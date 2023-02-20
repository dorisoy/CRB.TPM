using FastMember;
using System.Collections.Generic;
using System.Linq;

namespace CRB.TPM.Data.Sharding
{
    internal class RangeSharding<T> : ISharding<T> where T : class
    {
        private Dictionary<long, ITable<T>> _dict;
        private IEnumerable<long> _rangeList;

        public RangeSharding(Dictionary<long, ITable<T>> dict) : base(dict.Values.ToArray())
        {
            _dict = dict;
            _rangeList = dict.Keys.AsEnumerable().OrderBy(b => b).AsEnumerable();
        }

        public override ITable<T> GetTableById(object id)
        {
            var range = _rangeList.FirstOrDefault(f => (long)id <= f);
            return _dict[range];
        }

        public override ITable<T> GetTableByModel(T model)
        {
            var accessor = TypeAccessor.Create(typeof(T));
            var id = (long)accessor[model, SqlField.PrimaryKey];
            var range = _rangeList.First(f => id <= f);
            return _dict[range];
        }
    }
}
