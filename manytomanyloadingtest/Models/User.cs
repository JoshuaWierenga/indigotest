using System;
using System.Collections.Generic;

namespace manytomanyloadingtest.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<UserConversation> UserConversations { get; set; }       
    }
}