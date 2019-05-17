using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DekkersAuto.Web.Data;
using DekkersAuto.Web.Data.Models;
using DekkersAuto.Web.Models;

namespace DekkersAuto.Web.Services
{
    public class BannerService : DbServiceBase
    {
        public BannerService(ApplicationDbContext db) : base(db)
        {
        }

        public Banner GetBanner()
        {
            var banner = _db.Banners.FirstOrDefault();
            return banner;
        }
        public void CreateBanner(BannerViewModel model)
        {
            _db.Banners.Add(new Banner { Text = model.Text, IsActive = true });
            _db.SaveChanges();
        }
        public void UpdateBanner(BannerViewModel model)
        {
            var banner = _db.Banners.Find(model.BannerId);

            banner.Text = model.Text;
            banner.IsActive = model.IsActive;
            _db.Banners.Update(banner);
            _db.SaveChanges();
        }

    }
}
