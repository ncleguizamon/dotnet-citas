using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DatingApp.API.Data;// llamar la nreferencia de connención
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using DatingApp.API.Helpers;

namespace DatingApp.API
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
              var Key =Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value);// cave toquen 
             services.AddDbContext<DataContext>(x => x.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
          
            services.AddTransient<seed>();
            services.AddCors();
            services.AddAutoMapper();
            //añadir dependencias e instanacias
            services.AddScoped<IAuthRepository , AuthRepository>();// dependencias de logear
            services.AddScoped<IDatingRepository, DatingRepository>();// dependencias de users
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(Options=>{
                Options.TokenValidationParameters= new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
                    ValidateIssuerSigningKey= true,
                    IssuerSigningKey = new SymmetricSecurityKey(Key),
                    ValidateIssuer= false,
                    ValidateAudience = false
                };
            });
              services.AddMvc().AddJsonOptions(opt => {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            }

            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, seed seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else{
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context=>{
                        context.Response.StatusCode= (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if(error != null){
                            context.Response.AddApplicationError(error.Error.Message);
                 await context.Response.WriteAsync(error.Error.Message);
                        }
                });
            
                });
            }
           // seeder.SeedUser(); //migraciones 
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseAuthentication();//usar autenticacion user
            app.UseMvc();
            
        }
    }
}
