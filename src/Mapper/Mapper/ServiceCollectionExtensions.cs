using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using CRB.TPM.Mapper;
using CRB.TPM.Module.Abstractions;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Data.Abstractions.Annotations;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加对象映射
    /// </summary>
    /// <param name="services"></param>
    /// <param name="modules">模块集合</param>
    /// <returns></returns>
    public static IServiceCollection AddMappers(this IServiceCollection services, IModuleCollection modules)
    {
        var config = new MapperConfiguration(cfg =>
        {
            foreach (var module in modules)
            {
                //{CRB.TPM.Mod.Admin.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
                //TPM.Mod.TP.Core
                var types = module.LayerAssemblies.Core.GetTypes();

                foreach (var type in types)
                {
                    var map = (ObjectMapAttribute)Attribute.GetCustomAttribute(type, typeof(ObjectMapAttribute));
                    if (map != null)
                    {

                        //cfg.CreateMap(type, map.TargetType);

                        //if (map.TwoWay)
                        //{
                        //    cfg.CreateMap(map.TargetType, type);
                        //}

                        var expression = cfg.CreateMap(type, map.TargetType);
                        var sourceType = type;
                        foreach (var property in sourceType.GetProperties())
                        {
                            var descriptor = TypeDescriptor.GetProperties(sourceType)[property.Name];
                            var attribute = (NotMappingColumnAttribute)descriptor.Attributes[typeof(NotMappingColumnAttribute)];
                            if (attribute != null)
                                expression.ForMember(property.Name, opt => opt.Ignore());
                        }

                        if (map.TwoWay)
                        {
                            expression = cfg.CreateMap(map.TargetType, type);
                            foreach (var property in sourceType.GetProperties())
                            {
                                var descriptor = TypeDescriptor.GetProperties(sourceType)[property.Name];
                                var attribute = (NotMappingColumnAttribute)descriptor.Attributes[typeof(NotMappingColumnAttribute)];
                                if (attribute != null)
                                    expression.ForMember(property.Name, opt => opt.Ignore());
                            }
                        }
              
                    }
                }
            }
        });

        services.AddSingleton(config.CreateMapper());
        services.AddSingleton<CRB.TPM.Utils.Map.IMapper, DefaultMapper>();

        return services;
    }


}



//public static class IgnoreReadOnlyExtensions
//{
//    public static IMappingExpression IgnoreReadOnly(this IMappingExpression expression)
//    {
//        var sourceType = typeof(TSource);
//        foreach (var property in sourceType.GetProperties())
//        {
//            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(sourceType)[property.Name];
//            ReadOnlyAttribute attribute = (ReadOnlyAttribute)descriptor.Attributes[typeof(ReadOnlyAttribute)];

//            if (attribute.IsReadOnly == true) 
//                expression.ForMember(property.Name, opt => opt.Ignore());
//        }
//        return expression;
//    }
//}


//public static class ClaimMappingExtensions
//{
//    public static IMappingExpression IgnoreAllButMembersWithDataMemberAttribute(this IMappingExpression expression)
//    {
//        var sourceType = typeof(TSource);
//        var destinationType = typeof(TDestination);
//        foreach (var property in sourceType.GetProperties())
//        {
//            var descriptor = TypeDescriptor.GetProperties(sourceType)[property.Name];
//            var hasDataMemberAttribute = descriptor.Attributes.OfType().Any();
//            if (!hasDataMemberAttribute) 
//                expression.ForSourceMember(property.Name, opt => opt.Ignore());
//        }
//        foreach (var property in destinationType.GetProperties())
//        {
//            var descriptor = TypeDescriptor.GetProperties(destinationType)[property.Name];
//            var hasDataMemberAttribute = descriptor.Attributes.OfType().Any();
//            if (!hasDataMemberAttribute) 
//                expression.ForMember(property.Name, opt => opt.Ignore());
//        }
//        return expression;
//    }
//}

