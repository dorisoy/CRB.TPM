using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Core;
using Data.Common.Test;
using Data.Common.Test.Domain.Article;
using Data.Common.Test.Domain.Category;
using Data.Common.Test.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Data.Core.Test
{
    public class DbContextTests
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IDbContext _context;
        protected readonly IArticleRepository _articleRepository;
        protected readonly ICategoryRepository _categoryRepository;
        protected ITestOutputHelper _output;

        public DbContextTests(ITestOutputHelper output)
        {
            _output = output;

            //var connString = "server=localhost;user id=root;password=racing.1;port=3306;persistsecurityinfo=True;database=tpmtest;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;";

            //测试分表
            var connString = "server=localhost;user id=root;password=racing.1;port=3306;persistsecurityinfo=True;database=tpmtest_sharding;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;";

            var services = new ServiceCollection();
            //日志
            services.AddLogging(builder =>
            {
                builder.AddDebug();
            });

            services.AddSingleton<IOperatorResolver, CustomAccountResolver>();

            services
                .AddCRBTPMDb<BlogDbContext, ClientDbContext>()
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
            _context = _serviceProvider.GetService<BlogDbContext>();

            _articleRepository = _serviceProvider.GetService<IArticleRepository>();
            _categoryRepository = _serviceProvider.GetService<ICategoryRepository>();
        }

        [Fact]
        public void RepositoryDescriptorsCountTest()
        {
            Assert.Equal(2, _context.EntityDescriptors.Count);
        }

        [Fact]
        public void AccountResolverTest()
        {
            var accountResolverType = _context.AccountResolver.GetType();
            Assert.Equal(typeof(CustomAccountResolver), accountResolverType);
        }


        /// <summary>
        /// 测试分表，如： ArticleEntity 已经启用分表策略 -> [Sharding(ShardingPolicy.Month)]
        /// </summary>
        [Fact]
        public async void ArticleEntityAddTest()
        {
            var article = new ArticleEntity
            {
                Title = "test",
                Content = "test",
                //该字段为分表字段
                PublishedTime = DateTime.Parse("2022-06-06 00:00:00")
            };

            await _articleRepository.Add(article);

            Assert.True(article.Id > 0);
        }

        /// <summary>
        /// 测试读取分表数据
        /// </summary>
        [Fact]
        public async void ArticleEntityGetTest()
        {
            var data1 = DateTime.Parse("2022-05-06 00:00:00");
            var data2 = DateTime.Parse("2022-06-06 00:00:00");
            var data3 = DateTime.Parse("2022-09-06 00:00:00");

            var articles1 = await _articleRepository.GetArticles(data1);
            var articles2 = await _articleRepository.GetArticles(data2);
            var articles3 = await _articleRepository.GetArticles(data3);

            _output.WriteLine($"获取 {data1} 数据：");
            articles1?.ToList()?.ForEach(s =>
            {
                _output.WriteLine($"{s.Id} --> {s.PublishedTime}");
            });

            _output.WriteLine($"获取 {data2} 数据：");
            articles2?.ToList()?.ForEach(s =>
            {
                _output.WriteLine($"{s.Id} --> {s.PublishedTime}");
            });

            _output.WriteLine($"获取 {data3} 数据：");
            articles3?.ToList()?.ForEach(s =>
            {
                _output.WriteLine($"{s.Id} --> {s.PublishedTime}");
            });

            Assert.Null(articles1);
            Assert.Null(articles2);
            Assert.Null(articles3);
        }
    }
}
