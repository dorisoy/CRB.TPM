using CRB.TPM.Data.Sharding;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Data.Sharding.Test;

/// <summary>
/// 连接字符串解析测试
/// </summary>
public class ConnectionStringTests : BaseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ParsingTest()
    {

        var mssql = "initial catalog=test;data source=localhost;password=123456;User id=root;MultipleActiveResultSets=True";

        var oracle = "Data Source=test;User Id=root;Password=123456;";

        var mysql = "server=localhost;user id=root;password=123456;port=3306;persistsecurityinfo=True;database=db;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;";

        var sqlite = "Data Source=c:\\mydb.db;Password=123456;";

        var postgre = "User ID=root;Password=123456;Host=localhost;Port=5432;Database=myDataBase;";

        var config_mssql = ConnectionStringBuilder.ParsingSqlServer(mssql);
        var config_oracle = ConnectionStringBuilder.ParsingOracle(oracle);
        var config_mysql = ConnectionStringBuilder.ParsingMySql(mysql);
        var config_sqlite = ConnectionStringBuilder.ParsingSqlite(sqlite);
        var config_postgre = ConnectionStringBuilder.ParsingNpgsql(postgre);


        Assert.AreEqual(config_mssql.Server, "localhost");
        Assert.AreEqual(config_oracle.Server, "");
        Assert.AreEqual(config_mysql.Server, "localhost");
        Assert.AreEqual(config_sqlite.Server, "");
        Assert.AreEqual(config_postgre.Server, "localhost");

        Assert.AreEqual(config_mssql.UserId, "root");
        Assert.AreEqual(config_oracle.UserId, "root");
        Assert.AreEqual(config_mysql.UserId, "root");
        Assert.AreEqual(config_sqlite.UserId, "");
        Assert.AreEqual(config_postgre.UserId, "root");

        Assert.AreEqual(config_mssql.Password, "123456");
        Assert.AreEqual(config_oracle.Password, "123456");
        Assert.AreEqual(config_mysql.Password, "123456");
        Assert.AreEqual(config_sqlite.Password, "123456");
        Assert.AreEqual(config_postgre.Password, "123456");

    }

}


