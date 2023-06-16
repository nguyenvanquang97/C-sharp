using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DevExtreme.AspNet.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pos;
using Wini.Database;
using Wini.Simple;
using Wini.Utils;

namespace Wini.DA
{

    public interface INewsDa
    {


        /// <summary>
        /// Gets all News.
        /// </summary>
        /// <returns>danh sách News.</returns>
        Task<BaseResponse<IList<NewsItem>>> GetListSimpleByRequest(BaseRequest request);
        BaseResponse<NewsItem> GetbyNewsItem(int id);
        News GetbyId(int id);
        
        
        /// <summary>
        /// save.
        /// </summary>
        void Save();
        void Remove(News item);
        void Add(News News);
    }

    public class NewsDA : INewsDa
    {
        readonly ApplicationDbContext _context;


        public NewsDA(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<IList<NewsItem>>> GetListSimpleByRequest(BaseRequest request)
        {

            var source = from o in _context.News
                         where (!o.IsDeleted.HasValue || o.IsDeleted == false)  
                         
                         orderby o.ID descending
                         select new NewsItem
                         {
                             ID = o.ID,
                             Title = o.Title,
                             TitleAscii = o.TitleAscii,
                             Description = o.Description,
                             Details = o.Details,
                             PictureUrl = o.Picture.Folder + o.Picture.Url,
                             SEOTitle = o.SEOTitle,
                             SEODescription = o.SEODescription,
                             SEOKeyword = o.SEOKeyword,
                             IsHot = o.IsHot,
                             IsShow = o.IsShow,

                         };
            var query = await DataSourceLoader.LoadAsync(source, request.LoadOptions);
            BaseResponse<IList<NewsItem>> response = new BaseResponse<IList<NewsItem>>();
            response.Data = query.data.Cast<NewsItem>().ToList();
            if (request.LoadOptions.RequireTotalCount)
            {
                response.TotalCount = query.totalCount;
            }
            return BasiResponse.Success(response.Data, "Lấy dữ liệu thành công", response.TotalCount);

        }
        public BaseResponse<NewsItem> GetbyNewsItem( int Id)
        {

            var source = from o in _context.News
                where (!o.IsDeleted.HasValue || o.IsDeleted == false) &&
                    o.ID == Id
                orderby o.ID descending
                select new NewsItem
                {
                    ID = o.ID,
                    Title = o.Title,
                    TitleAscii = o.TitleAscii,
                    Description = o.Description,
                    Details = o.Details,
                    PictureUrl = o.Picture.Folder + o.Picture.Url,
                    SEOTitle = o.SEOTitle,
                    SEODescription = o.SEODescription,
                    SEOKeyword = o.SEOKeyword,
                    IsHot = o.IsHot,
                    IsShow = o.IsShow,
                };
            
            var data = source.FirstOrDefault();
            return BasiResponse.Success(data);
        }
        public News GetbyId(int id)
        {
            var query = from o in _context.News
                        where o.ID == id
                        orderby o.ID descending
                        select o;
            return query.FirstOrDefault();
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Remove(News item)
        {
            _context.News.Remove(item);
        }
        public void Add(News item)
        {
            _context.News.Add(item);
        }

    
    }
}
