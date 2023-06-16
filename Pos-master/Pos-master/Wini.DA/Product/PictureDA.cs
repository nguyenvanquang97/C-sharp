using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DevExtreme.AspNet.Data;
using Pos;
using Wini.Database;
using Wini.Simple;
using Wini.Utils;

namespace Wini.DA
{
    
     public interface IPictureDa
    {
         

        /// <summary>
        /// Gets all category.
        /// </summary>
        /// <returns>danh sách category.</returns>
       Task<BaseResponse<IList<PictureItem>>> GetListSimpleByRequest(BaseRequest request);
        Picture GetbyId(int id);
       BaseResponse<Picture> Update(Picture item);


        /// <summary>
        /// save.
        /// </summary>
        void Save();
        void Remove(Picture item);
        void Add(Picture category);
    }

    public class PictureDa : IPictureDa
    {
        readonly ApplicationDbContext _context;


        public PictureDa(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<IList<PictureItem>>> GetListSimpleByRequest(BaseRequest request)
        {

            var source = from o in _context.Pictures
                        where (!o.IsDeleted.HasValue || o.IsDeleted == false) && o.IsShow == true
                        orderby o.Id descending
                        select new PictureItem
                        {
                            Id = o.Id,
                            Name = o.Name,

                        };
            var query = await DataSourceLoader.LoadAsync(source, request.LoadOptions);
            BaseResponse<IList<PictureItem>> response = new BaseResponse<IList<PictureItem>>();
            response.Data = query.data.Cast<PictureItem>().ToList();
            if (request.LoadOptions.RequireTotalCount)
            {
                response.TotalCount = query.totalCount;
            }
            return BasiResponse.Success(response.Data, "Lấy dữ liệu thành công", response.TotalCount);

        }
        public Picture GetbyId(int id)
        {

            var query = from o in _context.Pictures
                        where o.Id == id
                        orderby o.Id descending
                        select o;
            return query.FirstOrDefault();
        }
        public BaseResponse<Picture> Update(Picture item)
        {
            var model = GetbyId(item.Id);
            if (model != null)
            {
                model.Name = HttpUtility.UrlDecode(item.Name);
                model.Description = HttpUtility.UrlDecode(item.Description);
                Save();
                return BasiResponse.Success(item);

            }
            return BasiResponse.Error(new Picture());


        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Remove(Picture item)
        {
            _context.Pictures.Remove(item);
        }
        public void Add(Picture item)
        {
            _context.Pictures.Add(item);
        }
    }
}
