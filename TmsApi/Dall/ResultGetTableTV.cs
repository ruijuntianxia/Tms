using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TmsApi.Factory;
using TmsApi.Models;

namespace TmsApi.Dall
{
    public class ResultGetTableTV
    {
        
       
        private DbUtility db;
        
        /// <summary>
        /// 运输
        /// </summary>
        private string sqlMapXy = @"SELECT * FROM [UV_VehicleTrack]  where terminalNo='14531737004'";
        private string oraTodOrder = @"select a.* from V_todorder a";
        private string oraTransPortaTion =@"select a.* from  v_transportation a";
        private string oraTptShipment= @"SELECT *  FROM (SELECT ROWNUM AS rowno, t.* FROM v_tptshipment t WHERE ROWNUM<={1} ) table_alias WHERE table_alias.rowno > {0}";

        private string oraTptShipmentCount = @"select *  from V_ShipMentCount ";//,'INTRANSIT'

        /// <summary>
        /// 储位信息
        /// </summary>
        private string sqlStorageDet = @"SELECT *  FROM [WMS_Zohoo_sx2].[dbo].[UV_StorageDet] where 1=1  ";
        private string sqlStorageVac = @"SELECT *  FROM [WMS_Zohoo_sx2].[dbo].[UV_StorageVac] where 1=1  ";
        private string sqlReserveDt = @"select * from [dbo].[UV_ReserveDt] order by [ResDate] ";
        private string sqlStorageDetl = @"SELECT [Location]      ,[ProductCode]      ,[MaterialName]      ,[ProductUnit]
      ,[SupName]      ,[Qty]  FROM [dbo].[UV_StorageDetl] where 1=1 ";

       

        private string sqlStorageDetl2 = @"SELECT [Location]      ,[storage],[ProductCode]      ,[MaterialName]      ,[ProductUnit]
      ,[SupName]      ,[Qty]  FROM [dbo].[UV_StorageDetl] where 1=1";

       

        private string sqlStorageDetN =@"SELECT [Location] ,[LocationType] ,[LocationNum] ,[Value]  FROM [UV_Storage] 
  where locationtype like '{0}%'";

        /// <summary>
        /// 大屏展示 储位信息
        /// </summary>
        /// <param name="olap"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public DataTable getresult(ReportTvModels olap, IOptions<AppSettings> settings)
        {
            
            if (olap.name == "SCNJ")
            {
                olap.sql = string.Format(sqlStorageDetN, olap.name);
                olap.connect = settings.Value.SqlConnectionString.Test127sc;
            }
            if (olap.name == "SX")
            {
                olap.sql = sqlStorageDet;
                olap.sql +=string.IsNullOrWhiteSpace( olap.warehouse) ? "" : string.Format(@" and warehouse='{0}'", olap.warehouse);
                olap.connect = settings.Value.SqlConnectionString.Test127sx;
            }
            db = new DbUtility(olap);

            return db.ExecuteReader(olap);
        }

        /// <summary>
        /// 储位空置率
        /// </summary>
        /// <param name="olap"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        internal DataTable GetVacancy(ReportTvModels olap, IOptions<AppSettings> settings)
        {
            if (olap.name == "SCNJ")
            {
                olap.sql = string.Format(sqlStorageDetN, olap.name);
                olap.connect = settings.Value.SqlConnectionString.Test127sc;
            }
            if (olap.name == "SX")
            {
                olap.sql =sqlStorageVac;
                olap.sql += string.IsNullOrWhiteSpace(olap.warehouse) ? "" : string.Format(@" and warehouse='{0}'", olap.warehouse);
                olap.sql += " order By [positions]";
                olap.connect = settings.Value.SqlConnectionString.Test127sx;
            }
            db = new DbUtility(olap);

            return db.ExecuteReader(olap);
        }

        /// <summary>
        /// 大屏展示 预约信息
        /// </summary>
        /// <param name="olap"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        internal DataTable getresultTable(ReportTvModels olap, IOptions<AppSettings> settings)
        {

            if (olap.name == "SCNJ")
            {
                olap.sql = sqlReserveDt;
                olap.connect = settings.Value.SqlConnectionString.Test127sc;

            }
            else if (olap.name == "SX")
            {
                olap.sql = sqlReserveDt;
                olap.connect = settings.Value.SqlConnectionString.Test127sx;

            }
            db = new DbUtility(olap);
            return db.ExecuteReader(olap);

        }


