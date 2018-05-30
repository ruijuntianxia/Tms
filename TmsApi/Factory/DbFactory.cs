using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmsApi.Factory
{
    /// <summary>
    /// 工厂类
    /// </summary>
    public class DbFactory
    {
        /// <summary>
        /// CreateDbConnection
        /// </summary>
        /// <param name="type"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IDbConnection CreateDbConnection(DbProvider type, string connectionString)
        {
            IDbConnection conn = null;
            switch (type)
            {
                case DbProvider.ORACLE:
                    conn = new OracleConnection(connectionString);                   
                    break;
                case DbProvider.SQLSERVER:
                    conn = new SqlConnection(connectionString);
                    break;
             
                case DbProvider.NONE:
                    throw new Exception("未设置数据库类型");
                default:
                    throw new Exception("不支持该数据库类型");
            }
            return conn;
        }

        /// <summary>
        /// 建立 DbCommand
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IDbCommand CreateDbCommand(DbProvider type)
        {
            IDbCommand cmd = null;
            switch (type)
            {
                case DbProvider.ORACLE:
                    cmd = new OracleCommand();
                    break;
                case DbProvider.SQLSERVER:
                    cmd = new SqlCommand();
                    break;
               
                case DbProvider.NONE:
                    throw new Exception("未设置数据库类型");
                default:
                    throw new Exception("不支持该数据库类型");
            }
            return cmd;
        }
        /// <summary>
        /// CreateDbCommand
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static IDbCommand CreateDbCommand(string sql, IDbConnection conn)
        {
            DbProvider type = DbProvider.NONE;
            if (conn is OracleConnection)
                type = DbProvider.ORACLE;
            else if (conn is SqlConnection)
                type = DbProvider.SQLSERVER;
            

            IDbCommand cmd = null;
            switch (type)
            {
                case DbProvider.ORACLE:
                    cmd = new OracleCommand(sql, (OracleConnection)conn);
                    break;
                case DbProvider.SQLSERVER:
                    cmd = new SqlCommand(sql, (SqlConnection)conn);
                    break;
             
                case DbProvider.NONE:
                    throw new Exception("未设置数据库类型");
                default:
                    throw new Exception("不支持该数据库类型");
            }
            return cmd;
        }

    }
}
