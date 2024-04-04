using InventarioAPI.Models;

namespace InventarioAPI.Service.ProdutoService
{
    public interface IProdutoInterface
    {
        Task<ServiceResponse<List<ProdutoModel>>> GetProducts();
        Task<ServiceResponse<List<ProdutoModel>>> CreateProduct(ProdutoModel novoProduto);
        Task<ServiceResponse<ProdutoModel>> GetProductById(int idProduto);
        Task<ServiceResponse<List<ProdutoModel>>> UpdateProduct(ProdutoModel novosDadosProduto);
        Task<ServiceResponse<List<ProdutoModel>>> DeleteProduct(int idProduto);
        Task<ServiceResponse<List<ProdutoModel>>> DisableProduct(int idProduto);






    }
}
