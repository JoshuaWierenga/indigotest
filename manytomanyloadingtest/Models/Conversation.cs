using System.Collections.Generic;

namespace manytomanyloadingtest.Models
{
    public class Conversation
    {
        public int ConversationId { get; set; }
        public string GroupName { get; set; }
        public bool IsGroupChat { get; set; }
        public ICollection<UserConversation> UserConversations { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}