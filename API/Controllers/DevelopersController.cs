using Application.Features.Developers.Commands.AuthorizeCommand;
using Application.Features.Developers.Commands.DeleteGithubProfileCommand;
using Application.Features.Developers.Commands.RegisterCommand;
using Application.Features.Developers.Commands.UpdateGithubProfileCommand;
using Application.Features.Developers.Dtos;
using Core.Application.Requests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Security.Entities;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DevelopersController : BaseController
    {
        [AllowAnonymous, HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerDeveloperCommand)
        {
            IdentityResult result = await Mediator!.Send(registerDeveloperCommand);
            return Created("", result);
        }

        [AllowAnonymous, HttpPost(nameof(Authorize))]
        public async Task<IActionResult> Authorize([FromBody] AuthorizeCommand developerLoginCommand)
        {
            AuthorizeResult? result = await Mediator!.Send(developerLoginCommand);
            return Ok(result);
        }

        [HttpPatch(nameof(UpdateGithubProfile))]
        public async Task<IActionResult> UpdateGithubProfile([FromBody] UpdateGitbubProfileCommand updateGithubProfileCommand)
        {
            UpdatedGithubProfileDto updatedGithubProfileDto = await Mediator!.Send(updateGithubProfileCommand);
            return Ok(updatedGithubProfileDto);
        }

        [HttpDelete(nameof(DeleteGithubProfile))]
        public async Task<IActionResult> DeleteGithubProfile([FromBody] DeleteGitbubProfileCommand deleteGitbubProfileCommand)
        {
            DeletedGithubProfileDto deletedGithubProfileDto = await Mediator!.Send(deleteGitbubProfileCommand);
            return Ok(deletedGithubProfileDto);
        }
    }
}
