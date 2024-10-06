using APP.Models;

namespace APP.Repository.Base
{
    public interface IEmpRepo :IRepository<Employee>
    {
        void setPayRoll(Employee employee);
        decimal getSalary(Employee employee);

    }
}
