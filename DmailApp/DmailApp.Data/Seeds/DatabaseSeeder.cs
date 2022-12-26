using DmailApp.Data.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Data.Seeds
{
    public static class DatabaseSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasData(new List<User>
                {
                    new User("mirko@gmail.com", "12345")
                    {
                        UserId= 1,
                    }
                }
                );
        }
    }
}
