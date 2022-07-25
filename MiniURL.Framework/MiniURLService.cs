using Microsoft.Extensions.Configuration;
using MiniURL.Common;
using MiniURL.Framework.Abstraction;
using System;
using System.Threading.Tasks;

namespace MiniURL.Framework
{
    public class MiniURLService : IMiniURLService
    {

        private readonly IConfiguration _configuration;
        public MiniURLService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> EncryptUrl(string originalUrl)
        {
            var shortHandUrl = await Cryptography.EncryptUrl(originalUrl, Convert.ToInt16(_configuration["ShrinkUrlSettings:MaxLength"]));
            return _configuration["ShrinkUrlSettings:BaseUrl"] + shortHandUrl;
        }
    }
}
