using Application.Features.Developers.Dtos;
using Application.Features.Developers.Rules;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Security.Entities;
using Security.Repositories;

namespace Application.Features.Developers.Commands.AuthorizeCommand
{
    public class AuthorizeCommand : IRequest<AuthorizeResult?>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public class AuthorizeCommandHandler : IRequestHandler<AuthorizeCommand, AuthorizeResult?>
        {
            private readonly SignInManager<Developer> _signInManager;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            public AuthorizeCommandHandler(SignInManager<Developer> signInManager, ITokenHelper tokenHelper, IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
            {
                _signInManager = signInManager;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _userOperationClaimRepository = userOperationClaimRepository;
            }

            public async Task<AuthorizeResult?> Handle(AuthorizeCommand request, CancellationToken cancellationToken)
            {
                Developer developer = await _signInManager.UserManager.FindByEmailAsync(request.Email);
                DeveloperBusinessRules.DeveloperShouldExistWhenRequested(developer);

                SignInResult signInresult = await _signInManager.CheckPasswordSignInAsync(developer, request.Password, false);
                AuthorizeResult? result = null;
                if (signInresult.Succeeded)
                {
                    IPaginate<UserOperationClaim> developerUserOperationClaims = await _userOperationClaimRepository.GetListAsync(c => c.UserId == developer.Id,
                                                                                                                                  include: c => c.Include(c => c.OperationClaim),
                                                                                                                                  cancellationToken: cancellationToken);
                    IList<OperationClaim> developerOperationClaims = developerUserOperationClaims.Items
                                                                                                    .Select(c => c.OperationClaim)
                                                                                                    .ToList();
                    AccessToken accessToken = _tokenHelper.CreateToken(developer, developerOperationClaims);
                    result = _mapper.Map<AuthorizeResult>(accessToken);
                }
                return result;
            }
        }
    }
}