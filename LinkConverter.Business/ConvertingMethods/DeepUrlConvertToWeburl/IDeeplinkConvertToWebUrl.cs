using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkConverter.Business.ConvertingMethods.DeepUrlConvertToWeburl
{
    public interface IDeeplinkConvertToWebUrl
    {
        string DeeplinkConverterToWeburl(string url);
        string SearchConverterToWeblink(string url);
    }
}
