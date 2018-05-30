using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TmsApi.DTO.Response;
using TmsApi.Models;

namespace TmsApi.Common
{
    public class ResultMessage
    {
        /// <summary>
        /// 返回结果拼接
        /// </summary>
        /// <param name="restring">返回数据Data</param>
        /// <param name="olap">前台传递参数</param>
        /// <param name="mess">返回查询结果消息成功消息,错误消息</param>
        /// <param name="n">返回查询结果状态</param>
        /// <param name="count">返回数据结果数量</param>
        /// <returns></returns>
        public string GetResultMessage(string restring, ReportTvModels olap, string mess, int n, int count)
        {


            olap.pageSize =olap.pageSize!=0 ? olap.pageSize : 0;
            olap.pageIndex = olap.pageIndex!=0 ? olap.pageIndex :0 ;
            restring = "{ \"data\":" + restring + ",";//成功的得到的数据
            restring += "\"message\":\"" + mess + "\",";//成功消息,错误消息
            restring += "\"resultCode\":" + n + ",";//返回成功与否 成功给正整数 1,2,3 这样 错误给负数 -1 -2 -3 
            restring += "\"pageSize\":" + olap.pageSize + ",";//每页条数
            restring += "\"pageIndex\":" + olap.pageIndex + ",";//当前页
            restring += "\"counts\":" + count +"}";//总记录数 没有翻页默认给0
            return restring;
        }

        /// <summary>
        /// 返回登录结果拼接
        /// </summary>
        /// <param name="restring">返回数据Data</param>
        /// <param name="olap">前台传递参数</param>
        /// <param name="mess">返回查询结果消息成功消息,错误消息</param>
        /// <param name="n">返回查询结果状态</param>
        /// <param name="count">返回数据结果数量</param>
        /// <returns></returns>
        public string GetResultMessage(string restring, User olap, string mess, int n, int count)
        {
            restring = "{ \"data\":\"" + restring + "\",";//成功的得到的数据
            restring += "\"message\":\"" + mess + "\",";//成功消息,错误消息
            restring += "\"resultCode\":" + n + "}";//返回成功与否 成功给正整数 1,2,3 这样 错误给负数 -1 -2 -3 
            return restring;
        }

        /// <summary>
        /// 返回登录结果拼接
        /// </summary>
        /// <param name="restring">返回数据Data</param>
        /// <param name="olap">前台传递参数</param>
        /// <param name="mess">返回查询结果消息成功消息,错误消息</param>
        /// <param name="n">返回查询结果状态</param>
        /// <param name="count">返回数据结果数量</param>
        /// <returns></returns>
        public string GetResultMessage(string restring, UserInfoDTO olap, string mess, int n, int count)
        {
            restring = "{ \"data\":\"" + olap.token + "\",";//成功的得到的数据
            restring += "\"message\":\"" + mess + "\",";//成功消息,错误消息
            restring += "\"resultCode\":" + n + "}";//返回成功与否 成功给正整数 1,2,3 这样 错误给负数 -1 -2 -3 
            return restring;
        }
    }
}
