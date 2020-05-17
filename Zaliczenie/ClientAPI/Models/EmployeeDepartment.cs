using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAPI.Models
{
    public class EmployeeDepartment
    {
        public EmployeeDepartment()
        {
            Employees = new HashSet<Employees>();
        }

        public long Id { get; set; }
        public string DepartmentName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
