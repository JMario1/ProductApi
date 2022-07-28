using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using productMgtApi.Domain.Models;
using productMgtApi.Domain.Repositories;
using productMgtApi.Domain.Security;
using productMgtApi.Domain.Services;
using productMgtApi.Persistence;
using productMgtApi.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite($"Data Source=/home/jude/csharp/prodapi.db");

});
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidAudience = builder.Configuration["TokenOptions:Audience"],
        ValidIssuer = builder.Configuration["TokenOptions:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenOptions:Secret"])),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITokenHandler, productMgtApi.Security.TokenHandler>();
builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();

try
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<IAppUserService>();

    context.Database.EnsureCreated();


    var superAdminUser = new AppUser
    {
        UserName = builder.Configuration["SuperAdmin:Username"],
        Email = builder.Configuration["SuperAdmin:Email"]
    };

    userManager.CreateUserAsync(superAdminUser, builder.Configuration["SuperAdmin:Password"], UserRoles.SuperAdmin).GetAwaiter().GetResult();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
