using Microsoft.EntityFrameworkCore;
using Projet.Models.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Projet.Services.Interfaces;
using Projet.Services;

var builder = WebApplication.CreateBuilder(args);


/***** Gestion des tokens JWT *****/
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretKey")),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

/***** Gestions des services *****/
builder.Services.AddScoped<IUtilisateurBusinessService, UtilisateurBusinessService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDossierBusinessService, DossierBusinessService>();
builder.Services.AddScoped<ITypePreuveBusinessService, TypePreuveBusinessService>();

/***** Gestion des controllers *****/
builder.Services.AddControllers();

/****** Gestion de la connexion à la base de données *****/
builder.Services.AddDbContext<MySqlContext>(opt => 
    opt.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

/***** Gestion de la documentation *****/
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
