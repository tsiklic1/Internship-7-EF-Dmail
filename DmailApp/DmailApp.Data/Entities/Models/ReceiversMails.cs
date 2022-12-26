﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DmailApp.Data.Entities.Models.Mails;

namespace DmailApp.Data.Entities.Models
{
    public class ReceiversMails
    {
        public int ReceiverId { get; set; }
        public User Receiver { get; set; } = null!;
        public int MailId { get; set; }
        public Mail SentMail { get; set; } = null!;
    }
}
