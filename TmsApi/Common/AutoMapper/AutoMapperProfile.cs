using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TmsApi.DTO.Request;
using TmsApi.DTO.Response;
using TmsApi.Models;

namespace TmsApi.Common.AutoMapper
{
    /// <summary>
    /// AutoMapperProfile 公共类
    /// </summary>

    public class AutoMapperProfile:Profile
    {
        /// <summary>
        /// automapper 声明
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<VacancyDTO, ReportTvModels>();
            CreateMap<GetReportDTO, ReportTvModels>();
            CreateMap<GetReportTableDTO, ReportTvModels>();
            CreateMap<GetReportDetDTO, ReportTvModels>();
            CreateMap<UserDTO, User>();
            CreateMap<User, UserInfoDTO>();
        }
        
    }
}
