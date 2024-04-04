using InventarioAPI.DataContext;
using InventarioAPI.Service.ProdutoService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//garante que a cada inje��o de depend�ncia na interface, os metodos a serem utilizados s�o os da Service
builder.Services.AddScoped<IProdutoInterface, ProdutoService>();

//configurando Conex�o com BD
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
