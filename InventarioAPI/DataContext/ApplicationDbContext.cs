using InventarioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioAPI.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ProdutoModel> Produtos { get; set; }
    }
}
