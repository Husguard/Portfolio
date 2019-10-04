using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Models
{
    public class MessageContext : DbContext
    {
        public MessageContext(DbContextOptions<MessageContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        // таблица users, связь 1-м
    }
    }
