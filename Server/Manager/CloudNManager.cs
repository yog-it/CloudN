using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Enums;
using Oqtane.Repository;
using YogIT.Module.CloudN.Repository;

namespace YogIT.Module.CloudN.Manager
{
    public class CloudNManager : MigratableModuleBase, IInstallable, IPortable
    {
        private readonly ICloudNRepository _CloudNRepository;
        private readonly IDBContextDependencies _DBContextDependencies;

        public CloudNManager(ICloudNRepository CloudNRepository, IDBContextDependencies DBContextDependencies)
        {
            _CloudNRepository = CloudNRepository;
            _DBContextDependencies = DBContextDependencies;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new CloudNContext(_DBContextDependencies), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new CloudNContext(_DBContextDependencies), tenant, MigrationType.Down);
        }

        public string ExportModule(Oqtane.Models.Module module)
        {
            string content = "";
            List<Models.CloudN> CloudNs = _CloudNRepository.GetCloudNs(module.ModuleId).ToList();
            if (CloudNs != null)
            {
                content = JsonSerializer.Serialize(CloudNs);
            }
            return content;
        }

        public void ImportModule(Oqtane.Models.Module module, string content, string version)
        {
            List<Models.CloudN> CloudNs = null;
            if (!string.IsNullOrEmpty(content))
            {
                CloudNs = JsonSerializer.Deserialize<List<Models.CloudN>>(content);
            }
            if (CloudNs != null)
            {
                foreach(var CloudN in CloudNs)
                {
                    _CloudNRepository.AddCloudN(new Models.CloudN { ModuleId = module.ModuleId, FileName = CloudN.FileName, ContentType = CloudN.ContentType, Url = CloudN.Url });
                }
            }
        }
    }
}
