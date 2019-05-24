using Autofac;
using DekkersAuto.Services;
using DekkersAuto.Services.Database;
using DekkersAuto.Services.Email;
using DekkersAuto.Services.Services;
using System;
using System.Net.Http;

namespace DekkersAuto.Dependencies
{
    public class DependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApiService>();
            builder.RegisterType<BannerService>();
            builder.RegisterType<DbService>();
            builder.RegisterType<IdentityService>();
            builder.RegisterType<ImageService>();
            builder.RegisterType<ListingService>();
            builder.RegisterType<OptionsService>();

            builder.RegisterType<EmailService>().As<IEmailService>();

            builder.Register(c => new HttpClient { BaseAddress = new Uri("https://vpic.nhtsa.dot.gov/api/") }).SingleInstance();
        }
    }
}
