using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.DataAccess.Class;
using Task.Entity.Entities;

namespace Task.BusinessLogic.Class
{
    public class EmployeeBL : IDisposable
    {
        private UnitOfWork UnitOfWork = new UnitOfWork();

        private Repository<Employee> employeeRepository;

        public EmployeeBL()
        {
            employeeRepository = UnitOfWork.Repository<Employee>();
        }
        public IEnumerable<Employee> FindAll()
        {

            try
            {
                IEnumerable<Employee> ListEmployees = employeeRepository.FindAll();
                return ListEmployees;
            }
            catch (Exception ex)
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
