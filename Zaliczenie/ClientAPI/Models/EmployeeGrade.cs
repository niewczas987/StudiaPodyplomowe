using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAPI.Models
{
    public class EmployeeGrade
    {
        public long Id { get; set; }
        public long? Idemployee { get; set; }
        public long? Value { get; set; }
        public DateTime? TimeStamp { get; set; }

        public virtual Employees IdemployeeNavigation { get; set; }
    }
}
