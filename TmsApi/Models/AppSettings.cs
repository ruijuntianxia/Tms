using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TmsApi.Models
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// sql 链接
        /// </summary>
        public SqlConnectionString SqlConnectionString { get; set; }
        /// <summary>
        /// oracle链接
        /// </summary>
        public OraConnectionString OraConnectionString { get; set; }
        /// <summary>
        /// redis 地图数据
        /// </summary>
        public RedisMap redisMap
        { get; set; }
    }
    /// <summary>
    /// sql server 链接
    /// </summary>
    public class SqlConnectionString
    {
        /// <summary>
        /// TestConnect
        /// </summary>
        public string Test193 { get; set; }
        public string Test127sx { get; set; }
        public string Test127sc { get; set; }

    }

    /// <summary>
    /// oracle 链接
    /// </summary>
    public class OraConnectionString
    {
        /// <summary>
        /// 测试oledb链接
        /// </summary>
        public string TestScmpOledb { get; set; }
        /// <summary>
        /// 测试odbc链接
        /// </summary>
        public string TestScmpOdbc { get; set; }
    }

    /// <summary>
    /// Redis 
    /// </summary>
    public class RedisMap
    {
        /// <summary>
        /// ip
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public string Post { get; set; }
        /// <summary>
        /// key值
        /// </summary>
        public string key { get; set; }

    }
}
