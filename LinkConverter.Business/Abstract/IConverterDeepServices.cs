using LinkConverter.Core.Utilities.Results;
using LinkConverter.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkConverter.Business.Abstract
{
    public interface IConverterDeepServices
    {
        IDataResult<List<Deeplink>> GetAll();
        IResult Create(Deeplink entity);
        string ConvertToWeburl(string deepLink);
        IDataResult<Deeplink> GetByUrl(string url);

    }
}       
