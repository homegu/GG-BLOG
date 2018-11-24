using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApp.Data.Extensions
{
    public class DbHelper
    {
        private static string connstring = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        public static object GetDataTable(string v, object commandType)
        {
            throw new NotImplementedException();
        }

        //数据提供程序
        private static string dbPro = "System.Data.SqlClient";

        //数据工厂
        private static DbProviderFactory db = DbProviderFactories.GetFactory(dbPro);
        private static DbConnection conn;
        private static DataSet dt;
        private static DbDataAdapter da;


        //创建并打开连接
        public static DbConnection Conn
        {
            get
            {
                if (conn == null)
                {
                    conn = db.CreateConnection();
                    conn.ConnectionString = connstring;
                }
                switch (conn.State)
                {
                    case ConnectionState.Broken:
                        conn.Close();
                        conn.Open();
                        break;
                    case ConnectionState.Closed:
                        conn.Open();
                        break;
                }

                return conn;
            }
        }

        /// <summary>
        /// 创建命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public static DbCommand CreateCommand(string sql, CommandType commandType, params DbParameter[] sqlParams)
        {
            DbCommand comm = db.CreateCommand();
            comm.Connection = Conn;
            comm.CommandType = commandType;
            comm.CommandText = sql;

            if (sqlParams != null)
            {
                comm.Parameters.Clear();
                comm.Parameters.AddRange(sqlParams);


            }

            return comm;
        }

        /// <summary>
        /// 返回受影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, CommandType commandType, params DbParameter[] sqlParams)
        {
            DbCommand comm = CreateCommand(sql, commandType, sqlParams);
            int num = comm.ExecuteNonQuery();
            comm.Connection.Close();
            return num;
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>       
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlTran(List<String> SQLStringList)
        {
            DbCommand comm = db.CreateCommand();
            comm.Connection = Conn;
            DbTransaction tx = Conn.BeginTransaction();
            comm.Transaction = tx;
            try
            {
                int count = 0;
                for (int n = 0; n < SQLStringList.Count; n++)
                {
                    string strsql = SQLStringList[n];
                    if (strsql.Trim().Length > 1)
                    {
                        comm.CommandText = strsql;
                        count += comm.ExecuteNonQuery();
                    }
                }
                tx.Commit();
                return count;
            }
            catch
            {
                tx.Rollback();
                return 0;
            }

        }

        /// <summary>
        /// 返回第一行第一列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public static T ExecuteScalar<T>(string sql, CommandType commandType, params DbParameter[] sqlParams)
        {
            DbCommand comm = CreateCommand(sql, commandType, sqlParams);
            T t = (T)comm.ExecuteScalar();
            comm.Connection.Close();
            return t;
        }

        /// <summary>
        /// 返回DataReader
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(string sql, CommandType commandType, params DbParameter[] sqlParams)
        {
            DbCommand comm = CreateCommand(sql, commandType, sqlParams);
            return comm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// 返回数据集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql, CommandType commandType, params DbParameter[] sqlParams)
        {
            DbCommand comm = CreateCommand(sql, commandType, sqlParams);
            da = db.CreateDataAdapter();
            da.SelectCommand = comm;
            dt = new DataSet();
            da.Fill(dt);

            return dt.Tables[0];
        }



    }
}
