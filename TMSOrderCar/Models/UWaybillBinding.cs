using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMSOrderCar.Models
{
    public partial class UWaybillBinding
    {

        public string DocEntry { get; set; }

        [Required(ErrorMessage = "请输入订单编号")]
        [Display(Name ="订单编号")]
        public string OrderNo { get; set; }

        [Display(Name = "车牌号")]
        [Required(ErrorMessage = "请输入车牌号")]
        public string Carnumber { get; set; }

        [Display(Name = "预计装车时间")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "请输入预计装车时间")]
        public DateTime ExpectDateLoad { get; set; }

        [Display(Name = "实际装车时间")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "请输入实际装车时间")]
        public DateTime ActualDateLoad { get; set; }

        [Display(Name = "提交人")]
        public string CreateUser { get; set; }

        [Display(Name = "提交时间")]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "修改人")]
        public string UpdateUser { get; set; }

        [Display(Name = "修改时间")]
        [DataType(DataType.DateTime)]
        public DateTime UpdateDate { get; set; }
    }
}
