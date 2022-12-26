using DmailApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Domain.Factories
{
    public class RepositoryFactory
    {
        public static TRepository Create<TRepository>()
        where TRepository : BaseRepository                  
        {
            var dbContext = DbContextFactory.GetDmailAppDbContext();
            var repositoryInstance = Activator.CreateInstance(typeof(TRepository), dbContext) as TRepository;

            return repositoryInstance!;
        }
    }
}
