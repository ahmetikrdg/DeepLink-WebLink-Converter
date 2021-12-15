using LinkConverter.Business.Abstract;
using LinkConverter.Business.ConvertingMethods.DeepUrlConvertToWeburl;
using LinkConverter.Business.ValidationRules.FluentValidation;
using LinkConverter.Core.Aspect.Autofac.Validation;
using LinkConverter.Core.Utilities.Business;
using LinkConverter.Core.Utilities.Results;
using LinkConverter.Data.Abstract;
using LinkConverter.Data.Concrate;
using LinkConverter.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkConverter.Business.Concrate
{
    public class ConvertDeepManager : IConverterDeepServices
    {
        IConverterDeepRepository _converterDeepRepository;
        IDeeplinkConvertToWebUrl _deeplinkConvertToWebUrl;

        public ConvertDeepManager(IConverterDeepRepository converterDeepRepository, IDeeplinkConvertToWebUrl deeplinkConvertToWebUrl)
        {
            _converterDeepRepository = converterDeepRepository;
            _deeplinkConvertToWebUrl = deeplinkConvertToWebUrl;
        }
  
        public string ConvertToWeburl(string deepLink)
        {
            return deepLink.Contains("Product&ContentId") ? _deeplinkConvertToWebUrl.DeeplinkConverterToWeburl(deepLink) :
               deepLink.Contains("Search&Query") ? _deeplinkConvertToWebUrl.SearchConverterToWeblink(deepLink) : "https://www.trendyol.com";

        }

        [ValidationAspect(typeof(DeeplinkValidator))]
        public IResult Create(Deeplink entity)
        {
            _converterDeepRepository.Create(entity);        
            return new SuccessResult("Ekleme İşlemi Başarılı");
        }

        public IDataResult<List<Deeplink>> GetAll()
        {
            return new SuccessDataResult<List<Deeplink>>(_converterDeepRepository.GetList().ToList());
        }

        public IDataResult<Deeplink> GetByUrl(string url)
        {
            IDataResult<Deeplink> result = (IDataResult<Deeplink>)BusinessRules.Run(CheckIfGetByUrl(url));
            return result != null ? result : new SuccessDataResult<Deeplink>(_converterDeepRepository.GetList(d => d.Deeplinks == url).FirstOrDefault());
        }

        //------ Business Code
        private IDataResult<Deeplink> CheckIfGetByUrl(string url)
        {
            var result = _converterDeepRepository.GetList(x => x.Deeplinks == url).Count();
            return result <= 0 ? new ErrorDataResult<Deeplink>("Aranan Url Db'de Bulunamadı.") : new SuccessDataResult<Deeplink>();
        }

    }
}
