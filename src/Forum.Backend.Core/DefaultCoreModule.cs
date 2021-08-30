using Autofac;
using Forum.Backend.Core.Interfaces;
using Forum.Backend.Core.Services;

namespace Forum.Backend.Core
{
    public class DefaultCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>()
                .As<IUserService>().InstancePerLifetimeScope();
        }
    }
}
