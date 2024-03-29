using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Modules;
using Oqtane.Security;
using Oqtane.Shared;
using YogIT.Module.CloudN.Repository;

namespace YogIT.Module.CloudN.Services
{
    public class ServerCloudNService : ICloudNService, ITransientService
    {
        private readonly ICloudNRepository _CloudNRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public ServerCloudNService(ICloudNRepository CloudNRepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _CloudNRepository = CloudNRepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public async Task<List<Models.CloudN>> GetCloudNsAsync(int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return (await _CloudNRepository.GetCloudNsAsync(ModuleId)).ToList();
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized CloudN Get Attempt {ModuleId}", ModuleId);
                return null;
            }
        }

        public async Task<Models.CloudN> GetCloudNAsync(int CloudNId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return await _CloudNRepository.GetCloudNAsync(CloudNId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized CloudN Get Attempt {CloudNId} {ModuleId}", CloudNId, ModuleId);
                return null;
            }
        }

        public async Task<Models.CloudN> AddCloudNAsync(Models.CloudN CloudN)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, CloudN.ModuleId, PermissionNames.Edit))
            {
                CloudN = await _CloudNRepository.AddCloudNAsync(CloudN);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "CloudN Added {CloudN}", CloudN);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized CloudN Add Attempt {CloudN}", CloudN);
                CloudN = null;
            }
            return CloudN;
        }

        public async Task<Models.CloudN> UpdateCloudNAsync(Models.CloudN CloudN)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, CloudN.ModuleId, PermissionNames.Edit))
            {
                CloudN = await _CloudNRepository.UpdateCloudNAsync(CloudN);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "CloudN Updated {CloudN}", CloudN);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized CloudN Update Attempt {CloudN}", CloudN);
                CloudN = null;
            }
            return CloudN;
        }

        public async Task DeleteCloudNAsync(int CloudNId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.Edit))
            {
                await _CloudNRepository.DeleteCloudNAsync(CloudNId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "CloudN Deleted {CloudNId}", CloudNId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized CloudN Delete Attempt {CloudNId} {ModuleId}", CloudNId, ModuleId);
            }
        }
    }
}
