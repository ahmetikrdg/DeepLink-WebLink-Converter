using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkConverter.Business.ConvertingMethods.WeburlConvertToDeeplink
{
    public interface IWebUrlToDeeplinkConvert
    {
        string LinkDetailConvertToDeeplink(string url);
        string SearchConverterToDeeplink(string url);
    }
}
