using System.Collections.Generic;
using TeamManager.Models;

namespace TeamManager.ViewModels
{
    public class KiddAndGroup
    {
        public KarateKid KarateKidModel { get; set; }
        public IEnumerable<Groups> GropusModel { get; set; }
    }
}
