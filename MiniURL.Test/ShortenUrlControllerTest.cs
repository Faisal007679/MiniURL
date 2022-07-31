using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MiniURL.Controllers;
using MiniURL.Framework;
using MiniURL.Models;
using MiniURL.ModelValidator;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MiniURL.Test
{
    public class ShortenUrlControllerTest
    {
        private readonly ShortenUrlController _shortenUrlController;
        private readonly IConfiguration _configuration;
        private readonly MiniURLService _miniURLService;
        private readonly ShortenUrlDataValidator _validator;
        public ShortenUrlControllerTest()
        {
            _configuration = InitConfiguration();
            _validator = new ShortenUrlDataValidator();
            _miniURLService = new MiniURLService(_configuration);

            var mock = new Mock<ILogger<ShortenUrlController>>();
            ILogger<ShortenUrlController> logger = mock.Object;
            _shortenUrlController = new ShortenUrlController(logger, _miniURLService, _validator);
        }

        [Fact]
        public async Task When_Called_Returns_Success_200_Result()
        {
            var data = new ShortenUrlData() { OriginalURL = "https://www.finning.com/welcome/canada/monitor/validate" };
            var objectResult = await _shortenUrlController.Post(data);

            var result = objectResult as OkObjectResult;
            Assert.NotNull(result);

            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public async Task When_Called_Returns_Exception_400_Result()
        {
            var data = new ShortenUrlData() { OriginalURL = string.Empty};
            var objectResult = await _shortenUrlController.Post(data);

            var result = objectResult as BadRequestObjectResult;
            Assert.NotNull(result);

            var response = result.Value as string;
            Assert.NotNull(response);

            Assert.Equal("OriginalURL is required field.", response);
        }


        [Fact]
        public async Task When_Called_Wrong_URL_Returns_Exception_422_Result()
        {
            var data = new ShortenUrlData() { OriginalURL = "htp://google.com" };
            var objectResult = await _shortenUrlController.Post(data);

            var result = objectResult as UnprocessableEntityObjectResult;
            Assert.NotNull(result);

            var response = result.Value as string;
            Assert.NotNull(response);
            
            Assert.Equal("OriginalURL has invalid URI.", result.Value as string);
        }


        [Fact]
        public async Task When_Called_No_Data_Returns_Exception_422_Result()
        {
            var data = new ShortenUrlData();
            var objectResult = await _shortenUrlController.Post(data);

            var result = objectResult as UnprocessableEntityObjectResult;
            Assert.NotNull(result);

            var response = result.Value as string;
            Assert.NotNull(response);
        }

        private static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .Build();
            return config;
        }
    }
}
