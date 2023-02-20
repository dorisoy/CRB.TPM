using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Data.Common.Test;
using Data.Common.Test.Infrastructure;
using Data.Common.Test.Service;
using CRB.TPM.Data.Abstractions;
using System.Linq;
using CRB.TPM.Data.Core;

namespace Data.Web.Test
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //自定义账户信息解析器
            services.AddSingleton<IOperatorResolver, CustomAccountResolver>();

            //按照约定，应用服务必须采用Service结尾
            var assembly = typeof(BlogDbContext).Assembly;
            var implementationTypes = assembly.GetTypes().Where(m => m.Name.EndsWith("Service") && !m.IsInterface).ToList();
            foreach (var implType in implementationTypes)
            {
                //按照约定，服务的第一个接口类型就是所需的应用服务接口
                var serviceType = implType.GetInterfaces()[0];
                services.AddScoped(implType);
            }

            AddMySql(services);

            services.AddControllers();
        }

        void AddMySql(IServiceCollection services)
        {
            var connString = "server=localhost;user id=root;password=racing.1;port=3306;persistsecurityinfo=True;database=tpmtest;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;";

            services
                .AddCRBTPMDb<BlogDbContext, ClientDbContext>(options =>
                {
                    //开启日志
                    options.Log = true;
                    options.UseClientMode = true;
                })
                .UseMySql(connString)
                .AddRepositoriesFromAssembly(typeof(BlogDbContext).Assembly)
                //添加事务特性
                .AddTransactionAttribute(typeof(IArticleService), typeof(ArticleService))
                .Build();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
