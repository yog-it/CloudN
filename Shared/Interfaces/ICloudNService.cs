using System.Collections.Generic;
using System.Threading.Tasks;

namespace YogIT.Module.CloudN.Services
{
    public interface ICloudNService 
    {
        Task<List<Models.CloudN>> GetCloudNsAsync(int ModuleId);

        Task<Models.CloudN> GetCloudNAsync(int CloudNId, int ModuleId);

        Task<Models.CloudN> AddCloudNAsync(Models.CloudN CloudN);

        Task<Models.CloudN> UpdateCloudNAsync(Models.CloudN CloudN);

        Task DeleteCloudNAsync(int CloudNId, int ModuleId);
    }
}
