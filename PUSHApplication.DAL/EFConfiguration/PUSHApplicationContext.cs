using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using PUSHApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PUSHApplication.DAL
{
    public class PUSHApplicationContext : DbContext
    {
        public DbSet<MobileApp> MobileApps { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageToPhoneNumber> MessageToPhoneNumbers { get; set; }

        public PUSHApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageToPhoneNumber>().HasKey(m => new { m.MessageId, m.MobileAppPhoneNumber });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }
    }
}
