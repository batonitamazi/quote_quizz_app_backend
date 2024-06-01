using Microsoft.EntityFrameworkCore;
using quote_quizz_app_backend.Context;
using quote_quizz_app_backend.Repositories.UserRepository;
using quote_quizz_app_backend.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;


using System.Text;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json")
       .Build();

var jwtSettings = configuration.GetSection("Jwt");



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

})
   .AddJwtBearer(options =>
   {
       options.RequireHttpsMetadata = false; 
       options.SaveToken = true;
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["Secret"])),
           ValidateIssuer = false,
           ValidateAudience = false,
           ValidateLifetime = false
       };
   });


builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton(configuration);

builder.Services.AddAuthorization();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
