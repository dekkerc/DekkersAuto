using DekkersAuto.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Services
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
