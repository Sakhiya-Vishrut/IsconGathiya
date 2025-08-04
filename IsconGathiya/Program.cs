using IsconGathiya.Common;
using IsconGathiya.Common.Utility;
using IsconGathiya.Domain.DataContext;
using IsconGathiya.Service;
using IsconGathiya.ViewModel;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetSection("Data")
                                           .GetSection("DefaultConnection")
                                           .GetValue<string>("ConnectionString");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHttpContextAccessor();
ConfigItems.Initialize(builder.Services.BuildServiceProvider().GetService<IHttpContextAccessor>(), configuration);
AWSHelper.Initialize(builder.Services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>());
ServiceRegistry.RegisterServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseExceptionHandler(
    options =>
    {
        options.Run(
            async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var ex = context.Features.Get<IExceptionHandlerFeature>();

                if (ex != null)
                {

                    string emailBody = $@"
                        <h3>Exception Details</h3>
                        <p><strong>Username:</strong> {CV.Username()}</p> 
                        <p><strong>URL:</strong> {context.Request.Host}</p>
                        <p><strong>Exception Path:</strong> {ex.Path}</p>
                        <p><strong>Endpoint:</strong> {ex.Endpoint}</p>
                        <p><strong>Route Values:</strong> {ex.RouteValues}</p>
                        <p><strong>Inner Exception:</strong> {ex.Error.InnerException?.Message}</p>
                        <p><strong>Exception Message:</strong> {ex.Error.Message}</p>
                        <p><strong>Error:</strong> <pre>{ex.Error.ToString()}</pre>
                        <p><strong>Stack Trace:</strong> <pre>{ex.Error.StackTrace}</pre>";

                    if (!app.Environment.IsDevelopment())
                    {
                        EmailHelper.SendMail("Vinayak Corporation MVC Exception Mail " + DateTime.Now, emailBody, "vishrutpatel360@gmail.com, vishrutsakhiya106@gmail.com");
                    }
                }
            }
        );
    }
);


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
