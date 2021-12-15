using LinkConverter.API.Controllers;
using LinkConverter.Business.Abstract;
using LinkConverter.Core.Utilities.Results;
using LinkConverter.Data.Abstract;
using LinkConverter.Entity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LinkConverter.Test
{
    public class LinkConverterController
    {
        private readonly Mock<IConverterDeepServices> _mockRepoDeeplink;
        private readonly Mock<IConverterWebUrlServices> _mockRepoWeburl;
        private readonly LinkConvertController _controller;
        private List<Urllink> urllinks;
        private List<Deeplink> deeplinks;

        public LinkConverterController()
        {
            _mockRepoDeeplink = new Mock<IConverterDeepServices>();
            _mockRepoWeburl = new Mock<IConverterWebUrlServices>();
            _controller = new LinkConvertController(_mockRepoDeeplink.Object, _mockRepoWeburl.Object);

            urllinks = new List<Urllink>()
            {
                new Urllink{Id=1,WebUrl="https://www.trendyol.com/casio/erkek-kol-saati-p-1925865?boutiqueId=439892?merchantId=105064",Deeplink="ty://?Page=Product&ContentId=1925865&CampaignId=439892&MerchantId=105064"},
                new Urllink{Id=2,WebUrl="https://www.trendyol.com/casio/erkek-kol-saati-p-1925865",Deeplink="ty://?Page=Product&ContentId=1925865"}
            };

            deeplinks = new List<Deeplink>()
            {
                new Deeplink{Id=1,Deeplinks="ty://?Page=Product&ContentId=1925865 ",WebUrl="https://www.trendyol.com/brand/name-p1925865"},
                new Deeplink{Id=1,Deeplinks="ty://?Page=Product&ContentId=1925865&CampaignId=439892&MerchantId=105064",WebUrl="https://www.trendyol.com/brand/name-p1925865?boutiqueId=439892&merchantId=105064"}
            };
        }


        [Theory]
        [InlineData(null)]
        public void UrlConvertToDeepLink_UrlIsNull_ReturnIsNullValue(string webUrl)
        {
            _mockRepoWeburl.Setup(x => x.GetByUrlCon(webUrl)).Returns(new ErrorDataResult<Urllink>());
            var result = _controller.UrlConvertToDeeplink(webUrl);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Null(okResult.Value);        
        }

        [Theory]
        [InlineData("https://www.trendyol.com/casio/erkek-kol-saati-p-1925865?boutiqueId=439892?merchantId=105064")]
        public void UrlConvertToDeepLink_UrlIsFully_ReturnGetByStatusCode(string webUrl)
        {
            _mockRepoWeburl.Setup(x => x.GetByUrlCon(webUrl)).Returns(new SuccessDataResult<Urllink>(urllinks.First()));
            var result = _controller.UrlConvertToDeeplink(webUrl);

            var okResult = Assert.IsType<OkObjectResult>(result);       

            Assert.Equal(200, okResult.StatusCode);
        }

        string convertDeeplink = "ty://?Page=Product&ContentId=1925865&CampaignId=439892";
        [Theory]
        [InlineData("https://www.trendyol.com/casio/erkek-kol-saatip-1925865?boutiqueId=439892")]
        public void UrlConvertToDeepLink_UrlIsFully_ReturnDeeplink(string webUrl)
        {
            _mockRepoWeburl.Setup(x => x.GetByUrlCon(webUrl)).Returns(new ErrorDataResult<Urllink>());
            _mockRepoWeburl.Setup(x => x.ConvertToDeeplink(webUrl)).Returns(convertDeeplink);

            var result = _controller.UrlConvertToDeeplink(webUrl);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same("ty://?Page=Product&ContentId=1925865&CampaignId=439892", okResult.Value);
        }


        [Theory]
        [InlineData("https://www.trendyol.com/casio/erkek-kol-saatip-1925865?boutiqueId=439892")]
        public void UrlConvertToDeepLink_AddedDatabase_ReturnOkeyObjectResult(string webUrl)
        {
            _mockRepoWeburl.Setup(x => x.GetByUrlCon(webUrl)).Returns(new ErrorDataResult<Urllink>());
            _mockRepoWeburl.Setup(x => x.ConvertToDeeplink(webUrl)).Returns(convertDeeplink);
            _mockRepoWeburl.Setup(x => x.Create(new Urllink { WebUrl = webUrl, Deeplink = convertDeeplink }));
            var result = _controller.UrlConvertToDeeplink(webUrl);
            Assert.IsType<OkObjectResult>(result);
        }   


        [Theory]
        [InlineData(null)]  
        public void DeeplinkConvertToWebUrl_UrlIsNull_ReturnIsNullValue(string deeplinkUrl)
        {
            _mockRepoDeeplink.Setup(x => x.GetByUrl(deeplinkUrl)).Returns(new ErrorDataResult<Deeplink>());

            var result = _controller.DeeplinkConvertToWebUrl(deeplinkUrl);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Null(okResult.Value);
        }

                
        [Theory]
        [InlineData("ty://?Page=Product&ContentId=1925865")]
        public void DeeplinkConvertToWebUrl_UrlIsFully_ReturnGetByStatusCode(string webUrl)
        {
            _mockRepoDeeplink.Setup(x => x.GetByUrl(webUrl)).Returns(new SuccessDataResult<Deeplink>(deeplinks.First()));
            var result = _controller.DeeplinkConvertToWebUrl(webUrl);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }


        string convertWeburlResponse = "https://www.trendyol.com/brand/name-p1925865?boutiqueId=439892&merchantId=105064";
        [Theory]
        [InlineData("ty://?Page=Product&ContentId=1925865&CampaignId=439892&MerchantId=105064")]
        public void DeeplinkConvertToWebUrl_UrlIsFully_ReturnOkeyObjectResult(string webUrl)
        {
            _mockRepoDeeplink.Setup(x => x.GetByUrl(webUrl)).Returns(new ErrorDataResult<Deeplink>());
            _mockRepoDeeplink.Setup(x => x.ConvertToWeburl(webUrl)).Returns(convertWeburlResponse);
            var result = _controller.DeeplinkConvertToWebUrl(webUrl);
            Assert.IsType<OkObjectResult>(result);
        }


        [Theory]
        [InlineData("ty://?Page=Product&ContentId=1925865&CampaignId=439892&MerchantId=105064")]
        public void DeeplinkConvertToWebUrl_AddedDatabase_ReturnOkeyObjectResult(string webUrl)
        {
            _mockRepoDeeplink.Setup(x => x.GetByUrl(webUrl)).Returns(new ErrorDataResult<Deeplink>());
            _mockRepoDeeplink.Setup(x => x.ConvertToWeburl(webUrl)).Returns(convertWeburlResponse);
            _mockRepoDeeplink.Setup(x => x.Create(deeplinks.Last()));
            var result = _controller.DeeplinkConvertToWebUrl(webUrl);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void GetWeburl_ActionExecutes_ReturnOkeyObjectResult()
        {
            _mockRepoWeburl.Setup(x => x.GetAll()).Returns(new SuccessDataResult<List<Urllink>>(urllinks));
            var result = _controller.WebUrlAllData();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetDeeplink_ActionExecutes_ReturnOkeyObjectResult()
        {
            _mockRepoDeeplink.Setup(x => x.GetAll()).Returns(new SuccessDataResult<List<Deeplink>>(deeplinks));
            var result = _controller.DeepLinkAllData();
            var okResult = Assert.IsType<OkObjectResult>(result);
        }
                
    }
}
