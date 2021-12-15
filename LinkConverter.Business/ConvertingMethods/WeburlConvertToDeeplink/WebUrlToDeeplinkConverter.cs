using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkConverter.Business.ConvertingMethods.WeburlConvertToDeeplink
{
    public class WebUrlToDeeplinkConverter: IWebUrlToDeeplinkConvert
    {   

        public string LinkDetailConvertToDeeplink(string url) //Creates a new url from scratch based on the incoming url value
        {
            string baseDetailUrl = "ty://?Page=Product&ContentId=";
            var convertUrl = new StringBuilder();
            var sectionalUrl = url.Split("?"); //Split ? with before and after data is separated
            string productContentId = sectionalUrl[0].Substring(sectionalUrl[0].IndexOf("-p-") + 3);
            convertUrl.Append(baseDetailUrl).Append(productContentId);

            if (url.Contains("boutiqueId"))
            {
                var andSectionalUrl = sectionalUrl[1].Split("&");

                string boutique = andSectionalUrl[0].Substring(andSectionalUrl[0].IndexOf("boutiqueId") + 11);
                convertUrl.Append("&CampaignId=").Append(boutique);
            }

            if (url.Contains("merchantId"))
            {
                string merchant = url.Substring(url.IndexOf("merchantId") + 11);
                convertUrl.Append("&MerchantId=").Append(merchant);
            }

            return convertUrl.ToString();
        }


        public string SearchConverterToDeeplink(string url)
        {
            string baseSearchUrl = "ty://?Page=Search&Query=";

            var convertUrl = new StringBuilder();
            string sectionalSearch = url.Substring(url.IndexOf("/sr?q") + 6);
            convertUrl.Append(baseSearchUrl).Append(sectionalSearch);

            return convertUrl.ToString();
        }
    }
}
