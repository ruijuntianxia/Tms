using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TmsApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.IO;
using AutoMapper;
using TmsApi.Common.ConfigurationOptions;

namespace TmsApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ConfigureServices
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddWebApiConventions();
            //AddWebApiConventions Controllers 的 HttpResponseMessage 返回值

            //EF添加链接
            services.AddDbContext<ZH_LocationContext>(options => {
                options.UseSqlServer(Configuration["ConnectionString"]);
            });
            services.AddAutoMapper();
            //注入appsettings.jsom参数配置
            services.AddOptions();
            services.Configure<AppSettings>(Configuration.GetSection("AppSetting"));

            services.Configure<JwtOptions>(Configuration.GetSection("Jwt"));
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });
            //配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title="TmsApi",Version="v1" });
                var xmlfile =$"{ Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlpath = Path.Combine(AppContext.BaseDirectory, xmlfile);
                c.IncludeXmlComments(xmlpath);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TmsApi v1");
            });
            
            app.UseMvc();
        }
    }
}
