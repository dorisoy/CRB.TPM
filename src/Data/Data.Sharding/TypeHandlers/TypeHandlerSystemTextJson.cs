#if CORE
using System;
using System.Reflection;
using Z.Dapper.Plus;
using Dapper;

namespace CRB.TPM.Data.Sharding
{
    public class TypeHandlerSystemTextJson
    {
        public static void Add(Type type)
        {
            TypeHandlerCache.Add(type, () =>
            {
                SqlMapper.AddTypeHandler(type, new SystemTextJsonTypeHandler());
                DapperPlusManager.AddValueConverter(type, new SystemTextJsonTypeHandlerZ());
            });

        }

        public static void Add(Assembly assembly)
        {
            TypeHandlerCache.GetJsonPropertyType(assembly, (type) =>
            {
                Add(type);
            });
        }
    }
}
#endif