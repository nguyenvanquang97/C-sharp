using Lazop.Api.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wini.DA;
using Wini.Database;
using Wini.SaleMultipleChannel;
using Wini.SaleMultipleChannel.Lazop;
using Wini.SaleMultipleChannel.CreateProductRequest;
using Wini.Simple.Identity;
using Wini.Utils;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using DocumentFormat.OpenXml.Spreadsheet;
using Wini.Simple;
using System.Data.Entity.Validation;
using System.Text;
using System.Text.Json.Nodes;
using Wini.SaleMultipleChannel.Model.Token;
using Formatting = Newtonsoft.Json.Formatting;

namespace Wini.Test
{
    [TestClass]
    public class LazadaTest
    {

        [TestMethod]
        public async Task GetProduct2()
        {

            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer("Data Source=14.160.25.159;Initial Catalog=NNIG4;User Id=sa;Password=Fdi@t34dfsDe3421#@4;"));
            services.AddScoped<ISale, SaleLazada>();

            var serviceProvider = services.BuildServiceProvider();

            var dbContext = serviceProvider.GetService<ApplicationDbContext>();

            var product = dbContext.ProductDetails.Include(m=>m.Pictures).FirstOrDefault(e => e.Id == 113);

            product.Pictures.Add(new ProductDetailPicture() {
                PictureId = 14,
                
            });

            try
            {
                dbContext.SaveChanges();
               
            }
            catch (Exception ex)
            {
                var b1 = 1;
            }
            var b = 1;
            //var b = await sale.GetProducts(id, "VN33ZJH4GR");

        }

        [TestMethod]
        public async Task GetToken()
        {

            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer("Data Source=14.160.25.159;Initial Catalog=NNIG4;User Id=sa;Password=Fdi@t34dfsDe3421#@4;"));
            services.AddScoped<ISale, SaleLazada>();

            var serviceProvider = services.BuildServiceProvider();

            var sale = serviceProvider.GetService<ISale>();

