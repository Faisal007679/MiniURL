using System.Threading.Tasks;
using Xunit;

namespace MiniURL.Common.Test
{
    public class CryptographyTest
    {
        [Fact]
        public async Task When_Called_Returns_String_Result()
        {
            string originalUrl = "https://www.finning.com/welcome/canada/monitor/validate";
            int maxLength = 6;
            var stringResult = await Cryptography.EncryptUrl(originalUrl, maxLength);
            // Assert
            Assert.IsType<string>(stringResult);
        }

        [Fact]
        public async Task When_Called_Returns_Correct_Result()
        {
            string originalUrl = "https://www.finning.com/welcome/canada/monitor/validate";
            int maxLength = 6;
            string shortUrl = "https://example.co/ysrAXm";
            var stringResult = await Cryptography.EncryptUrl(originalUrl, maxLength);
            // Assert
            Assert.Equal("https://example.co/" + stringResult, shortUrl);
        }
    }
}
