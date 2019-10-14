// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace NotesMicroservice
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessManager.Interface;
    using BusinessManager.Service;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using RepositoryManager.DBContext;
    using RepositoryManager.Interface;
    using RepositoryManager.Service;
    using Serilog;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using Operation = Swashbuckle.AspNetCore.Swagger.Operation;

    /// <summary>
    /// startup class of our application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// instance initialize.
        /// </summary>
        /// <param name="configuration">initialize configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
            // Init Serilog configuration
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;
        }

        /// <summary>
        /// the Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">service.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<INotesRepositoryManager, NotesRepositoryManager>();
            services.AddTransient<IBusinessManager, BusinessManagerService>();

            services.AddTransient<ILabelRepositoryManager, LabelRepositoryService>();
            services.AddTransient<ILabelBusinessManager, LabelBusinessService>();
            
            ////Get Connection to database
            services.AddDbContext<AuthenticationContext>(options =>
               options.UseSqlServer(this.Configuration.GetConnectionString("IdentityConnection")));

            //Allow origin backend to run on different port
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.WithOrigins("http://localhost:4200"));
            });

            services.AddCors(cors => cors.AddPolicy("AllowOrigin", builder =>
            {
                builder.AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowAnyOrigin();
            }));

            ////Add swagger 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyFandooApp", Version = "v1" , Description = "Fandoo App" });
                c.OperationFilter<FileUploadedOperation>();
            });
            
            ////Add Authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy =>
                    policy.RequireClaim("Id"));
            });

            ////Jwt Authentication
            var key = Encoding.UTF8.GetBytes(this.Configuration["ApplicationSettings:JWT_Secret"].ToString());
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
        
        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">app parameter.</param>
        /// <param name="env">env parameter.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                ////The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            ////Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "MyFandooApp");
            });
            // logging
            loggerFactory.AddSerilog();
            app.UseCors("AllowOrigin");
            app.UseCors(options =>
            options.WithOrigins("http://localhost:4200")
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowAnyOrigin()
           .AllowCredentials());
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
     
    /// <summary>
    /// authorization header for swagger 
    /// </summary>
    public class FileUploadedOperation : IOperationFilter
    {
        /// <summary>
        /// Apply function
        /// </summary>
        /// <param name="swaggerDocument">swaggerDocument parameter</param>
        /// <param name="documentFilter">documentFilter parameter </param>
        public void Apply(Operation swaggerDocument, OperationFilterContext documentFilter)
        {
            if (swaggerDocument.Parameters == null)
            {
                swaggerDocument.Parameters = new List<IParameter>();
            }

            swaggerDocument.Parameters.Add(new NonBodyParameter
            {
                Name = "Authorization",
                In = "header",
                Type = "string",
                Required = true
            });
        }
    }
}
