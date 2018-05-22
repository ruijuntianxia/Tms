using System;
using System.Collections.Generic;

namespace TMSOrderCar.Models
{
    public partial class Users
    {
        public long UserId { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Email { get; set; }
        public string LoginName { get; set; }
        public string Mobile { get; set; }
        public string Pwd { get; set; }
        public int? UserStatus { get; set; }
    }
}
