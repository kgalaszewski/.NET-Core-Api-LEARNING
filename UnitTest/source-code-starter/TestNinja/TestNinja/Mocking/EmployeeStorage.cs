using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    class EmployeeStorage : IEmployeeStorage
    {
        private EmployeeContext db;

        public EmployeeContext Db { get => db; set => db = value; }

        public void DeleteEmployee(int id)
        {
            var employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
        }
    }
}
