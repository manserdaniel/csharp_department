using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbteilungenData
{
    public class DepartmentRepository : Repository<Department>
    {

        DepartmentEntities entities = new DepartmentEntities();

        public int create(Department entity)
        {
            entities.Department.Add(entity);
            return entities.SaveChanges();
        }

        public List<Department> findAll()
        {
            return entities.Department.ToList();
        }

        public Department findOne(int id)
        {
            return entities.Department.FirstOrDefault(x => x.id == id);
        }

        public void deleteOne(Department department)
        {
            entities.Department.Remove(department);
            entities.SaveChanges();
        }
    }
}
