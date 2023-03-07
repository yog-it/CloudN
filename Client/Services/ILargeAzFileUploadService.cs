using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using YogIT.LargeAzFileUpload.Shared.Models;

namespace YogIT.LargeAzFileUpload.Services
{
    public interface ILargeAzFileUploadService
    {
        Task<Uri> UploadFileToStorage(AzureStorageConfig options, Stream stream, string container, string prefix, string fileName);
        Task<List<string>> GetContainersAsync(AzureStorageConfig options, string prefix, int segmentSize);
    }
}
