using DmailApp.Data;
using DmailApp.Data.Entities.Models;
using DmailApp.Domain.Enums;

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

        public ResponseResultType Delete(int userId, int spamId)
        {
            var userSpamToDelete = DbContext.UsersSpams
                .Where(u => u.UserId == userId && u.SpamId == spamId)
                .FirstOrDefault();
            if (userSpamToDelete is null)
            {
                return ResponseResultType.NotFound;
            }

            DbContext.UsersSpams.Remove(userSpamToDelete);

            return SaveChanges();
        }

        public List<UsersSpams> GetSpamAccounts(string adress)
        {
            var spamAccounts = DbContext.UsersSpams
                .Where(u => u.User.Adress == adress)
                .ToList();

            return spamAccounts;
        }

        public bool CheckIfUserSpamPairExists(int userId, int spamId)
        {
            var usersSpams = DbContext.UsersSpams
                .Where(u => u.UserId == userId && u.SpamId == spamId)
                .ToList();
            if (usersSpams.Count()>0)
            {
                return true;
            }
            return false;
        }
    }
}
