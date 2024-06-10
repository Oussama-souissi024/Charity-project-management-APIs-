using CityProjects.Core;
using CityProjects.Core.Donation_Mapper;
using CityProjects.Core.Mapper.Mondate_Mapper;
using CityProjects.Core.Material_Mapper;
using CityProjects.Core.Project_Mapper;
using CityProjects.Core.Transportation_Mapper;
using CityProjects.Core.User_Mapper;
using CityProjects.Data;
using CityProjects.Data.SqlServerEF;
using CityProjectss.Data.SqlServerEF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});



builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
    builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .WithMethods("PUT", "DELETE", "GET", "OPTIONS", "POST");
    });
});

builder.Services.AddAutoMapper(typeof(UserMappingProfile));
builder.Services.AddAutoMapper(typeof(ProjectMappingProfile));
builder.Services.AddAutoMapper(typeof(DonnationMappingProfile));
builder.Services.AddAutoMapper(typeof(MaterialMappingProfile));
builder.Services.AddAutoMapper(typeof(TransportationMappingProfile));
builder.Services.AddAutoMapper(typeof(MondateMappingProfile));


builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DataContext>();

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    /*options.JsonSerializerOptions.MaxDepth = 64;*/ // Increase max depth if necessary
});

//Inject Table
builder.Services.AddScoped<IDataHelper<Users>, UserEntity>();
builder.Services.AddScoped<IDataHelper<CityUserRole>, CityUserRoleEntity>();
builder.Services.AddScoped<IDataHelper<Donations>, DonationEntity>();
builder.Services.AddScoped<IDataHelper<Materials>, MaterialEntity>();
builder.Services.AddScoped<IDataHelper<Projects>, ProjectEntity>();
builder.Services.AddScoped<IDataHelper<Transportations>, TransportationEntity>();
builder.Services.AddScoped<IDataHelper<Mandates>, MandateEntity>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<IdentityUser>();

app.UseCors(option =>
option.WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "President", "Secretary", "ProjectManager", "Member" };

    foreach (var role in roles)
    {
        if (!await roleManger.RoleExistsAsync(role))
            await roleManger.CreateAsync(new IdentityRole(role));
    }
}

app.Run();
