using System.Security.Claims;
using System.Security.Principal;
using Application.Core;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        private UserManager<AppUser> _userManager;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected UserManager<AppUser> UserManager => _userManager ??= HttpContext.RequestServices.GetService<UserManager<AppUser>>();

        // Method to get the currently logged-in user by email
        protected async Task<AppUser> GetCurrentUserAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return await UserManager.FindByEmailAsync(email);
        }
        
        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) return NotFound();
            if (result.IsSuccess && result.Value != null)
            {
                return Ok(result.Value);
            }
            if (result.IsSuccess && result.Value == null)
            {
                return NotFound();
            }
            return BadRequest(result.Error);
        }
    }
}