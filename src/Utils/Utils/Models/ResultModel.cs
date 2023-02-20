using System;
using System.Threading.Tasks;


namespace CRB.TPM;


/// <summary>
/// 公共错误码
/// </summary>
public enum CommonErrorCode : int
{
    /// <summary>
    /// 请求成功
    /// </summary>
    SuccessfulOperation = 10000,
    /// <summary>
    /// 操作失败
    /// </summary>
    FailedOperation = 40000,
    /// <summary>
    /// 内部错误
    /// </summary>
    InternalError = 40001,
    /// <summary>
    /// 接口不存在
    /// </summary>
    InvalidAction = 40002,
    /// <summary>
    /// 接口已下线
    /// </summary>
    ActionOffline = 40003,
    /// <summary>
    ///  请求头部的 Authorization 不符标准
    /// </summary>
    AuthFailure_InvalidAuthorization = 20000,
    /// <summary>
    ///  密钥非法（不是API 密钥类型）
    /// </summary>
    AuthFailure_InvalidSecretId = 20001,
    /// <summary>
    /// 密钥不存在。请在 控制台 检查密钥是否已被删除或者禁用，如状态正常，请检查密钥是否填写正确，注意前后不得有空格
    /// </summary>
    AuthFailure_SecretIdNotFound = 20002,
    /// <summary>
    /// 签名过期。Timestamp 和服务器时间相差不得超过五分钟，请检查本地时间是否和标准时间同步
    /// </summary>
    AuthFailure_SignatureExpire = 20003,
    /// <summary>
    /// 签名错误。签名计算错误，请对照调用方式中的签名方法文档检查签名计算过程
    /// </summary>
    AuthFailure_SignatureFailure = 20004,
    /// <summary>
    ///  token 错误
    /// </summary>
    AuthFailure_TokenFailure = 20005,
    /// <summary>
    /// 请求未授权
    /// </summary>
    AuthFailure_UnauthorizedOperation = 20006,
    /// <summary>
    /// 参数错误（包括参数格式、类型等错误）
    /// </summary>
    InvalidParameter = 30000,
    /// <summary>
    /// 参数取值错误
    /// </summary>
    InvalidParameterValue = 30001,
    /// <summary>
    /// 请求 body 的 multipart 格式错误
    /// </summary>
    InvalidRequest = 30002,
    /// <summary>
    /// IP地址在黑名单中
    /// </summary>
    IpInBlacklist = 300013,
    /// <summary>
    ///  IP地址不在白名单中
    /// </summary>
    IpNotInWhitelist = 30004,
    /// <summary>
    /// 超过配额限制
    /// </summary>
    LimitExceeded = 30005,
    /// <summary>
    /// 缺少参数
    /// </summary>
    MissingParameter = 30006,
    /// <summary>
    /// 接口版本不存在
    /// </summary>
    NoSuchVersion = 30007,
    /// <summary>
    /// 请求的次数超过了频率限制
    /// </summary>
    RequestLimitExceeded = 30008,
    /// <summary>
    /// 主账号超过频率限制
    /// </summary>
    RequestLimitExceeded_GlobalRegionUinLimitExceeded = 30009,
    /// <summary>
    /// IP限频
    /// </summary>
    RequestLimitExceeded_IPLimitExceeded = 50000,
    /// <summary>
    /// 主账号限频
    /// </summary>
    RequestLimitExceeded_UinLimitExceeded = 50001,
    /// <summary>
    /// 请求包超过限制大小
    /// </summary>
    RequestSizeLimitExceeded = 50002,
    /// <summary>
    /// 资源被占用
    /// </summary>
    ResourceInUse = 50003,
    /// <summary>
    /// 资源不足
    /// </summary>
    ResourceInsufficient = 50004,
    /// <summary>
    /// 资源不存在
    /// </summary>
    ResourceNotFound = 500015,
    /// <summary>
    /// 资源不可用
    /// </summary>
    ResourceUnavailable = 50006,
    /// <summary>
    /// 返回包超过限制大小
    /// </summary>
    ResponseSizeLimitExceeded = 50007,
    /// <summary>
    /// 当前服务暂时不可用
    /// </summary>
    ServiceUnavailable = 500018,
    /// <summary>
    /// 未授权操作
    /// </summary>
    UnauthorizedOperation = 50009,
    /// <summary>
    /// 未知参数错误，用户多传未定义的参数会导致错误
    /// </summary>
    UnknownParameter = 60000,
    /// <summary>
    /// 操作不支持
    /// </summary>
    UnsupportedOperation = 60001,
    /// <summary>
    /// http(s) 请求协议错误，只支持 GET 和 POST 请求
    /// </summary>
    UnsupportedProtocol = 60002,
    /// <summary>
    /// 接口不支持所传地域
    /// </summary>
    UnsupportedRegion = 60003,
    /// <summary>
    /// 参数格式错误
    /// </summary>
    InvalidParameter_FormatError = 60004,
    /// <summary>
    /// 非法的后端ip地址
    /// </summary>
    InvalidParameterValue_IllegalProxyIp = 60005,
    /// <summary>
    /// 密钥错误
    /// </summary>
    InvalidParameterValue_InvalidAccessKeyIds = 60006,
    /// <summary>
    /// 传入的Api业务类型必须为OAUTH
    /// </summary>
    InvalidParameterValue_InvalidApiBusinessType = 60007,
    /// <summary>
    ///  API Id错误
    /// </summary>
    InvalidParameterValue_InvalidApiIds = 60008,
    /// <summary>
    /// 无效的API配置
    /// </summary>
    InvalidParameterValue_InvalidApiRequestConfig = 60009,
    /// <summary>
    /// API类型错误，微服务API只支持TSF后端服务类型
    /// </summary>
    InvalidParameterValue_InvalidApiType = 70000,
    /// <summary>
    /// 后端服务路径配置错误
    /// </summary>
    InvalidParameterValue_InvalidBackendPath = 70001,
    /// <summary>
    /// 不合法的常量参数
    /// </summary>
    InvalidParameterValue_InvalidConstantParameters = 70002,
    /// <summary>
    /// 服务当前环境状态，不支持此操作
    /// </summary>
    InvalidParameterValue_InvalidEnvStatus = 70003,
    /// <summary>
    /// 参数取值错误
    /// </summary>
    InvalidParameterValue_InvalidFilterNotSupportedName = 700004,
    /// <summary>
    /// 方法错误。仅支持 ANY, BEGIN, GET, POST, DELETE, HEAD, PUT, OPTIONS, TRACE, PATCH，请修改后重新操作
    /// </summary>
    InvalidParameterValue_InvalidMethod = 70005,
    /// <summary>
    /// 不合法的请求参数
    /// </summary>
    InvalidParameterValue_InvalidRequestParameters = 70006
}

