using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using TmsApi.Models;
using TmsApi.Common;
using TmsApi.Bll;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Cors;
using TmsApi.DTO.Request;
using AutoMapper;

namespace TmsApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/ReportTv")]
    public class ReportTvController : Controller
    {
        //定义配置信息对象
        private readonly IOptions<AppSettings> _settings;
        private IMapper _mapper;

        /// <summary>
        /// 重写构造函数，包含注入的配置信息
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="mapper"></param>
        public ReportTvController(IOptions<AppSettings> setting,
            IMapper mapper)
        {
            _settings = setting;
            _mapper = mapper;
        }
        ResultGetTableBll dbGet = new ResultGetTableBll();
        ResultRedisBll dbRedis = new ResultRedisBll();
        ResultMessage Remess = new ResultMessage();
        HttpResponseMessage result = new HttpResponseMessage();
        int count = 0;

        // POST: api/ReportTable
        /// <summary>
        /// 储位资料 Api,返回json存在子项
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("GetReport")]
        [EnableCors("any")]
        public HttpResponseMessage GetReport([FromBody]GetReportDTO req)
        {
            ReportTvModels olap = _mapper.Map<ReportTvModels>(req);
            string restring = string.Empty;
            if (string.IsNullOrWhiteSpace(olap.name))
            {   //当条件为空 返回-2
                restring = Remess.GetResultMessage("[]", olap, "条件Name不能为空！", -2, 0);
                result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");
                return result;
            }
            try
            {
                //restring =Json.DataTableToJsonWithJavaScriptSerializer(dbGet.getresult(olap.name, ""));
                restring = JsonConvert.SerializeObject(dbGet.getresult(olap, _settings), new DataTableConverter());

                restring = restring.Replace("\\\"", "\"").Replace("Value\":\"{", "Value\":[{").Replace("]\"},", "]},{").Replace("},{{", "},{").Replace("]\"}]", "]}]");
            }
            catch (Exception)
            {
                //当查询语句报错 返回-3
                restring = Remess.GetResultMessage("[]", olap, "defeated", -3, 0);
                result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");
                return result;
            }
            //查询语句正确
            if (restring.Length > 2)
            {
                //参数正确有回传结果 1
                restring = Remess.GetResultMessage(restring, olap, "success", 1, 0);
            }
            else
            {
                //参数正确 回传结果为空 -1
                restring = Remess.GetResultMessage(restring, olap, "defeated", -1, 0);
            }


            result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");

            return result;
        }

        // POST: api/ReportTable
        /// <summary>
        /// 出入库预约，返回json为table直接转化
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("GetReportTable")]
        [EnableCors("any")]
        public HttpResponseMessage GetReportTable([FromBody]GetReportTableDTO req)
        {
            ReportTvModels olap = _mapper.Map<ReportTvModels>(req);
            string restring = string.Empty;
            if (string.IsNullOrWhiteSpace(olap.name))
            {   //当条件为空 返回-2
                restring = Remess.GetResultMessage("[]", olap, "条件Name不能为空！", -2, 0);
                result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");
                return result;
            }
            try
            {

                restring = JsonConvert.SerializeObject(dbGet.getresultTable(olap, _settings), new DataTableConverter());

            }
            catch (Exception)
            {
                //当查询语句报错 返回-3
                restring = Remess.GetResultMessage("[]", olap, "defeated", -3, 0);
                result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");
                return result;
            }
            //查询语句正确
            if (restring.Length > 2)
            {
                //参数正确有回传结果 1
                restring = Remess.GetResultMessage(restring, olap, "success", 1, 0);
            }
            else
            {
                //参数正确 回传结果为空 -1
                restring = Remess.GetResultMessage(restring, olap, "defeated", -1, 0);
            }


            result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");

            return result;
        }


        // POST: api/ReportTable
        /// <summary>
        /// 储位明细资料API，返回json为table直接转化
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("GetReportDet")]
        [EnableCors("any")]
        public HttpResponseMessage GetReportDet([FromBody]GetReportDetDTO req)
        {
            ReportTvModels olap = _mapper.Map<ReportTvModels>(req);
            string restring = string.Empty;
            if (string.IsNullOrWhiteSpace(olap.name))
            {   //当条件为空 返回-2
                restring = Remess.GetResultMessage("[]", olap, "条件Name不能为空！", -2, 0);
                result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");
                return result;
            }
            try
            {
                restring = JsonConvert.SerializeObject(dbGet.getresultDet(olap, _settings), new DataTableConverter());

            }
            catch (Exception)
            {
                //当查询语句报错 返回-3
                restring = Remess.GetResultMessage("[]", olap, "defeated", -3, 0);
                result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");
                return result;
            }
            //查询语句正确
            if (restring.Length > 2)
            {
                //参数正确 有回传结果 1
                restring = Remess.GetResultMessage(restring, olap, "success", 1, 0);
            }
            else
            {
                //参数正确 回传结果为空 -1
                restring = Remess.GetResultMessage(restring, olap, "defeated", -1, 0);
            }


            result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");

            return result;
        }



        /// <summary>
        /// 运单+车牌+看货狗 redis查询
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("TptShip")]
        [EnableCors("any")]
        public HttpResponseMessage GetReportTvMapRedis([FromBody]GetReportTableDTO req)
        {
            ReportTvModels olap = _mapper.Map<ReportTvModels>(req);
            string restring = string.Empty;
            if (string.IsNullOrWhiteSpace(olap.name))
            {   //当条件为空 返回-2
                restring = Remess.GetResultMessage("[]", olap, "条件Name不能为空！", -2, 0);
                result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");
                return result;
            }
            try
            {

                restring = JsonConvert.SerializeObject(dbRedis.GetTvMapRedis(olap,_settings), new DataTableConverter());
                count = Convert.ToInt32(dbRedis.GetTptShipmentCountRedis(olap, _settings));
                restring = restring.Replace('\'', '\"').Replace("\"{\"", "{\"").Replace("\"}\"", "}").Replace("\"[[", "[[").Replace("\"None\"", "\"\"").Replace("None", "\"\"").Replace("}\",{", "},{");
             

            }
            catch (Exception)
            {
                //当查询语句报错 返回-3
                restring = Remess.GetResultMessage("[]", olap, "defeated", -3, count);
                result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");
                return result;
            }
            //查询语句正确
            if (restring.Length > 2)
            {
                //参数正确有回传结果 1
                restring = Remess.GetResultMessage(restring, olap, "success", 1, count);
            }
            else
            {
                //参数正确 回传结果为空 -1
                restring = Remess.GetResultMessage(restring, olap, "defeated", -1, 0);
            }


            result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");

            return result;

        }

        /// <summary>
        /// 今日运输订单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("TodOrder")]
        [EnableCors("any")]
        public HttpResponseMessage GetReportTodOrder([FromBody]GetReportDTO req)
        {
            ReportTvModels olap = _mapper.Map<ReportTvModels>(req);
            string restring = string.Empty;
            if (string.IsNullOrWhiteSpace(olap.name))
            {   //当条件为空 返回-2
                restring = Remess.GetResultMessage("[]", olap, "条件Name不能为空！", -2, 0);
                result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");
                return result;
            }
            try
            {

                restring = JsonConvert.SerializeObject(dbGet.GetTodOrder(olap, _settings), new DataTableConverter());
                restring = restring.Replace("\"[{\\", "[{").Replace("\\\"", "\"").Replace("}]\"", "}]");

            }
            catch (Exception ex)
            {
                //当查询语句报错 返回-3
                restring = Remess.GetResultMessage("[]", olap, "defeated", -3, 0);
                result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");
                return result;
            }
            //查询语句正确
            if (restring.Length > 2)
            {
                //参数正确有回传结果 1
                restring = Remess.GetResultMessage(restring, olap, "success", 1, 0);
            }
            else
            {
                //参数正确 回传结果为空 -1
                restring = Remess.GetResultMessage(restring, olap, "defeated", -1, 0);
            }


            result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");

            return result;

        }

        /// <summary>
        /// 在途运输订单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("TransPorta")]
        [EnableCors("any")]
        public HttpResponseMessage GetReportTransPortaTion([FromBody]GetReportDTO req)
        {
            ReportTvModels olap = _mapper.Map<ReportTvModels>(req);
            string restring = string.Empty;
            if (string.IsNullOrWhiteSpace(olap.name))
            {   //当条件为空 返回-2
                restring = Remess.GetResultMessage("[]", olap, "条件Name不能为空！", -2, 0);
                result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");
                return result;
            }
            try
            {

                restring = JsonConvert.SerializeObject(dbGet.GetTransPortaTion(olap, _settings), new DataTableConverter());
                restring = restring.Replace("\"[{\\", "[{").Replace("\\\"", "\"").Replace("}]\"", "}]");

            }
            catch (Exception ex)
            {
                //当查询语句报错 返回-3
                restring = Remess.GetResultMessage("[]", olap, "defeated", -3, 0);
                result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");
                return result;
            }
            //查询语句正确
            if (restring.Length > 2)
            {
                //参数正确有回传结果 1
                restring = Remess.GetResultMessage(restring, olap, "success", 1, 0);
            }
            else
            {
                //参数正确 回传结果为空 -1
                restring = Remess.GetResultMessage(restring, olap, "defeated", -1, 0);
            }


            result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");

            return result;

        }

        /// <summary>
        /// 地图路线坐标 运单+车牌+看货狗 oracle查询
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("TptMap")]
        [EnableCors("any")]
        public HttpResponseMessage GetReportTptShipment([FromBody]GetReportTableDTO req)
        {
            ReportTvModels olap = _mapper.Map<ReportTvModels>(req);
            string restring = string.Empty;
            
            if (string.IsNullOrWhiteSpace(olap.name))
            {   //当条件为空 返回-2
                restring = Remess.GetResultMessage("[]", olap, "条件Name不能为空！", -2, 0);
                result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");
                return result;
            }
            try
            {

                restring = JsonConvert.SerializeObject(dbGet.GetTptShipment(olap, _settings), new DataTableConverter());
                count =Convert.ToInt32( dbGet.GetTptShipmentCount(olap, _settings).Rows[0][0].ToString()); 
                restring = restring.Replace("\"[{\\", "[{").Replace("\\\"", "\"").Replace("}]\"", "}]").Replace(":\"[[", ":[[").Replace("]]\"}", "]]}");
             
            }
            catch (Exception ex)
            {
                //当查询语句报错 返回-3
                restring = Remess.GetResultMessage("[]", olap, "defeated", -3, count);
                result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");
                return result;
            }
            //查询语句正确
            if (restring.Length > 2)
            {
                //参数正确有回传结果 1
                restring = Remess.GetResultMessage(restring, olap, "success", 1, count);
            }
            else
            {
                //参数正确 回传结果为空 -1
                restring = Remess.GetResultMessage(restring, olap, "defeated", -1, count);
            }


            result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");

            return result;

        }

        /// <summary>
        /// 储位空置率
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("vacancy")]
        [EnableCors("any")]
        public HttpResponseMessage GetReportVacancy([FromBody]VacancyDTO req)
        {
            ReportTvModels olap = _mapper.Map<ReportTvModels>(req);

            string restring = string.Empty;
            int count = 0;
            if (string.IsNullOrWhiteSpace(olap.name))
            {   //当条件为空 返回-2
                restring = Remess.GetResultMessage("[]", olap, "条件Name不能为空！", -2, 0);
                result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");
                return result;
            }
            try
            {

                restring = JsonConvert.SerializeObject(dbGet.GetVacancy(olap, _settings), new DataTableConverter());
                //count = Convert.ToInt32(dbGet.GetTptShipmentCount(olap, _settings).Rows[0][0].ToString());
               // restring = restring.Replace("\"[{\\", "[{").Replace("\\\"", "\"").Replace("}]\"", "}]").Replace(":\"[[", ":[[").Replace("]]\"}", "]]}");

            }
            catch (Exception ex)
            {
                //当查询语句报错 返回-3
                restring = Remess.GetResultMessage("[]", olap, "defeated", -3, count);
                result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");
                return result;
            }
            //查询语句正确
            if (restring.Length > 2)
            {
                //参数正确有回传结果 1
                restring = Remess.GetResultMessage(restring, olap, "success", 1, count);
            }
            else
            {
                //参数正确 回传结果为空 -1
                restring = Remess.GetResultMessage(restring, olap, "defeated", -1, count);
            }


            result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");

            return result;

        }


    }
}