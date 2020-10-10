using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NeoAnalytica.AppCore.Models;
using NeoAnalytica.Application;
using NeoAnalytica.Infrastructure;
using NeoAnalytica.Infrastructure.Identity;
using NeoAnalytica.Infrastructure.Interfaces;

namespace NeoAnalytica.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string allowReactpApp = "_allowReactApp";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure services

            services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();

            //allow cors policy - temporary 
            services.AddCors(options =>
            {
                options.AddPolicy(name: allowReactpApp,
                                  builder =>
                                  {
                                      builder.WithOrigins("https://neoanalytica-test.azurewebsites.net");
                                  });
            });

            // Add and configure the default identity system that will be used in the application.
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 10;
                options.Password.RequireNonAlphanumeric = true;
                options.SignIn.RequireConfirmedEmail = true;
            });

            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/api/auth/login";
            });

            var connectionDict = new Dictionary<DatabaseConnectionName, string>
            {
                { DatabaseConnectionName.DefaultConnection, this.Configuration.GetConnectionString("DefaultConnection") },
                //{ DatabaseConnectionName.Connection2, this.Configuration.GetConnectionString("dbConnection2") }
            };

            // Inject this dict
            services.AddSingleton<IDictionary<DatabaseConnectionName, string>>(connectionDict);

            // Inject the factory
            services.AddTransient<IDatabaseConnectionFactory, DbConnectionFactory>();

            // Register your regular repositories
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISurveyService, SurveyService>();

            // auth/token configuration
            services.Configure<TokenManagement>(Configuration.GetSection("tokenManagement"));
            var token = Configuration.GetSection("tokenManagement").Get<TokenManagement>();
            var secret = Encoding.ASCII.GetBytes(token.Secret);

            var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailSettings>();
            services.AddSingleton(emailConfig);

            services.AddScoped<IEmailService, EmailService>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                    ValidIssuer = token.Issuer,
                    ValidAudience = token.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });

            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = Configuration["Authentication:Facebook:AppId"];
                options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                options.AccessDeniedPath = "/AccessDeniedPathInfo";
            });

            services.AddControllers();

            // Configure Swagger
            services.AddSwaggerGen(genOptions =>
            {
                genOptions.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "RESTFull data services",
                    Description = "A set of RESTFull services that gives information about surveys",
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                // Enable full name as schema id
                genOptions.CustomSchemaIds(x => x.FullName);
                genOptions.IncludeXmlComments(xmlPath);
            });

            services.AddRouting(options => options.LowercaseUrls = true);
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

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NeoAnalytica API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
