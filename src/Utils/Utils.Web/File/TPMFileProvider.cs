using CRB.TPM.Utils.Abstracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;

namespace CRB.TPM.Utils.Web;

/// <summary>
/// 使用系统磁盘文件的IO操作
/// </summary>
public class TPMFileProvider : PhysicalFileProvider, ITPMFileProvider
{

    public TPMFileProvider(IWebHostEnvironment webHostEnvironment) : base(System.IO.File.Exists(webHostEnvironment.ContentRootPath) ? Path.GetDirectoryName(webHostEnvironment.ContentRootPath) : webHostEnvironment.ContentRootPath)
    {
        WebRootPath = System.IO.File.Exists(webHostEnvironment.WebRootPath)
            ? Path.GetDirectoryName(webHostEnvironment.WebRootPath)
            : webHostEnvironment.WebRootPath;
    }

    protected string WebRootPath { get; }


    #region Utilities

    private static void DeleteDirectoryRecursive(string path)
    {
        Directory.Delete(path, true);
        const int maxIterationToWait = 10;
        var curIteration = 0;

        while (Directory.Exists(path))
        {
            curIteration += 1;
            if (curIteration > maxIterationToWait)
            {
                return;
            }

            Thread.Sleep(100);
        }
    }

    protected static bool IsUncPath(string path)
    {
        return Uri.TryCreate(path, UriKind.Absolute, out var uri) && uri.IsUnc;
    }

    #endregion

    #region Methods


    public virtual string Combine(params string[] paths)
    {
        var path = Path.Combine(paths.SelectMany(p => IsUncPath(p) ? new[] { p } : p.Split('\\', '/')).ToArray());

        if (Environment.OSVersion.Platform == PlatformID.Unix && !IsUncPath(path))
        {
            //在UNIX系统中添加前导斜杠以正确形成路径
            path = "/" + path;
        }

        return path;
    }

    public virtual void CreateDirectory(string path)
    {
        if (!DirectoryExists(path))
        {
            Directory.CreateDirectory(path);
        }
    }


    public virtual void CreateFile(string path)
    {
        if (FileExists(path))
        {
            return;
        }

        var fileInfo = new FileInfo(path);
        CreateDirectory(fileInfo.DirectoryName);

        using (System.IO.File.Create(path))
        {
        }
    }

    public void DeleteDirectory(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException(path);
        }


        foreach (var directory in Directory.GetDirectories(path))
        {
            DeleteDirectory(directory);
        }

