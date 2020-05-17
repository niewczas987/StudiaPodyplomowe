using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ServerAPI.Models
{
    public partial class ItemType
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
