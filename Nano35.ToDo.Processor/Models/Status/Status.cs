using System;

namespace Nano35.ToDo.Processor.Models
{
    public class Status : 
        ICastable,
        IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}