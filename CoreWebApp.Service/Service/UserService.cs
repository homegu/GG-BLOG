using CoreWebApp.Data.DBContext;
using CoreWebApp.Model;
using CoreWebApp.Repository;
using CoreWebApp.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebApp.Service
{
    public class UserService: Repository<User>
    {
        protected DataContext _repo;

        public UserService(DataContext repo):base(repo)
        {
            _repo = repo;
        }

    }
}
