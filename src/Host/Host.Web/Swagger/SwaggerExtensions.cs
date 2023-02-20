using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using CRB.TPM.Host.Web.Swagger.Filters;
using CRB.TPM.Module.Abstractions;
using HostOptions = CRB.TPM.Host.Web.Options.HostOptions;
using NJsonSchema.Generation;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;

namespace CRB.TPM.Host.Web.Swagger;

public static class SwaggerExtensions
{
    /// <summary>
    /// 添加Swagger
    /// </summary>
    /// <param name="services"></param>
    /// <param name="modules">模块集合</param>
    /// <param name="hostOptions"></param>
    /// <param name="environment"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services, IModuleCollection modules, HostOptions hostOptions, IHostEnvironment environment)
    {
        if (Check(modules, hostOptions, environment))
        {
            // 注册 swagger 服务
            if (modules != null)
            {
                foreach (var module in modules)
                {
                    services.AddOpenApiDocument(c =>
                    {
                        c.DocumentName = $"{module.Name}({module.Code})";
                        c.Title = $"{module.Name}({module.Code})";
                        c.Description = module.Description;
                        c.Version = module.Version;
                        c.DefaultReferenceTypeNullHandling = ReferenceTypeNullHandling.NotNull;

                        c.PostProcess = doc =>
                        {
                            doc.Info.Version = module.Version;
                            doc.Info.Title = $"{module.Name}({module.Code})";
                            doc.Info.Description = module.Description;
                            doc.Info.TermsOfService = "None";
                        };

                        c.ApiGroupNames = new[] { module.Code.ToLower() };

                        #region APIKEY

                        c.AddSecurity(
                            "ApiKey",
                            new NSwag.OpenApiSecurityScheme
                            {
                                Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
                                Name = "ApiKey",
                                In = NSwag.OpenApiSecurityApiKeyLocation.Header,
                                Description = "ApiKey必须携带在请求头中"
                            });

                        c.OperationProcessors.Add(new OperationSecurityScopeProcessor("ApiKey"));

                        #endregion


                        #region JWT
     
                        //添加安全声明
                        c.AddSecurity("Bearer", new NSwag.OpenApiSecurityScheme
                        {
                            Description = "JWT认证请求头格式: \"Authorization: Bearer {token}\"",
                            Name = "Authorization",
                            In = NSwag.OpenApiSecurityApiKeyLocation.Header,
                            Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
                            Scheme = "Bearer"
                        });

                        #endregion

                    });
                }
            }
            services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();
            
            services.AddSwaggerGen(c =>
            {
                /*
                if (modules != null)
                {
                    foreach (var module in modules)
                    {
                        c.SwaggerDoc(module.Code.ToLower(), new OpenApiInfo
                        {
                            Title = $"{module.Name}({module.Code})",
                            Version = module.Version,
                            Description = module.Description
                        });


                        //加载xml文档
                        //\\modules\\WebHost\\bin\\Debug\\net6.0\\CRB.TPM.Mod.TP.Web.xml"
                        var xmlPath = module.LayerAssemblies.Web.Location.Replace(".dll", ".xml", StringComparison.OrdinalIgnoreCase);
                        if (File.Exists(xmlPath))
                        {
                            c.IncludeXmlComments(xmlPath);
                        }
                    }
                }

                #region JWT
                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT认证请求头格式: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                };
                //添加安全声明
                c.AddSecurityDefinition("Bearer", securityScheme);
                //添加Jwt验证设置
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
                #endregion

                #region APIKEY

                //c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                //{
                //    Description = "ApiKey必须携带在请求头中",
                //    Name = "TPM.ApiKey",
                //    Type = SecuritySchemeType.ApiKey,
                //    In = ParameterLocation.Header,
                //    Scheme = "ApiKeyScheme"
                //});
                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme()
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "ApiKey"
                //            },
                //            In = ParameterLocation.Header
                //        },
                //        new List<string>() }
                //});

                #endregion

                */

                //启用注解
                c.EnableAnnotations();

                //隐藏属性
                c.SchemaFilter<SwaggerIgnoreSchemaFilter>();
                c.OperationFilter<SwaggerIgnoreOperationFilter>();
            });
            
        }

        return services;
    }

    /// <summary>
    /// 启用Swagger
    /// </summary>
    public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IModuleCollection modules, HostOptions hostOptions, IHostEnvironment environment)
    {
        //手动开启或者开发模式下才会启用swagger功能
        if (Check(modules, hostOptions, environment))
        {
            /*
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                if (modules == null) return;

                foreach (var module in modules)
                {
                    var code = module.Code.ToLower();
                    var url = $"{code}/swagger.json";

                    c.SwaggerEndpoint(url, $"{module.Name}({module.Code})");
                }
            });
            */

            //添加swagger生成api文档（默认路由文档 /swagger/v1/swagger.json）
            app.UseOpenApi(c =>
            {
                c.Path = "/swagger/{documentName}/swagger.json";
            });
            //添加Swagger UI到请求管道中(默认路由: /swagger).
            app.UseSwaggerUi3(c =>
            {
                c.DocumentPath = "/swagger/{documentName}/swagger.json";
            });
        }

        return app;
    }

    /// <summary>
    /// 检测是否开启Swagger
    /// </summary>
    private static bool Check(IModuleCollection modules, HostOptions hostOptions, IHostEnvironment environment)
    {
        //手动开启或者开发模式以及本地模式下才会启用swagger功能
        return hostOptions.Swagger || !environment.IsProduction() || environment.EnvironmentName.Equals("Local");
    }
}