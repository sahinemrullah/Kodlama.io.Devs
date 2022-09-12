using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistence.Contexts;
using Security.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class OperationClaimRepository : EfRepositoryBase<OperationClaim, ApplicationDbContext>, IOperationClaimRepository
    {
        public OperationClaimRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

