﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using rift.Dto;

namespace rift.web.Controllers
{
    [Route("management")]
    [ApiController]
    public class ProfileInfoController : ControllerBase
    {

        private readonly ILogger<ProfileInfoController> _log;
        private readonly IHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public ProfileInfoController(ILogger<ProfileInfoController> log, IHostEnvironment environment, IConfiguration configuration)
        {
            _log = log;
            _environment = environment;
            _configuration = configuration;
        }

        [HttpGet("info")]
        public ActionResult<ProfileInfoDto> GetProfileInfos()
        {
            _log.LogDebug("REST request to get profile informations");
            return Ok(new ProfileInfoDto(GetActiveProfile()));
        }

        private List<string> GetActiveProfile()
        {
            var activeProfiles = new List<string>
            {
                "swagger"
            };

            if (_environment.IsDevelopment())
            {
                activeProfiles.Add("dev");
            }
            else if (_environment.IsProduction())
            {
                activeProfiles.Add("prod");
            }
            else if (_environment.IsStaging())
            {
                activeProfiles.Add("stag");
            }
            return activeProfiles;
        }
    }
}
