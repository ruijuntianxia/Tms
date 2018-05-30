using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TmsApi.Dall;
using TmsApi.Models;

namespace TmsApi.Bll
{
    public class ResultRedisBll
    {
        ResultRedisDAll resultGetRedis = new ResultRedisDAll();

        internal object GetTvMapRedis(ReportTvModels olap, IOptions<AppSettings> settings) => resultGetRedis.GetTvMapRedis(olap, settings);

        internal object GetTptShipmentCountRedis(ReportTvModels olap, IOptions<AppSettings> settings)
            => resultGetRedis.GetTptShipmentCountRedis(olap, settings);
        
    }
}
