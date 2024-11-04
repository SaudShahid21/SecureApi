using Microsoft.AspNetCore.Identity;
using SecureApi.dbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.ConfigureApplicationCookie(option =>
{
    option.Cookie.Name = "session";
    option.Cookie.SameSite = SameSiteMode.Lax;
    option.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
builder.Services.AddCors(option =>
{
    option.AddPolicy("CustomCORS", builder =>
    {
        builder.WithOrigins("https://localhost:4200")
        .WithMethods("Get", "POST").WithHeaders("Content-Type");
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CustomeCORS");
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-XSS-Protection", "1;mode=block");
    context.Response.Headers.Add("X-Content-Type-Options-Options", "nosniff");
    context.Response.Headers.Add("Refferrer-Policy", "strict-origin-when-cross-origin");
    await next();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
