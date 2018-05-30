using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TmsApi.Models;
using ServiceStack;
using ServiceStack.Redis;
using TmsApi.DTO.Response;
using ServiceStack.Text;

namespace TmsApi.Dall
{
    public class ResultRedisDAll
    {
       /// <summary>
       /// 地图坐标和看货狗编号
       /// </summary>
       /// <param name="olap"></param>
       /// <param name="settings"></param>
       /// <returns></returns>
        internal object GetTvMapRedis(ReportTvModels olap, Microsoft.Extensions.Options.IOptions<AppSettings> settings)
        {
            string connectstring = settings.Value.redisMap.Host + ":" + settings.Value.redisMap.Post;
            var redisManger = new RedisManagerPool(connectstring);      //Redis的连接字符串  
            var redis = redisManger.GetClient();                           //获取一个Redis Client  

            //redisTodos.Store(newTodo);                                    //把newTodo实例保存到数据库中    增       
            //ResponseMapDTO saveTodo = redisTodos.GetById(newTodo.ROWNO);               //根据Id查询        查  
            //"Saved Todo: {0}".Print(saveTodo.Dump());

            //saveTodo.Done = true;                                         //改  
            //redisTodos.Store(saveTodo);

            //var updateTodo = redisTodos.GetById(newTodo.ROWNO);            //查  
            // "Updated Todo: {0}".Print(updateTodo.Dump());

            /* redisTodos.DeleteById(newTodo.ROWNO);                      */     //删除  

            //var remainingTodos = redis.GetRangeFromSortedSet(settings.Value.redisMap.key, (olap.pageIndex - 1) * olap.pageSize, (olap.pageIndex * olap.pageSize)-1);
            // "No more Todos:".Print(remainingTodos.Dump());
            var remainingTodos = redis.GetRangeFromSortedList(settings.Value.redisMap.key, (olap.pageIndex - 1) * olap.pageSize, (olap.pageIndex * olap.pageSize));
            return remainingTodos;
            
        }

        internal object GetTptShipmentCountRedis(ReportTvModels olap, Microsoft.Extensions.Options.IOptions<AppSettings> settings)
        {
            string connectstring = settings.Value.redisMap.Host + ":" + settings.Value.redisMap.Post;
            var redisManger = new RedisManagerPool(connectstring);      //Redis的连接字符串  
            var redis = redisManger.GetClient();                           //获取一个Redis Client  
            var remainingTodos = redis.GetValue("shipmentcount");
            return remainingTodos;
        }
    }
}
