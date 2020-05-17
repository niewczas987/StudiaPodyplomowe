using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAPI.Models
{
    public class ItemType
    {
        public ItemType()
        {
            Item = new HashSet<Item>();
        }

        public long Id { get; set; }
        public string TypeName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Item> Item { get; set; }
    }
}
