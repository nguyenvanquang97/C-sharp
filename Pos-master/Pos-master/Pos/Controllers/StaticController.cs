using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Wini.DA;
using Wini.Database;
using Wini.Simple;
using Wini.Utils;

namespace Pos.Controllers
{

    public class StaticController : BaseController
    {
        private readonly ILogger<StaticController> _logger;
        
        private IStaticDa _staticDa;

        public StaticController(ILogger<StaticController> logger, IStaticDa staticDa)
        {
            _logger = logger;
            _staticDa = staticDa;
        }

        public BaseResponse<IList<StaticOrder>> getStaticOrder(int? type, string date)
        {
            // type == 1 xem thông kê theo tháng. result trả ra tổng số ngày trong tháng.
            var time = string.IsNullOrEmpty(date) ? DateTime.Now : DateTime.Parse(date);
            var model = _staticDa.getStaticOrder(type ?? 1, AgencyId, time.Year, time.Month);
            return model;
        }
        public BaseResponse<IList<StaticOrder>> getStaticPriceOrder(int? type, string date)
        {
            // type == 1 xem thông kê theo tháng. result trả ra tổng số ngày trong tháng.
            var time = string.IsNullOrEmpty(date) ? DateTime.Now : DateTime.Parse(date);
            var model = _staticDa.getStaticPriceOrder(type ?? 1, AgencyId, time.Year, time.Month);
            return model;
        }
        public BaseResponse<List<StaticDebtItem>> getStaticDebt(string datef, string datet, int? agenyId)
        {
            var model = _staticDa.staticDebt(agenyId ?? 0, datef.StringToDecimal(), datet.StringToDecimal());
            return model;
        }
        public BaseResponse<IList<StaticOrder>> getStaticAgency(int? type, string date)
        {
            var time = string.IsNullOrEmpty(date) ? DateTime.Now : DateTime.Parse(date);

            var model = _staticDa.GetStaticAgency(type ?? 1, AgencyId, time.Year, time.Month);
            return model;
        }
        public BaseResponse<IList<StaticOrder>> GetStaticCustomer(int? type, string date)
        {
            var time = string.IsNullOrEmpty(date) ? DateTime.Now : DateTime.Parse(date);

            var model = _staticDa.GetStaticCustomer(type ?? 1, AgencyId, time.Year, time.Month);
            return model;
        }

    }
}
