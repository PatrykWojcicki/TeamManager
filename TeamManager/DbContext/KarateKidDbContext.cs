using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace TeamManager.Models
{
    public class KarateKidDbContext : DbContext
    {
        public KarateKidDbContext(DbContextOptions<KarateKidDbContext> options) : base(options) { }

        public DbSet<KarateKid> KarateKidsAll { get; set; }
        public DbSet<DateModel> DateBaseAll { get; set; }
        public DbSet<Groups> Groups { get; set; }
    }



}
