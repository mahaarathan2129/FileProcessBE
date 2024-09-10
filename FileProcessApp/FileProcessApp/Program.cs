using AutoMapper;
using FileProcessApp.Common;
using FileProcessApp.Services.Interface;
using FileProcessingApp;
using FileProcessingApp.Common.Mappings;
using FileProcessingApp.Models.Entities.Data;
using FileProcessingApp.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.AddProviders(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITokenUtil, TokenUtil>();

var app = builder.Build();

var httpContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();
HttpContextHelper.SetHttpContextAccessor(httpContextAccessor);
var mapper = app.Services.GetRequiredService<IMapper>();
MapperProvider.Initialize(mapper);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler("/error");

app.UseAuthentication();
app.UseCors("BasePolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