        try
        {
            DeleteDirectoryRecursive(path);
        }
        catch (IOException)
        {
            DeleteDirectoryRecursive(path);
        }
        catch (UnauthorizedAccessException)
        {
            DeleteDirectoryRecursive(path);
        }
    }


    public virtual void DeleteFile(string filePath)
    {
        if (!FileExists(filePath))
        {
            return;
        }

        System.IO.File.Delete(filePath);
    }


    public virtual bool DirectoryExists(string path)
    {
        return Directory.Exists(path);
    }


    public virtual void DirectoryMove(string sourceDirName, string destDirName)
    {
        Directory.Move(sourceDirName, destDirName);
    }

    public virtual IEnumerable<string> EnumerateFiles(string directoryPath, string searchPattern,
        bool topDirectoryOnly = true)
    {
        return Directory.EnumerateFiles(directoryPath, searchPattern,
            topDirectoryOnly ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories);
    }


    public virtual void FileCopy(string sourceFileName, string destFileName, bool overwrite = false)
    {
        System.IO.File.Copy(sourceFileName, destFileName, overwrite);
    }


    public virtual bool FileExists(string filePath)
    {
        return System.IO.File.Exists(filePath);
    }


    public virtual long FileLength(string path)
    {
        if (!FileExists(path))
        {
            return -1;
        }

        return new FileInfo(path).Length;
    }

    public virtual void FileMove(string sourceFileName, string destFileName)
    {
        System.IO.File.Move(sourceFileName, destFileName);
    }


    public virtual string GetAbsolutePath(params string[] paths)
    {
        var allPaths = new List<string>();

        if (paths.Any() && !paths[0].Contains(WebRootPath, StringComparison.InvariantCulture))
        {
            allPaths.Add(WebRootPath);
        }

        allPaths.AddRange(paths);

        return Combine(allPaths.ToArray());
    }


    public virtual DirectorySecurity GetAccessControl(string path)
    {
        return new DirectoryInfo(path).GetAccessControl();
    }


    public virtual DateTime GetCreationTime(string path)
    {
        return System.IO.File.GetCreationTime(path);
    }


    public virtual string[] GetDirectories(string path, string searchPattern = "", bool topDirectoryOnly = true)
    {
        if (string.IsNullOrEmpty(searchPattern))
        {
            searchPattern = "*";
        }

        return Directory.GetDirectories(path, searchPattern,
            topDirectoryOnly ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories);
    }


    public virtual string GetDirectoryName(string path)
    {
        return Path.GetDirectoryName(path);
    }


    public virtual string GetDirectoryNameOnly(string path)
    {
        return new DirectoryInfo(path).Name;
    }


    public virtual string GetFileExtension(string filePath)
    {
        return Path.GetExtension(filePath);
    }


    public virtual string GetFileName(string path)
    {
        return Path.GetFileName(path);
    }


    public virtual string GetFileNameWithoutExtension(string filePath)
    {
        return Path.GetFileNameWithoutExtension(filePath);
    }


    public virtual string[] GetFiles(string directoryPath, string searchPattern = "", bool topDirectoryOnly = true)
    {
        if (string.IsNullOrEmpty(searchPattern))
        {
            searchPattern = "*.*";
        }

        return Directory.GetFiles(directoryPath, searchPattern,
            topDirectoryOnly ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories);
    }


    public virtual DateTime GetLastAccessTime(string path)
    {
        return System.IO.File.GetLastAccessTime(path);
    }


    public virtual DateTime GetLastWriteTime(string path)
    {
        return System.IO.File.GetLastWriteTime(path);
    }


    public virtual DateTime GetLastWriteTimeUtc(string path)
    {
        return System.IO.File.GetLastWriteTimeUtc(path);
    }


    public virtual string GetParentDirectory(string directoryPath)
    {
        return Directory.GetParent(directoryPath).FullName;
    }

    public virtual string GetVirtualPath(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return path;
        }

        if (!IsDirectory(path) && FileExists(path))
        {
            path = new FileInfo(path).DirectoryName;
        }

        path = path?.Replace(WebRootPath, string.Empty).Replace('\\', '/').Trim('/').TrimStart('~', '/');

        return $"~/{path ?? string.Empty}";
    }

    public virtual bool IsDirectory(string path)
    {
        return DirectoryExists(path);
    }


    public virtual string MapPath(string path)
    {
        path = path.Replace("~/", string.Empty).TrimStart('/');

        //如果虚拟路径的末尾有斜线，则应该在将虚拟路径转换为物理路径之后
        var pathEnd = path.EndsWith('/') ? Path.DirectorySeparatorChar.ToString() : string.Empty;

        return Combine(Root ?? string.Empty, path) + pathEnd;
    }


    public virtual byte[] ReadAllBytes(string filePath)
    {
        return System.IO.File.Exists(filePath) ? System.IO.File.ReadAllBytes(filePath) : Array.Empty<byte>();
    }


    public virtual string ReadAllText(string path, Encoding encoding)
    {
        using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var streamReader = new StreamReader(fileStream, encoding);
        return streamReader.ReadToEnd();
    }


    public virtual void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
    {
        System.IO.File.SetLastWriteTimeUtc(path, lastWriteTimeUtc);
    }


    public virtual void WriteAllBytes(string filePath, byte[] bytes)
    {
        System.IO.File.WriteAllBytes(filePath, bytes);
    }


    public virtual void WriteAllText(string path, string contents, Encoding encoding)
    {
        System.IO.File.WriteAllText(path, contents, encoding);
    }


    public new IFileInfo GetFileInfo(string subpath)
    {
        subpath = subpath.Replace(Root, string.Empty);

        return base.GetFileInfo(subpath);
    }


    #endregion

}
