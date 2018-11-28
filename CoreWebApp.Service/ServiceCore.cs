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
    }
}
