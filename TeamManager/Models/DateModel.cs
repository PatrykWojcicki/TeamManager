using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TeamManager.Models
{
    public class DateModel
    {
        [Key]
        public int ID { get; set; }
        public DateTime ActualDate { get; set; }
        public string ChildName { get; set; }
        public string GroupName { get; set; }
        public bool ItIsPayment { get; set; }

        public DateModel(string ChildName, string GroupName, bool ItIsPayment)
        {
            this.ActualDate = DateTime.Today;
            this.ChildName = ChildName;
            this.GroupName = GroupName;
        }

        public DateModel(string ChildName, bool ItIsPayment)
        {
            this.ActualDate = DateTime.Today;
            this.ChildName = ChildName;
            this.ItIsPayment = ItIsPayment;
        }

        public string NumberToMonth()
        {
            switch (Int32.Parse(ActualDate.Month.ToString()))
            {
                case 1:
                    return "Styczeń";
                case 2:
                    return "Luty";
                case 3:
                    return "Marzec";
                case 4:
                    return "Kwiecień";
                case 5:
                    return "Maj";
                case 6:
                    return "Czerwiec";
                case 7:
                    return "Lipiec";
                case 8:
                    return "Sierpień";
                case 9:
                    return "Wrzesień";
                case 10:
                    return "Październik";
                case 11:
                    return "Listopad";
                case 12:
                    return "Grudzień";
            }
            return null;
        }
    }
}
