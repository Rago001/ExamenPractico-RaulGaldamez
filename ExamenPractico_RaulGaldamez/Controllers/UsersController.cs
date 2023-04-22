using AutoMapper;
using ExamenPractico_RaulGaldamez.DTOs;
using ExamenPractico_RaulGaldamez.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExamenPractico_RaulGaldamez.Controllers {

    [ApiController]
    [Route("api/users")]
    public class UsersController: ControllerBase {

        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public UsersController(AppDbContext context, IMapper mapper, IConfiguration configuration) {
            this.context = context;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistroUsuarioDTO>> RegisterUser(CredentialsDTO credentialsDTO) {

            var newUser = mapper.Map<Users>(credentialsDTO);

            context.Add(newUser);
            await context.SaveChangesAsync();

            var wasCreated = await context.Users.AnyAsync(x => x.userName == newUser.userName);

            if (wasCreated) {
                return createToken(credentialsDTO);
            } else {
                return BadRequest("Error al crear el usuario");
            }
        
        }

        [HttpPost("login")]
        public async Task<ActionResult<RegistroUsuarioDTO>> LoginUser(CredentialsDTO credentialsDTO) {

            var areCredentialsOk = await context.Users.AnyAsync(x => x.userName == credentialsDTO.userName && x.userPassword == credentialsDTO.userPassword);

            if (areCredentialsOk) {
                return createToken(credentialsDTO);
            } else {
                return BadRequest("Login incorrecto");
            }

        }

        private RegistroUsuarioDTO createToken(CredentialsDTO credentials) {

            var claims = new List<Claim>() { 
                new Claim("userName", credentials.userName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["secretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expirationTime = DateTime.UtcNow.AddMinutes(30);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expirationTime, signingCredentials: creds);

            return new RegistroUsuarioDTO {
                token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                expiration = expirationTime
            };
        
        }

    }
}
