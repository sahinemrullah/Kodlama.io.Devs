using Application.Features.Developers.Commands.AuthorizeCommand;
using Application.Features.Developers.Commands.RegisterCommand;
using Application.Features.Developers.Commands.UpdateGithubProfileCommand;
using Application.Features.Developers.Dtos;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Profiles
{
    public class DeveloperMappingProfile : Profile
    {
        public DeveloperMappingProfile()
        {
            CreateMap<RegisterCommand, Developer>()
                .ReverseMap();
            CreateMap<AuthorizeCommand, Developer>()
                .ReverseMap();
            CreateMap<AccessToken, AuthorizeResult>()
                .ReverseMap();
            CreateMap<UpdatedGithubProfileDto, Developer>()
                .ReverseMap();
            CreateMap<UpdateGitbubProfileCommand, Developer>()
                .ForMember(c => c.GithubLink, opt => opt.MapFrom(c => c.NewGithubProfileLink))
                .ReverseMap();
            CreateMap<DeletedGithubProfileDto, Developer>()
                .ReverseMap();
        }
    }
}
