using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAPI.Models
{
    public class ItemCategory
    {
        public ItemCategory()
        {
            Item = new HashSet<Item>();
        }

        public long Id { get; set; }
        public string CategoryName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Item> Item { get; set; }
    }
}
