using System;

namespace Nano35.ToDo.Processor.Models
{
    public class ChatMembership :
        ICastable,
        IEntity
    {
        public DateTime Date { get; set; }
        
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }
        public Guid ChatMembershipId { get; set; }
        public ChatMembershipStatus ChatMembershipStatus { get; set; }
    }
}