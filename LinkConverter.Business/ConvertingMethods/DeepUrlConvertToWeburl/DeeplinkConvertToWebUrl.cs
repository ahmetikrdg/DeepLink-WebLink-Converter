using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkConverter.Business.ConvertingMethods.DeepUrlConvertToWeburl
{
    public class DeeplinkConvertToWebUrl : IDeeplinkConvertToWebUrl
    {

        public string DeeplinkConverterToWeburl(string url) //Creates a new url from scratch based on the incoming url value
        {
            string baseUrl = "https://www.trendyol.com/brand/name-p-";
            var convertUrl = new StringBuilder(); //builds a new StringBuilder
            var sectionalUrl = url.Split("&");
            string productContentId = sectionalUrl[1].Substring(10);
            convertUrl.Append(baseUrl).Append(productContentId); //parsed values ​​continue by adding convertUrl


            if (url.Contains("CampaignId"))
            {
                string CampaignId = sectionalUrl[2].Substring(sectionalUrl[2].IndexOf("CampaignId") + 11);// When we search for a value with IndexOf, it gives us the first index of the word we find.Then it is necessary to go as far as the number of words and then get the vieri.
                convertUrl.Append("?boutiqueId=").Append(CampaignId);
            }

            if (url.Contains("MerchantId"))
            {
                string MerchantId = url.Substring(url.IndexOf("MerchantId") + 11);
                convertUrl.Append("&merchantId=").Append(MerchantId);

                if (convertUrl.ToString().Contains("boutiqueId"))
                {
                    convertUrl.Replace("name", "namei");
                }                
            }
            return convertUrl.ToString();
        }


        public string SearchConverterToWeblink(string url)
        {
            string baseSearchUrl = "https://www.trendyol.com/sr?q=";
            var convertUrl = new StringBuilder();

            string sectionalSearch = url.Substring(url.IndexOf("Search&Query") + 13);
            convertUrl.Append(baseSearchUrl).Append(sectionalSearch);

            return convertUrl.ToString();
        }
    }
}
