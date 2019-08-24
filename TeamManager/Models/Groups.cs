using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamManager.Models
{
    public enum Day
    {
        Poniedziałek,
        Wtorek,
        Środa,
        Czwartek,
        Piątek,
        Sobota,
        Niedziela,
    }
   
    public class Groups
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Nazwa grupy")]
        public string groupName { get; set; }
        public bool isChecked { get; set; } = false;
        [Display(Name = "Dzien treningu")]
        public Day TrainingDay {get; set; }
        [Display(Name = "Czas treningu")]
        public string TrainingTime { get; set; }
        public void AddNewGroupToList(string name)
        {
            KarateKid.AddToList(name);
        }

        public string PLtoEN()
        {
            switch(TrainingDay)
            {
                case Day.Poniedziałek:
                    return "Monday";
                case Day.Wtorek:
                    return "Tuesday";
                case Day.Środa:
                    return "Wednesday";
                case Day.Czwartek:
                    return "Thursday";
                case Day.Piątek:
                    return "Friday";
                case Day.Sobota:
                    return "Saturday";
                case Day.Niedziela:
                    return "Sunday";
            }
            return null;
        }
            
    }

}
