using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _repository;

        public TechnologyBusinessRules(ITechnologyRepository repository)
        {
            _repository = repository;
        }

        public static void TechnologyShouldExistWhenRequested(Technology? programmingLanguage)
        {
            if (programmingLanguage == null) throw new BusinessException("Requested technology does not exist");
        }
    }
}
