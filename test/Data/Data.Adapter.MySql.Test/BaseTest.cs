using System;
using System.Threading.Tasks;
using Data.Common.Test;
using Data.Common.Test.Domain.Article;
using Data.Common.Test.Domain.Category;
using Data.Common.Test.Infrastructure;
using Divergic.Logging.Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CRB.TPM.Data.Abstractions;
using Xunit.Abstractions;

using CRB.TPM.Data.Abstractions.Options;
using CRB.TPM.Data.Abstractions.ReadWriteSeparation;
using System.Collections.Generic;
using CRB.TPM.Data.Core.ReadWriteSeparation;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Diagnostics.Metrics;
using System.Numerics;
using CRB.TPM.Data.Core;

namespace Data.Adapter.MySql.Test
{
    public class BaseTest
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IDbContext _dbContext;
        protected readonly IArticleRepository _articleRepository;
        protected readonly ICategoryRepository _categoryRepository;

        protected readonly IReadWriteConnectionStringManager _connectionStringManager;
        protected readonly IReadWriteConnectorFactory _readWriteConnectorFactory;
        protected readonly IReadWriteManager _readWriteManager;

        protected ITestOutputHelper _output;

        //master
        protected readonly string master_db1 = "server=localhost;user id=root;password=racing.1;port=3306;persistsecurityinfo=True;database=tpmtest_write_1;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;";
        protected readonly string master_db2 = "server=localhost;user id=root;password=racing.1;port=3306;persistsecurityinfo=True;database=tpmtest_write_2;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;";

        //slave
        protected readonly string slave_db1 = "server=localhost;user id=root;password=racing.1;port=3306;persistsecurityinfo=True;database=tpmtest_read_1;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;";
        protected readonly string slave_db2 = "server=localhost;user id=root;password=racing.1;port=3306;persistsecurityinfo=True;database=tpmtest_read_2;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;";

        public BaseTest(ITestOutputHelper output)
        {
            _output = output;

            //default
            var connString = "server=localhost;user id=root;password=racing.1;port=3306;persistsecurityinfo=True;database=tpmtest;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;";

            var services = new ServiceCollection();

            //日志
            services.AddLogging(builder =>
            {
                builder.AddXunit(output, new LoggingConfig
                {
                    LogLevel = LogLevel.Trace
                });
            });

            //自定义账户信息解析器
            services.AddSingleton<IOperatorResolver, CustomAccountResolver>();


            //添加数据库
            services.AddCRBTPMDb<BlogDbContext, ClientDbContext>(options =>
            {
                //开启日志
                options.Log = true;

                //启用读写分离,当开启全局读写分离时，默认connString 将被主节点任意数据源接管
                options.UseReadWriteSeparation = true;

                //读写分离配置项方式1
                //options.ReadWriteSeparationOptions = new ReadWriteSeparationOptions
                //{
                //    DefaultEnable = true,
                //    DefaultPriority = 10,
                //    ReadConnStringGetStrategy = ReadConnStringGetStrategyEnum.LatestFirstTime,
                //    ReadStrategy = ReadStrategyEnum.Loop,
                //    Master = new Node[]  { new()  { Name = "master-db1", ConnectionString = master_db1 } },
                //    Slave = new Node[] { new()  { Name = "slave-db1", ConnectionString = slave_db1 }, new()  { Name = "slave-db2", ConnectionString = slave_db2 } }
                //};

                //读写分离配置项方式2
                options.UseReadWriteSeparation(cfg =>
                {
                    cfg.DefaultEnable = true;
                    cfg.DefaultPriority = 10;
                    cfg.ReadConnStringGetStrategy = ReadConnStringGetStrategyEnum.LatestFirstTime;
                    cfg.ReadStrategy = ReadStrategyEnum.Loop;
                    cfg.Master = new Node[] { new() { Name = "master-db1", ConnectionString = master_db1 } };
                    cfg.Slave = new Node[] { new() { Name = "slave-db1", ConnectionString = slave_db1 }, new() { Name = "slave-db2", ConnectionString = slave_db2 } };
                });
            })
            .UseMySql(connString)
            .AddRepositoriesFromAssembly(typeof(BlogDbContext).Assembly)
            //开启代码优先
            .AddCodeFirst(options =>
            {
                //创建库
                options.CreateDatabase = true;

                //更新列
                options.UpdateColumn = true;

                options.BeforeCreateDatabase = ctx =>
                {
                    ctx.Logger.Write("BeforeCreateDatabase", "数据库创建前事件");
                };
                options.AfterCreateDatabase = ctx =>
                {
                    ctx.Logger.Write("AfterCreateDatabase", "数据库创建后事件");
                };
                options.BeforeCreateTable = (ctx, entityDescriptor) =>
                {
                    ctx.Logger.Write("BeforeCreateTable", "表创建前事件，表名称：" + entityDescriptor.TableName);
                };
                options.AfterCreateTable = (ctx, entityDescriptor) =>
                {
                    ctx.Logger.Write("AfterCreateTable", "表创建后事件，表名称：" + entityDescriptor.TableName);
                };
            })
            .Build();

            _serviceProvider = services.BuildServiceProvider();
            _dbContext = _serviceProvider.GetService<BlogDbContext>();
            _articleRepository = _serviceProvider.GetService<IArticleRepository>();
            _categoryRepository = _serviceProvider.GetService<ICategoryRepository>();

            //手动ReadWrite
            _readWriteConnectorFactory = _serviceProvider.GetService<IReadWriteConnectorFactory>();
            _readWriteManager= _serviceProvider.GetService<IReadWriteManager>();
            _connectionStringManager = new ReadWriteConnectionStringManager(_dbContext.Options, _readWriteConnectorFactory);
           
        }

        protected async Task ClearTable()
        {
            await _articleRepository.Execute("truncate article;");
            await _articleRepository.Execute("truncate mycategory;");
        }
    }
}
