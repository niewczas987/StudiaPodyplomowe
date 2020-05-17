using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAPI.Models
{
    public class Item
    {
        public Item()
        {
            Transactions = new HashSet<Transactions>();
        }

        public long Id { get; set; }
        public string ItemName { get; set; }
        public decimal? ItemPrice { get; set; }
        public long? ItemCategory { get; set; }
        public long? ItemType { get; set; }

        public virtual ItemCategory ItemCategoryNavigation { get; set; }
        public virtual ItemType ItemTypeNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}
