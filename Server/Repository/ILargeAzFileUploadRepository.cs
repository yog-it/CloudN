using System.Collections.Generic;
using YogIT.LargeAzFileUpload.Models;

namespace YogIT.LargeAzFileUpload.Repository
{
    public interface ILargeAzFileUploadRepository
    {
        IEnumerable<Models.LargeAzFileUpload> GetLargeAzFileUploads(int ModuleId);
        Models.LargeAzFileUpload GetLargeAzFileUpload(int LargeAzFileUploadId);
        Models.LargeAzFileUpload GetLargeAzFileUpload(int LargeAzFileUploadId, bool tracking);
        Models.LargeAzFileUpload AddLargeAzFileUpload(Models.LargeAzFileUpload LargeAzFileUpload);
        Models.LargeAzFileUpload UpdateLargeAzFileUpload(Models.LargeAzFileUpload LargeAzFileUpload);
        void DeleteLargeAzFileUpload(int LargeAzFileUploadId);
    }
}
