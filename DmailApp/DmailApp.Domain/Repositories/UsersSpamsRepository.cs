using DmailApp.Data;
using DmailApp.Data.Entities.Models;
using DmailApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Domain.Repositories
{
    public class UsersSpamsRepository : BaseRepository
    {
        public UsersSpamsRepository(DmailAppDbContext dbContext) : base(dbContext)
        {
        }

        public ResponseResultType Add(UsersSpams userSpam)
        {
            DbContext.UsersSpams.Add(userSpam);

            return SaveChanges();
        }

        public ResponseResultType Delete(int id)
        {
            var userSpamToDelete = DbContext.UsersSpams.Find(id);
            if (userSpamToDelete is null)
            {
                return ResponseResultType.NotFound;
            }

            DbContext.UsersSpams.Remove(userSpamToDelete);

            return SaveChanges();
        }
    }
}
