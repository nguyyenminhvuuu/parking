using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection.Metadata;
using User.Repositories.ConfigFirebase;
using User.Repository;
using User.Service.Implement;
using User.Service.Interface;

namespace User
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => {
                options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme { 
                    Description= "Type \"bearer {token} \"",
                    In=ParameterLocation.Header,
                    Type=SecuritySchemeType.ApiKey,
                    Name="Authorization"
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            builder.Services.AddCors();
            builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();


            builder.Services.AddSingleton<IParkingService, ParkingService>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<IParkingDetailServices, ParkingDetailServices>();
            builder.Services.AddSingleton<IOrderService, OrderService>();
            builder.Services.AddSingleton<ILocationService, LocationService>();

            builder.Services.AddSingleton<Authentications.Authentication>();
            builder.Services.AddSingleton<ConfigFireBaseClient>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSetting:SecretKey").Value)),
                    ClockSkew = TimeSpan.Zero
                };
            
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(p =>
            {
                app.MapControllers();
            });
           
            app.Run();
        }
    }
}