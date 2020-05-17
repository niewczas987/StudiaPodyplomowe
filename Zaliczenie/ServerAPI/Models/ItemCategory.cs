using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ServerAPI.Models
{
    public partial class ItemCategory
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
