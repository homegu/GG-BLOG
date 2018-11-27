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
    /// <summary>
    /// 工作单元接口
    /// <para> 适合在一下情况使用:</para>
    /// <para>1 在同一事务中进行多表操作</para>
    /// <para>2 需要多表联合查询</para>
    /// <para>因为架构采用的是EF访问数据库，暂时可以不用考虑采用传统Unit Work的注册机制</para>
    /// </summary>
    public interface IUnitWork
    {
        DbTransaction BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}
