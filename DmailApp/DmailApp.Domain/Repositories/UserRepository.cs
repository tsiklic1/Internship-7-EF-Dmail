using DmailApp.Data;
using DmailApp.Data.Entities.Models;
using DmailApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DmailApp.Domain.Repositories
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(DmailAppDbContext dbContext) : base(dbContext)
        {
        }

        public ResponseResultType Add(User user)
        {
            DbContext.Users.Add(user);

            return SaveChanges();
        }

        public ResponseResultType Delete(int id)
        {
            var userToDelete = DbContext.Users.Find(id);
            if (userToDelete is null)
            {
                return ResponseResultType.NotFound;
            }

            DbContext.Users.Remove(userToDelete);

            return SaveChanges();
        }

        public ResponseResultType Update(User user, int id)
        {
            var userToUpdate = DbContext.Users.Find(id);
            if (userToUpdate is null)
            {
                return ResponseResultType.NotFound;
            }

            userToUpdate.Adress = user.Adress;
            userToUpdate.Password = user.Password;

            return SaveChanges();
        }

        public User? GetById(int id) => DbContext.Users.FirstOrDefault(u => u.UserId == id);

        public ICollection<User> GetAll() => DbContext.Users.ToList();

        public bool CheckIfAdressPasswordCombinationExists(string adress, string password)
        {
            var users = DbContext.Users
                .Select(u => new User(u.Adress, u.Password))
                .ToList();

            if (!users.Any(u => u.Adress == adress && u.Password == password))
            {
                return false;
            }
            return true;
        }

        public bool CheckIfAdressIsUnique(string adress)
        {
            var usersAdresses = DbContext.Users
                .Select(u => u.Adress) 
                .ToList();
            if (usersAdresses.Contains(adress))
            {
                return false;
            }
            return true;
        }

        public int GetIdByAdress(string adress)
        {
            var user = DbContext.Users
                .Where(u => u.Adress == adress)
                .FirstOrDefault();
            var id = user!.UserId;
            return id;            
        }

        

    }
}
