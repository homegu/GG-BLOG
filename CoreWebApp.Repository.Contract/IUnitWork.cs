using CoreWebApp.Data.DBContext;
using CoreWebApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CoreWebApp.Repository.Contract
{
    public interface IUnitWork
    {
        DbTransaction BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}
