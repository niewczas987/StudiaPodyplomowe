using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAPI.Models
{
    public class Film
    {
        public Film()
        {
            Klient = new HashSet<Klient>();
        }

        public long Id { get; set; }
        public string Nazwa { get; set; }
        public long RokProdukcji { get; set; }
        public long RezyserId { get; set; }

        public virtual Rezyser Rezyser { get; set; }
        public virtual ICollection<Klient> Klient { get; set; }
    }
}
