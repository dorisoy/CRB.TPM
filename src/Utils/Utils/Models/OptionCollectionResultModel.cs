using CRB.TPM.Utils.Abstracts;


namespace CRB.TPM;

/// <summary>
/// 选项集合模型
/// </summary>
public class OptionCollectionResultModel<T> : CollectionAbstract<OptionResultModel<T>>
{
}

/// <summary>
/// 选项集合模型
/// </summary>
public class OptionCollectionResultModel : OptionCollectionResultModel<object>
{

}