            var b = await sale.GetToken("0_117002_EWaopTTNckCnYe3bXafMdxnd111815", "VN33ZJH4GR");

        }

        [TestMethod]
        public async Task GetProduct()
        {

            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer("Data Source=14.160.25.159;Initial Catalog=NNIG4;User Id=sa;Password=Fdi@t34dfsDe3421#@4;"));
            services.AddScoped<ISale, SaleLazada>();

            var serviceProvider = services.BuildServiceProvider();

            var sale = serviceProvider.GetService<ISale>();

            Guid id = new Guid("71A90541-C7D7-4997-A5D3-7F3917CAE3DE");
            //var b = await sale.GetProducts(id, "VN33ZJH4GR");

        }
        [TestMethod]
        public async Task SyncProductFromEcom()
        {

            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer("Data Source=14.160.25.159;Initial Catalog=NNIG4;User Id=sa;Password=Fdi@t34dfsDe3421#@4;"));
            services.AddScoped<ISale, SaleLazada>();

            var serviceProvider = services.BuildServiceProvider();

            var sale = serviceProvider.GetService<ISale>();


            //var b = await sale.SyncProductFromEcom(1);

        }
        [TestMethod]
        public async Task CreateProduct()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer("Data Source=14.160.25.159;Initial Catalog=NNIG4;User Id=sa;Password=Fdi@t34dfsDe3421#@4;"));
            services.AddScoped<ISale, SaleLazada>();

            var serviceProvider = services.BuildServiceProvider();

            var sale = serviceProvider.GetService<ISale>();
            var dbContext = serviceProvider.GetService<ApplicationDbContext>();

            Product product = dbContext.Products.FirstOrDefault(e => e.Id == 130);
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(product));



            //Guid id = new Guid("71A90541-C7D7-4997-A5D3-7F3917CAE3DE");
            //var b = sale.CreateProduct(1, product);

        }




        [TestMethod]
        public void UpdateProduct()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer("Data Source=14.160.25.159;Initial Catalog=NNIG4;User Id=sa;Password=Fdi@t34dfsDe3421#@4;"));
            services.AddScoped<ISale, SaleLazada>();

            var serviceProvider = services.BuildServiceProvider();

            var sale = serviceProvider.GetService<ISale>();
            var dbContext = serviceProvider.GetService<ApplicationDbContext>();
            var picture = dbContext.ProductDetails.Include(p => p.Pictures).Where(p => p.Id == 113).FirstOrDefault();
            //ProductDetail productDetail = dbContext.ProductDetails.FirstOrDefault(pd => pd.Id == 113);
           var json= JsonConvert.SerializeObject(picture, Formatting.Indented,
            new JsonSerializerSettings
            {
              PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            Console.WriteLine(json);
            Guid id = new Guid("71A90541-C7D7-4997-A5D3-7F3917CAE3DE");
            //var b = sale.UpdateProduct(1,  productDetail,"TVXQ11");
        }

        [TestMethod]
        public async Task UpdatePrice()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer("Data Source=14.160.25.159;Initial Catalog=NNIG4;User Id=sa;Password=Fdi@t34dfsDe3421#@4;"));
            services.AddScoped<ISale, SaleLazada>();

            var serviceProvider = services.BuildServiceProvider();
            var sale = serviceProvider.GetService<ISale>();

            Guid id = new Guid("71A90541-C7D7-4997-A5D3-7F3917CAE3DE");
            //var b = sale.UpdatePrice(id, "VN33ZJH4GR", "2f803789bbff4f7da11c04a428863d77", "500000");

        }
        [TestMethod]
        public async Task GetAllProductAsynchronous()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer("Data Source=14.160.25.159;Initial Catalog=NNIG4;User Id=sa;Password=Fdi@t34dfsDe3421#@4;"));
            services.AddScoped<ISale, SaleLazada>();

            var serviceProvider = services.BuildServiceProvider();
            var sale = serviceProvider.GetService<ISale>();
            await Console.Out.WriteLineAsync("Ok");

            var b = sale.GetAllProductAsynchronous(1);

        }
        [TestMethod]
        public async Task GetAllProductSync()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer("Data Source=14.160.25.159;Initial Catalog=NNIG4;User Id=sa;Password=Fdi@t34dfsDe3421#@4;"));
            services.AddScoped<ISale, SaleLazada>();

            var serviceProvider = services.BuildServiceProvider();

            var sale = serviceProvider.GetService<ISale>();
            var dbContext = serviceProvider.GetService<ApplicationDbContext>();
            Wini.Database.Product product = dbContext.Products.FirstOrDefault(p => p.Id == 519);
            //List<Wini.Database.Product> products = dbContext.Products
            //   .Where(p => dbContext.AgencyProductEcoms.Any(a => a.ProductId == p.Id))
            //   .ToList();
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(product));
            Guid id = new Guid("71A90541-C7D7-4997-A5D3-7F3917CAE3DE");
            var b = sale.GetAllProductSynced(1);

        }
        [TestMethod]
        public async Task UpdateQuantity()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer("Data Source=14.160.25.159;Initial Catalog=NNIG4;User Id=sa;Password=Fdi@t34dfsDe3421#@4;"));
            services.AddScoped<ISale, SaleLazada>();

            var serviceProvider = services.BuildServiceProvider();
            var sale = serviceProvider.GetService<ISale>();

            Guid id = new Guid("71A90541-C7D7-4997-A5D3-7F3917CAE3DE");
            //var b = sale.UpdateQuantity(id, "VN33ZJH4GR", "2f803789bbff4f7da11c04a428863d77", "11");

        }
        [TestMethod]
        public async Task DeleteProduct()
        {

            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer("Data Source=14.160.25.159;Initial Catalog=NNIG4;User Id=sa;Password=Fdi@t34dfsDe3421#@4;"));
            services.AddScoped<ISale, SaleLazada>();

            var serviceProvider = services.BuildServiceProvider();

            var sale = serviceProvider.GetService<ISale>();
            var dbContext = serviceProvider.GetService<ApplicationDbContext>();

            List<string> sellerSkus = new List<string> { "e228c3c4f4a44463ae27ea61ca112733", "test2022 12" };
            Guid id = new Guid("71A90541-C7D7-4997-A5D3-7F3917CAE3DE");
            //var b = sale.DeleteProduct(id, "VN33ZJH4GR", sellerSkus);

        }
        [TestMethod]
        public async Task UploadImage()
        {

            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer("Data Source=14.160.25.159;Initial Catalog=NNIG4;User Id=sa;Password=Fdi@t34dfsDe3421#@4;"));
            services.AddScoped<ISale, SaleLazada>();

            var serviceProvider = services.BuildServiceProvider();

            var sale = serviceProvider.GetService<ISale>();

            Guid id = new Guid("71A90541-C7D7-4997-A5D3-7F3917CAE3DE");
            FileItem fileItem = new FileItem("C:/Users/A Quang/Desktop/tải xuống.jpg");
            //var b =  sale.UploadImage(fileItem,id, "VN33ZJH4GR");


        }


        [TestMethod]
        public async Task MigrateImage()
        {

            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer("Data Source=14.160.25.159;Initial Catalog=NNIG4;User Id=sa;Password=Fdi@t34dfsDe3421#@4;"));
            services.AddScoped<ISale, SaleLazada>();

            var serviceProvider = services.BuildServiceProvider();

            var sale = serviceProvider.GetService<ISale>();

            Guid id = new Guid("71A90541-C7D7-4997-A5D3-7F3917CAE3DE");
            var b = sale.MigrateImage(1, "https://images2.thanhnien.vn/Uploaded/chinhan/2022_10_17/thitheo-sqha-hwmf-5203.jpg");

        }

        [TestMethod]
        public async Task GetCategorySuggestion()
        {

            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer("Data Source=14.160.25.159;Initial Catalog=NNIG4;User Id=sa;Password=Fdi@t34dfsDe3421#@4;"));
            services.AddScoped<ISale, SaleLazada>();

            var serviceProvider = services.BuildServiceProvider();

            var sale = serviceProvider.GetService<ISale>();

            Guid id = new Guid("71A90541-C7D7-4997-A5D3-7F3917CAE3DE");
            //var b = sale.GetCategorySuggestion(id, "VN33ZJH4GR","Sườn heo cốt lết");
        }


        [TestMethod]
        public async Task GetOrders()
        {

            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer("Data Source=14.160.25.159;Initial Catalog=NNIG4;User Id=sa;Password=Fdi@t34dfsDe3421#@4;"));
            services.AddScoped<ISale, SaleLazada>();

            var serviceProvider = services.BuildServiceProvider();

            var sale = serviceProvider.GetService<ISale>();

            Guid id = new Guid("71A90541-C7D7-4997-A5D3-7F3917CAE3DE");
            var b = sale.GetOrders(1);


        }
        [TestMethod]
        public async Task RefreshToken()
        {

            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer("Data Source=14.160.25.159;Initial Catalog=NNIG4;User Id=sa;Password=Fdi@t34dfsDe3421#@4;"));
            services.AddScoped<ISale, SaleLazada>();

            var serviceProvider = services.BuildServiceProvider();

            var sale = serviceProvider.GetService<ISale>();

            Guid id = new Guid("71A90541-C7D7-4997-A5D3-7F3917CAE3DE");
            var b = await sale.RefreshAccessToken(1);



        }


        [TestMethod]
        public void GetAllCategory()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer("Data Source=14.160.25.159;Initial Catalog=NNIG4;User Id=sa;Password=Fdi@t34dfsDe3421#@4;"));
            services.AddScoped<ISale, SaleLazada>();

            //    var serviceProvider = services.BuildServiceProvider();

            //var orderDa = serviceProvider.GetService<ISale>();
            //var context = serviceProvider.GetService<ApplicationDbContext>();
            //var lst = context.Products.Where(m => m.Id == 1).FirstOrDefault();
            //orderDa.GetAllCategory("50000100d209b5qwoTEhbf2flUgyrcWF105a9b6eEtiOTUljdKh1FVUAkifcu4Dt");



            //}
        }


    }
}