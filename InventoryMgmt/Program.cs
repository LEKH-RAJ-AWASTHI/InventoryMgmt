using Common;
using InventoryMgmt;
using InventoryMgmt.DataAccess;
using InventoryMgmt.DependencyInjections;
using InventoryMgmt.Filters;
using InventoryMgmt.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers
    (
        options => options.Filters.Add(new ItemValidationFilter())
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
    .WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
});
//

// Initialize the InventoryHub function once in the startup
//this is adding serilog in global level
//builder.Services.AddSerilog();

//-----------------------this is Addition of serilog with static file names-----------------
//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Debug()
//    .WriteTo.Console()
//    .WriteTo.File("E:/All of Lekhraj/IMark practice/InventoryMgmt/InventoryMgmt/Log/SampleLog-.txt", rollingInterval: RollingInterval.Day)
//    .CreateLogger();


//--------------------this is addition of serilog reading the configuration from the appsettings.json file---------------

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

//builder.Services.AddCors()

//builder.Services.AddSession(option =>
//{
//    option.IdleTimeout = TimeSpan.FromHours(2);
//    option.Cookie.HttpOnly = true;
//    option.Cookie.IsEssential = true;
//});
builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "JWT Token Authentication API",
        Description = ".NET 8 Web API"
    });
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                            new string[] {}

                    }
                });
});
//builder.Services.AddMvc(options => options.Filters.Add(new ValidationActionFilter()));
builder.Services.RegisterAppServices();
builder.Services.RegisterFluentValidationServices();

builder.Services.AddMemoryCache();

builder.Services.AddSignalR();

#region ValidationRules Adding From Assembly
//builder.Services.AddValidatorsFromAssemblyContaining<ItemValidationRules>();
//builder.Services.AddValidatorsFromAssemblyContaining<UserValidationRules>();
//builder.Services.AddValidatorsFromAssemblyContaining<StoreValidationRules>();
//builder.Services.AddValidatorsFromAssemblyContaining<StockValidationRules>();
//builder.Services.AddIdentity<UserModel, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();
#endregion


builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDb")));

var emailConfiguation = builder.Configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();

builder.Services.AddSingleton(emailConfiguation);

var jwt = builder.Configuration.GetSection("jwt").Get<Jwt>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            ClockSkew = TimeSpan.Zero,

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
        };

    });

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
//    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));

//});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
                      policy.RequireClaim("Role", "Admin"));

    options.AddPolicy("UserOnly", policy =>
              policy.RequireClaim("Role", "User"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandling>();
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<InventoryHub>("/hub");

app.MapControllers();
app.Run();
