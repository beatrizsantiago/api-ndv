using System;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Repository;
using Repository.Interfaces;
using Repository.Services;
using WebApplication.Configurations;

namespace WebApplication
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
            services.AddDbContext<ApplicationContext>(options =>
            {
                options
                    .UseLazyLoadingProxies()
                    .UseNpgsql(Configuration.GetConnectionString("Default"));
            });

            services
                .AddIdentity<User, IdentityRole<long>>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfiguration = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection(nameof(TokenConfigurations))
            ).Configure(tokenConfiguration);
            services.AddSingleton(tokenConfiguration);

            services.AddScoped<ILifeService, LifeService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IProgressStepsLifeService, ProgressStepsLifeService>();

            services.AddAutoMapper(GetType().Assembly);

            services.AddAuthentication().AddJwtBearer("Bearer", options =>
            {
                var paramsValidation = options.TokenValidationParameters;
                paramsValidation.ValidateAudience = true;
                paramsValidation.ValidAudience = tokenConfiguration.Audience;
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateIssuer = true;
                paramsValidation.ValidIssuer = tokenConfiguration.Issuer;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "Bearer",
                    new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build()
                );
            });

            services.AddControllers();
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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
