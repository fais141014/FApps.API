using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using FApps.Services.User.Query;
using FApps.Core.Domain;
using FApps.Services.User.Command;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace FApps.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IConfiguration _config;
        public UsersController(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _config = config;
        }
        #region Get All User
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new ReadQuery());
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }
        #endregion

        #region Insert user       
        [HttpPost]
       public async Task<IActionResult>Create([FromBody] User model)
        {
            if (model == null || !ModelState.IsValid) return BadRequest(ModelState);
            var id = await _mediator.Send(new InsertCommand(model));
            return string.IsNullOrEmpty(id.ToString()) ? NotFound() : (IActionResult)Created(string.Empty, id);
        }
        #endregion

        #region login via jwt
        [AllowAnonymous]
        [HttpGet("{email}/{password}", Name = "GetUser")]      
        public async Task<IActionResult> GetByConditions([FromRoute] string email, [FromRoute] string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("token can not be created");
            }
            var result = await _mediator.Send(new ReadQueryByConditions(email, password));
            if (result == null)
            {
                return Unauthorized();
            }

            var claims = new[]
             {
                new Claim(JwtRegisteredClaimNames.Email,email),
                new Claim(JwtRegisteredClaimNames.Sub,"subject"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //shared key between the token server and the resource server
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("condimentumvestibulumSuspendissesitametpulvinarorcicondimentummollisjusto"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var SecurityToken = new JwtSecurityToken(
                issuer: _config["AuthSection:JWtConfig:Issuer"],
                audience: _config["AuthSection:JWtConfig:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials) ;
            result.token = new JwtSecurityTokenHandler().WriteToken(SecurityToken);
            result.expiration = SecurityToken.ValidTo;
            return Ok(result);            
        }
        #endregion
    }
}