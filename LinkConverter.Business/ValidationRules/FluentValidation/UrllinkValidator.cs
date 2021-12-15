using FluentValidation;
using LinkConverter.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkConverter.Business.ValidationRules.FluentValidation
{
    public class UrllinkValidator : AbstractValidator<Urllink>
    {
        public UrllinkValidator()
        {
            RuleFor(u => u.WebUrl).NotEmpty().WithMessage("Web Url girmelisiniz!");
            RuleFor(u => u.Deeplink).NotEmpty().WithMessage("Dönüştürülmüş bir deeplink gönderilmedi!");
            RuleFor(u => u.WebUrl).Must(StartWithWWW).WithMessage("Url www.trendyol.com ile veya http://www.trendyol.com ile başlamalıdır");
            RuleFor(u => u.Deeplink).Must(StartWithTy).WithMessage("Deeplink dönüş değeri ty://?Page ile başlamalı");

            bool StartWithWWW(string arg)
            {
                return arg.StartsWith("www.trendyol.com") || arg.StartsWith("https://www.trendyol.com");
            }

            bool StartWithTy(string arg)
            {
                return arg.StartsWith("ty://?Page=");
            }

        }
    }
}
