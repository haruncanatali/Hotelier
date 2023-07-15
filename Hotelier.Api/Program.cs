using FluentValidation.AspNetCore;
using Hotelier.Api.Configs;
using Hotelier.Api.Services;
using Hotelier.Application;
using Hotelier.Application.Common.Interfaces;
using Hotelier.Persistence;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddAuthenticationConfig(builder.Configuration);

builder.Services.AddSettingsConfig(builder.Configuration);

builder.Services.AddApplication();

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IApplicationContext>());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfig();

var app = builder.Build();

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
    var response = new { error = exception.Message };
    await context.Response.WriteAsJsonAsync(response);
}));

app.UseSwagger();
app.UseSwaggerUI();
app.MigrateDatabase();

app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();