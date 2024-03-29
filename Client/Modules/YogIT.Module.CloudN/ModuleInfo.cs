using Oqtane.Models;
using Oqtane.Modules;

namespace YogIT.Module.CloudN
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "CloudN",
            Description = "A file upload tool for cloud storage in Oqtane",
            Version = "1.0.0",
            ServerManagerType = "YogIT.Module.CloudN.Manager.CloudNManager, YogIT.Module.CloudN.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "YogIT.Module.CloudN.Shared.Oqtane",
            PackageName = "YogIT.Module.CloudN" 
        };
    }
}
