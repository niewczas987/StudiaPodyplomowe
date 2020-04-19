using System;
using System.Collections.Generic;

namespace ApiFilmowe.Modele
{
    public partial class Film
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
