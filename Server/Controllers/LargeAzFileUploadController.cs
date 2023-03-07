using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using YogIT.LargeAzFileUpload.Repository;
using Oqtane.Controllers;
using System.Net;

namespace YogIT.LargeAzFileUpload.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class LargeAzFileUploadController : ModuleControllerBase
    {
        private readonly ILargeAzFileUploadRepository _LargeAzFileUploadRepository;

        public LargeAzFileUploadController(ILargeAzFileUploadRepository LargeAzFileUploadRepository, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _LargeAzFileUploadRepository = LargeAzFileUploadRepository;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.LargeAzFileUpload> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && ModuleId == AuthEntityId(EntityNames.Module))
            {
                return _LargeAzFileUploadRepository.GetLargeAzFileUploads(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized LargeAzFileUpload Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.LargeAzFileUpload Get(int id)
        {
            Models.LargeAzFileUpload LargeAzFileUpload = _LargeAzFileUploadRepository.GetLargeAzFileUpload(id);
            if (LargeAzFileUpload != null && LargeAzFileUpload.ModuleId == AuthEntityId(EntityNames.Module))
            {
                return LargeAzFileUpload;
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized LargeAzFileUpload Get Attempt {LargeAzFileUploadId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.LargeAzFileUpload Post([FromBody] Models.LargeAzFileUpload LargeAzFileUpload)
        {
            if (ModelState.IsValid && LargeAzFileUpload.ModuleId == AuthEntityId(EntityNames.Module))
            {
                LargeAzFileUpload = _LargeAzFileUploadRepository.AddLargeAzFileUpload(LargeAzFileUpload);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "LargeAzFileUpload Added {LargeAzFileUpload}", LargeAzFileUpload);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized LargeAzFileUpload Post Attempt {LargeAzFileUpload}", LargeAzFileUpload);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                LargeAzFileUpload = null;
            }
            return LargeAzFileUpload;
        }

        // PUT api/<controller>/5
        [ValidateAntiForgeryToken]
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.LargeAzFileUpload Put(int id, [FromBody] Models.LargeAzFileUpload LargeAzFileUpload)
        {
            if (ModelState.IsValid && LargeAzFileUpload.ModuleId == AuthEntityId(EntityNames.Module) && _LargeAzFileUploadRepository.GetLargeAzFileUpload(LargeAzFileUpload.LargeAzFileUploadId, false) != null)
            {
                LargeAzFileUpload = _LargeAzFileUploadRepository.UpdateLargeAzFileUpload(LargeAzFileUpload);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "LargeAzFileUpload Updated {LargeAzFileUpload}", LargeAzFileUpload);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized LargeAzFileUpload Put Attempt {LargeAzFileUpload}", LargeAzFileUpload);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                LargeAzFileUpload = null;
            }
            return LargeAzFileUpload;
        }

        // DELETE api/<controller>/5
        [ValidateAntiForgeryToken]
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            Models.LargeAzFileUpload LargeAzFileUpload = _LargeAzFileUploadRepository.GetLargeAzFileUpload(id);
            if (LargeAzFileUpload != null && LargeAzFileUpload.ModuleId == AuthEntityId(EntityNames.Module))
            {
                _LargeAzFileUploadRepository.DeleteLargeAzFileUpload(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "LargeAzFileUpload Deleted {LargeAzFileUploadId}", id);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized LargeAzFileUpload Delete Attempt {LargeAzFileUploadId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
