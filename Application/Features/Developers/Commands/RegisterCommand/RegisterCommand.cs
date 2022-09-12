using Application.Features.Developers.Rules;
using Application.Features.Technologies.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Security.Entities;
using Security.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Commands.RegisterCommand
{
    public class RegisterCommand : IRequest<IdentityResult>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, IdentityResult>
        {
            private readonly UserManager<Developer> _userManager;
            private readonly IMapper _mapper;
            private readonly IOperationClaimRepository _operationClaimRepository;

            public RegisterCommandHandler(UserManager<Developer> userManager, IMapper mapper, IOperationClaimRepository operationClaimRepository)
            {
                _userManager = userManager;
                _mapper = mapper;
                _operationClaimRepository = operationClaimRepository;
            }

            public async Task<IdentityResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                Developer mappedDeveloper = _mapper.Map<Developer>(request);

                OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(c => c.Name == "User");
                DeveloperBusinessRules.RoleShouldExistWhenRequested(operationClaim);

                mappedDeveloper.UserOperationClaims.Add(new UserOperationClaim()
                {
                    OperationClaim = operationClaim!
                });

                IdentityResult result = await _userManager.CreateAsync(mappedDeveloper, request.Password);
                DeveloperBusinessRules.IdentityResultMustSucceededWhenRegister(result);

                return result;
            }
        }
    }
}
