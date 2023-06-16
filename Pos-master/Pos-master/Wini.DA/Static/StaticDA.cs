// ---------------------------------------------------
// <copyright file="StaticDA.cs" company="Wini">
// Copyright (c) Wini. All rights reserved.
// author : phuocnh
// </copyright>
// ---------------------------------------------------

namespace Wini.DA
{
    using DevExtreme.AspNet.Data;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Wini.Database;
    using Wini.Simple;
    using Wini.Utils;

    /// <summary>
    /// IStaticDa.
    /// </summary>
    public interface IStaticDa
    {
        BaseResponse<IList<StaticOrder>> GetStaticCustomer(int type, int agencyId, int year, int month);
        BaseResponse<IList<StaticOrder>> getStaticOrder(int type, int agencyId, int year, int month);
        BaseResponse<IList<StaticOrder>> getStaticPriceOrder(int type, int agencyId, int year, int month);
        BaseResponse<IList<StaticAll>> getStaticPriceAll(int type, int agencyId, int year, int month);
        BaseResponse<List<StaticDebtItem>> staticDebt(int? agenyId, decimal? datef, decimal? datet);
        BaseResponse<IList<StaticOrder>> GetStaticAgency(int type, int agencyId, int year, int month);


    }

    /// <summary>
    /// implement IStaticDA.
    /// </summary>
    public class StaticDA : IStaticDa
    {
        readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticDA"/> class.
        /// </summary>
        /// <param name="context">context.</param>
        public StaticDA(ApplicationDbContext context)
        {
            _context = context;
        }
        public BaseResponse<IList<StaticOrder>> GetStaticCustomer(int type, int agencyId, int year, int month)
        {
            var p1 = new SqlParameter("@year", year);
            var p2 = new SqlParameter("@month", month);
            var p3 = new SqlParameter("@aid", agencyId);
            var p4 = new SqlParameter("@type", type);
            var souce = _context.Set<StaticOrder>().FromSqlRaw("EXECUTE StaticChartsCustomer", p1, p2, p3, p4).ToList();
            var result = new BaseResponse<IList<StaticOrder>>() { Code = (int)ResponseCode.Success, Data = souce, Message = "Lấy dữ liệu thành công" };
            return result;
        }
        public BaseResponse<IList<StaticOrder>> getStaticOrder(int type, int agencyId, int year, int month)
        {
            var p1 = new SqlParameter("@year", year);
            var p2 = new SqlParameter("@month", month);
            var p3 = new SqlParameter("@aid", agencyId);
            var p4 = new SqlParameter("@type", type);
            var souce = _context.Set<StaticOrder>().FromSqlRaw("EXECUTE StaticOrder", p1, p2, p3, p4).ToList();
            var result = new BaseResponse<IList<StaticOrder>>() { Code = (int)ResponseCode.Success, Data = souce, Message = "Lấy dữ liệu thành công" };
            return result;
        }
        public BaseResponse<IList<StaticOrder>> getStaticPriceOrder(int type, int agencyId, int year, int month)
        {
            var p1 = new SqlParameter("@year", year);
            var p2 = new SqlParameter("@month", month);
            var p3 = new SqlParameter("@aid", agencyId);
            var p4 = new SqlParameter("@type", type);
            var souce = _context.Set<StaticOrder>().FromSqlRaw("EXECUTE StaticPriceOrder", p1, p2, p3, p4).ToList();
            var result = new BaseResponse<IList<StaticOrder>>() { Code = (int)ResponseCode.Success, Data = souce, Message = "Lấy dữ liệu thành công" };
            return result;
        }
        public BaseResponse<IList<StaticAll>> getStaticPriceAll(int type, int agencyId, int year, int month)
        {
            var p1 = new SqlParameter("@year", year);
            var p2 = new SqlParameter("@month", month);
            var p3 = new SqlParameter("@aid", agencyId);
            var p4 = new SqlParameter("@type", type);
            var souce = _context.Set<StaticAll>().FromSqlRaw("EXECUTE StaticAll", p1, p2, p3, p4).ToList();
            var result = new BaseResponse<IList<StaticAll>>() { Code = (int)ResponseCode.Success, Data = souce, Message = "Lấy dữ liệu thành công" };
            return result;
        }
        public BaseResponse<IList<StaticOrder>> GetStaticAgency(int type, int agencyId, int year, int month)
        {
            var p1 = new SqlParameter("@year", year);
            var p2 = new SqlParameter("@month", month);
            var p4 = new SqlParameter("@type", type);
            var souce = _context.Set<StaticOrder>().FromSqlRaw("EXECUTE StaticChartsAgency", p1, p2,  p4).ToList();
            var result = new BaseResponse<IList<StaticOrder>>() { Code = (int)ResponseCode.Success, Data = souce, Message = "Lấy dữ liệu thành công" };
            return result;
        }
        public BaseResponse<List<StaticDebtItem>> staticDebt(int? agencyId, decimal? datef, decimal? datet)
        {

            var source = from o in _context.Agencies
                where (!o.IsDelete.HasValue || o.IsDelete == false)

                orderby o.Id descending
                select new StaticDebtItem
                {
                    AgencyName = o.Name,
                    Address = o.Address,
                    Phone = o.Phone,
                    Email = o.Email,
                    Total = o.Debts.Where(c => (agencyId == 0 || c.AgencyId == agencyId) && (datef <= c.DateCreate && c.DateCreate <= datet)).Sum(a => a.Price - a.Payment),
                };

            return new BaseResponse<List<StaticDebtItem>>()
                { Code = (int)ResponseCode.Success, Data = source.ToList(), Message = "Lấy dữ liệu thành công" };

        }


    }
}
