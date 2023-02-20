namespace CRB.TPM.Data.Sharding
{
    internal class IdHelper
    {
        internal static IIdGenerator IdGenInstance = new DefaultIdGenerator(new IdGeneratorOptions() { WorkerId = 0 });
    }
}
