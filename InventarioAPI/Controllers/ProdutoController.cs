
using InventarioAPI.Models;
using InventarioAPI.Service.ProdutoService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoInterface _produtoInterface;

        public ProdutoController(IProdutoInterface produtoInterface) 
        {
            _produtoInterface = produtoInterface;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponse<List<ProdutoModel>>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<string>), 400)]
        public async Task<ActionResult<ServiceResponse<List<ProdutoModel>>>> GetProducts()
        {
            ServiceResponse<List<ProdutoModel>> serviceResponse = await _produtoInterface.GetProducts();

            if (serviceResponse.Sucesso)
            {
                return Ok(serviceResponse);
            }
            else
            {
                return BadRequest(serviceResponse);

            }
        }

        [HttpGet("{idProduto}")]
        [ProducesResponseType(typeof(ServiceResponse<List<ProdutoModel>>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<string>), 400)]
        public async Task<ActionResult<ServiceResponse<ProdutoModel>>> GetProductById(int idProduto)
        {
            ServiceResponse<ProdutoModel> serviceResponse = await _produtoInterface.GetProductById(idProduto);
            if (serviceResponse.Sucesso)
            {
                return Ok(serviceResponse); 
            }
            else
            {
                return BadRequest(serviceResponse); 

            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<List<ProdutoModel>>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<string>), 400)]
        public async Task<ActionResult<ServiceResponse<List<ProdutoModel>>>> CreateProduct(ProdutoModel novoProduto)
        {
            ServiceResponse<List<ProdutoModel>> serviceResponse = await _produtoInterface.CreateProduct(novoProduto);

            if (serviceResponse.Sucesso)
            {
                return Ok(serviceResponse);
            }
            else
            {
                return BadRequest(serviceResponse);

            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ServiceResponse<List<ProdutoModel>>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<string>), 400)]
        public async Task<ActionResult<ServiceResponse<List<ProdutoModel>>>> UpdateProduct(ProdutoModel novosDadosProduto)
        {
            ServiceResponse<List<ProdutoModel>> serviceResponse = await _produtoInterface.UpdateProduct(novosDadosProduto);

            if (serviceResponse.Sucesso)
            {
                return Ok(serviceResponse);
            }
            else
            {
                return BadRequest(serviceResponse);

            }
        }


        [HttpPut("indisponibilizaProduto/{idProduto}")]
        [ProducesResponseType(typeof(ServiceResponse<List<ProdutoModel>>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<string>), 400)]
        public async Task<ActionResult<ServiceResponse<List<ProdutoModel>>>> DisableProduct(int idProduto)
        {
            ServiceResponse<List<ProdutoModel>> serviceResponse = await _produtoInterface.DisableProduct(idProduto);

            if (serviceResponse.Sucesso)
            {
                return Ok(serviceResponse);
            }
            else
            {
                return BadRequest(serviceResponse);

            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ServiceResponse<List<ProdutoModel>>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<string>), 400)]
        public async Task<ActionResult<ServiceResponse<List<ProdutoModel>>>> DeleteProduct(int idProduto)
        {
            ServiceResponse<List<ProdutoModel>> serviceResponse = await _produtoInterface.DeleteProduct(idProduto);

            if (serviceResponse.Sucesso)
            {
                return Ok(serviceResponse);
            }
            else
            {
                return BadRequest(serviceResponse);

            }
        }


    }
}
