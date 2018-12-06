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
using Newtonsoft.Json;

namespace CoreWebApp.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IConfiguration _configuration;
        protected readonly JwtSettings jwtSettings;
        protected readonly HttpContext context;

        public BaseController()
        {
            var httpContextAccessor = AutofacCore.GetFromFac<IHttpContextAccessor>();
            context = httpContextAccessor.HttpContext;
            _configuration = AutofacCore.GetFromFac<IConfiguration>();
            var jwtSetting = AutofacCore.GetFromFac<IOptions<JwtSettings>>();
            jwtSettings = jwtSetting.Value;
        }

        //[ApiExplorerSettings(IgnoreApi = true)]
        //public Task OutputJSON(Response response)
        //{
        //    var result = JsonConvert.SerializeObject(response);
        //    context.Response.ContentType = "application/json;charset=utf-8";
        //    return context.Response.WriteAsync(result);
        //}

        [ApiExplorerSettings(IgnoreApi = true)]
        public Response Success(string message)
        {
            return new Response()
            {
                Code =  ResponseCodeEnum.success,
                Message = message,
            };
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public Response<TEntity> Success<TEntity>(string message,TEntity data)
        {
            return new Response<TEntity>()
            {
                Code = ResponseCodeEnum.success,
                Message = message,
                Result = data
            };
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected Response Failed(string message)
        {
            return new Response
            {
                Code =  ResponseCodeEnum.error,
                Message = message,
            };
        }
    }
}