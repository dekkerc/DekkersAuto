using DekkersAuto.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Services.Database
{
    public class DbServiceBase
    {
        protected ApplicationDbContext _db;

        protected DbServiceBase(ApplicationDbContext db)
        {
            _db = db;
        }

    }
}
