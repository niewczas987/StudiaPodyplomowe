using System;
using System.Collections.Generic;

namespace ApiFilmowe.Modele
{
    public partial class Klient
    {
        public long Id { get; set; }
        public string Nazwisko { get; set; }
        public string Imie { get; set; }
        public string Telefon { get; set; }
        public long AdresId { get; set; }
        public long? FilmId { get; set; }

        public virtual Adres Adres { get; set; }
        public virtual Film Film { get; set; }
    }
}
