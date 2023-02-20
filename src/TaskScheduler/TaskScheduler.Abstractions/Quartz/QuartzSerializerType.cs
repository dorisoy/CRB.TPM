using System.ComponentModel;

namespace CRB.TPM.TaskScheduler.Abstractions.Quartz;

public enum QuartzSerializerType
{
    [Description("JSON")]
    Json,
    [Description("XML")]
    Xml
}
