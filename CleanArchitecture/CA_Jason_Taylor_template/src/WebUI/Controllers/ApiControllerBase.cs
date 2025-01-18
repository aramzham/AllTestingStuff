using CA_Jason_Taylor_template.WebUI.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CA_Jason_Taylor_template.WebUI.Controllers;

[ApiController]
[ApiExceptionFilter]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
