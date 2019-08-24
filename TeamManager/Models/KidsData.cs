using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace TeamManager.Models
{
    public static class KidsData
    {

        public static void EnsureKids(IApplicationBuilder app)
        {
            KarateKidDbContext context = app.ApplicationServices
                .GetRequiredService<KarateKidDbContext>();
            context.Database.Migrate();
            if (!context.KarateKidsAll.Any())
            {
                context.KarateKidsAll.AddRange(
                    new KarateKid
                    {
                        name = "Child",
                        phone = 919919919,
                    },
                    new KarateKid
                    {
                        name = "Child1",
                        phone = 999888777,
                      
                    },
                    new KarateKid
                    {
                        name = "Child2",
                        phone = 123456789,
                    },
                     new KarateKid
                     {
                         name = "Child3",
                         phone = 999888777,
                     },
                      new KarateKid
                      {
                          name = "Child4",
                          phone = 999888777,
                      },
                       new KarateKid
                       {
                           name = "Child5",
                           phone = 789456123,
                       }
                );
              
                context.SaveChanges();
            }
        }

        public static void EnsureGroups(IApplicationBuilder app)
        {
            KarateKidDbContext context = app.ApplicationServices
                .GetRequiredService<KarateKidDbContext>();
            context.Database.Migrate();
            if (!context.Groups.Any())
            {
                context.Groups.AddRange(
                    new Groups
                    {
                        groupName = "Pszczółki",
                        TrainingDay = Day.Sobota,
                        TrainingTime = "11.00",
                        isChecked = false
                        
                    },
                    new Groups
                    {
                        groupName = "Skowarcz",
                        TrainingDay = Day.Wtorek,
                        TrainingTime = "12.00",
                        isChecked = false
        },
                    new Groups
                    {
                        groupName = "Tczew",
                        TrainingDay = Day.Środa,
                        TrainingTime = "13.00",
                        isChecked = false

                    },
                    new Groups
                    {
                        groupName = "Różyny",
                        TrainingDay = Day.Środa,
                        TrainingTime = "15.00",
                        isChecked = false
                    },
                    new Groups
                    {
                        groupName = "Kolnik",
                        TrainingDay = Day.Poniedziałek,
                        TrainingTime = "21.00",
                        isChecked = false
                    },
                    new Groups
                    {
                        groupName = "Pruszcz Gdański",
                        TrainingDay = Day.Środa,
                        TrainingTime = "14.00",
                        isChecked = false
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
