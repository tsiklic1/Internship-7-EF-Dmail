using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Data.Entities.Models
{
    public class UsersSpams
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int SpamId { get; set; }
        public User Spam { get; set; } = null!;
    }
}
