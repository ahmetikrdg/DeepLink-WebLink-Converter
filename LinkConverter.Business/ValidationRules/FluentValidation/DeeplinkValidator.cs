using FluentValidation;
using LinkConverter.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkConverter.Business.ValidationRules.FluentValidation
{
    public class DeeplinkValidator : AbstractValidator<Deeplink>
    {
        public DeeplinkValidator()
        {
            RuleFor(d => d.Deeplinks).NotEmpty().WithMessage("Deeplink alanı boş geçilemez!");
            RuleFor(d => d.WebUrl).NotEmpty().WithMessage("Dönüştürülmüş bir weblink gönderilmedi!");
            RuleFor(d => d.Deeplinks).Must(StartWithTy).WithMessage("Deeplink dönüş değeri ty://?Page ile başlamalı!");
            RuleFor(d => d.WebUrl).Must(StartWithWWW).WithMessage("Url www.trendyol.com ile veya http://www.trendyol.com ile başlamalıdır!");

            bool StartWithTy(string arg)
            {
                return arg.StartsWith("ty://?Page=");
            }

            bool StartWithWWW(string arg)
            {
                return arg.StartsWith("www.trendyol.com") || arg.StartsWith("https://www.trendyol.com");
            }
        }
    }
}
