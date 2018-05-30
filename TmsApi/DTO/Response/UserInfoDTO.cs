using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TmsApi.Models;

namespace TmsApi.DTO.Response
{
    public class UserInfoDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public long UserId { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Email { get; set; }
        public string LoginName { get; set; }
        public string Mobile { get; set; }
        public string Pwd { get; set; }
        public int? PowerType { get; set; }
        public int? UserStatus { get; set; }
        public string token { get; set; }
    }

   
}
