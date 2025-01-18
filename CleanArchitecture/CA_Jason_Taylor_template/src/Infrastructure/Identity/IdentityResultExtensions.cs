using CA_Jason_Taylor_template.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace CA_Jason_Taylor_template.Infrastructure.Identity;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success()
            : Result.Failure(result.Errors.Select(e => e.Description));
    }
}
