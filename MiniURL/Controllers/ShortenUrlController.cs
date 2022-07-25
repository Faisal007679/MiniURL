using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniURL.Framework.Abstraction;
using MiniURL.Models;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniURL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortenUrlController : ControllerBase
    {
        private readonly ILogger<ShortenUrlController> _logger;
        private readonly IMiniURLService _miniURLService;
        private readonly IValidator<ShortenUrlData> _shortenUrlDataValidator;
        public ShortenUrlController(ILogger<ShortenUrlController> logger,
                                    IMiniURLService miniURLService,
                                    IValidator<ShortenUrlData> shortenUrlDataValidator)
        {
            _logger = logger;
            _miniURLService = miniURLService;
            _shortenUrlDataValidator = shortenUrlDataValidator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ShortenUrlData data)
        {
            try
            {
                _logger.LogInformation("ShortenUrlController POST execution started.");
                var validationResult = await _shortenUrlDataValidator.ValidateAsync(data);
                if (!validationResult.IsValid)
                {
                    _logger.LogInformation($"ShortenUrlController POST model validation failed : {JsonSerializer.Serialize(validationResult.Errors)}");
                    switch (validationResult.Errors.Select(x => x.ErrorCode).FirstOrDefault())
                    {
                        case "400":
                            throw new ArgumentException(validationResult.Errors.Where(x => x.ErrorCode == "400").FirstOrDefault().ErrorMessage);
                        case "403":
                            throw new UriFormatException(validationResult.Errors.Where(x => x.ErrorCode == "403").FirstOrDefault().ErrorMessage);
                        default:
                            throw new Exception(validationResult.Errors.FirstOrDefault().ErrorMessage);
                    }
                }

                _logger.LogInformation("ShortenUrlController POST model validation passed.");
                var shortHandUrl = await _miniURLService.EncryptUrl(data.OriginalURL);
                _logger.LogInformation("ShortenUrlController POST execution ended.");
                return Ok(shortHandUrl);
            }
            catch (ArgumentException ae)
            {
                _logger.LogError($"ShortenUrlController POST ArgumentException: {ae.Message}");
                return BadRequest(ae.Message);
            }
            catch (UriFormatException ufe)
            {
                _logger.LogError($"ShortenUrlController POST UriFormatException: {ufe.Message}");
                return UnprocessableEntity(ufe.Message);
            }
            catch (Exception e)
            {
                _logger.LogError($"ShortenUrlController POST Exception: {e.Message}");
                return UnprocessableEntity(e.Message);
            }
        }
    }
}
