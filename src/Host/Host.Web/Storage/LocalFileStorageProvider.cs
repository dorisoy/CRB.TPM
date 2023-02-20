using CRB.TPM.Config.Abstractions;
using CRB.TPM.Config.Core;
using CRB.TPM.Utils.Abstracts;
using CRB.TPM.Utils.Enums;
using CRB.TPM.Utils.File;
using CRB.TPM.Utils.Web.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CRB.TPM.Host.Web;

/// <summary>
/// OSS 本地文件存储提供器
/// </summary>
public class LocalFileStorageProvider : IFileStorageProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfigProvider _configProvider;

    public LocalFileStorageProvider(IHttpContextAccessor httpContextAccessor, IConfigProvider configProvider)
    {
        _httpContextAccessor = httpContextAccessor;
        _configProvider = configProvider;
    }

    public ValueTask<bool> Upload(FileObject fileObject)
    {
        fileObject.FileInfo.Url = GetUrl(fileObject.FileInfo.FullPath, fileObject.AccessMode);
        return new ValueTask<bool>(true);
    }

    public ValueTask<bool> Delete(FileObject fileObject)
    {
        if (fileObject == null || fileObject.FileInfo == null || fileObject.FileInfo.FullPath.IsNull())
            return new ValueTask<bool>(false);

        var config = _configProvider.Get<PathConfig>();
        var path = Path.Combine(config.UploadPath, "Admin", "OSS", fileObject.AccessMode == FileAccessMode.Open ? "Open" : "Private", fileObject.FileInfo.FullPath);

        if (File.Exists(path))
        {
            File.Delete(path);
        }
        return new ValueTask<bool>(true);
    }

    public string GetUrl(string fullPath, FileAccessMode accessMode = FileAccessMode.Open)
    {
        if (fullPath.IsNull())
            return string.Empty;
        if (fullPath.StartsWith("http:", StringComparison.OrdinalIgnoreCase) || fullPath.StartsWith("https:", StringComparison.OrdinalIgnoreCase))
            return fullPath;

        var request = _httpContextAccessor.HttpContext.Request;

        //p表示私有的文件private，o表示公开的文件open
        var path = $"/oss/{(accessMode == FileAccessMode.Open ? "o" : "p")}/{fullPath}";

        return new Uri(request.GetUrl(path)).ToString();
    }
}
