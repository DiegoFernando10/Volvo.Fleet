using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volvo.Fleet.Entities.Vehicle;

namespace Volvo.Fleet.Database
{
    public class VolvoDbContext : DbContext
    {
        public VolvoDbContext(DbContextOptions<VolvoDbContext> options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // CommunicationStatus
            modelBuilder.Entity<Vehicle>(r =>
            {
                r.Property(c => c.TypeEnum)
                    .HasColumnName("Type");

                r.HasKey(r => r.ChassisId);
            });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql();
        }

        public static VolvoDbContext Create()
        {
            var builder = new DbContextOptionsBuilder<VolvoDbContext>();

            return new VolvoDbContext(builder.Options);
        }
    }
}
