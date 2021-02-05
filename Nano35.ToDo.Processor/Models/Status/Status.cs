using System;

namespace Nano35.ToDo.Processor.Models.Status
{
    public class Status : 
        ICastable,
        IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}