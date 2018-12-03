using CoreWebApp.Infrastructure;
using CoreWebApp.Model;
using CoreWebApp.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebApp.Service
{
    /// <summary>
    /// 为了更加方便的autofac注册
    /// </summary>
    public interface IService
    {

    }

    public class ServiceCore<T>: IService where T : Entity
    {
        protected IRepository<T> _repo;
        protected IUnitWork _unitwork;

        public ServiceCore(IRepository<T> repo, IUnitWork unitwork)
        {
            _repo = repo;
            _unitwork = unitwork;
        }

        public Response<T> Success(T data)
        {
            return new Response<T>
            {
                Code = 200,
                Message = string.Empty,
                Result = data
            };
        }

        public Response<TEntity> Success<TEntity>(TEntity data)
        {
            return new Response<TEntity>
            {
                Code = 200,
                Message = string.Empty,
                Result = data
            };
        }

        protected Response<TEntity> Failed<TEntity>(string message)
        {
            return new Response<TEntity>
            {
                Code = 500,
                Message = message,
            };
        }

        protected Response<T> Failed(string message,int code)
        {
            return new Response<T>
            {
                Code = code,
                Message = message,
            };
        }

        protected Response<T> Failed(string message)
        {
            return new Response<T>
            {
                Code = 500,
                Message = message,
            };
        }

    }
    
}
