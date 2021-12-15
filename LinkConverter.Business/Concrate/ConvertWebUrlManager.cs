using FluentValidation;
using LinkConverter.Business.Abstract;
using LinkConverter.Business.ConvertingMethods.WeburlConvertToDeeplink;
using LinkConverter.Business.ValidationRules.FluentValidation;
using LinkConverter.Core.Aspect.Autofac.Validation;
using LinkConverter.Core.CrossCuttingConcerns.Validation;
using LinkConverter.Core.Utilities.Business;
using LinkConverter.Core.Utilities.Results;
using LinkConverter.Data.Abstract;
using LinkConverter.Data.Concrate;
using LinkConverter.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkConverter.Business.Concrate
{
    public class ConvertWebUrlManager : IConverterWebUrlServices
    {
        IConverterWebUrlRepository _converterWebUrlRepository;
        IWebUrlToDeeplinkConvert _webUrlToDeeplinkConvert;

        public ConvertWebUrlManager(IConverterWebUrlRepository converterWebUrlRepository, IWebUrlToDeeplinkConvert webUrlToDeeplinkConvert)
        {
            _converterWebUrlRepository = converterWebUrlRepository;
            _webUrlToDeeplinkConvert = webUrlToDeeplinkConvert;
        }


        public string ConvertToDeeplink(string url)
        {
            return url.Contains("-p-") ? _webUrlToDeeplinkConvert.LinkDetailConvertToDeeplink(url) :
                url.Contains("/sr?q") ? _webUrlToDeeplinkConvert.SearchConverterToDeeplink(url) : "ty://?Page=Home";
        }

        [ValidationAspect(typeof(UrllinkValidator))]
        public IResult Create(Urllink entity)
        {
            _converterWebUrlRepository.Create(entity);
            return new SuccessResult("Ekleme İşlemi Başarılı");
        }

        public IDataResult<List<Urllink>> GetAll()
        {
            return new SuccessDataResult<List<Urllink>>(_converterWebUrlRepository.GetList().ToList());
        }

        public IDataResult<Urllink> GetByUrlCon(string webUrl)
        {
            IDataResult<Urllink> result = (IDataResult<Urllink>)BusinessRules.Run(CheckIfGetByUrl(webUrl));
            return result != null ? result : new SuccessDataResult<Urllink>(_converterWebUrlRepository.GetList(u => u.WebUrl == webUrl).FirstOrDefault());
        }

        //------ Business Code
        private IDataResult<Urllink> CheckIfGetByUrl(string url)
        {

            var result = _converterWebUrlRepository.GetList(x => x.WebUrl == url).Count();
            return result <= 0 ? new ErrorDataResult<Urllink>("Aranan Url Db'de Bulunamadı.") : new SuccessDataResult<Urllink>();
        }




    }
}
