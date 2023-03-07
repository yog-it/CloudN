using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Enums;
using YogIT.LargeAzFileUpload.Repository;

namespace YogIT.LargeAzFileUpload.Manager
{
    public class LargeAzFileUploadManager : MigratableModuleBase, IInstallable, IPortable
    {
        private ILargeAzFileUploadRepository _LargeAzFileUploadRepository;
        private readonly ITenantManager _tenantManager;
        private readonly IHttpContextAccessor _accessor;

        public LargeAzFileUploadManager(ILargeAzFileUploadRepository LargeAzFileUploadRepository, ITenantManager tenantManager, IHttpContextAccessor accessor)
        {
            _LargeAzFileUploadRepository = LargeAzFileUploadRepository;
            _tenantManager = tenantManager;
            _accessor = accessor;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new LargeAzFileUploadContext(_tenantManager, _accessor), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new LargeAzFileUploadContext(_tenantManager, _accessor), tenant, MigrationType.Down);
        }

        public string ExportModule(Module module)
        {
            string content = "";
            List<Models.LargeAzFileUpload> LargeAzFileUploads = _LargeAzFileUploadRepository.GetLargeAzFileUploads(module.ModuleId).ToList();
            if (LargeAzFileUploads != null)
            {
                content = JsonSerializer.Serialize(LargeAzFileUploads);
            }
            return content;
        }

        public void ImportModule(Module module, string content, string version)
        {
            List<Models.LargeAzFileUpload> LargeAzFileUploads = null;
            if (!string.IsNullOrEmpty(content))
            {
                LargeAzFileUploads = JsonSerializer.Deserialize<List<Models.LargeAzFileUpload>>(content);
            }
            if (LargeAzFileUploads != null)
            {
                foreach(var LargeAzFileUpload in LargeAzFileUploads)
                {
                    _LargeAzFileUploadRepository.AddLargeAzFileUpload(new Models.LargeAzFileUpload { ModuleId = module.ModuleId, Name = LargeAzFileUpload.Name });
                }
            }
        }
    }
}