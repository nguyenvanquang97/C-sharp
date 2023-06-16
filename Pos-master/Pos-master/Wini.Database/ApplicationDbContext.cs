using Microsoft.EntityFrameworkCore;
using System;
using Wini.Database;
using Wini.Database.Multipe_Channel;

namespace Wini.Database
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StaticOrder>().HasNoKey();

            

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Employment> Employments { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<StorageProduct> StorageProducts { get; set; }
        public DbSet<ImportProduct> ImportProducts { get; set; }
        public DbSet<ProductDetailPicture> ProductDetailPictures { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<UserModule> UserModules { get; set; }
        public DbSet<UserModuleActive> UserModuleActives { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<RoleModuleActive> RoleModuleActives { get; set; }
        //public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }

        public DbSet<ExportProduct> ExportProducts { get; set; }
        public DbSet<ExportProductDetail> ExportProductDetails { get; set; }
        public DbSet<Debt> Debts { get; set; }
        public DbSet<DebtDetail> DebtDetails { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<AgencyType> AgencyTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; } 
        public DbSet<StaticOrder> StaticOrders { get; set; }

        public DbSet<AppEcommerce>AppEcommerces { get; set; }
        public DbSet<ShopAppEcommerce> ShopAppEcommerces { get; set; }
        public DbSet<AgencyProductEcom> AgencyProductEcoms { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<PictureEcom> PictureEcoms { get; set;}
    }
}
