using System;

namespace Nano35.ToDo.Processor.Models.User
{
    public class User : 
        ICastable,
        IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        
        public Guid RoleId { get; set; }
        public Role.Role Role { get; set; }
        
        public Guid StatusId { get; set; }
        public Status.Status Status { get; set; }
    }
}