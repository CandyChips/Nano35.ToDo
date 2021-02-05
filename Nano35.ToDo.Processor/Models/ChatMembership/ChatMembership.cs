using System;

namespace Nano35.ToDo.Processor.Models.ChatMembership
{
    public class ChatMembership :
        ICastable,
        IEntity
    {
        public DateTime Date { get; set; }
        
        public Guid UserId { get; set; }
        public User.User User { get; set; }
        public Guid ChatId { get; set; }
        public Chat.Chat Chat { get; set; }
        public Guid ChatMembershipId { get; set; }
        public ChatMembershipStatus.ChatMembershipStatus ChatMembershipStatus { get; set; }
    }
}