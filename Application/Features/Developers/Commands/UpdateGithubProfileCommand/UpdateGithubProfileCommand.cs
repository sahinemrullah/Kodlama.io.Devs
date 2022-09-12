using Application.Features.Developers.Dtos;
using Application.Features.Developers.Rules;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Extensions;
using Core.Security.JWT;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Security.Accessors.UserAccessor;
using Security.Entities;
using Security.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Commands.UpdateGithubProfileCommand
{
    public class UpdateGitbubProfileCommand : IRequest<UpdatedGithubProfileDto>, ISecuredRequest
    {
        public string NewGithubProfileLink { get; set; } = null!;

        public string[] Roles => new string[] { "User" };

        public class UpdateGitbubProfileCommandHandler : IRequestHandler<UpdateGitbubProfileCommand, UpdatedGithubProfileDto>
        {
            private readonly IDeveloperRepository _developerRepository;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            public UpdateGitbubProfileCommandHandler(IDeveloperRepository developerRepository, IMapper mapper, IUserAccessor userAccessor)
            {
                _developerRepository = developerRepository;
                _mapper = mapper;
                _userAccessor = userAccessor;
            }

            public async Task<UpdatedGithubProfileDto> Handle(UpdateGitbubProfileCommand request, CancellationToken cancellationToken)
            {
                ClaimsPrincipal user = _userAccessor.User;
                Developer? developer = await _developerRepository.GetAsync(d => d.Id == user.GetUserId());
                DeveloperBusinessRules.DeveloperShouldExistWhenRequested(developer);

                _mapper.Map(request, developer);

                Developer updatedDeveloper = await _developerRepository.UpdateAsync(developer!);
                UpdatedGithubProfileDto updatedGithubProfileDto = _mapper.Map<UpdatedGithubProfileDto>(updatedDeveloper);
                return updatedGithubProfileDto;
            }
        }
    }
}
