using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.DeleteTechnology;
using Application.Features.Technologies.Commands.UpdateTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Profiles
{
    public class TechnologyMappingProfile : Profile
    {
        public TechnologyMappingProfile()
        {
            CreateMap<IPaginate<Technology>, TechnologyListModel>()
                .ReverseMap();
            CreateMap<Technology, TechnologyListDto>()
                .ForMember(c => c.ProgrammingLanguageName, opt => opt.MapFrom(c => c.ProgrammingLanguage.Name))
                .ReverseMap();

            CreateMap<Technology, GetByIdTechnologyDto>()
                .ForMember(c => c.ProgrammingLanguageName, opt => opt.MapFrom(c => c.ProgrammingLanguage.Name))
                .ReverseMap();

            CreateMap<Technology, CreateTechnologyCommand>()
                .ReverseMap();
            CreateMap<Technology, CreatedTechnologyDto>()
                .ForMember(c => c.ProgrammingLanguageName, opt => opt.MapFrom(c => c.ProgrammingLanguage.Name))
                .ReverseMap();

            CreateMap<Technology, UpdateTechnologyCommand>()
                .ReverseMap();
            CreateMap<Technology, UpdatedTechnologyDto>()
                .ForMember(c => c.ProgrammingLanguageName, opt => opt.MapFrom(c => c.ProgrammingLanguage.Name))
                .ReverseMap();

            CreateMap<Technology, DeleteTechnologyCommand>()
                .ReverseMap();
            CreateMap<Technology, DeletedTechnologyDto>()
                .ForMember(c => c.ProgrammingLanguageName, opt => opt.MapFrom(c => c.ProgrammingLanguage.Name))
                .ReverseMap();
        }
    }
}
