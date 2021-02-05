using System.Collections.Generic;
using Nano35.ToDo.Processor.Models.Chat;
using Nano35.ToDo.Processor.Models.ChatMembership;
using Nano35.ToDo.Processor.Models.ChatMembershipStatus;
using Nano35.ToDo.Processor.Models.Role;
using Nano35.ToDo.Processor.Models.Status;
using Nano35.ToDo.Processor.Models.User;

namespace Nano35.ToDo.Processor.Contexts
{
    public class ApplicationContext
    {
        public List<Chat> Chats { get; set; }
        public List<ChatMembership> ChatMemberships { get; set; }
        public List<ChatMembershipStatus> ChatMembershipStatuses { get; set; }
        public List<Role> Roles { get; set; }
        public List<Status> Statuses { get; set; }
        public List<User> Users { get; set; }
    }
}