        /// <summary>
        /// 大屏展示 储位明细信息
        /// </summary>
        /// <param name="olap"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        internal DataTable getresultDet(ReportTvModels olap, IOptions<AppSettings> settings)
        {
            if (olap.name == "SCNJ")
            {
                olap.sql =
                    sqlStorageDetl += string.IsNullOrWhiteSpace(olap.name + olap.location) ? "" :
                        string.Format(@" and Location='{0}'", olap.name + olap.location);

                olap.connect = settings.Value.SqlConnectionString.Test127sc;

            }
            else if (olap.name == "SX")
            {
                olap.sql = sqlStorageDetl2;
                olap.sql +=string.IsNullOrWhiteSpace( olap.warehouse) ? "" : string.Format(@"  and warehouse ='{0}'", olap.warehouse);
                olap.sql += string.IsNullOrWhiteSpace(olap.positions) ? "" : string.Format(@"  and positions ='{0}'", olap.positions);
                olap.sql += string.IsNullOrWhiteSpace(olap.location)  ? "" : (olap.location == "down" ? @"  and dan <=20" : "  and dan >20");
                olap.connect = settings.Value.SqlConnectionString.Test127sx;


            }
            db = new DbUtility(olap);
            return db.ExecuteReader(olap);
        }


        /// <summary>
        /// 大屏展示 地图坐标 弃用 MapOrder
        /// </summary>
        /// <param name="olap"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        internal DataTable GetTvMap(ReportTvModels olap, IOptions<AppSettings> settings)
        {
            if (olap.name == "SX")
            {
                olap.sql = sqlMapXy;
                olap.connect = settings.Value.SqlConnectionString.Test193;//OnlineTjSX;

            }
            db = new DbUtility(olap);
            return db.ExecuteReader(olap);
        }



        /// <summary>
        /// 大屏展示 Today Order
        /// </summary>
        /// <param name="olap"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        internal DataTable GetTodOrder(ReportTvModels olap, IOptions<AppSettings> settings)
        {
            if (olap.name == "SX")
            {
                olap.sql = oraTodOrder;
                olap.connect = settings.Value.OraConnectionString.TestScmpOdbc;//OnlineTjSX;

            }
            db = new DbUtility(olap, DbProvider.ORACLE);
            return db.ExecuteReader(olap);
        }

        /// <summary>
        /// TransPortaTion
        /// </summary>
        /// <param name="olap"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        internal DataTable GetTransPortaTion(ReportTvModels olap, IOptions<AppSettings> settings)
        {
            if (olap.name == "SX")
            {
                olap.sql = oraTransPortaTion;
                olap.connect = settings.Value.OraConnectionString.TestScmpOdbc;//OnlineTjSX;

            }
            db = new DbUtility(olap, DbProvider.ORACLE);
            return db.ExecuteReader(olap);
        }


        /// <summary>
        /// TptShipment
        /// </summary>
        /// <param name="olap"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        internal DataTable GetTptShipment(ReportTvModels olap, IOptions<AppSettings> settings)
        {
            if (olap.name == "SX")
            {
                olap.sql =string.Format(oraTptShipment,(olap.pageIndex-1)*olap.pageSize,(olap.pageIndex*olap.pageSize)) ;
                olap.connect = settings.Value.OraConnectionString.TestScmpOdbc;//OnlineTjSX;

            }
            db = new DbUtility(olap, DbProvider.ORACLE);
            return db.ExecuteReader(olap);
        }
        /// <summary>
        /// 运单总数
        /// </summary>
        /// <returns></returns>
        internal DataTable GetTptShipmentCount(ReportTvModels olap, IOptions<AppSettings> settings)
        {
            if (olap.name == "SX")
            {
                olap.sql = oraTptShipmentCount;
                olap.connect = settings.Value.OraConnectionString.TestScmpOdbc;//OnlineTjSX;

            }
            db = new DbUtility(olap, DbProvider.ORACLE);
            return db.ExecuteReader(olap);
        }
    }
}
