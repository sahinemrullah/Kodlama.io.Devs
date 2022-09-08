using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Queries.GetByIdTechnology
{
    public class GetByIdTechnologyQuery : IRequest<GetByIdTechnologyDto>
    {
        public int Id { get; set; }
        public class GetByIdTechnologyQueryHandler : IRequestHandler<GetByIdTechnologyQuery, GetByIdTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;

            public GetByIdTechnologyQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
            }

            public async Task<GetByIdTechnologyDto> Handle(GetByIdTechnologyQuery request, CancellationToken cancellationToken)
            {
                Technology? technology = await _technologyRepository.GetAsync(b => b.Id == request.Id, include: e => e.Include(e => e.ProgrammingLanguage), cancellationToken: cancellationToken);

                TechnologyBusinessRules.TechnologyShouldExistWhenRequested(technology);

                GetByIdTechnologyDto technologyGetByIdDto = _mapper.Map<GetByIdTechnologyDto>(technology);
                return technologyGetByIdDto;
            }
        }
    }
}
