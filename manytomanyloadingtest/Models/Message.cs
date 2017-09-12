using System;

namespace manytomanyloadingtest.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public string UserMessage { get; set; }
        public User Sender{ get; set; }
        public DateTime CreationTime { get; set; }
    }
}