using Autofac;
using DekkersAuto.Services;
using System;
using System.Net.Http;

namespace DekkersAuto.Dependencies
{
    public class DependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApiService>();
            builder.RegisterType<HttpClient>().SingleInstance();
        }
    }
}
