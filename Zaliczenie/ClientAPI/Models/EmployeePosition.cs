using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAPI.Models
{
    public class EmployeePosition
    {
        public EmployeePosition()
        {
            Employees = new HashSet<Employees>();
        }

        public long Id { get; set; }
        public string PositionName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
