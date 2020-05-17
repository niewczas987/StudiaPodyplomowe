using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ServerAPI.Models
{
    public partial class EmployeePosition
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
