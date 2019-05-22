using DekkersAuto.Database;
using DekkersAuto.Database.Models;
using DekkersAuto.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Services.Database
{
    public class BannerService : DbServiceBase
    {
        public BannerService(ApplicationDbContext db) : base(db)
        {
        }

        public BannerModel GetBanner()
        {
            var banner = _db.Banners.FirstOrDefault();
            if(banner == null)
            {
                return null;
            }
            var result = new BannerModel
            {
                BannerId = banner.Id,
                Text = banner.Text,
                IsActive = banner.IsActive
            };
            return result;
        }
        public void CreateBanner(BannerModel model)
        {
            _db.Banners.Add(new Banner { Text = model.Text, IsActive = true });
            _db.SaveChanges();
        }
        public void UpdateBanner(BannerModel model)
        {
            var banner = _db.Banners.Find(model.BannerId);

            banner.Text = model.Text;
            banner.IsActive = model.IsActive;
            _db.Banners.Update(banner);
            _db.SaveChanges();
        }

    }
}
