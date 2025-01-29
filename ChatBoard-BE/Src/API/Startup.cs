using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using API.Filters;
using Context;
using Logger;
using Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using API.Middleware;
using Service.Implementations;
using Common.Helpers;

namespace API
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
            services.AddAutoMapper(typeof(Startup));
            services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            services.AddControllers().AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            services.AddHttpContextAccessor();
            services.AddEventLogger();
            services.AddService();

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.Http
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement{{
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });
            }).AddSwaggerGenNewtonsoftSupport();

            // Add Filter
            services.AddScoped<CheckJwtTokenFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            if (!Directory.Exists(Path.Combine(env.ContentRootPath, "Images")))
            {
                Directory.CreateDirectory(Path.Combine(env.ContentRootPath, "Images"));
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(env.ContentRootPath, "Images")),
                RequestPath = "/Images"
            });

            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint($"/swagger/v1/swagger.json", "API v1");
                option.RoutePrefix = "";
                option.InjectStylesheet(@"/swagger-ui/SwaggerDark.css");
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            InitializeDatabase(app);
        }
        private void InitializeDatabase(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetRequiredService<DBContext>().Database.EnsureCreated();
        }
    }
}