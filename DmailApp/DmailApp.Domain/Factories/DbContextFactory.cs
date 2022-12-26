using DmailApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Domain.Factories
{
    public static class DbContextFactory
    {
        public static DmailAppDbContext GetDmailAppDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseNpgsql(ConfigurationManager.ConnectionStrings["DmailApp"].ConnectionString)
                .Options;

            return new DmailAppDbContext(options);
        }
    }
}
