using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using chambapp.bll.Interviews;
using chambapp.bll.Companies;
using chambapp.bll.Services;
using chambapp.bll.Services.Email;
using chambapp.dal.Interviews;
using chambapp.dal.Companies;
using chambapp.bll.AutoMapper;
using chambapp.dto;
using System.Net.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;

namespace chambapp.services
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

            services.AddControllers();

            //  clients 
            services.AddHttpClient("GMP", client =>
            {
                client.BaseAddress = new System.Uri("https://maps.googleapis.com/maps/api/");
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "Chambapp API",
                    Description = ".NET Core Web API, I'm going to chase the chop",
                    TermsOfService = new Uri("https://github.com/stbndev/chambaap-rest/blob/main/LICENSE"),
                    Contact = new OpenApiContact
                    {
                        Name = "Eban",
                        Email = "eban.blanquel@gmail.com",
                        Url = new Uri("https://github.com/stbndev"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "WTFPL",
                        Url = new Uri("https://github.com/stbndev/chambaap-rest/blob/main/WTFPL.txt"),
                    }
                });
                var filePath = System.IO.Path.Combine(AppContext.BaseDirectory, "QDM.CDM.API.xml");
                c.IncludeXmlComments(filePath);
            });

            services.AddSingleton<MainMapper>();
            services.AddSingleton<IGoogleMaps, GoogleMaps>();
            
            services.AddScoped<IEmailService,EmailService>();
            services.AddScoped<ResponseModel>();

            // Business Logic Layer
            services.AddScoped<IInterviewBll, InterviewBll>();
            services.AddScoped<ICompanyBll, CompanyBll>();

            // Data Access Layer
            services.AddScoped<IInterviewDal, InterviewDal>();
            services.AddScoped<ICompanyDal, CompanyDal>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui(HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Chambaap API v1.0");
                options.RoutePrefix = string.Empty;
                options.DocumentTitle = "Chambaap Documentation";
                options.DocExpansion(DocExpansion.List);
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
