using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAPI.Models
{
    public class Rezyser
    {
        public Rezyser()
        {
            Film = new HashSet<Film>();
        }

        public long Id { get; set; }
        public string Nazwisko { get; set; }
        public string Imie { get; set; }
        public string KrajPochodzenia { get; set; }
        [JsonIgnore]
        public virtual ICollection<Film> Film { get; set; }
    }
}
