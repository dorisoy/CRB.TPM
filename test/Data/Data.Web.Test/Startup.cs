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
            //�Զ����˻���Ϣ������
            services.AddSingleton<IOperatorResolver, CustomAccountResolver>();

            //����Լ����Ӧ�÷���������Service��β
            var assembly = typeof(BlogDbContext).Assembly;
            var implementationTypes = assembly.GetTypes().Where(m => m.Name.EndsWith("Service") && !m.IsInterface).ToList();
            foreach (var implType in implementationTypes)
            {
                //����Լ��������ĵ�һ���ӿ����;��������Ӧ�÷���ӿ�
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
                    //������־
                    options.Log = true;
                    options.UseClientMode = true;
                })
                .UseMySql(connString)
                .AddRepositoriesFromAssembly(typeof(BlogDbContext).Assembly)
                //�����������
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
