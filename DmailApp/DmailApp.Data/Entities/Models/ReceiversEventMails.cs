using DmailApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Data.Entities.Models
{
    public class ReceiversEventMails : ReceiversMails
    {
        public StautsEnum Status { get; set; }
    }
}
