using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using FApps.Core.Domain;
using FApps.Services.Contact.Command;
using FApps.Services.Contact.Query;
using Microsoft.AspNetCore.Cors;

namespace FApps.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class ContacsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContacsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Get Contact
        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            var result = await _mediator.Send(new ReadQuery());
            return result != null ? (IActionResult)Ok(result) : StatusCode(500);
        }

        [HttpGet("{id}",Name ="GetContacts")]
        public async Task <IActionResult>GetById([FromRoute] Guid id)
        {
            if (id == Guid.Empty) return NotFound();
            var result = await _mediator.Send(new ReadByIdQuery() { Id=id});
            return result != null ? (IActionResult)Ok(result) : NotFound();
;        }
        #endregion

        #region Insert/Update
        [HttpPost]
        public async Task<IActionResult>Create([FromBody] Contact model)
        {
            if (model == null || !ModelState.IsValid) return BadRequest(ModelState);
            var id = await _mediator.Send(new InsertCommand(model));
            return string.IsNullOrEmpty(id.ToString()) ? NotFound() : (IActionResult)Created(string.Empty, id);
        }

        [HttpPut]
        public async Task<IActionResult>Update(Guid id,[FromBody] Contact model)
        {
            if (model == null || !ModelState.IsValid) return BadRequest(ModelState);
            var result = await _mediator.Send(new UpdateCommand(id, model));
            return result ? (IActionResult)Created(string.Empty, id) : Ok(id);
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete([FromRoute] Guid? id)
        {
            if (id == null || id == Guid.Empty) return BadRequest(ModelState);
            var result = await _mediator.Send(new DeleteCommand(id));
            return result ? (IActionResult)Ok(result) : NotFound();
        }
        #endregion
    }
}