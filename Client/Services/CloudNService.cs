using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;

namespace YogIT.Module.CloudN.Services
{
    public class CloudNService : ServiceBase, ICloudNService, IService
    {
        public CloudNService(IHttpClientFactory http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("CloudN");

        public async Task<List<Models.CloudN>> GetCloudNsAsync(int ModuleId)
        {
            List<Models.CloudN> CloudNs = await GetJsonAsync(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId), Enumerable.Empty<Models.CloudN>().ToList());
            return [.. CloudNs.OrderByDescending(item => item.CreatedOn)];
        }

        public async Task<Models.CloudN> GetCloudNAsync(int CloudNId, int ModuleId)
        {
            return await GetJsonAsync<Models.CloudN>(CreateAuthorizationPolicyUrl($"{Apiurl}/{CloudNId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.CloudN> AddCloudNAsync(Models.CloudN CloudN)
        {
            return await PostJsonAsync(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, CloudN.ModuleId), CloudN);
        }

        public async Task<Models.CloudN> UpdateCloudNAsync(Models.CloudN CloudN)
        {
            return await PutJsonAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{CloudN.CloudNId}", EntityNames.Module, CloudN.ModuleId), CloudN);
        }

        public async Task DeleteCloudNAsync(int CloudNId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{CloudNId}", EntityNames.Module, ModuleId));
        }
    }
}
