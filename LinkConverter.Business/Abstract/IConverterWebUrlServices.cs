using LinkConverter.Core.Utilities.Results;
using LinkConverter.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkConverter.Business.Abstract
{       
    public interface IConverterWebUrlServices
    {
        IDataResult<List<Urllink>> GetAll();
        IResult Create(Urllink entity);
        string ConvertToDeeplink(string url);
        IDataResult<Urllink> GetByUrlCon(string webUrl);


    }
}
