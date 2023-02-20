using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Models;
using CRB.TPM.Utils.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace CRB.TPM.Host.Web.Swagger.Filters
{
    /// <summary>
    /// 过滤输出参数中的忽略属性
    /// </summary>
    public class SwaggerIgnoreSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
            {
                return;
            }

            var ignoreDataMemberProperties = context.Type.GetProperties().Where(t => t.GetCustomAttribute<SwaggerIgnoreAttribute>() != null);

            foreach (var ignoreDataMemberProperty in ignoreDataMemberProperties)
            {
                var propertyToHide = schema.Properties.Keys.SingleOrDefault(x => x.ToLower() == ignoreDataMemberProperty.Name.ToLower());

                if (propertyToHide != null)
                {
                    schema.Properties.Remove(propertyToHide);
                }
            }
        }
    }

    /// <summary>
    /// 移除Schemas
    /// </summary>
    public class SwaggerRemoveSchemasFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {

            IDictionary<string, OpenApiSchema> _remove = swaggerDoc.Components.Schemas;
            foreach (KeyValuePair<string, OpenApiSchema> _item in _remove)
            {
                swaggerDoc.Components.Schemas.Remove(_item.Key);
            }
        }
    }
}
