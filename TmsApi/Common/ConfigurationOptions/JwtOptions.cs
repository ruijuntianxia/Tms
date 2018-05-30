using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TmsApi.Common.ConfigurationOptions
{
    public class JwtOptions
    {
        /// <summary>
        /// jwt的密钥。
        /// </summary>
        public string Secret { get; set; } = "";

        /// <summary>
        /// token的有效期，单位为秒,默认1200s
        /// </summary>
        public int expire { get; set; } = 1200;
    }
}
