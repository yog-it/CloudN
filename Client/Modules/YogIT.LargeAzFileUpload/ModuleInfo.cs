using Oqtane.Models;
using Oqtane.Modules;

namespace YogIT.LargeAzFileUpload
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "LargeAzFileUpload",
            Description = "Large Azure File Uploader",
            Version = "1.0.1",
            ServerManagerType = "YogIT.LargeAzFileUpload.Manager.LargeAzFileUploadManager, YogIT.LargeAzFileUpload.Server.Oqtane",
            ReleaseVersions = "1.0.1",
            Dependencies = "YogIT.LargeAzFileUpload.Shared.Oqtane",
            PackageName = "YogIT.LargeAzFileUpload" 
        };
    }
}
