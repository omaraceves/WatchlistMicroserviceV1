using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Watchlist.API.Context;
using AutoMapper;
using Watchlist.API.Helper;
using Watchlist.API.DataServices;
using System.Reflection;
using System.IO;

namespace Watchlist.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Use SQL Server
            var connectionString = Configuration["ConnectionStrings:WatchlistDBConnectionString"];
            services.AddDbContext<WatchlistContext>(o => o.UseSqlServer(connectionString));
            services.AddScoped<IWatchlistsRepository, WatchlistsRepository>();

            //Automapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new WatchlistMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            #region SWAGGER

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                        $"WatchlistOpenAPISpecification",
                        new Microsoft.OpenApi.Models.OpenApiInfo()
                        {
                            Title = "Watchlist API",
                            Version = "v1",
                            Description = @"Watchlist Microservice exposes an API that allow clients to access manage user watchlists. 
                            Find the code for this service at Omar's github.",
                            Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                            {
                                Email = "lcc.omar.aceves@gmail.com",
                                Name = "Omar Aceves",
                                Url = new Uri("https://github.com/omaraceves/WatchlistMicroserviceV1")
                            },
                            License = new Microsoft.OpenApi.Models.OpenApiLicense()
                            {
                                Name = "MIT License",
                                Url = new Uri("https://github.com/omaraceves/WatchlistMicroserviceV1/blob/master/LICENSE")
                            }
                        });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint(
                    "/swagger/WatchlistOpenAPISpecification/swagger.json",
                    "Watchlist API");

                setupAction.RoutePrefix = "";

                setupAction.DefaultModelExpandDepth(2);
                setupAction.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
            });
        }
    }
}
