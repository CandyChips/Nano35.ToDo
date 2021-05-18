using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nano35.ToDo.Processor.Models
{
    public class Message 
    {
        public Guid Id { get; init; }
        public Guid InstanceId { get; init; }
        public Guid FromUserId { get; init; }
        public Guid ToUserId { get; init; }
        public string Text { get; init; }
        public DateTime Date { get; set; }
        public DateTime Seen { get; set; }
        public Message() { }
        public class Configuration : IEntityTypeConfiguration<Message>
        {
            public void Configure(EntityTypeBuilder<Message> builder)
            {
                builder.ToTable("Messages");
                builder.HasKey(u => new { u.Id });
                builder.Property(b => b.FromUserId)
                    .IsRequired();
                builder.Property(b => b.FromUserId)
                    .IsRequired();
                builder.Property(b => b.ToUserId)
                    .IsRequired();
                builder.Property(b => b.Text)
                    .IsRequired();
                builder.Property(b => b.Date)
                    .IsRequired();
            }
        }
    }
}