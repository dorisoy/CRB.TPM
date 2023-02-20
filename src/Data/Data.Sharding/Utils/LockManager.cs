using System.Collections.Generic;

namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// 用于表示锁管理器
    /// </summary>
    public class LockManager
    {
        private readonly object _lock = new object();

        private readonly Dictionary<string, object> dict = new Dictionary<string, object>();

        public object GetObject(string name)
        {
            var exists = dict.TryGetValue(name, out var val);
            if (!exists)
            {
                lock (_lock)
                {
                    if (!dict.ContainsKey(name))
                    {
                        dict.Add(name, new object());
                    }
                }
                val = dict[name];
            }  
            return val;
        }
    }
}
