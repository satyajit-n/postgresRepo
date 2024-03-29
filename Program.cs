using CRUD_Operations_PostGresSQl.Data;
using CRUD_Operations_PostGresSQl.Mapper;
using CRUD_Operations_PostGresSQl.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<CrudDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("CRUDOperations")));

builder.Services.AddDbContext<AuthDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("AuthConnectionString")));

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddIdentityCore<IdentityUser>()
            .AddRoles<IdentityRole>() // Adding roles
            .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("LOSApi")
            .AddEntityFrameworkStores<AuthDbContext>() // Identity will use this store
            .AddDefaultTokenProviders(); // used generate token for change emails, reset passwords, 

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.SlidingExpiration = false;
        options.ExpireTimeSpan = new TimeSpan(0, 10, 0);
    });

builder.Services.AddScoped<IUserRepository, PGUserRepository>();

builder.Services.AddScoped<ILeadListRepository, PGLeadListRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
