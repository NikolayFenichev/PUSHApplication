using MessageProcessing.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageProcessing.DAL
{
    public class MessageProcessingContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public MessageProcessingContext(DbContextOptions options) : base(options)
        {
        }
    }
}
