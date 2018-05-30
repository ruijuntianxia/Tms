using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TmsApi.Models
{

    public class ReportTvModels
    {
        /// <summary>
        /// 业务分类
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string page { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 每页显示项目数
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// 当前页（跳转后的页面编号）
        /// </summary>
        public int pageIndex { get; set; }

        /// <summary>
        /// 承接数据库链接
        /// </summary>
        public string connect { get; set; }

        /// <summary>
        /// 承接sql语句
        /// </summary>
        public string sql { get; set; }



        /// <summary>
        /// 仓库
        /// </summary>
        public string warehouse { get; set; }


        /// <summary>
        /// 仓位
        /// </summary>
        public string positions { get; set; }



        /// <summary>
        /// 储位信息
        /// </summary>
        public string location { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 装运编号
        /// </summary>
        public string ShipmentNo{ get; set; }
    }
}
