﻿using DmailApp.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Domain.Models
{
    public class MailTitleWithReceiverAdress
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ReceiverAdress { get; set; }
        public ICollection<User> Receivers { get; set; } = new List<User>();
    }
}