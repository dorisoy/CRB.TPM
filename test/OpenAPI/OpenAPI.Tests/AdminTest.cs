namespace OpenAPI.Tests
{
    using CRB.TPM.Mod.Admin;
    using CommonClient = CRB.TPM.Mod.Admin.CommonClient;

    public class AdminTest
    {
        protected CommonClient _commonClient;

        [SetUp]
        public void Setup()
        {
            var http = new HttpClient(new HttpClientHandler());
            http.DefaultRequestHeaders.Add("ApiKey", "8e421ff965cb4935ba56ef7833bf4750");
            _commonClient = new CommonClient(http)
            {
                ReadResponseAsString = true
            };

        }

        [Test]
        public async Task AccountClientTest()
        {
            var result = await _commonClient.AccountTypeSelectAsync();
            Assert.True(result.Successful);
        }
    }
}