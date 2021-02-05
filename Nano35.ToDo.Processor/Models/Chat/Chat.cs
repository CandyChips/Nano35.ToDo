using System;

namespace Nano35.ToDo.Processor.Models.Chat
{
    public class Chat :
        ICastable,
        IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}