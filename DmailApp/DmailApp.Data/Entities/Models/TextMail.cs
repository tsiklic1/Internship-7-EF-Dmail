using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Data.Entities.Models
{
    public class TextMail : Mail
    {
        public string Content { get; set; } 
        public TextMail(string title, string content) : base(title)
        {
            Content = content;
        }
    }
}
