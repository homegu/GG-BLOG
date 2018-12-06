using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebApp.Infrastructure
{
    public class Response
    {
        /// <summary>
        /// 操作消息【当Status不为 200时，显示详细的错误信息】
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 操作状态码，200为正常
        /// </summary>
        public ResponseCodeEnum Code { get; set; }

        public Response()
        {
            Code =  ResponseCodeEnum.success;
            Message = "操作成功";
        }
    }

    /// <summary>
    /// 状态码
    /// </summary>
    public enum ResponseCodeEnum
    {
        success = 200,
        error = 500,
        badRequest = 400,
        noFind = 404,
        unauthorized = 401,
        requestError = 502,
        user_break = 999,
    }

    /// <summary>
    /// WEBAPI通用返回泛型基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T> : Response
    {
        /// <summary>
        /// 回传的结果
        /// </summary>
        public T Result { get; set; }
    }
}
