using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OnlineShopping.Bll.AdminBll;
using OnlineShopping.Bll.CartBll;
using OnlineShopping.Bll.ItemBll;
using OnlineShopping.Data;
using OnlineShopping.Data.Repositories;
using OnlineShopping.Data.UnitOfWork;
using OnlineShopping.Entities;
using OnlineShopping.Services.AdminService;
using OnlineShopping.Services.CartService;
using OnlineShopping.Services.ItemService;
using OnlineShopping.Shared.JWT;

namespace OnlineShopping.API
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
            //CORS config
            services.AddCors(o => o.AddPolicy("Policy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            //init swagger
            services.AddSwaggerGen();

            services.AddControllers();

            //Database Context
            services.AddDbContext<ProjectContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));





            //Add Identity
            services.AddIdentity<Admin, IdentityRole>()
                .AddEntityFrameworkStores<ProjectContext>()
                .AddDefaultTokenProviders();

            //Unit Of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Entity
            services.AddScoped(typeof(IRepository<Admin>), typeof(GenericRepository<Admin>));

            //admin
            services.AddScoped<IRegisterBll, RegisterBll>();
            services.AddScoped<ILoginBll, LoginBll>();
            services.AddScoped<IValidationBll, ValidationBll>();
            services.AddScoped<IAdminService, AdminService>();

            //items
            services.AddScoped<IGetItemsBll, GetItemsBll>();
            services.AddScoped<IItemService, ItemService>();

            //cart
            services.AddScoped<IAddBll, AddBll>();
            services.AddScoped<IValidatioBll, ValidatioBll>();
            services.AddScoped<IGetOrderListBll, GetOrderListBll>();
            services.AddScoped<IDeleteBll, DeleteBll>();
            services.AddScoped<IGetBll, GetBll>();
            services.AddScoped<ICheckoutBll, CheckoutBll>();
            services.AddScoped<ICartService, CartService>();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IRepository<Admin>>();
                        var jwtToken = (JwtSecurityToken)context.SecurityToken;
                        var userId = jwtToken.Claims.First(x => x.Type == "id").Value;
                        var user = userService.Get(userId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("Policy");

            app.UseAuthentication();

            CreateRoleIfNotExist(serviceProvider).Wait();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

            });
        }
        private async Task CreateRoleIfNotExist(IServiceProvider serviceProvider)
        {
            var roleManger = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var AdminRole = await roleManger.RoleExistsAsync("Admin");
            if (!AdminRole)
                await roleManger.CreateAsync(new IdentityRole("Admin"));
            var CustomerRole = await roleManger.RoleExistsAsync("Customer");
            if (!CustomerRole)
                await roleManger.CreateAsync(new IdentityRole("Customer"));
        }
    }
}
