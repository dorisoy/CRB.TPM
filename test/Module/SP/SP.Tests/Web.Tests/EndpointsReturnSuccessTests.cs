using CRB.TPM.Data.Sharding;
using Microsoft.AspNetCore.Mvc.Testing;
using SP.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace SP.Tests.Web.Tests
{
    public class EndpointsReturnSuccessTests : BasicTests
    {
        public EndpointsReturnSuccessTests(WebTestFixture factory, ITestOutputHelper output) : base(factory, output)
        {
            Login("admin", "123456").Wait();
        }

        [Theory]
        [InlineData("/api/SP/SP_ConfigBusinessItemL1/Query")]
        [InlineData("/api/SP/SP_ConfigBusinessItemL2/Query")]
        public async Task Get_EndpointsReturnSuccess(string url)
        {
            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