/// <summary>
/// 业务错误码
/// </summary>
public enum BusinessErrorCode
{

    //TODO... 这里扩展
}

/// <summary>
/// 返回结果
/// </summary>
public class ResultModel<T> : IResultModel<T>
{
    /// <summary>
    /// 处理是否成功
    /// </summary>
    public bool Successful { get; private set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public string Msg { get; private set; }

    /// <summary>
    /// 业务码
    /// </summary>
    public string Code { get; set; } 

    /// <summary>
    /// 时间戳
    /// </summary>
    public long Timestamp { get; private set; }

    /// <summary>
    /// 返回数据
    /// </summary>
    public T Data { get; private set; }

    /// <summary>
    /// 成功
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="msg">说明</param>
    public ResultModel<T> Success(T data = default, string msg = "success")
    {
        Successful = true;
        Data = data;
        Msg = msg;
        Code = $"{CommonErrorCode.SuccessfulOperation.ToInt()}";
        return this;
    }

    /// <summary>
    /// 失败
    /// </summary>
    /// <param name="msg">说明</param>
    public ResultModel<T> Failed(string msg = "failed")
    {
        Successful = false;
        Msg = msg;
        Code = $"{CommonErrorCode.FailedOperation.ToInt()}";
        return this;
    }


    public ResultModel()
    {
        Timestamp = DateTime.Now.ToTimestamp();
    }


}

/// <summary>
/// 返回结果
/// </summary>
public static partial class ResultModel
{
    /// <summary>
    /// 成功
    /// </summary>
    /// <param name="data">返回数据</param>
    /// <returns></returns>
    public static IResultModel<T> Success<T>(T data = default)
    {
        return new ResultModel<T>().Success(data);
    }

    /// <summary>
    /// 成功
    /// </summary>
    /// <returns></returns>
    public static IResultModel Success()
    {
        return Success<string>();
    }

    /// <summary>
    /// 失败
    /// </summary>
    /// <param name="error">错误信息</param>
    /// <returns></returns>
    public static IResultModel<T> Failed<T>(string error = null)
    {
        return new ResultModel<T>().Failed(error ?? "failed");
    }

    /// <summary>
    /// 失败
    /// </summary>
    /// <returns></returns>
    public static IResultModel Failed(string error = null)
    {
        return Failed<string>(error);
    }

    /// <summary>
    /// 根据布尔值返回结果
    /// </summary>
    /// <param name="success"></param>
    /// <returns></returns>
    public static IResultModel<T> Result<T>(bool success)
    {
        return success ? Success<T>() : Failed<T>();
    }

    /// <summary>
    /// 根据布尔值返回结果
    /// </summary>
    /// <param name="success"></param>
    /// <returns></returns>
    public static async Task<IResultModel> Result(Task<bool> success)
    {
        return await success ? Success() : Failed();
    }

    /// <summary>
    /// 根据布尔值返回结果
    /// </summary>
    /// <param name="success"></param>
    /// <returns></returns>
    public static IResultModel Result(bool success)
    {
        return success ? Success() : Failed();
    }

    /// <summary>
    /// 数据已存在
    /// </summary>
    /// <returns></returns>
    public static IResultModel HasExists => Failed("数据已存在");

    /// <summary>
    /// 数据不存在
    /// </summary>
    public static IResultModel NotExists => Failed("数据不存在");
}