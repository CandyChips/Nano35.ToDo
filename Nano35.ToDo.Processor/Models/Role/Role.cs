using System;

namespace Nano35.ToDo.Processor.Models
{
    public class Role : 
        ICastable,
        IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}