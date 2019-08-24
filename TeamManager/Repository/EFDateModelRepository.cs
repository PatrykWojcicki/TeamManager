using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamManager.Models
{
    public class EFDateModelRepository : IDateModelRepository
    {
        private KarateKidDbContext context;
        public EFDateModelRepository(KarateKidDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<DateModel> IDateModel => context.DateBaseAll;
    }
}
