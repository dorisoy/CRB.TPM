using System.Text.Json;
using CRB.TPM;
using Xunit;

namespace Utils.Tests.Models
{
    public class ResultModelTests
    {
        [Fact]
        public void SuccessTest()
        {
            var result = ResultModel.Success();
            var json = JsonSerializer.Serialize(result);

            Assert.NotNull(json);
        }
    }
}
