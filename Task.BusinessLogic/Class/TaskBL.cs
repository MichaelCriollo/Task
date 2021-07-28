using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.DataAccess.Class;

namespace Task.BusinessLogic.Class
{
    public class TaskBL : IDisposable
    {
        private UnitOfWork UnitOfWork = new UnitOfWork();

        private Repository<Entity.Entities.Task> taskRepository;

        public TaskBL()
        {
            taskRepository = UnitOfWork.Repository<Entity.Entities.Task>();
        }

        public Entity.Entities.Task FindById(int idTask)
        {
            return taskRepository.FindById(idTask);
        }

        public IEnumerable<Entity.Entities.Task> FindAll()
        {
            string[] Props = new string[] { "Employee" };
            try
            {
                return taskRepository.FindAll(Props);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Entity.Entities.Task Create(Entity.Entities.Task task)
        {
            Entity.Entities.Task taskCreate = new Entity.Entities.Task();
            try
            {
                using (DbContextTransaction transaction = UnitOfWork.GetContext().Database.BeginTransaction())
                {
                    try
                    {
                        taskCreate = task;
                        taskRepository.Create(taskCreate);
                        UnitOfWork.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return task;
        }

        public bool Update(Entity.Entities.Task task)
        {
            string responseMessage = string.Empty;

            using (DbContextTransaction transaction = UnitOfWork.GetContext().Database.BeginTransaction())
            {
                try
                {
                    taskRepository.Update(task);
                    UnitOfWork.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            return true;
        }

        public bool Delete(Entity.Entities.Task task)
        {
            string responseMessage = string.Empty;
            try
            {
                using (DbContextTransaction transaction = UnitOfWork.GetContext().Database.BeginTransaction())
                {
                    try
                    {
                        taskRepository.Delete(task);
                        UnitOfWork.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
