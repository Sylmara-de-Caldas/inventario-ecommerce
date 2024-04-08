using InventarioAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var token = GerarTokenJWT();
            if(login.Login == "admin" && login.Senha == "syl123")
            {
                return Ok(new { token });

            }
            return BadRequest(new {mensagem = "Credenciais inválidas..."});
            
        }

        private string GerarTokenJWT()
        {
            var chaveSecreta = "6152d114-f988-46f4-b2de-b650a4d34fef";

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            var credencial = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256); //algoritimo do tipo de token - header

            var claims = new[]
            {
                new Claim("login", "admin"),
                new Claim("nome", "adminitrador da Aplicação")
            };

            var token = new JwtSecurityToken
                (
                    issuer: "seu_inventario",
                    audience: "sua_aplicacao",
                    claims: claims, //payload vazio
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credencial
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
