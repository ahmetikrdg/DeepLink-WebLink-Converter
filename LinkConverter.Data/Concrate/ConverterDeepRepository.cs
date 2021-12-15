using LinkConverter.Core.DataAccess;
using LinkConverter.Data.Abstract;
using LinkConverter.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkConverter.Data.Concrate
{
    public class ConverterDeepRepository : GenericRepository<Deeplink, LinkConvertDbContext>, IConverterDeepRepository
    {
    }
}
