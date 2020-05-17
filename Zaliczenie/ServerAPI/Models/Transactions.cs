using System;
using System.Collections.Generic;

namespace ServerAPI.Models
{
    public partial class Transactions
    {
        public long Id { get; set; }
        public DateTime? TimeStamp { get; set; }
        public long? IdItem { get; set; }
        public decimal? UnitPrice { get; set; }
        public long? Amount { get; set; }
        public decimal? TotalPrice { get; set; }

        public virtual Item IdItemNavigation { get; set; }
    }
}
