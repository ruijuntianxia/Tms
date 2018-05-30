using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TmsApi.DTO.Request
{
    public class GetReportTableDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public string name { get; set; }
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
    }
}
