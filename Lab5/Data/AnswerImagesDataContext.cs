using Lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Data
{
    public class AnswerImagesDataContext : DbContext
    {

        public AnswerImagesDataContext(DbContextOptions<AnswerImagesDataContext> options) : base(options)
        {
        }

        public DbSet<AnswerImage> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnswerImage>().ToTable("AnswerImage");
        }
    }
}
