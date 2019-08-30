using Autofac;
using DekkersAuto.Services.Database;
using DekkersAuto.Services.Email;

namespace DekkersAuto.Dependencies
{
    public class DependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BannerService>();
            builder.RegisterType<DbService>();
            builder.RegisterType<IdentityService>();
            builder.RegisterType<ImageService>();
            builder.RegisterType<ListingService>();
            builder.RegisterType<OptionsService>();
            builder.RegisterType<EmailService>().As<IEmailService>();
        }
    }
}
