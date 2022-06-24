using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Rbac.Application;
using Rbac.Entity;
using Rbac.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
namespace Rbac.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //automapper注册
            services.AddAutoMapper(Assembly.Load("Rbac.Application"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rbac.WebApi", Version = "v1" });
            });

            //上下文
            services.AddDbContext<MyDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("sqlserver")));

            //跨域
            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(opt =>
                {
                    opt.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
                });
            });

            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuService, MenuService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rbac.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //使用跨域写在这里最佳
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
