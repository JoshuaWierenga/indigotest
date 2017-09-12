using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace manytomanyloadingtest.Models
{
    public class UserConversation
    {
        public int UserConversationId { get; set; }
        public int UserId { get; set; }
        public int ConversationId { get; set; }

        public User User { get; set; }
        public Conversation Conversation { get; set; }

        public bool IsAdmin { get; set; }
    }
}