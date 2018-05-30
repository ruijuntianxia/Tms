using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TmsApi.DTO.Response
{
    public class ResponseMapDTO
    {
        public string ROWNO { get; set; }
        public string TMS_SHIPMENT_NO { get; set; }
        public string TMS_PLATE_NUMBER { get; set; }
        public string DOGNUMBER { get; set; }
        public string NAME { get; set; }
        public string STARTDATE { get; set; }
        public string OTS_RETURN_DATE { get; set; }
        public string PATH { get; set; }
    }
}
