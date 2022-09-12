using Core.Persistence.Repositories;
using Core.Security.Entities;
using Security.Entities;

namespace Security.Repositories
{
    public interface IDeveloperRepository : IRepository<Developer>, IAsyncRepository<Developer>
    {
    }
}
