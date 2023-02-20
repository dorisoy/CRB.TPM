using CRB.TPM.TaskScheduler.Abstractions.Quartz;
using CRB.TPM.Utils.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.TaskScheduler.Core.Quartz;

/// <summary>
/// 表示Quartz模块集合
/// </summary>
public class QuartzModuleCollection : CollectionAbstract<QuartzModuleDescriptor>, IQuartzModuleCollection
{

}
