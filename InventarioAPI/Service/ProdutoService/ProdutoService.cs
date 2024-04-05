using InventarioAPI.DataContext;
using InventarioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioAPI.Service.ProdutoService
{
    public class ProdutoService : IProdutoInterface
    {
        private readonly ApplicationDbContext _context;
        public ProdutoService(ApplicationDbContext context) 
        { 
            _context = context;
        }
        public async Task<ServiceResponse<List<ProdutoModel>>> CreateProduct(ProdutoModel novoProduto)
        {
            ServiceResponse<List<ProdutoModel>> serviceResponse = new ServiceResponse<List<ProdutoModel>>();

            try 
            {
                if(novoProduto == null) 
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Faltam dados!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }
                _context.Add(novoProduto);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Produtos.ToList(); 
            
            }catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ProdutoModel>>> DeleteProduct(int idProduto)
        {
            ServiceResponse<List<ProdutoModel>> serviceResponse = new ServiceResponse<List<ProdutoModel>>();

            try
            {
                ProdutoModel? produto = _context.Produtos.FirstOrDefault(x => x.Id == idProduto);
                if (produto == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Item não encontrado";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Produtos.ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
           
        }

        public async Task<ServiceResponse<ProdutoModel>> GetProductById(int idProduto)
        {
            ServiceResponse<ProdutoModel> serviceResponse = new ServiceResponse<ProdutoModel>();

            try 
            {
                ProdutoModel? produto = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == idProduto);
                
                if (produto == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Produto não localizado";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                serviceResponse.Dados = produto;

            }catch (Exception ex) 
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ProdutoModel>>> GetProducts()
        {
            ServiceResponse<List<ProdutoModel>> serviceResponse = new ServiceResponse<List<ProdutoModel>>();
            
            try
            {
                serviceResponse.Dados = await _context.Produtos.ToListAsync();

                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenhum dado localizado";
                }


            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse; 
        }

        public async Task<ServiceResponse<List<ProdutoModel>>> DisableProduct(int idProduto)
        {
            ServiceResponse<List<ProdutoModel>> serviceResponse = new ServiceResponse<List<ProdutoModel>>();
            try
            {
                ProdutoModel? produto = _context.Produtos.FirstOrDefault(x => x.Id == idProduto);
                
                if (produto == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Item não localizado";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                produto.Disponivel = false;
                produto.Status = Enums.StatusEnum.Esgotado;
                produto.Quantidade = 0;
                produto.DataAlteracao = DateTime.Now.ToLocalTime();

                _context.Produtos.Update(produto);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Produtos.ToList();
            }
            catch(Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ProdutoModel>>> UpdateProduct(ProdutoModel novosDadosProduto)
        {
            ServiceResponse<List<ProdutoModel>> serviceResponse = new ServiceResponse<List<ProdutoModel>>();
            try
            {
                ProdutoModel? produto = _context.Produtos.AsNoTracking().FirstOrDefault(x => x.Id == novosDadosProduto.Id);
                if (produto == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Item não localizado";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                produto.DataAlteracao = DateTime.Now.ToLocalTime();
                _context.Produtos.Update(novosDadosProduto);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Produtos.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }
    }
}
