using Autofac;
using JetBrains.Annotations;
using KP.Api.Core.DI;
using KP.Service;
using KP.Storage.Domain;
using KP.Storage.Domain.Context;
using KP.Storage.Repository;

namespace KP.Api.DI
{
    public class ApiAutofacModule : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>));

            builder.RegisterType<DbContextFactory>().As<IDbContextFactory>().InstancePerRequest();

            builder.RegisterControllers(ThisAssembly);
            builder.RegisterNonControllers(ThisAssembly)
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();
            builder.RegisterAssemblyTypes(
                typeof(ManningTableService).Assembly,
                typeof(StaffObject).Assembly)
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();
        }
    }
}