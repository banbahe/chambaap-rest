using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using chambapp.bll.Interviews;
using chambapp.bll.Companies;
using chambapp.bll.Services;
using chambapp.dal.Interviews;
using chambapp.dal.Companies;
using chambapp.bll.AutoMapper;
using chambapp.dto;
using System.Net.Http;

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
            //  clients 
            services.AddHttpClient("GMP", client =>
            {
                client.BaseAddress = new System.Uri("https://maps.googleapis.com/maps/api/");
            });

            services.AddControllers();
            // services.AddScoped<IHttpClientFactory>();
            services.AddScoped<IGoogleMaps, GoogleMaps>();
            services.AddSingleton<MainMapper>();
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
