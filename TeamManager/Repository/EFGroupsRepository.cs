using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamManager.Models
{
    public class EFGroupsRepository : IGroupsRepository
    {
        private KarateKidDbContext context;

        public EFGroupsRepository(KarateKidDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Groups> Groups => context.Groups;
    }
}
