using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Entity.Entities;

namespace Task.DataAccess.Context
{
    public partial class TaskContext : DbContext
    {
        public TaskContext() 
            : base("name=TaskContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
        }

        public virtual DbSet<Employee> Employee { get; set; }

        public virtual DbSet<Entity.Entities.Task> Task { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            modelBuilder.Entity<Employee>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
               .Property(e => e.FirstName)
               .IsUnicode(false);

            modelBuilder.Entity<Employee>()
               .Property(e => e.LastName)
               .IsUnicode(false);

            modelBuilder.Entity<Employee>()
               .Property(e => e.Mobile)
               .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Entity.Entities.Task>()
                .Property(t => t.Name)
                .IsUnicode(false);
        }
    }
}
