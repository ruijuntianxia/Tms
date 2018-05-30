using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TmsApi.Dall;
using TmsApi.Models;

namespace TmsApi.Bll
{
    public class ResultGetTableBll
    {

        ResultGetTableTV resultGetTableTV = new ResultGetTableTV();


        public DataTable GetTvMap(ReportTvModels olap, IOptions<AppSettings> settings) => resultGetTableTV.GetTvMap(olap, settings);

        internal DataTable GetTodOrder(ReportTvModels olap, IOptions<AppSettings> settings)=> resultGetTableTV.GetTodOrder(olap, settings);

        internal DataTable GetTransPortaTion(ReportTvModels olap, IOptions<AppSettings> settings) => resultGetTableTV.GetTransPortaTion(olap, settings);

        internal DataTable GetTptShipment(ReportTvModels olap, IOptions<AppSettings> settings) => resultGetTableTV.GetTptShipment(olap, settings);

        internal DataTable getresult(ReportTvModels olap, IOptions<AppSettings> settings) => resultGetTableTV.getresult(olap, settings);

        internal DataTable getresultTable(ReportTvModels olap, IOptions<AppSettings> settings) => resultGetTableTV.getresultTable(olap, settings);
        

        internal DataTable getresultDet(ReportTvModels olap, IOptions<AppSettings> settings) => resultGetTableTV.getresultDet(olap, settings);

        internal DataTable GetTptShipmentCount(ReportTvModels olap, IOptions<AppSettings> settings) => resultGetTableTV.GetTptShipmentCount(olap, settings);

        internal object GetVacancy(ReportTvModels olap, IOptions<AppSettings> settings) => resultGetTableTV.GetVacancy(olap, settings);
        
    }
}
