using CoreWebApp.Data.DBContext;
using CoreWebApp.Model;
using CoreWebApp.Repository;
using CoreWebApp.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebApp.Service.Module
{
    public class UserService: ServiceCore<User>
    {
        public UserService(IRepository<User> repo, IUnitWork unitwork) :
            base(repo, unitwork)
        {

        }

        public void test() {

            using (var tran = _unitwork.BeginTransaction())
            {
                var user = _repo.FindBy(x => x.Id ==
                "01d93810-e753-4e22-a187-6047fb554ac7");
                user.UserName = "admin";
                _repo.Update(user);
                tran.Commit();
            }
        }
    }
}
