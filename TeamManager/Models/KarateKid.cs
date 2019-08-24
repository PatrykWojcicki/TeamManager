using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TeamManager.Models
{
    
    public class KarateKid
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Imie i Nazwisko")]
        public string name { get; set; }
        [Display(Name = "Nr. telefonu")]
        public int phone { get; set; }
        [Display(Name = "Grupa")]
        public string Group { get; set; }
        public List<DateModel> PresentList { get; set; } = new List<DateModel>();
        public List<DateModel> PaymentList { get; set; } = new List<DateModel>();

        public static  KarateKidDbContext _context;
        public static List<string> Trial = new List<string>();
        public List<string> TrialCopy =>Trial;

        public static string GroupRellol(int x)
        {
            int y = x % 9;
            switch (y)
            {
                case 1:
                    return "Pszczółki";
                case 2:
                    return "Skowarcz";
                case 3:
                    return "Tczew";
                case 4:
                    return "Różyny";
                case 5:
                    return "Kolnik";
                case 6:
                    return "Pruszcz Gdański";
                case 7:
                    return "Zadupie";
                case 8:
                    return "Piątek";
                case 0:
                    return "Czwartek";
            }
            return null;
        }

        public static void AddToList(string NewGroup)
        {
            bool GroupsEgsist = false;
            foreach(string item in Trial)
            {
                if(item == NewGroup)
                {
                    GroupsEgsist = true;
                }
            }

            if(GroupsEgsist == false)
            {
                Trial.Add(new string(NewGroup));
            }
        }
        public static void Remove(string name)
        {
            Trial.Remove(name);
        }

        
    }


}
