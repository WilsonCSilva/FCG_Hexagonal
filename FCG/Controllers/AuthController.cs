using Infra.Helpers;
using Core.Interfaces;
using FCG.Middlewares;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FCG.Controllers
{
    [ApiController] 
    [Route("[controller]")]
    public class AuthController(IConfiguration configuration, IUsuarioRepository usuarioRepository, CriptografiaHelper criptografiaHelper, LoginHelper loginHelper, BaseLogger<AuthController> logger) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly CriptografiaHelper _criptografiaHelper = criptografiaHelper;
        private readonly LoginHelper _loginHelper = loginHelper;
        private readonly BaseLogger<AuthController> _logger = logger;

        [HttpPost("login")]
        public IActionResult Logar(string usuario, string email, string senha)
        {
            var _usuario = _usuarioRepository.ObterPorNome(usuario);

            if (_usuario != null)
            {
                bool emailCorreto = _loginHelper.VerificarCadastroEmail(_usuario.Email, email);
                bool senhaCorreta = _loginHelper.VerificarCadastroSenha(_criptografiaHelper.Descriptografar(_usuario.Senha), senha);

                if (!emailCorreto)
                {
                    string erroResponse = "Email incorreto.";
                    _logger.LogError(erroResponse);
                    return BadRequest(erroResponse);
                }
                else if (!senhaCorreta)
                {
                    string erroResponse = "Senha incorreta.";
                    _logger.LogError(erroResponse);
                    return BadRequest(erroResponse);
                }

                var (token, dataCriacao, dataExpiracao) = GerarToken(_usuario.Nome, _usuario.Email, _usuario.TipoUsuario);

                _logger.LogInformation("Token gerado com sucesso.");

                return Ok(new
                {
                    token,
                    type = "Bearer",
                    iat = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expires = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss")
                });
            }
            else
            {
                var erroResponse = new ErroResponse
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Erro = "Unauthorized",
                    Detalhe = "Usuário ou senha inválidos."
                };

                _logger.LogError("Erro ao gerar token.");

                return Unauthorized(erroResponse);
            }
        }

        private (string token, DateTime dataCriacao, DateTime dataExpiracao) GerarToken(string userName, string email, short role)
        {
            DateTime dataCriacao = DateTime.UtcNow;
            DateTime dataExpiracao = dataCriacao.AddMinutes(double.Parse(_configuration["Jwt:ExpirationMinutes"]));

            var dataCriacaoUnix = new DateTimeOffset(dataCriacao).ToUnixTimeSeconds();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, role == 1 ? "Admin" : "User"),
                new Claim(JwtRegisteredClaimNames.Iat, dataCriacaoUnix.ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: dataExpiracao,
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return (tokenString, dataCriacao, dataExpiracao);
        }
    }
}
