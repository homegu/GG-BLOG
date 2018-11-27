using CoreWebApp.Data.DBContext;
using CoreWebApp.Model;
using CoreWebApp.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Z.EntityFramework.Plus;
using CoreWebApp.Core;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Storage;

namespace CoreWebApp.Repository
{
    public class UnitWork: IUnitWork
    {
        private readonly DbContext _context;

        public UnitWork(DbContext context)
        {
            _context = context;
        }

        public DbTransaction BeginTransaction()
        {
            _context.Database.BeginTransaction();

            return _context.Database.CurrentTransaction.GetDbTransaction();
        }

        public void CommitTransaction()
        {
            _context.SaveChanges();
            _context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

    }
}
