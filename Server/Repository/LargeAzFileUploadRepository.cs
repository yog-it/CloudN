using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using YogIT.LargeAzFileUpload.Models;

namespace YogIT.LargeAzFileUpload.Repository
{
    public class LargeAzFileUploadRepository : ILargeAzFileUploadRepository, IService
    {
        private readonly LargeAzFileUploadContext _db;

        public LargeAzFileUploadRepository(LargeAzFileUploadContext context)
        {
            _db = context;
        }

        public IEnumerable<Models.LargeAzFileUpload> GetLargeAzFileUploads(int ModuleId)
        {
            return _db.LargeAzFileUpload.Where(item => item.ModuleId == ModuleId);
        }

        public Models.LargeAzFileUpload GetLargeAzFileUpload(int LargeAzFileUploadId)
        {
            return GetLargeAzFileUpload(LargeAzFileUploadId, true);
        }

        public Models.LargeAzFileUpload GetLargeAzFileUpload(int LargeAzFileUploadId, bool tracking)
        {
            if (tracking)
            {
                return _db.LargeAzFileUpload.Find(LargeAzFileUploadId);
            }
            else
            {
                return _db.LargeAzFileUpload.AsNoTracking().FirstOrDefault(item => item.LargeAzFileUploadId == LargeAzFileUploadId);
            }
        }

        public Models.LargeAzFileUpload AddLargeAzFileUpload(Models.LargeAzFileUpload LargeAzFileUpload)
        {
            _db.LargeAzFileUpload.Add(LargeAzFileUpload);
            _db.SaveChanges();
            return LargeAzFileUpload;
        }

        public Models.LargeAzFileUpload UpdateLargeAzFileUpload(Models.LargeAzFileUpload LargeAzFileUpload)
        {
            _db.Entry(LargeAzFileUpload).State = EntityState.Modified;
            _db.SaveChanges();
            return LargeAzFileUpload;
        }

        public void DeleteLargeAzFileUpload(int LargeAzFileUploadId)
        {
            Models.LargeAzFileUpload LargeAzFileUpload = _db.LargeAzFileUpload.Find(LargeAzFileUploadId);
            _db.LargeAzFileUpload.Remove(LargeAzFileUpload);
            _db.SaveChanges();
        }
    }
}
