using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Common.Test.Domain.Article;
using Microsoft.Extensions.DependencyInjection;
using CRB.TPM.Data.Abstractions.Extensions;
using CRB.TPM.Data.Abstractions.Pagination;
using Xunit;
using Xunit.Abstractions;
using CRB.TPM.Data.Core.ReadWriteSeparation;
using CRB.TPM.Data.Abstractions.ReadWriteSeparation;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using CRB.TPM.Data.Abstractions.Annotations;
using System;
using System.Diagnostics.Metrics;
using System.Numerics;
using Microsoft.Extensions.Logging;
using System.Drawing;

namespace Data.Adapter.MySql.Test
{
    /// <summary>
    /// 用于读写分离单元测试
    /// </summary>
    public class ReadWriteSeparationTest : BaseTest
    {
        public ReadWriteSeparationTest(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        ///  测试 ReadWriteConnectionStringManager 注入类型
        /// </summary>
        [Fact]
        public void GetConnectionStringManagerType()
        {
            var type = _connectionStringManager.GetType();
            Assert.Equal(typeof(ReadWriteConnectionStringManager), type);
            Assert.Equal("ReadWriteConnectionStringManager", type.Name);
        }

        /// <summary>
        ///  测试 ReadWriteConnectionStringManager 注入类型
        /// </summary>
        [Fact]
        public void GetReadWriteConnectorFactoryType()
        {
            var type = _readWriteConnectorFactory.GetType();
            Assert.Equal(typeof(ReadWriteConnectorFactory), type);
            Assert.Equal("ReadWriteConnectorFactory", type.Name);
        }

        /// <summary>
        /// 测试获取指定节点类型的全部连接字符串
        /// </summary>
        [Fact]
        public void GetAllConnectionString()
        {
            var masterConnString = _connectionStringManager.GetAllConnectionString(NodeType.Master);
            masterConnString?.ToList()?.ForEach(s =>
            {
                _output.WriteLine(s);
            });

            var slaveConnString = _connectionStringManager.GetAllConnectionString(NodeType.Slave);
            slaveConnString?.ToList()?.ForEach(s =>
            {
                _output.WriteLine(s);
            });

            Assert.NotNull(masterConnString);
            Assert.NotNull(slaveConnString);
        }

        /// <summary>
        /// 测试添加节点
        /// </summary>
        [Fact]
        public void AddDataSourceReadNode()
        {
            string nodeName = "";

            using (_readWriteManager.CreateScope())
            {
                var context = _readWriteManager.GetCurrent();
                context.AddDataSourceReadNode(NodeType.Master, "slave-db1");
                nodeName = context.TryGetDataSourceReadNode(NodeType.Master);
            }

            Assert.Equal("slave-db1", nodeName);
        }

        /// <summary>
        ///测试获取指定数据库连接
        /// </summary>
        [Fact]
        public void GetConnectionString()
        {
            var connString1 = _connectionStringManager.GetConnectionString(NodeType.Master, "master-db1");
            var connString2 = _connectionStringManager.GetConnectionString(NodeType.Master, "master-db2");
            var connString3 = _connectionStringManager.GetConnectionString(NodeType.Slave, "slave-db1");
            var connString4 = _connectionStringManager.GetConnectionString(NodeType.Slave, "slave-db2");

            Assert.Equal(master_db1, connString1);
            Assert.Equal(master_db2, connString2);
            Assert.Equal(slave_db1, connString3);
            Assert.Equal(slave_db2, connString4);
        }


        /// <summary>
        /// 测试获取指定数据库连接
        /// </summary>
        [Fact]
        public void GetConnectionStringByReadStrategy()
        {
            var connStrings = new List<string>();

            Parallel.ForEach(new int[5], s => 
            {
                var connString = _connectionStringManager.GetConnectionString(NodeType.Master);
                _output.WriteLine(connString);
                connStrings.Add(connString);
            });

            /*
            server=localhost;user id=root;password=racing.1;port=3306;persistsecurityinfo=True;database=tpmtest_write_2;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;
            server=localhost;user id=root;password=racing.1;port=3306;persistsecurityinfo=True;database=tpmtest_write_1;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;
            server=localhost;user id=root;password=racing.1;port=3306;persistsecurityinfo=True;database=tpmtest_write_2;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;
            server=localhost;user id=root;password=racing.1;port=3306;persistsecurityinfo=True;database=tpmtest_write_2;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;
            server=localhost;user id=root;password=racing.1;port=3306;persistsecurityinfo=True;database=tpmtest_write_1;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;
             */

            Parallel.ForEach(new int[5], s =>
            {
                var connString = _connectionStringManager.GetConnectionString(NodeType.Slave);
                _output.WriteLine(connString);
                connStrings.Add(connString);
            });

            /*
            server=localhost;user id=root;password=racing.1;port=3306;persistsecurityinfo=True;database=tpmtest_read_2;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;
            server=localhost;user id=root;password=racing.1;port=3306;persistsecurityinfo=True;database=tpmtest_read_2;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;
            server=localhost;user id=root;password=racing.1;port=3306;persistsecurityinfo=True;database=tpmtest_read_2;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;
            server=localhost;user id=root;password=racing.1;port=3306;persistsecurityinfo=True;database=tpmtest_read_1;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;
            server=localhost;user id=root;password=racing.1;port=3306;persistsecurityinfo=True;database=tpmtest_read_1;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;
             */

            Assert.Equal(10, connStrings.Count);
        }

        /// <summary>
        /// 测试分表插入
        /// </summary>
        [Fact]
        public async void ShardingAddTest()
        {
            /*
            当前环境：
            Master  -> master-db1
            Slave   ->  slave-db1  / slave-db2
            */

            var article = new ArticleEntity
            {
                Title = "test",
                Content = "test",
                //该字段为分表字段
                PublishedTime = DateTime.Parse("2022-06-06 00:00:00")
            };

            await _articleRepository.Add(article);

            //读取
            //注意:主从复制没有开启的情况下，数量始终为0,开启Master与Slave主从复制, 将从 slave-db1  / slave-db2 取得
            var results = await _articleRepository.GetArticles(article.PublishedTime.Value);

            _output.WriteLine($"获取 {results.Count} 数据：");
            results?.ToList()?.ForEach(s =>
            {
                _output.WriteLine($"{s.Id} --> {s.PublishedTime}");
            });

            Assert.True(article.Id > 0);
        }
    }
}
