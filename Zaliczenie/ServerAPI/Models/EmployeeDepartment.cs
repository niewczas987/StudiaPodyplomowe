using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ServerAPI.Models
{
    public partial class EmployeeDepartment
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
