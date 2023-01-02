using DmailApp.Data;
using DmailApp.Domain.Enums;

namespace DmailApp.Domain.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly DmailAppDbContext DbContext;

        protected BaseRepository(DmailAppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected ResponseResultType SaveChanges()
        {
            var hasChanges = DbContext.SaveChanges() > 0;
            if (hasChanges)
                return ResponseResultType.Success;

            return ResponseResultType.NoChanges;
        }

    }
}
