using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ServerAPI.Models
{
    public partial class Employees
    {
        public Employees()
        {
            EmployeeGrade = new HashSet<EmployeeGrade>();
            EmployeeRise = new HashSet<EmployeeRise>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public long? Age { get; set; }
        public long? Position { get; set; }
        public long? Department { get; set; }
        public decimal? Salary { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public virtual EmployeeDepartment DepartmentNavigation { get; set; }
        public virtual EmployeePosition PositionNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<EmployeeGrade> EmployeeGrade { get; set; }
        [JsonIgnore]
        public virtual ICollection<EmployeeRise> EmployeeRise { get; set; }
    }
}
