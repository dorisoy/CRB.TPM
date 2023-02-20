using System;
using System.Reflection;
using Z.Dapper.Plus;
using Dapper;
namespace CRB.TPM.Data.Sharding
{
    public class TypeHandlerJsonNet
    {
        public static void Add(Type type)
        {
            TypeHandlerCache.Add(type, () =>
            {
                SqlMapper.AddTypeHandler(type, new JsonNetTypeHandler());
                DapperPlusManager.AddValueConverter(type, new JsonNetTypeHandlerZ());
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
