using Lab4.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Data
{
    public class SchoolCommunityContext : DbContext
    {
        public SchoolCommunityContext(DbContextOptions<SchoolCommunityContext> options) : base(options)
        {
        }
        public DbSet <Student> Student { get; set; }

        public DbSet <Community> Communities { get; set; }

        public DbSet <CommunityMembership> CommunityMemberships { get; set; }

        public DbSet <Advertisements> Advertisements { get; set; }

        public DbSet <CommunityAdvertisement> CommunityAdvertisements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Community>().ToTable("Community");
            modelBuilder.Entity<CommunityMembership>().ToTable("Membership");
            modelBuilder.Entity<Advertisements>().ToTable("Advertisements");
            modelBuilder.Entity<CommunityAdvertisement>()
                .HasKey(c => new { c.CommunityID, c.AdvertisementID });
            modelBuilder.Entity<CommunityMembership>()
                .HasKey(c => new { c.StudentId, c.CommunityId });
        }
    }
}
