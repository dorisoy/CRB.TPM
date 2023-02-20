namespace CRB.TPM.Auth.Abstractions
{
    /// <summary>
    /// 平台类型集合
    /// </summary>
    public class PlatformCollection
    {
        /// <summary>
        /// Web平台
        /// </summary>
        public PlatformDescriptor Web { get; }

        /// <summary>
        /// Android平台
        /// </summary>
        public PlatformDescriptor Android { get; }

        /// <summary>
        /// IOS苹果
        /// </summary>
        public PlatformDescriptor IOS { get; }

        /// <summary>
        /// PC端
        /// </summary>
        public PlatformDescriptor PC { get; }

        /// <summary>
        /// 移动端
        /// </summary>
        public PlatformDescriptor Mobile { get; }

        /// <summary>
        /// 微信
        /// </summary>
        public PlatformDescriptor WeChat { get; }

        /// <summary>
        /// 小程序
        /// </summary>
        public PlatformDescriptor MiniProgram { get; }

        /// <summary>
        /// 支付宝
        /// </summary>
        public PlatformDescriptor Alipay { get; }

        /// <summary>
        /// 未知
        /// </summary>
        public PlatformDescriptor UnKnown { get; }



        public PlatformCollection()
        {
            UnKnown = new() { Name = "未知", Value = -1 };
            Web = new() { Name = "Web", Value = 0 };
            Android = new() { Name = "安卓", Value = 1 };
            IOS = new() { Name = "苹果", Value = 2 };
            PC = new() { Name = "PC", Value = 3 };
            Mobile = new() { Name = "移动端", Value = 4 };
            WeChat = new() { Name = "微信", Value = 5 };
            MiniProgram = new() { Name = "小程序", Value = 6 };
            Alipay = new() { Name = "支付宝", Value = 7 };
        }
    }

    /// <summary>
    /// 平台描述
    /// </summary>
    public class PlatformDescriptor
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public int Value { get; set; }
    }
}
