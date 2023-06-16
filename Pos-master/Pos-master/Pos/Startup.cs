using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Pos.Helpers;
using System.Text;
using Wini.DA;
using Wini.DA.Cache;
using Wini.DA;
using Wini.Database;
using Wini.DL;
using Wini.SaleMultipleChannel;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Pos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSetting>(Configuration.GetSection("AppSettings"));
            services.AddControllersWithViews();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Wini API", Version = "v1" });
            });
            services.AddScoped<ICacheService, CacheService>();
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")));
         
            services.AddScoped<IModuleDA, ModuleDA>();
            services.AddScoped<IUserDA, UserDA>();
            services.AddScoped<IModuleDL, ModuleDL>();
            //services.AddTransient<ICustomerDa, CustomerDA>();

            //services.AddTransient<ICustomerDa, CustomerDA>();
            services.AddTransient<IShopDa, ShopDa>();
            services.AddScoped<ICategoryDa, CategoryDa>();
            services.AddScoped<IPictureDa, PictureDa>();
            services.AddScoped<IColorDa, ColorDa>();
            services.AddScoped<ISizeDa, SizeDa>();
            services.AddScoped<IUnitDa, UnitDa>();
            services.AddScoped<IBrandDa, BrandDa>();
            
            services.AddScoped<IProductDetailDa, ProductDetailDa>();
            services.AddScoped<IProductDa, ProductDa>();
            services.AddScoped<IStorageProductDa, StorageProductDa>();
            services.AddScoped<IExportProductDa, ExportProductDa>();
            services.AddScoped<IDebtDa, DebtDa>();
            services.AddScoped<IAgencyDa, AgencyDa>();
            services.AddScoped<IOrderDa, OrderDa>();
            //services.AddScoped<ISale, SaleLazada>();
          

            services.AddScoped<IStaticDa, StaticDA>();
            services.AddScoped<IUserDA, UserDA>();

          

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver() { };
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=> {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:JwtKey"])),
                    ValidIssuer="wini",
                    ValidAudience=null,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,

                };
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ApplicationModule());
        }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wini API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
