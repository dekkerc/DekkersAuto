using DekkersAuto.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//I, Christopher Dekker, student number 000311337, certify that all code
//submitted is my own work; that I have not copied it from any other source
//I also certify that I have not allowed by work to be copied by others
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
