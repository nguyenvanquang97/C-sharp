using Wini.DA.Cache;
using Wini.DA;

namespace Wini.DL
{
    public interface IModuleDL
    {

        Task<List<int>> GetPermissionByTagUserId(string tag, Guid userId);
    }

    public class ModuleDL : IModuleDL
    {
        private readonly IModuleDA _moduleDa;
        private readonly IUserDA _userDa;
        private readonly ICacheService _cacheService;
        private readonly DateTimeOffset expirationTime = DateTimeOffset.Now.AddMinutes(30.0);

        public ModuleDL(IModuleDA moduleDa, IUserDA userDa, ICacheService cacheService)
        {
            _moduleDa = moduleDa;
            _userDa = userDa;
            _cacheService = cacheService;
        }
        public async Task<List<int>> GetPermissionByTagUserId(string tag, Guid userId)
        {
            var key = string.Format("{0}-{1}-Permission", userId, tag);
            var roldIds = await _userDa.GetAllRold(userId, true);
            var lstCache = new List<int>();


            lstCache = _cacheService.GetData<List<int>>(key);

            if (lstCache != null && lstCache.Count() > 0)
            {

                //return lstCache;
            }

            lstCache = await _moduleDa.GetPermissionByTagUserId(tag, userId, roldIds);
            _cacheService.SetData(key, lstCache, expirationTime);

            return lstCache;

        }
    }
}