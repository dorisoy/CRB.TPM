using Microsoft.Extensions.DependencyInjection;
using CRB.TPM.Utils.Annotations;
using Xunit;

namespace Utils.Tests.Extensions
{
    public class ServiceProviderExtensionsTests : BaseTest
    {
        [Fact]
        public void GetStartWithTest()
        {
            var test = ServiceProvider.GetStartWith<ITest>("Dorisoy");
            Assert.Equal("Dorisoy", test.Test());
        }
    }

    public interface ITest
    {
        string Test();
    }

    [SingletonInject]
    public class DorisoyTest : ITest
    {
        public string Test()
        {
            return "Dorisoy";
        }
    }

    [SingletonInject]
    public class LaoliTest : ITest
    {
        public string Test()
        {
            return "LAOLI";
        }
    }
}
