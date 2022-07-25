using FluentValidation;
using MiniURL.Models;

namespace MiniURL.ModelValidator
{
    public class ShortenUrlDataValidator : AbstractValidator<ShortenUrlData>
    {
        public ShortenUrlDataValidator()
        {
            RuleFor(request => request.OriginalURL).NotNull()
                                                   .NotEmpty()
                                                   .WithErrorCode("400")
                                                   .WithMessage("OriginalURL is required field.");

            RuleFor(request => request.OriginalURL).Matches(@"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?")
                                                   .WithErrorCode("403")
                                                   .WithMessage("OriginalURL has invalid URI.");
        }
    }
}
