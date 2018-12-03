using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebApp.Infrastructure;
using CoreWebApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CoreWebApp.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IConfiguration _configuration;
        protected readonly JwtSettings jwtSettings;

        public BaseController()
        {
            _configuration =  AutofacCore.GetFromFac<IConfiguration>();
            var jwtSetting = AutofacCore.GetFromFac<IOptions<JwtSettings>>();
            jwtSettings = jwtSetting.Value;
        }
    }
}