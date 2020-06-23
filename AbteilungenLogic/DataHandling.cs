using AbteilungenData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbteilungenLogic
{
    public class DataHandling
    {
        private DepartmentRepository departmentRepository = new DepartmentRepository();

        public void addDepartment(string department, string parentDepartment)
        {
            List<Department> departments = departmentRepository.findAll();
            departmentRepository.create(new Department() { name =  department, parent_id = departments.FirstOrDefault(x => x.name == parentDepartment).id});
        }
        
        
        public void deleteDepartment(string department)
        {
            List<Department> departments = departmentRepository.findAll();
            departmentRepository.deleteOne(departments.FirstOrDefault(x => x.name == department));
        }

        public List<Department> getDepartments()
        {
            return departmentRepository.findAll();
        }

        public void updateDepartments(List<Department> departments)
        {
            departmentRepository.update(departments);
        }
    }
}
