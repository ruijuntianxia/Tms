using System;
using System.Collections.Generic;

namespace TmsApi.Models
{
    public partial class User
    {
        public long UserId { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Email { get; set; }
        public string LoginName { get; set; }
        public string Mobile { get; set; }
        public string Pwd { get; set; }
        public int? PowerType { get; set; }
        public int? UserStatus { get; set; }
    }
}
