# CRB.TPM

CRB.TPM基于领域驱动的设计模式，原理和最佳实践的模块化企业级开发框架，具备快速的领域生成或者代码生成功能，快速集成，以模块化为思想，以业务领域为理念，以包管理(nuget\npm )为基础，充分解耦业务功能，使业务最大化的得到复用，极大减少重复开发时间，结合在线代码生成器，可轻松接入更多交易服务，供应链金融服务及物流服务等。提供通用权限管理(Admin)、基础数据(Common)、任务调度(Quartz)、代码生成(CodeGenerator )等模块，开箱即用，该平台实现业务财务一体化，从销售费用业务发起、过程及费用应用全过程闭环管理；实现业务管理动作、财务测算、检查与审批一个平台一体化解决方案，达到业财工作效率大大提升的目的，在业务规范、流程规范、财务标准管理、业财制度建设等方面完成全国统一平台初步搭建，为数字化营销费用过程化管理奠定了基础。

## 文档

- 开发文档: [http://tpm.dorisoy.com/](http://tpm.dorisoy.com/)
- 前端UI: [http://tpmui.dorisoy.com/#/doc/home](http://tpmui.dorisoy.com/#/doc/home)

## 开发环境

> IDE/开发语言
>
> > [Visual Studio 2019+](https://visualstudio.microsoft.com/zh-hans/downloads/)、
[Visual Studio Code](https://code.visualstudio.com/)、
[C#](https://developer.android.google.cn/studio/)、
[Javas\Script](https://developer.android.google.cn/studio/)、
[TypeScript](https://developer.android.google.cn/studio/)、

> 持的数据库
>
> > [Redis](https://redis.io/)、
[RabbitMQ](https://www.rabbitmq.com/)、
[MSSQL Server 2016+](https://www.mssql.com/)、
[MySQL8.0+](https://www.mysql.com/)、
[Sqlite](https://www.sqlite.org/index.html)、
[PostgreSQL >= 9.5](https://www.postgresql.org/)

> 后端
>
> > [.Net Core 6.0](https://dotnet.microsoft.com/download)、
[Redis](https://redis.io/)、
[RabbitMQ](https://www.rabbitmq.com/)、
[Dapper](https://github.com/StackExchange/Dapper)、
[Quartz](http://www.quartz-scheduler.org/)、
[Serilog](https://serilog.net/)、
[AutoMapper](https://automapper.org/)、
[FluentValidation](https://fluentvalidation.net)、
[Swagger](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

> Web前端
>
> > [Node.js 10+](https://nodejs.org/en/)、[TypeScript 4.0 +](https://www.typescriptlang.org/)、[Vue.js 3.0+](https://cn.vuejs.org/)、[Vue CLI](https://cli.vuejs.org/zh/guide/)、[Vuex](https://vuex.vuejs.org/zh/)、[VueRouter](https://router.vuejs.org/zh/)、[Element-Plus](https://element.eleme.cn/#/zh-CN/component/installation)


# 快速上手

本篇文档让你能够快速的启动`CRB.TPM`项目。

GitLab示例源码仓库：

- CRB.TPM.Doc: [https://github.com/dorisoy/CRB.TPM.Docs.git](https://github.com/dorisoy/CRB.TPM.Docs.git)
- CRB.TPM: [https://github.com/dorisoy/CRB.TPM.git](https://github.com/dorisoy/CRB.TPM.git)
- CRB.TPM.Rigger: [https://github.com/dorisoy/CRB.TPM.Rigger.git](https://github.com/dorisoy/CRB.TPM.Rigger.git)
- CRB.TPM.UI.Style: [https://github.com/dorisoy/CRB.TPM.UI.Style.git](https://github.com/dorisoy/CRB.TPM.UI.Style.git)
- CRB.TPM.UI: [https://github.com/dorisoy/CRB.TPM.UI.git](https://github.com/dorisoy/CRB.TPM.UI.git)

## 1、配置

配置信息都保存在`appsettings.json`文件中，根据功能来区分，如下：

```xml
{
  //主机配置
  "Host": {
    //绑定URL
    "Urls": "http://*:6220"
  },
  //Serilog日志配置
  "Serilog": {
    "MinimumLevel": {
      "Default": "Error",
      "Override": {
        "Microsoft": "Error",
        "System": "Error"
      }
    },
    "WriteTo": [
      //输出到文件
      {
        "Name": "File",
        "Args": {
          //文件路径
          "path": "log/log.log",
          //文件滚动方式
          "rollingInterval": "Day",
          //消息输出格式
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
          //文件数量
          "retainedFileCountLimit": 60,
          //使用缓冲，提高写入效率
          "buffered": false
        }
      }
    ]
  },
  //CRB.TPM框架本身的配置
  "CRB.TPM": {
    //通用配置
    "Common": {
      //临时文件目录，默认应用程序根目录中的Temp目录
      "TempDir": "",
      //默认语言
      "Lang": "zh",
      //是否启用全局客户端模式（使用该模式可以更加灵活使用数据提供上下文）
      "UseClientMode": true,
      //通用全局数据配置（在UseReadWriteSeparation开启下，该配置会被模块配置替换）
      "Db": {
        //数据库类型，0：SqlServer 1：MySql 2：Sqlite  3：PostgreSQL  4：Oracle
        "Provider": 1,
        //数据库连接字符串
        "ConnectionString": "server=localhost;user id=root;password=你的密码;port=3306;persistsecurityinfo=True;database=tpmtest;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;",
        //开启日志
        "Log": true,
        //启用代码优先模式
        "CodeFirst": true,
        //自动创建数据库
        "CreateDatabase": true,
        //自动更新列信息
        "UpdateColumn": true,
        //创建数据库后初始化数据
        "InitData": true,

        //是否启用全局读写分离
        "UseReadWriteSeparation": false
      }
    },
    //模块列表
    "Modules": {
      //主管理模块，注：（该模块不可缺失）
      "Admin": {
        "Config": {
          //创建账户时默认密码
          "DefaultPassword": "123456789"
        },
        //数据库配置
        "Db": {
          //数据库类型，0：SqlServer 1：MySql 2：Sqlite  3：PostgreSQL  4：Oracle
          "Provider": 1,
          //数据库连接字符串
          "ConnectionString": "server=localhost;user id=root;password=你的密码;port=3306;persistsecurityinfo=True;database=tpmtest;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;",
          //开启日志
          "Log": true,
          //启用代码优先模式
          "CodeFirst": true,
          //自动创建数据库
          "CreateDatabase": true,
          //自动更新列信息
          "UpdateColumn": true,
          //创建数据库后初始化数据
          "InitData": true,
          //读写分离配置
          "UseReadWriteSeparation": false,
          "ReadWriteSeparationOptions": {
            //读取策略 1:Random,2:Loop
            "ReadStrategy": 2,
            //是否默认启用
            "DefaultEnable": true,
            //默认策略（优先级）
            "DefaultPriority": 10,
            //连接字符串读取策略 0:LatestEveryTime,1:LatestFirstTime
            "ReadConnStringGetStrategy": 1,
            //主节点
            "Master": [
              {
                "Name": "Module-db1",
                "ConnectionString": "server=localhost;user id=root;password=你的密码;port=3306;persistsecurityinfo=True;database=tpmtest_write_1;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;"
              },
              {
                "Name": "Module-db2",
                "ConnectionString": "server=localhost;user id=root;password=你的密码;port=3306;persistsecurityinfo=True;database=tpmtest_write_2;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;"
              }
            ],
            //从节点
            "Slave": [
              {
                "Name": "Module-db3",
                "ConnectionString": "server=localhost;user id=root;password=你的密码;port=3306;persistsecurityinfo=True;database=tpmtest_read_1;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;"
              },
              {
                "Name": "Module-db4",
                "ConnectionString": "server=localhost;user id=root;password=你的密码;port=3306;persistsecurityinfo=True;database=tpmtest_read_2;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;"
              }
            ]
          }
        }
      },
      //示例模块1
      "TP": {
        //在这里自定义配置...
      }
    },
    //身份认证与授权配置项
    "Auth": {
      //启用权限验证(生产环境慎重关闭)
      "EnablePermissionVerify": true,
      //启用验证码功能
      "EnableVerifyCode": false,
      //启用审计日志
      "EnableAuditLog": true,
      //启用检测IP地址功能
      "EnableCheckIP": true,
      //对登录凭证进行加密
      "EncryptCert": true,
      //Jwt配置
      "Jwt": {
        //密钥
        "Key": "twAJ$j5##pVc5*y&",
        //发行人
        "Issuer": "http://127.0.0.1:6220",
        //消费者
        "Audience": "http://127.0.0.1:6220",
        //令牌有效期，单位分钟，(默认120)
        "Expires": 120,
        //刷新令牌有效期(单位：天，默认7)
        "RefreshTokenExpires": 7
      }
    },
    //缓存配置
    "Cache": {
      //缓存提供器，0、MemoryCache 1、Redis
      "Provider": 0,
      //Redis配置
      "Redis": {
        //默认数据库
        "DefaultDb": 0,
        //缓存键前缀
        "KeyPrefix": "",
        //链接字符串
        "ConnectionString": ""
      }
    }
  }
}
```

::: warning 警告

本框架支持多种数据库，代码中默认采用 SQLite 数据库，所以您获取最新的代码后是可以直接就能跑起来的。如果需要其它数据库，请按照上面的说明修改配置信息。

同时目前本框架已支持自动创建数据库以及初始化数据功能(目前仅支持初始化 Admin 模块的数据)。

:::

## 2、配置节点分解
Appsetting 配置项总共有3大配置节点，分别是Host（主机配置），Serilog（日志配置），CRB.TPM（CRB.TPM框架本身的配置）

```xml
{
  //主机配置
  "Host": {
    //在这里主机相关配置...
  },
  //Serilog日志配置
  "Serilog": {
    //在这里日日志记录配置...
  },
  //CRB.TPM框架本身的配置
  "CRB.TPM": {
    //在这里配置...
  }
}
```


### 3.1、主机配置
在应用程序启动时，由HostBootstrap（主机引导）启动加载程序程序根目录下的appsettings.json 配置文件解析为HostOptions 对象，通过ConfigurationBuilder 构建 IConfigurationRoot后给IConfigurationRoot Host 绑定 hostOptions 配置，监听端口以及绑定的地址(默认：http://*:5000)

> Host 结构

```xml
//主机配置
"Host": {
  //绑定URL
  "Urls": "http://*:6220",
  //是否开启Swagger功能
  "Swagger":true
  //...
  //...
  //...其他属性
}
```

> HostOptions

```csharp
/// <summary>
/// 宿主配置项
/// </summary>
public class HostOptions
{
    /// <summary>
    /// 绑定的地址(默认：http://*:5000)
    /// </summary>
    public string Urls { get; set; }

    /// <summary>
    /// 基础路径
    /// </summary>
    public string Base { get; set; }

    /// <summary>
    /// 是否开启Swagger功能
    /// </summary>
    public bool Swagger { get; set; }

    /// <summary>
    /// 指定跨域访问时预检请求的有效期，单位秒，默认30分钟
    /// </summary>
    public int PreflightMaxAge { get; set; }

    /// <summary>
    /// 是否启用代理
    /// </summary>
    public bool Proxy { get; set; }

    /// <summary>
    /// 开放的wwwroot下的目录列表
    /// </summary>
    public List<string> OpenDirs { get; set; } = new() { "web" };

    /// <summary>
    /// 默认目录
    /// </summary>
    public string DefaultDir { get; set; } = "web";
}
```


### 3.2、日志配置
Serilog是将文件资源常驻内存的方式写入 优点是写入效率快 缺点是内存一直被占用 如果文件意外被删除 在不重启程序的情况下不会在进行日志写入，如果想个性化设置日志输出到什么地方时，Serilog为我们提供了非常多的Sink接收器支持，Serilog的输出对象称之为Sink（水槽，也就是接收器），而.NetCore 内置日志记录提供程序则不具备这种能力。

> Serilog 配置

```xml
//Serilog日志配置
"Serilog": {
  "MinimumLevel": {
    "Default": "Error",
    "Override": {
      "Microsoft": "Error",
      "System": "Error"
    }
  },
  "WriteTo": [
    //输出到文件
    {
      "Name": "File",
      "Args": {
        //文件路径
        "path": "log/log.log",
        //文件滚动方式
        "rollingInterval": "Day",
        //消息输出格式
        "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
        //文件数量
        "retainedFileCountLimit": 60,
        //使用缓冲，提高写入效率
        "buffered": false
      }
    }
  ]
},
```
> 要在CRB.TPM 中启用Serilog组件替换,.NetCore 内置日志记录提供程序原生支持，需要使用SerilogHostBuilderExtensions 扩展来覆盖，如：

```csharp
 //使用Serilog日志
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom
        .Configuration(hostingContext.Configuration)
        .Enrich
        .FromLogContext();
});
```

### 3.3、框架配置
其它功能模块的配置信息使用默认值就行，如果想修改可以自己查看文件中的说明。

> CRB.TPM

```xml
  //CRB.TPM框架本身的配置
"CRB.TPM": {
  //通用配置
  "Common": {
    //在这里自定义通用配置...
  },
  //模块列表
  "Modules": {
    //主管理模块，注：（该模块不可缺失）
    "Admin": {
    //在这里配置...
    },
    //示例模块1
    "TP": {
      //在这里配置...
    }
  },
  //身份认证与授权配置项
  "Auth": {
    //在这里配置...
  },
  //缓存配置
  "Cache": {
    //在这里配置...
  }
}
```

### 3.3.1、通用配置
其它功能模块的配置信息使用默认值就行，如果想修改可以自己查看文件中的说明。

> Common

```xml
//通用配置
"Common": {
  //临时文件目录，默认应用程序根目录中的Temp目录
  "TempDir": "",
  //默认语言
  "Lang": "zh",
  //是否启用全局客户端模式（使用该模式可以更加灵活使用数据提供上下文）
  "UseClientMode": true,
  //通用全局数据配置（在UseReadWriteSeparation开启下，该配置会被模块配置替换）
  "Db": {
    //全局Db配置...
  }
}
```

> 全局Db配置

```xml
//通用全局数据配置（在UseReadWriteSeparation开启下，该配置会被模块配置替换）
"Db": {
  //数据库类型，0：SqlServer 1：MySql 2：Sqlite  3：PostgreSQL  4：Oracle
  "Provider": 1,
  //数据库连接字符串
  "ConnectionString": "server=localhost;user id=root;password=racing.1;port=3306;persistsecurityinfo=True;database=tpmtest;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;",
  //开启日志
  "Log": true,
  //启用代码优先模式
  "CodeFirst": true,
  //自动创建数据库
  "CreateDatabase": true,
  //自动更新列信息
  "UpdateColumn": true,
  //创建数据库后初始化数据
  "InitData": true,

  //是否启用全局读写分离
  "UseReadWriteSeparation": false
}
```

> CommonOptions 通用配置

```csharp
/// <summary>
/// 通用配置
/// </summary>
public class CommonOptions
{
    /// <summary>
    /// 临时文件目录，默认应用程序根目录中的Temp目录
    /// </summary>
    public string DefaultTempDir => Path.Combine(AppContext.BaseDirectory, "Temp");

    /// <summary>
    /// 临时目录，默认是应用程序根目录下的Temp目录
    /// </summary>
    public string TempDir { get; set; }

    /// <summary>
    /// 默认语言
    /// </summary>
    public string Lang { get; set; }

    /// <summary>
    /// 数据库配置（模块数据配置）
    /// </summary>
    public ModuleDbOptions Db { get; set; }

    /// <summary>
    /// 是否启用全局客户端模式（使用该模式有别于基于工作单元的仓储模式，可以更加灵活使用数据提供上下文）
    /// </summary>
    public bool UseClientMode { get; set; } = true;
}
```

### 3.3.2、模块配置
其它功能模块的配置信息使用默认值就行，如果想修改可以自己查看文件中的说明。

> Modules

```xml
 //模块列表
"Modules": {
  //主管理模块，注：（该模块不可缺失）
  "Admin": {
    "Config": {
      //创建账户时默认密码
      "DefaultPassword": "123456789"
    },
    //模块数据库配置
    "Db": {
      //在这里配置模块的数据库...
    }
  },
  //示例模块1
  "TP": {
    //在这里自定义模块配置...
  }
}
```
> Modules 模块配置项

```csharp
/// <summary>
/// 模块配置项
/// </summary>
public class ModuleOptions
{
    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 加载顺序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 模块配置文件所在目录
    /// </summary>
    public string Dir { get; set; }

    /// <summary>
    /// 数据库配置项
    /// </summary>
    public ModuleDbOptions Db { get; set; }
}
```

> ModuleDbOptions 模块数据库配置项

```csharp
/// <summary>
/// 模块数据库配置项
/// </summary>
public class ModuleDbOptions
{
    /// <summary>
    /// 数据库提供器
    /// </summary>
    public DbProvider Provider { get; set; }

    /// <summary>
    /// 连接字符串
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// 是否开启日志
    /// </summary>
    public bool Log { get; set; }

    /// <summary>
    /// 数据库版本
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// 表名称前缀
    /// </summary>
    public string TableNamePrefix { get; set; }

    /// <summary>
    /// 表名称分隔符
    /// </summary>
    public string TableNameSeparator { get; set; } = "_";

    /// <summary>
    /// 启用代码优先
    /// </summary>
    public bool CodeFirst { get; set; } = true;

    /// <summary>
    /// 代码优先是否创建库
    /// </summary>
    public bool CreateDatabase { get; set; } = true;

    /// <summary>
    /// 代码优先是否更新列
    /// </summary>
    public bool UpdateColumn { get; set; } = true;

    /// <summary>
    /// 是否创建数据库后初始化数据
    /// </summary>
    public bool InitData { get; set; } = true;

    /// <summary>
    /// 是否模块使用读写分离
    /// </summary>
    /// <returns></returns>
    public bool UseReadWriteSeparation { get; set; } = false;

    /// <summary>
    /// 模块读写分离配置，当UseReadWriteSeparation 启用时，默认 ConnectionString 将失效
    /// </summary>
    public ReadWriteSeparationOptions ReadWriteSeparationOptions { get; set; }

}
```

### 3.3.3、身份认证与授权配置项
其它功能模块的配置信息使用默认值就行，如果想修改可以自己查看文件中的说明。

> Auth

```xml
//身份认证与授权配置项
"Auth": {
  //启用权限验证(生产环境慎重关闭)
  "EnablePermissionVerify": true,
  //启用验证码功能
  "EnableVerifyCode": false,
  //启用审计日志
  "EnableAuditLog": true,
  //启用检测IP地址功能
  "EnableCheckIP": true,
  //对登录凭证进行加密
  "EncryptCert": true,
  //Jwt配置
  "Jwt": {
    //密钥
    "Key": "twAJ$j5##pVc5*y&",
    //发行人
    "Issuer": "http://127.0.0.1:6220",
    //消费者
    "Audience": "http://127.0.0.1:6220",
    //令牌有效期，单位分钟，(默认120)
    "Expires": 120,
    //刷新令牌有效期(单位：天，默认7)
    "RefreshTokenExpires": 7
  }
}
```
> Auth

```csharp
/// <summary>
/// 认证与授权配置
/// </summary>
public class AuthOptions
{
    /// <summary>
    /// 启用权限验证
    /// </summary>
    public bool EnablePermissionVerify { get; set; } = true;

    /// <summary>
    /// 启用验证码功能
    /// </summary>
    public bool EnableVerifyCode { get; set; } = false;

    /// <summary>
    /// 启用登录
    /// </summary>
    public bool EnableLoginLog { get; set; } = true;

    /// <summary>
    /// 启用审计日志
    /// </summary>
    public bool EnableAuditLog { get; set; } = true;

    /// <summary>
    /// 启用检测用户IP地址
    /// </summary>
    public bool EnableCheckIP { get; set; } = true;

    /// <summary>
    /// 对登录凭证进行加密
    /// </summary>
    public bool EncryptCert { get; set; } = true;
}
```

### 3.3.4、缓存配置
其它功能模块的配置信息使用默认值就行，如果想修改可以自己查看文件中的说明。

> Cache 提供器，支持MemoryCache 和 Redis

```xml
//缓存配置
"Cache": {
  //缓存提供器，0、MemoryCache 1、Redis
  "Provider": 0,
  //Redis配置
  "Redis": {
    //默认数据库
    "DefaultDb": 0,
    //缓存键前缀
    "KeyPrefix": "",
    //链接字符串
    "ConnectionString": ""
  }
}
```
> 要使用缓存，添加服务即可

```csharp
/// <summary>
/// 添加缓存
/// </summary>
/// <param name="services"></param>
/// <param name="cfg"></param>
/// <returns></returns>
public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration cfg)
{
    var builder = services.AddCache();
    var provider = cfg["CRB.TPM:Cache:Provider"];
    if (provider != null && provider.ToLower() == "redis")
    {
        builder.UseRedis(cfg);
    }
    builder.Build();
    return services;
}
```

## 3、读写分离配置

CRB.TPM 的读写分离基于MSSQL 的always on 和 MySQL的 ProxySQL模式，分表使用基于时间的策略。

> 要开启读写分离，需要配置全局提供节点 CRB.TPM.Common.Db: 下的 UseReadWriteSeparation 和 Modules 列表下的具体米宽的数据库配置，当模块下的让任意个UseReadWriteSeparation 开启式，全局公共配置将被覆盖，保留全局配置的目的是，主项目在没有模块的前提下，能使用自己的配置。

```xml
//读写分离配置
"UseReadWriteSeparation": false
```

> ReadWriteSeparationOptions 读写分离配置
```xml
//读写分离配置
"UseReadWriteSeparation": false,
"ReadWriteSeparationOptions": {
  //读取策略 1:Random,2:Loop
  "ReadStrategy": 2,
  //是否默认启用
  "DefaultEnable": true,
  //默认策略（优先级）
  "DefaultPriority": 10,
  //连接字符串读取策略 0:LatestEveryTime,1:LatestFirstTime
  "ReadConnStringGetStrategy": 1,
  //主节点
  "Master": [
    {
      "Name": "Module-db1",
      "ConnectionString": "server=localhost;user id=root;password=******;port=3306;persistsecurityinfo=True;database=tpmtest_write_1;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;"
    },
    {
      "Name": "Module-db2",
      "ConnectionString": "server=localhost;user id=root;password=******;port=3306;persistsecurityinfo=True;database=tpmtest_write_2;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;"
    }
  ],
  //从节点
  "Slave": [
    {
      "Name": "Module-db3",
      "ConnectionString": "server=localhost;user id=root;password=******;port=3306;persistsecurityinfo=True;database=tpmtest_read_1;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;"
    },
    {
      "Name": "Module-db4",
      "ConnectionString": "server=localhost;user id=root;password=******;port=3306;persistsecurityinfo=True;database=tpmtest_read_2;Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;"
    }
  ]
}
```

> ReadWriteSeparationOptions 读写分离配置项

```csharp
/// <summary>
/// 读写分离配置
/// </summary>
public class ReadWriteSeparationOptions
{
    /// <summary>
    /// 主节点配置
    /// </summary>
    public Node[] Master { get; set; }

    /// <summary>
    /// 从节点配置
    /// </summary>
    public Node[] Slave { get; set; }

    /// <summary>
    /// 读取策略
    /// </summary>
    public ReadStrategyEnum ReadStrategy { get; set; } = ReadStrategyEnum.Loop;

    /// <summary>
    /// 是否默认启用
    /// </summary>
    public bool DefaultEnable { get; set; } = false;

    /// <summary>
    /// 默认策略
    /// </summary>
    public int DefaultPriority { get; set; } = 10;

    /// <summary>
    /// 连接字符串读取策略
    /// </summary>
    public ReadConnStringGetStrategyEnum ReadConnStringGetStrategy { get; set; } = ReadConnStringGetStrategyEnum.LatestFirstTime;
}
```
> NodeType 节点类型

```csharp
/// <summary>
/// 节点类型
/// </summary>
[Flags]
public enum NodeType
{
    /// <summary>
    /// 主节点
    /// </summary>
    Master = 1,
    /// <summary>
    /// 从节点
    /// </summary>
    Slave = 2
}
```

> ReadStrategyEnum 读取策略

```csharp
/// <summary>
/// 读取策略
/// </summary>
public enum ReadStrategyEnum
{
    /// <summary>
    /// 表示针对同一个数据源获取链接采用随机策略,（可以设置同一个链接多次就是所谓的权重）
    /// </summary>
    Random = 1,
    /// <summary>
    /// 表示同一个数据源的从库链接读取策略为轮询一个接一个公平读取,（可以设置同一个链接多次就是所谓的权重）
    /// </summary>
    Loop = 2,
}
```

## 4、启动服务端

这里就使用命令来启动，进入`CRB.TPM\modules\WebHost`目录，打开 CMD 或 PowerShell 执行以下命令

```dotnet
#cd 进入目录
PS C:\Users\Administrator> cd D:\Git\CRB\CRB.TPM\modules\WebHost
PS D:\Git\CRB\CRB.TPM\modules\WebHost>

#运行tch run
dotnet watch run
```

> 如果结果如下图所示则表示启动成功

> <img src="http://tpm.dorisoy.com/images/1d3e8860-6acd-46d2-a9d0-e7c50cb4c81a.png"/>

> 可访问 <a href="http://localhost:6220/swagger/index.html" >http://localhost:6220/swagger/index.html</a> 浏览接口文档。

> <img src="http://tpm.dorisoy.com/images/65f38232-e950-44f7-93d2-b7534173c077.png"/>

## 5、启动前端

::: warning 警告
前端运行环境依赖`Node.js 10+`，如果无法启动请检查自己的版本是否匹配`node -v`。  
前端运行环境依赖`Node.js 10+`，如果无法启动请检查自己的版本是否匹配`node -v`。  
前端运行环境依赖`Node.js 10+`，如果无法启动请检查自己的版本是否匹配`node -v`。
:::

> 进入`CRB.TPM\modules\Admin\Admin.UI`目录，执行以下命令打包前端代码：

```dotnet
#cd 进入目录
PS C:\Users\Administrator> cd D:\Git\CRB\CRB.TPM\modules\Admin\Admin.UI
PS D:\Git\CRB\CRB.TPM\modules\Admin\Admin.UI>

#如果没有安装Npm包，请执行 npm install
npm install
#如果需要更新Npm包，请执行 npm update
npm update
#运行启动服务
npm run serve
```

> 如得到以下结构，则表示运行成功

```dotnet
PS D:\Git\CRB\CRB.TPM\modules\Admin\Admin.UI> npm run dev
Debugger attached.

> tpm-mod-admin@1.0.0 dev
> vite --host --config=./build/app.config.js

Debugger attached.

vite v2.9.15 dev server running at:

> Network:  http://192.168.0.211:5220/
> Local:    http://localhost:5220/

ready in 759ms.
```


> 访问 [http://localhost:5220](http://localhost:5220) 浏览，默认账户密码为 admin/123456789

> <nm-img id="7ac0338d-cb30-48f3-aa3b-99ccecb380ac"/>

::: warning 警告
单独启动前端，路径的 web 后面一定要带上/
:::

## 6、发布前端

> 进入`CRB.TPM\modules\Admin\Admin.UI`目录，执行以下命令打包前端代码：

```dotnet
#cd 进入目录
PS C:\Users\Administrator> cd D:\Git\CRB\CRB.TPM\modules\Admin\Admin.UI
PS D:\Git\CRB\CRB.TPM\modules\Admin\Admin.UI>

#执行build命令
npm run build
```

> 如得到以下结构，则表示运行成功

```dotnet
PS D:\Git\CRB\CRB.TPM\modules\Admin\Admin.UI> npm run build
Debugger attached.

> tpm-mod-admin@1.0.0 build
> vite build --config=./build/app.config.js

Debugger attached.
vite v2.9.15 building for production...
✓ 2039 modules transformed.
../../WebHost/wwwroot/web/index.html                    4.54 KiB
../../WebHost/wwwroot/web/assets/index.fce097fc.css     413.11 KiB / gzip: 56.02 KiB
../../WebHost/wwwroot/web/assets/iconfont.75808ef3.js   439.80 KiB / gzip: 105.71 KiB
../../WebHost/wwwroot/web/assets/index.9d9baee3.js      2295.28 KiB / gzip: 737.87 KiB

(!) Some chunks are larger than 500 KiB after minification. Consider:
- Using dynamic import() to code-split the application
- Use build.rollupOptions.output.manualChunks to improve chunking: https://rollupjs.org/guide/en/#outputmanualchunks
- Adjust chunk size limit for this warning via build.chunkSizeWarningLimit.
Waiting for the debugger to disconnect...
Waiting for the debugger to disconnect...
PS D:\Git\CRB\CRB.TPM\modules\Admin\Admin.UI>
```

> 成功后打包的文件会保存到 `modules\WebHost\wwwroot\web` 目录下面, 然后用 vs 打开项目，发布 WebHost 即可。

## 7、快速创建模块

> 拉取项目Dory.Rigger，打开解决方法，设置Dory.Rigger.Generator 项目为启动项

> <img src="http://tpm.dorisoy.com/images/48ce9fbf-95a5-4c75-93b3-65d766955363.png"/>

> F5运行项目，选择数据源，编辑本地或者远程数据库连接

> <img src="http://tpm.dorisoy.com/images/676c3e7f-bc5b-4a88-9de0-edf4553229f4.png"/>

> 填写模块名称和编码，选择获取表按钮，点击“生成项目”，等待生成完毕打包下载。

> <img src="http://tpm.dorisoy.com/images/527e745b-4c86-4502-9490-c6341fbeab0d.png"/>

> 这里创建了名为“CRB.TPM.Mod.TP” 的协议终端模块，解压下载的包，到本地工作目录，如：D:\Git\CRB.TPM.Mod.TP

> <img src="http://tpm.dorisoy.com/images/3abfb038-4d85-481c-b68b-3451ccce42c3.png"/>

> 用VS 打开解决方案，更改 appsettings.json 中为你自动添加的数据链接

> <img src="http://tpm.dorisoy.com/images/23eaa7d4-930e-4972-8faa-986e29315175.png"/>

> 更改数据配置，配置自动创建数据库，然后F5 运行WebHost项目

```xml
{
  //主机配置
  "Host": {
    //绑定URL
    "Urls": "http://*:6221",
    //wwwroot目录下开放的目录列表
    "OpenDirs": ["web"],
    //目录目录
    "DefaultDir": "web"
  },
  //Serilog日志配置
  "Serilog": {
    "MinimumLevel": {
      "Default": "Error",
      "Override": {
        "Microsoft": "Error",
        "System": "Error"
      }
    },
    "WriteTo": [
      //输出到文件
      {
        "Name": "File",
        "Args": {
          //文件路径
          "path": "log/log.log",
          //文件滚动方式
          "rollingInterval": "Day",
          //消息输出格式
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
          //文件数量
          "retainedFileCountLimit": 60,
          //使用缓冲，提高写入效率
          "buffered": false
        }
      }
    ]
  },
  //TPM框架本身的配置
  "CRB.TPM": {
    //通用配置
    "Common": {
      //临时文件目录，默认应用程序根目录中的Temp目录
      "TempDir": "",
      //默认语言
      "Lang": "zh", 
      //数据库配置
      "Db": {
        //数据库类型，0：SqlServer 1：MySql 2：Sqlite  3：PostgreSQL  4：Oracle
        "Provider": 2,
        //数据库连接字符串
        "ConnectionString": "",
        //开启日志
        "Log": true,
        //启用代码优先模式
        "CodeFirst": true,
        //自动创建数据库
        "CreateDatabase": true,
        //自动更新列信息
        "UpdateColumn": true,
        //创建数据库后初始化数据
        "InitData": true
      }
    },
    //模块列表
    "Modules": {
      "Admin": {
        "Config": {
          //创建账户时默认密码
          "DefaultPassword": "123456789"
        }
      },
      "TP": {
        //数据库配置
        "Db": {
          //数据库类型，0：SqlServer 1：MySql 2：Sqlite  3：PostgreSQL  4：Oracle
          "Provider": 1,
          //数据库连接字符串
          "ConnectionString": "server=localhost;user id={账户名};password={密码};port=3306;persistsecurityinfo=True;database={模块数据库};Convert Zero Datetime=True;TreatTinyAsBoolean=true;allowuservariables=True;",
          //开启日志
          "Log": true,
          //启用代码优先模式
          "CodeFirst": true,
          //自动创建数据库
          "CreateDatabase": true,
          //自动更新列信息
          "UpdateColumn": true,
          //创建数据库后初始化数据
          "InitData": true
        }
      }
    },
    //身份认证与授权配置项
    "Auth": {
      //启用权限验证(生产环境慎重关闭)
      "EnablePermissionVerify": true,
      //启用验证码功能
      "EnableVerifyCode": false,
      //启用审计日志
      "EnableAuditLog": true,
      //启用检测IP地址功能
      "EnableCheckIP": true,
      //对登录凭证进行加密
      "EncryptCert": true,
      //Jwt配置
      "Jwt": {
        //密钥
        "Key": "kPhtBHNBkmRdiKFF",
        //发行人
        "Issuer": "http://127.0.0.1:6220",
        //消费者
        "Audience": "http://127.0.0.1:6220",
        //令牌有效期，单位分钟，(默认120)
        "Expires": 120,
        //刷新令牌有效期(单位：天，默认7)
        "RefreshTokenExpires": 7
      }
    },
    //缓存配置
    "Cache": {
      //缓存提供器，0、MemoryCache 1、Redis
      "Provider": 0,
      //Redis配置
      "Redis": {
        //默认数据库
        "DefaultDb": 0,
        //缓存键前缀
        "KeyPrefix": "",
        //链接字符串
        "ConnectionString": ""
      }
    }
  }
}
```

> 到此，你创建的TP模块就跑起来了，启动，然后和上面的方法一样，启动前端，去创建页面绑定Vue 控件搭建内容就OK啦！！

> <img src="http://tpm.dorisoy.com/images/b752e9e7-b454-4080-b9a8-0ccd787d37d3.png"/>
