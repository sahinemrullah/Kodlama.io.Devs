using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class TechnologyRepository : EfRepositoryBase<Technology, ApplicationDbContext>, ITechnologyRepository
    {
        public TechnologyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
