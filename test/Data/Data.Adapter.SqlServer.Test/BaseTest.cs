using System;
using System.Threading.Tasks;
using Data.Common.Test;
using Data.Common.Test.Domain.Article;
using Data.Common.Test.Domain.Category;
using Data.Common.Test.Infrastructure;
using Divergic.Logging.Xunit;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CRB.TPM.Data.Abstractions;
using Xunit.Abstractions;
using CRB.TPM.Data.Core;

namespace Data.Adapter.SqlServer.Test
{
    public class BaseTest
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IDbContext _dbContext;
        protected readonly IArticleRepository _articleRepository;
        protected readonly ICategoryRepository _categoryRepository;

        public BaseTest(ITestOutputHelper output)
        {
  
            var connString = new SqlConnectionStringBuilder
            {
                DataSource = "DESKTOP-N3DG9IC",
                UserID = "sa",
                Password = "racing.1",
                MultipleActiveResultSets = true,
                InitialCatalog = "TPM_Test",
                TrustServerCertificate = true
            }.ToString();

            var services = new ServiceCollection();
            //��־
            services.AddLogging(builder =>
            {
                builder.AddXunit(output, new LoggingConfig
                {
                    LogLevel = LogLevel.Trace
                });
            });

            //�Զ����˻���Ϣ������
            services.AddSingleton<IOperatorResolver, CustomAccountResolver>();

            services
                .AddCRBTPMDb<BlogDbContext, ClientDbContext>(options =>
                {
                     //������־
                     options.Log = true;

                    //���ö�д����,������ȫ�ֶ�д����ʱ��Ĭ��connString �������ڵ���������Դ�ӹ�
                    options.UseReadWriteSeparation = false;
                })
                .UseSqlServer(connString)
                .AddRepositoriesFromAssembly(typeof(BlogDbContext).Assembly)
                .AddCodeFirst(options =>
                {
                    options.EnableShardingPolicy = false;
                    options.CreateDatabase = true;
                    options.UpdateColumn = true;
                    options.BeforeCreateDatabase = ctx =>
                    {
                        ctx.Logger.Write("BeforeCreateDatabase", "���ݿⴴ��ǰ�¼�");
                    };
                    options.AfterCreateDatabase = ctx =>
                    {
                        ctx.Logger.Write("AfterCreateDatabase", "���ݿⴴ�����¼�");
                    };
                    options.BeforeCreateTable = (ctx, entityDescriptor) =>
                    {
                        ctx.Logger.Write("BeforeCreateTable", "���ݿⴴ��ǰ�¼��������ƣ�" + entityDescriptor.TableName);
                    };
                    options.AfterCreateTable = (ctx, entityDescriptor) =>
                    {
                        ctx.Logger.Write("AfterCreateTable", "���ݿⴴ�����¼��������ƣ�" + entityDescriptor.TableName);
                    };
                })
                .Build();

            _serviceProvider = services.BuildServiceProvider();
            _dbContext = _serviceProvider.GetService<BlogDbContext>();
            _articleRepository = _serviceProvider.GetService<IArticleRepository>();
            _categoryRepository = _serviceProvider.GetService<ICategoryRepository>();
        }

        protected async Task ClearTable()
        {
            await _articleRepository.Execute("truncate table article;");
            await _articleRepository.Execute("truncate table mycategory;");
        }
    }
}
