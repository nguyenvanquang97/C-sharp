using Autofac;
using Wini.SaleMultipleChannel;

namespace Pos
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<SaleLazada>().As<ISale>().InstancePerLifetimeScope();
            builder.RegisterType<SaleLazada>().Named<ISale>("lazada").InstancePerLifetimeScope();
            builder.RegisterType<SaleShopee>().Named<ISale>("shoppe").InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
