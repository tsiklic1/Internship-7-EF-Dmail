using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Data.Entities.Models
{
    public abstract class Mail
    {
        public int Id { get; set; }
     
        public string Title { get; set; } = "Title";
        public DateTime DateTimeOfSending { get; set; }
        public bool WasRead { get; set; } = false;
        
        //sender dodat referencu na user - 1 to many 
        
        public Mail(string title) 
        {
            Title = title;
        }

    }
}
