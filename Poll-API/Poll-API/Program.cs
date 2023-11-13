using Poll_API.Data;
using Poll_API.Interfaces;
using Poll_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

DataContext.Init();

IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
IConfigurationRoot config = configurationBuilder.Build();

// Add services to the container.

builder.Services.AddCors(o => o.AddPolicy("CORS", builder => {
    builder.AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials()
           .SetIsOriginAllowed(hostName => true);
}));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
           ValidateIssuer = false,
           ValidateAudience = false
       };
   });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}
app.UseCors("CORS");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
