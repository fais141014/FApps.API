using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using FApps.Services.User.Query;
using FApps.Core.Domain;
using FApps.Services.User.Command;

namespace FApps.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region Get All User
        [HttpGet]
        public async Task<IActionResult>GetAll()
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
        //[HttpGet("{Email}/{Password}",Name ="GetUser")]
        //public async Task<IActionResult>GetByConditions([FromBody] string email,[FromBody] string password)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return BadRequest("token can not be created");
        //    }
        //    var result = await _mediator.Send(new ReadQueryByConditions(email, password));
        //    if(result==null)
        //    {
        //        return Unauthorized();
        //    }
        //    // TODO:implement jwt pending
        //    return (IActionResult)Ok(result);
        //}
        #endregion
    }
}