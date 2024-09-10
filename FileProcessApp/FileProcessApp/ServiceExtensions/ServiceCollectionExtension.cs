using FileProcessingApp.Common.Dapper;
using FileProcessingApp.Common.Mappings;
using FileProcessingApp.Models.Entities.Data;
using FileProcessingApp.Repositories.Implementation;
using FileProcessingApp.Repositories.Interface;
using FileProcessingApp.Services.Implementation;
using FileProcessingApp.Services.Interface;
using FileProcessingApp.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Data;
using Unity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Unity.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using FileProcessApp.Model;
using FileProcessApp.Common;

namespace FileProcessingApp
{

    public static class ServiceCollectionExtension
    {
        public static WebApplicationBuilder AddProviders(this WebApplicationBuilder builder, ConfigurationManager configuration)
        {


            #region DI

            builder.Services.AddTransient<IUsersService, UsersService>();
            builder.Services.AddTransient<IUsersRepository, UsersRepository>();
            builder.Services.AddTransient<IFilesService, FilesService>();
            builder.Services.AddTransient<IFilesRepository, FilesRepository>();
            builder.Services.AddTransient<IDapperContext, DapperContext>();
            builder.Services.AddSingleton<TokenUtil>();
            builder.Services.AddSingleton<MessageBrokerService>();

            #endregion

            #region db config
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddTransient<IDbConnection>(db => new SqlConnection(
                configuration.GetConnectionString("DefaultConnection")));
            #endregion

            #region Swagger Config

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FileProcessApp.EndPoints", Version = "v1" });
                c.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description =
                            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                    }
                );
                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    }
                );
            });
            #endregion

            #region CORS Policy

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    "BasePolicy",
                    builder =>
                    {
                        builder
                            .AllowAnyMethod()
                            .SetIsOriginAllowed(origin => true) // allow any origin
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .AllowAnyMethod();
                    }

                );
            });
            #endregion

            #region JWT Config

            var _jwtSetting = builder.Configuration.GetSection("JWTSettings");
            builder.Services.Configure<JWTSettings>(_jwtSetting);

            string jwtkey = builder.Configuration.GetValue<string>("JWTSettings:SecretKey");
            builder.Services.AddAuthentication(item =>
            {
                item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(item => {
                item.RequireHttpsMetadata = true;
                item.SaveToken = true;
                item.IncludeErrorDetails = true;
                item.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtkey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            #endregion

            builder.Services.AddDataProtection();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            return builder;
        }
    }
}
