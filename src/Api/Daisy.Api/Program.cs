using Daisy.Api.Extensions;
using Daisy.Domain.Models;
using Daisy.Infrastructure.Context;
using Daisy.Infrastructure.Extensions;
using Daisy.Shared.Configs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

string CORSOpenPolicy = "OpenCORSPolicy";
var builder = WebApplication.CreateBuilder(args);
var jwtSettings = builder.Configuration.GetSection("Auth").GetSection("Jwt");
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
var connectionString = builder.Configuration.GetConnectionString("Daisy");
AuthExtensions.SecurityKey(out string key);

IdentityModelEventSource.ShowPII = true;

builder.Services.Configure<Daisy.Shared.Configs.JwtSettings>(builder.Configuration.GetSection("Auth").GetSection("Jwt"));
builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Daisy Api", 
        Version = "v1",
        Description = "Basic CRUD",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Allan Odhiambo",
            Email = "aamodhiambo@gmail.com",
        },
        License = new OpenApiLicense
        {
            Name = "",
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        Description = "Please insert JWT token into field. Example: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
      name: CORSOpenPolicy,
      builder => {
          builder.WithOrigins("https://localhost:7144", "http://localhost:5294",
              "").AllowAnyHeader().AllowAnyMethod();
      });
});

builder.Services.AddDbContextPool<DBContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddSingleton<DapperContext>();

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<DBContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.ClaimsIdentity.UserNameClaimType = "Username";
    //options.Stores.ProtectPersonalData = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

});

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
   opt.TokenLifespan = TimeSpan.FromHours(2));

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.Cookie.Name = "AppCookie.Tuomoke";
//    options.Cookie.HttpOnly = true;
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
//    options.LoginPath = "/Auth/AccessDenied/";
//    options.AccessDeniedPath = "/Auth/AccessDenied/";

//    //ReturnUrlParameter requires using Microsoft.AspNetCore.Authentication.Cookies;
//    //options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
//    options.SlidingExpiration = true;

//    options.Events.OnRedirectToLogin = context =>
//    {
//        context.Response.Headers["Location"] = context.RedirectUri;
//        context.Response.StatusCode = 401;
//        return Task.CompletedTask;
//    };
//    options.Events.OnRedirectToAccessDenied = context =>
//    {
//        context.Response.Headers["Location"] = context.RedirectUri;
//        context.Response.StatusCode = 403;
//        return Task.CompletedTask;
//    };
//});

builder.Services.AddAuthentication((options) =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    //options.Authority = builder.Configuration["Auth:Jwt:JwtIssuer"];
    //options.Audience = builder.Configuration["Auth:Jwt:JwtAudience"];
    options.SaveToken = true;
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(builder.Configuration["Auth:Jwt:ClockSkew"])),
        ValidIssuer = builder.Configuration["Auth:Jwt:JwtIssuer"],
        ValidAudience = builder.Configuration["Auth:Jwt:JwtAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Auth:Jwt:JwtSecutityKey"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

    };
});//.AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();

});

builder.Services.AddSingleton(emailConfig);

builder.Services.AddHttpContextAccessor();

builder.Services.AddInfrastructure();

builder.Services.AddAutoMapper(typeof(Program));




var app = builder.Build();

app.ConfigureGlobalExceptionHandler();

if (app.Environment.IsProduction())
{
    //app.UseStatusCodePagesWithRedirects("/500/{0}");
    app.UseExceptionHandler(builder.Configuration["Errors:Prod"]);
}


app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Daisy Api V1");
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors(CORSOpenPolicy);

app.UseAuthentication();

app.UseAuthorization(); 

app.MapControllers().RequireAuthorization();

app.Run();
