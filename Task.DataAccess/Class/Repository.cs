using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Task.DataAccess.Context;
using Task.DataAccess.Interface;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace Task.DataAccess.Class
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> dbSet;
        private readonly TaskContext dbContext;

        public Repository(TaskContext TasksContext)
        {
            this.dbContext = TasksContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        public virtual void Create(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Create(IEnumerable<TEntity> entity)
        {
            foreach (var item in entity)
            {
                dbSet.Add(item);
            }

        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, string[] IncludeProperties = null)
        {

            IQueryable<TEntity> Query = dbSet;
            if (IncludeProperties != null)
            {
                foreach (var inlcudeProperty in IncludeProperties)
                {
                    Query = Query.Include(inlcudeProperty);
                }
            }
            return Query.AsNoTracking().Where(predicate);
            //return Query.Where(predicate).ToList();

        }

        public virtual IQueryable<TEntity> FindAll(string[] IncludeProperties = null)
        {
            IQueryable<TEntity> Query = dbSet;
            if (IncludeProperties != null)
            {
                foreach (var inlcudeProperty in IncludeProperties)
                {
                    Query = Query.Include(inlcudeProperty);
                }
            }
            return Query.AsNoTracking();
        }

        public virtual TEntity FindById(int Id)
        {
            return dbSet.Find(Id);
        }

        public virtual void Update(TEntity entity)
        {
            DbEntityEntry DbEntityEntry = dbContext.Entry(entity);
            var key = this.GetPrimaryKey(DbEntityEntry);
            if (DbEntityEntry.State == EntityState.Detached)
            {
                TEntity currentEntity = this.FindById(key);
                if (currentEntity != null)
                {
                    DbEntityEntry AttachedEntry = dbContext.Entry(currentEntity);
                    AttachedEntry.CurrentValues.SetValues(entity);

                }
                else
                {
                    dbSet.Attach(entity);
                    DbEntityEntry.State = EntityState.Modified;
                }
            }
        }

        private int GetPrimaryKey(DbEntityEntry entry)
        {
            var myObject = entry.Entity;
            var property =
                myObject.GetType()
                    .GetProperties()
                    .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));
            return (int)property.GetValue(myObject, null);
        }

        public virtual void Delete(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = dbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                dbSet.Attach(entity);
                dbSet.Remove(entity);
            }
        }

        public virtual void Delete(int Id)
        {
            TEntity entity = FindById(Id);
            if (entity == null) return;
            Delete(entity);
        }
    }
}
