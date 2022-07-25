using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Xunit;

namespace MiniURL.Framework.Test
{
    public class MiniURLServiceTest
    {
        private readonly MiniURLService _miniURLService;
        private readonly IConfiguration _configuration;
        public MiniURLServiceTest()
        {
            _configuration = InitConfiguration();
            _miniURLService = new MiniURLService(_configuration);
        }

        [Fact]
        public async Task When_Called_Returns_String_Result()
        {
            string originalUrl = "https://www.finning.com/welcome/canada/monitor/validate";
            var stringResult = await _miniURLService.EncryptUrl(originalUrl);
            // Assert
            Assert.IsType<string>(stringResult);
        }

        [Fact]
        public async Task When_Called_Returns_Correct_Result()
        {
            string originalUrl = "https://www.finning.com/welcome/canada/monitor/validate";
            string shortUrl = "https://example.co/ysrAXm";
            var stringResult = await _miniURLService.EncryptUrl(originalUrl);
            // Assert
            Assert.Equal(stringResult, shortUrl);
        }

        private static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }
    }
}
