using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TmsApi.Common;
using TmsApi.Common.ConfigurationOptions;
using TmsApi.DTO.Request;
using TmsApi.DTO.Response;
using TmsApi.Models;

namespace TmsApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {

        private ZH_LocationContext _userContext;
        private readonly IMapper _mapper;
        private IOptions<JwtOptions> _jwtOptions;
        private ILogger<LoginController> _logger;
        public LoginController(ZH_LocationContext context,
            IMapper mapper,
            IOptions<JwtOptions> jwtOptions,
            ILogger<LoginController> logger
            )
        {
            _userContext = context;
            _mapper = mapper;
            _jwtOptions = jwtOptions;
            _logger = logger;
        }
        ResultMessage Remess = new ResultMessage();
        HttpResponseMessage result = new HttpResponseMessage();

        /// <summary>
        /// 通过用户名密码登陆。
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("LoginByName")]
        public async Task<HttpResponseMessage> LoginByNameAsync([FromBody]UserDTO req)
        {
            string restring = string.Empty;
            User user = _mapper.Map<User>(req);
            try
            {
                user = await this._userContext.User.FirstOrDefaultAsync(u => u.LoginName == req.LoginName && u.Pwd == req.Pwd);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
               // return res.GetRes(-1, "用户名或密码错误");
                restring = Remess.GetResultMessage(restring, user, "success", -1, 0);
            }

            if (user != null)
            {
                UserInfoDTO userInfoDTO = _mapper.Map<UserInfoDTO>(user);
                var secret = this._jwtOptions.Value.Secret;
                var token = new JwtBuilder()
                  .WithAlgorithm(new HMACSHA256Algorithm())
                  .WithSecret(secret)
                  .AddClaim("exp", DateTimeOffset.UtcNow.AddSeconds(_jwtOptions.Value.expire).ToUnixTimeSeconds())
                  .AddClaim("userId", user.UserId.ToString())
                  .Build();
                userInfoDTO.token = token;
               
                
                restring = Remess.GetResultMessage(restring, userInfoDTO, "success", 1, 0);

                
            }
            else
            {
                restring = Remess.GetResultMessage(restring, user, "success", -1, 0);
            }
            result.Content = new StringContent(restring, Encoding.GetEncoding("UTF-8"), "application/json");
            return result;

        }

    }
}