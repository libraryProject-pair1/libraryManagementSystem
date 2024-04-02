using Application.Features.AuthorBooks.Commands.Create;
using Application.Features.AuthorBooks.Commands.Delete;
using Application.Features.AuthorBooks.Commands.Update;
using Application.Features.AuthorBooks.Queries.GetById;
using Application.Features.AuthorBooks.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorBooksController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateAuthorBooksCommand createAuthorBooksCommand)
    {
        CreatedAuthorBooksResponse response = await Mediator.Send(createAuthorBooksCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAuthorBooksCommand updateAuthorBooksCommand)
    {
        UpdatedAuthorBooksResponse response = await Mediator.Send(updateAuthorBooksCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedAuthorBooksResponse response = await Mediator.Send(new DeleteAuthorBooksCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdAuthorBooksResponse response = await Mediator.Send(new GetByIdAuthorBooksQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListAuthorBooksQuery getListAuthorBooksQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListAuthorBooksListItemDto> response = await Mediator.Send(getListAuthorBooksQuery);
        return Ok(response);
    }
}