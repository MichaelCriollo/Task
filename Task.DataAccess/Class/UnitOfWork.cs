using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.DataAccess.Context;

namespace Task.DataAccess.Class
{
    public class UnitOfWork : IDisposable
    {
        private readonly TaskContext dbContext;
        private bool disposed;
        private Dictionary<string, object> repositories;

        public UnitOfWork(TaskContext TaskContext)
        {
            dbContext = TaskContext;
        }

        public UnitOfWork()
        {
            dbContext = new TaskContext();
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public TaskContext GetContext()
        {
            return this.dbContext;
        }

        public Repository<T> Repository<T>() where T : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }
            var type = typeof(T).Name;
            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), dbContext);
                repositories.Add(type, repositoryInstance);
            }
            return (Repository<T>)repositories[type];
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
