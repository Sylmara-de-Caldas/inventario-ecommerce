using InventarioAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace InventarioAPI.Models
{
    public class ProdutoModel
    {

        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public CategoriaEnum Categoria { get; set; }
        public bool Disponivel { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime DataInclusao { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime DataAlteracao { get; set; }

    }
}
