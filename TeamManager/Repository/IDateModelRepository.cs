using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamManager.Models
{
    public interface IDateModelRepository
    {
        IQueryable<DateModel> IDateModel { get; }
    }

}
