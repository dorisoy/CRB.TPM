#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# ENV LANG C.UTF-8
ENV ASPNETCORE_ENVIRONMENT Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./modules/WebHost/WebHost.csproj", "./modules/WebHost/"]
COPY ["./src/Data/Adapters/Data.Adapter.MySql/CRB.TPM.Data.Adapter.MySql.csproj", "./src/Data/Adapters/Data.Adapter.MySql/"]
COPY ["./src/Data/Data.Core/CRB.TPM.Data.Core.csproj", "./src/Data/Data.Core/"]
COPY ["./src/Data/Data.Abstractions/CRB.TPM.Data.Abstractions.csproj", "./src/Data/Data.Abstractions/"]
COPY ["./src/Utils/Utils/CRB.TPM.Utils.csproj", "./src/Utils/Utils/"]
COPY ["./src/Data/Data.Sharding/CRB.TPM.Data.Sharding.csproj", "./src/Data/Data.Sharding/"]
COPY ["./src/Host/Host.Web/CRB.TPM.Host.Web.csproj", "./src/Host/Host.Web/"]
COPY ["./src/Module/Module.Web/CRB.TPM.Module.Web.csproj", "./src/Module/Module.Web/"]
COPY ["./src/Utils/Utils.Web/CRB.TPM.Utils.Web.csproj", "./src/Utils/Utils.Web/"]
COPY ["./src/Excel/Excel.Abstractions/CRB.TPM.Excel.Abstractions.csproj", "./src/Excel/Excel.Abstractions/"]
COPY ["./src/Auth/Auth.Abstractions/CRB.TPM.Auth.Abstractions.csproj", "./src/Auth/Auth.Abstractions/"]
COPY ["./src/Module/Module.Abstractions/CRB.TPM.Module.Abstractions.csproj", "./src/Module/Module.Abstractions/"]
COPY ["./src/Module/Module.Core/CRB.TPM.Module.Core.csproj", "./src/Module/Module.Core/"]
COPY ["./src/Auth/Auth.Jwt/CRB.TPM.Auth.Jwt.csproj", "./src/Auth/Auth.Jwt/"]
COPY ["./src/Auth/Auth.Core/CRB.TPM.Auth.Core.csproj", "./src/Auth/Auth.Core/"]
COPY ["./src/Config/Config.Abstractions/CRB.TPM.Config.Abstractions.csproj", "./src/Config/Config.Abstractions/"]
COPY ["./src/Logging/Logging.Abstractions/CRB.TPM.Logging.Abstractions.csproj", "./src/Logging/Logging.Abstractions/"]
COPY ["./src/Cache/Cache.Core/CRB.TPM.Cache.Core.csproj", "./src/Cache/Cache.Core/"]
COPY ["./src/Cache/Cache.Abstractions/CRB.TPM.Cache.Abstractions.csproj", "./src/Cache/Cache.Abstractions/"]
COPY ["./src/Cache/Cache.Redis/CRB.TPM.Cache.Redis.csproj", "./src/Cache/Cache.Redis/"]
COPY ["./src/Mapper/Mapper/CRB.TPM.Mapper.csproj", "./src/Mapper/Mapper/"]
COPY ["./src/Config/Config.Core/CRB.TPM.Config.Core.csproj", "./src/Config/Config.Core/"]
COPY ["./src/Logging/Logging.Core/CRB.TPM.Logging.Core.csproj", "./src/Logging/Logging.Core/"]
COPY ["./src/Excel/Excel.Core/CRB.TPM.Excel.Core.csproj", "./src/Excel/Excel.Core/"]
COPY ["./src/Excel/Providers/Excel.EPPlus/CRB.TPM.Excel.EPPlus.csproj", "./src/Excel/Providers/Excel.EPPlus/"]
COPY ["./src/Validation/Validation.FluentValidation/CRB.TPM.Validation.FluentValidation.csproj", "./src/Validation/Validation.FluentValidation/"]
COPY ["./src/Validation/Validation.Abstractions/CRB.TPM.Validation.Abstractions.csproj", "./src/Validation/Validation.Abstractions/"]
COPY ["./src/Data/Adapters/Data.Adapter.PostgreSQL/CRB.TPM.Data.Adapter.PostgreSQL.csproj", "./src/Data/Adapters/Data.Adapter.PostgreSQL/"]
COPY ["./src/Data/Adapters/Data.Adapter.Sqlite/CRB.TPM.Data.Adapter.Sqlite.csproj", "./src/Data/Adapters/Data.Adapter.Sqlite/"]
COPY ["./src/Data/Adapters/Data.Adapter.SqlServer/CRB.TPM.Data.Adapter.SqlServer.csproj", "./src/Data/Adapters/Data.Adapter.SqlServer/"]
COPY ["./modules/Admin/Admin.Web/Admin.Web.csproj", "./modules/Admin/Admin.Web/"]
COPY ["./modules/AuditInfo/AuditInfo.Web/AuditInfo.Web.csproj", "./modules/AuditInfo/AuditInfo.Web/"]
COPY ["./modules/AuditInfo/AuditInfo.Core/AuditInfo.Core.csproj", "./modules/AuditInfo/AuditInfo.Core/"]
COPY ["./modules/Admin/Admin.Core/Admin.Core.csproj", "./modules/Admin/Admin.Core/"]
COPY ["./modules/Logging/Logging.Core/Logging.Core.csproj", "./modules/Logging/Logging.Core/"]
COPY ["./modules/Logging/Logging.Web/Logging.Web.csproj", "./modules/Logging/Logging.Web/"]
COPY ["./modules/MainData/MainData.Web/MainData.Web.csproj", "./modules/MainData/MainData.Web/"]
COPY ["./modules/MainData/MainData.Core/MainData.Core.csproj", "./modules/MainData/MainData.Core/"]
COPY ["./modules/PersonnelFiles/PersonnelFiles.Web/PS.Web.csproj", "./modules/PersonnelFiles/PersonnelFiles.Web/"]
COPY ["./modules/PersonnelFiles/PersonnelFiles.Core/PS.Core.csproj", "./modules/PersonnelFiles/PersonnelFiles.Core/"]
COPY ["./modules/Scheduler/Scheduler.Web/Scheduler.Web.csproj", "./modules/Scheduler/Scheduler.Web/"]
COPY ["./modules/Scheduler/Scheduler.Core/Scheduler.Core.csproj", "./modules/Scheduler/Scheduler.Core/"]
COPY ["./src/TaskScheduler/TaskScheduler.Core/CRB.TPM.TaskScheduler.Core.csproj", "./src/TaskScheduler/TaskScheduler.Core/"]
COPY ["./src/TaskScheduler/TaskScheduler.Abstractions/CRB.TPM.TaskScheduler.Abstractions.csproj", "./src/TaskScheduler/TaskScheduler.Abstractions/"]
COPY ["./src/Data/Z.Dapper.Plus/net6.0/Z.Dapper.Plus.dll", "./src/Data/Z.Dapper.Plus/net6.0/Z.Dapper.Plus.dll"]
COPY ["./modules/MainData/MainData.Web/icudt50.dll", "./modules/MainData/MainData.Web/icudt50.dll"]
COPY ["./modules/MainData/MainData.Web/icuin50.dll", "./modules/MainData/MainData.Web/icuin50.dll"]
COPY ["./modules/MainData/MainData.Web/icuuc50.dll", "./modules/MainData/MainData.Web/icuuc50.dll"]
COPY ["./modules/MainData/MainData.Web/sapnwrfc.dll", "./modules/MainData/MainData.Web/sapnwrfc.dll"]
COPY ["./modules/MainData/MainData.Web/sapnwrfc.lib", "./modules/MainData/MainData.Web/sapnwrfc.lib"]
COPY ["./src/Directory.Build.props", "./src/"]
COPY ["./nuget.config", "./"]

RUN dotnet restore "modules/WebHost/WebHost.csproj" --configfile "./nuget.config"
COPY . .
WORKDIR "/src/modules/WebHost"
RUN dotnet build "WebHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebHost.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebHost.dll"]