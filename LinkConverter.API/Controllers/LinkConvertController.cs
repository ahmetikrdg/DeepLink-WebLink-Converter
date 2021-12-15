using LinkConverter.Business.Abstract;
using LinkConverter.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkConverter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkConvertController : ControllerBase
    {
        private IConverterWebUrlServices _converterWebUrlServices;
        private IConverterDeepServices _converterDeepServices;

        public LinkConvertController(IConverterDeepServices converterDeepServices, IConverterWebUrlServices converterWebUrlServices)
        {
            _converterWebUrlServices = converterWebUrlServices;
            _converterDeepServices = converterDeepServices;
        }

        [HttpGet("UrlConvertToDeeplink")]
        public IActionResult UrlConvertToDeeplink(string webUrl)
        {

            var urlControl = _converterWebUrlServices.GetByUrlCon(webUrl);
            if (urlControl.Success)
            {
                return Ok(urlControl);
            }

            var convert = _converterWebUrlServices.ConvertToDeeplink(webUrl);
            _converterWebUrlServices.Create(new Urllink { WebUrl = webUrl, Deeplink = convert });

            return Ok(convert);

        }

        [HttpGet("DeepLinkConvertToWebUrl")]
        public IActionResult DeeplinkConvertToWebUrl(string deeplinkUrl)
        {

            var urlControl = _converterDeepServices.GetByUrl(deeplinkUrl);
            if (urlControl.Success)
            {
                return Ok(urlControl);
            }

            var convert = _converterDeepServices.ConvertToWeburl(deeplinkUrl);
            _converterDeepServices.Create(new Deeplink { Deeplinks = deeplinkUrl, WebUrl = convert });

            return Ok(convert);

        }

        [HttpGet("WebUrlAllData")]
        public IActionResult WebUrlAllData()
        {
            var result = _converterWebUrlServices.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("DeeplinkAllData")]
        public IActionResult DeepLinkAllData()
        {
            var result = _converterDeepServices.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }


    }


}
