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

namespace Application.Features.Developers.Commands.DeleteGithubProfileCommand
{
    public class DeleteGitbubProfileCommand : IRequest<DeletedGithubProfileDto>, ISecuredRequest
    {
        public string[] Roles => new string[] { "User" };

        public class DeleteGitbubProfileCommandHandler : IRequestHandler<DeleteGitbubProfileCommand, DeletedGithubProfileDto>
        {
            private readonly IDeveloperRepository _developerRepository;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            public DeleteGitbubProfileCommandHandler(IDeveloperRepository developerRepository, IMapper mapper, IUserAccessor userAccessor)
            {
                _developerRepository = developerRepository;
                _mapper = mapper;
                _userAccessor = userAccessor;
            }

            public async Task<DeletedGithubProfileDto> Handle(DeleteGitbubProfileCommand request, CancellationToken cancellationToken)
            {
                ClaimsPrincipal user = _userAccessor.User;
                Developer? developer = await _developerRepository.GetAsync(d => d.Id == user.GetUserId());
                DeveloperBusinessRules.DeveloperShouldExistWhenRequested(developer);

                developer!.GithubLink = null;

                Developer deletedDeveloper = await _developerRepository.UpdateAsync(developer!);
                DeletedGithubProfileDto deletedGithubProfileDto = _mapper.Map<DeletedGithubProfileDto>(deletedDeveloper);
                return deletedGithubProfileDto;
            }
        }
    }
}
