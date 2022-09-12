using Core.Persistence.Repositories;
using Persistence.Contexts;
using Security.Entities;
using Security.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class DeveloperRepository : EfRepositoryBase<Developer, ApplicationDbContext>, IDeveloperRepository
    {
        public DeveloperRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
