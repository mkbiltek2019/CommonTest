using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace SqlSugarDemo.API
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
            //ע��swagger����,����1�����߶��swagger�ĵ�
            services.AddSwaggerGen(s =>
                {
                    //����swagger�ĵ������Ϣ
                    s.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Bingle API",
                        Description = "һ���򵥵�ASP.NET Core Web API",
                        TermsOfService = new Uri("https://www.cnblogs.com/taotaozhuanyong"),
                        Contact = new OpenApiContact
                        {
                            Name = "bingle",
                            Email = string.Empty,
                            Url = new Uri("https://www.cnblogs.com/taotaozhuanyong"),
                        },
                        License = new OpenApiLicense
                        {
                            Name = "���֤",
                            Url = new Uri("https://www.cnblogs.com/taotaozhuanyong"),
                        }
                    });
                    //��ȡxmlע���ļ���Ŀ¼
                    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                    // ����xmlע��
                    s.IncludeXmlComments(xmlPath);
                });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            //�����м����������Swagger��ΪJSON�ս��
            app.UseSwagger();
            //�����м�������swagger-ui��ָ��Swagger JSON�ս��
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
