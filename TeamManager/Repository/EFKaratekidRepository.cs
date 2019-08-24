using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamManager.Models
{
    public class EFKaratekidRepository : IKidsRepository
    {
        private KarateKidDbContext context;
        public EFKaratekidRepository(KarateKidDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<KarateKid> KarateKidsx => context.KarateKidsAll;
    }
}
