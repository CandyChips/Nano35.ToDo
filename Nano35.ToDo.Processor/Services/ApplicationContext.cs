using Microsoft.EntityFrameworkCore;
using Nano35.ToDo.Processor.Models;

namespace Nano35.ToDo.Processor.Services
{
    public class ApplicationContext : DbContext
    {
        
        public DbSet<Message> Messages {get;set;}
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            Update();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Message.Configuration());
            base.OnModelCreating(modelBuilder);
        }

        private void Update()
        {
            Messages.Load();
        }
    }
}
