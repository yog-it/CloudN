using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using YogIT.Module.CloudN.Repository;
using Oqtane.Controllers;
using System.Net;

namespace YogIT.Module.CloudN.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class CloudNController : ModuleControllerBase
    {
        private readonly ICloudNRepository _CloudNRepository;

        public CloudNController(ICloudNRepository CloudNRepository, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _CloudNRepository = CloudNRepository;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.CloudN> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return _CloudNRepository.GetCloudNs(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized CloudN Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.CloudN Get(int id)
        {
            Models.CloudN CloudN = _CloudNRepository.GetCloudN(id);
            if (CloudN != null && IsAuthorizedEntityId(EntityNames.Module, CloudN.ModuleId))
            {
                return CloudN;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized CloudN Get Attempt {CloudNId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.CloudN Post([FromBody] Models.CloudN CloudN)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, CloudN.ModuleId))
            {
                CloudN = _CloudNRepository.AddCloudN(CloudN);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "CloudN Added {CloudN}", CloudN);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized CloudN Post Attempt {CloudN}", CloudN);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                CloudN = null;
            }
            return CloudN;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.CloudN Put(int id, [FromBody] Models.CloudN CloudN)
        {
            if (ModelState.IsValid && CloudN.CloudNId == id && IsAuthorizedEntityId(EntityNames.Module, CloudN.ModuleId) && _CloudNRepository.GetCloudN(CloudN.CloudNId, false) != null)
            {
                CloudN = _CloudNRepository.UpdateCloudN(CloudN);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "CloudN Updated {CloudN}", CloudN);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized CloudN Put Attempt {CloudN}", CloudN);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                CloudN = null;
            }
            return CloudN;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            Models.CloudN CloudN = _CloudNRepository.GetCloudN(id);
            if (CloudN != null && IsAuthorizedEntityId(EntityNames.Module, CloudN.ModuleId))
            {
                _CloudNRepository.DeleteCloudN(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "CloudN Deleted {CloudNId}", id);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized CloudN Delete Attempt {CloudNId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
