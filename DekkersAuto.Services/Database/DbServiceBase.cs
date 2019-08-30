using DekkersAuto.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Services.Database
{
    /// <summary>
    /// Base Database service, requires all inheriting services to take a DB context
    /// </summary>
    public class DbServiceBase
    {
        protected ApplicationDbContext _db;

        protected DbServiceBase(ApplicationDbContext db)
        {
            _db = db;
        }

    }
}
