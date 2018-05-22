using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ResponseApi
{
    /// <summary>
    /// 接口调用时的响应模型。
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// api接口调用的返回码。默认为1
        /// </summary>
        public int retCode { get; set; } = 200;

        /// <summary>
        /// api接口调用的返回信息。
        /// </summary>
        public string retMsg { get; set; } = "success";

        public ResponseModel() { }

        public ResponseModel(int retCode, string retMsg)
        {
            this.retCode = retCode;
            this.retMsg = retMsg;
        }

        /// <summary>
        /// 通过返回码和返回信息返回一个响应模型。
        /// </summary>
        /// <param name="retCode"></param>
        /// <param name="retMsg"></param>
        /// <returns></returns>
        public ResponseModel GetRes(int retCode, string retMsg)
        {
            ResponseModel res = new ResponseModel();
            res.retCode = retCode;
            res.retMsg = retMsg;
            return res;
        }
    }
}
