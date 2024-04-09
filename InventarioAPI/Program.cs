using InventarioAPI.DataContext;
using InventarioAPI.Service.ProdutoService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

string chaveSecreta = "6152d114-f988-46f4-b2de-b650a4d34fef";

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventário - API", Version = "v1" });
    var securitySchema = new OpenApiSecurityScheme
    {
        Name = "JWT Autenticação",
        Description = "Entre com JWT Bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme,
        }
    };
    config.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchema);
    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            securitySchema, new string[] {}
        }
    });
});

//garante que a cada injeção de dependência na interface, os metodos a serem utilizados são os da Service
builder.Services.AddScoped<IProdutoInterface, ProdutoService>();

//configurando Conexão com BD
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true, 
        ValidateAudience = true,
        ValidateLifetime = true, //valida expiração
        ValidateIssuerSigningKey = true,
        ValidIssuer = "seu_inventario",
        ValidAudience = "sua_aplicacao",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta))
        //caso todos esses item estejam validados, é feito a autorização para acessar a aplicação
    };
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
    builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:4200")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
