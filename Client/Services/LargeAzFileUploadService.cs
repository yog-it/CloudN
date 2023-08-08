using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage;
using Microsoft.Extensions.Options;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;
using YogIT.LargeAzFileUpload.Shared.Models;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;

namespace YogIT.LargeAzFileUpload.Services
{
    public class LargeAzFileUploadService : ServiceBase, ILargeAzFileUploadService, IService
    {
        private readonly IOptions<AzureStorageConfig> _options;

        public LargeAzFileUploadService(
            HttpClient http,
            SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("LargeAzFileUpload");

        public async Task<List<string>> GetContainersAsync(AzureStorageConfig options, string prefix, int segmentSize)
        {
            Uri blobUri = new Uri("https://" +
                                  options.AccountName +
                                  ".blob.core.windows.net/");

            StorageSharedKeyCredential storageCredentials =
            new StorageSharedKeyCredential(options.AccountName, options.AccountKey);

            BlobServiceClient blobClient = new BlobServiceClient(blobUri, storageCredentials);

            var resultSegment =
                        blobClient.GetBlobContainersAsync(BlobContainerTraits.Metadata, "", default)
                        .AsPages(default, segmentSize);

            List<string> containers = new List<string>();
            await foreach (Azure.Page<BlobContainerItem> containerPage in resultSegment)
            {
                foreach (BlobContainerItem containerItem in containerPage.Values)
                {
                    Uri blobContainerUri = new Uri("https://" +
                                  options.AccountName +
                                  ".blob.core.windows.net/" + containerItem.Name);
                    BlobContainerClient blobContainerClient = new BlobContainerClient(blobContainerUri, storageCredentials);
                    var containerSegment = blobContainerClient.GetBlobsAsync(prefix: prefix).AsPages(pageSizeHint: 50).GetAsyncEnumerator();
                    await containerSegment.MoveNextAsync();
                    var blobPage = containerSegment.Current.Values;
                    if (blobPage.Count != 0) 
                        containers.Add(containerItem.Name);
                }
            }
            return containers;
        }

        public async Task<Uri> UploadFileToStorage(AzureStorageConfig options, Stream stream, string container, string prefix, string fileName)
        {
            Uri blobUri = new Uri("https://" +
                                  options.AccountName +
                                  ".blob.core.windows.net/" +
                                  container + "/" + prefix + "/" + fileName);

            StorageSharedKeyCredential storageCredentials =
                new StorageSharedKeyCredential(options.AccountName, options.AccountKey);

            // Upload the file
            BlockBlobClient blobClient = new BlockBlobClient(blobUri, storageCredentials);
            await blobClient.UploadAsync(stream);

            return blobUri;
        }
    }
}
