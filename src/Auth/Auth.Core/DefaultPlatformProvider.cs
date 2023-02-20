using System.Collections.Generic;
using System.Reflection;
using CRB.TPM.Auth.Abstractions;

namespace CRB.TPM.Auth.Core;

internal class DefaultPlatformProvider : IPlatformProvider
{
    public string ToDescription(int platform)
    {
        switch (platform)
        {
            case 0:
                return "Web";
            case 1:
                return "Android";
            case 2:
                return "IOS";
            case 3:
                return "PC";
            case 4:
                return "Mobile";
            case 5:
                return "WeChat";
            case 6:
                return "MiniProgram";
            case 7:
                return "Alipay";
            default:
                return "Other";
        }
    }


public List<OptionResultModel> SelectOptions()
    {
        return new List<OptionResultModel>
        {
            new() { Label = "其它", Value = -1 },
            new() { Label = "Web", Value = 0 },
            new() { Label = "安卓", Value = 1 },
            new() { Label = "苹果", Value = 2 },
            new() { Label = "PC", Value = 3 },
            new() { Label = "移动端", Value = 4 },
            new() { Label = "微信", Value = 5 },
            new() { Label = "小程序", Value = 6 },
            new() { Label = "支付宝", Value = 7 },
        };
    }
}