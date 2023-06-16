using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Wini.DA;
using Wini.DA.Cache;
using Wini.Database;
using Wini.Simple;
using Wini.Utils;


namespace Wini.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var b = PasswordUtils.HashPassword("123");
            var b1 = PasswordUtils.HashPassword("123");
            var c = PasswordUtils.VerifyHashedPassword(b, "123");

            var c1 = PasswordUtils.VerifyHashedPassword(b1, "123");


            List<RoleAddModuleItem> modules = new List<RoleAddModuleItem>();
            modules.Add(new RoleAddModuleItem() { ActiveId = 1, ModuleId = 1, Check = true });

            var a1 = JsonConvert.SerializeObject(modules);
            var d = 1;


        }

        [TestMethod]
        public async void TestMethod2()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer("Data Source=14.160.25.159;Initial Catalog=NNIG4;User Id=sa;Password=Fdi@t34dfsDe3421#@4;"));
            services.AddScoped<IOrderDa, OrderDa>();
            services.AddScoped<ICacheService, CacheService>();

            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetService<ApplicationDbContext>();
            var a1 = serviceProvider.GetService<ICacheService>();
            await a1.RemoveWithWildCardAsync("3B208E07-464A-4CF8-AF26-08DB3C98CC6D");
            var b = context.UserInRoles.ToList();
            var d = 1;



        }
    }


}
