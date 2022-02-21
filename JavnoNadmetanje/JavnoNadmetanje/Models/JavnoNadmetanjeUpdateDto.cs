using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    /// <summary>
    /// Model za azuriranje javnog nadmetanja
    /// </summary>
    public class JavnoNadmetanjeUpdateDto
    {
        /// <summary>
        /// ID javnog nadmetanja.
        /// </summary>
        public Guid JavnoNadmetanjeID { get; set; }

        /// <summary>
        /// Datum odrzavanja javnog nadmetanja.
        /// </summary>
        public DateTime Datum { get; set; }

        /// <summary>
        /// Vreme pocetka javnog nadmetanja.
        /// </summary>
        public DateTime VremePocetka { get; set; }

        /// <summary>
        /// Vreme kraja javnog nadmetanja.
        /// </summary>
        public DateTime VremeKraja { get; set; }

        /// <summary>
        /// Pocetna cena po hektaru.
        /// </summary>
        public int PocetnaCenaPoHektaru { get; set; }

        /// <summary>
        /// Izuzetost.
        /// </summary>
        public bool Izuzeto { get; set; }

        /// <summary>
        /// Tip javnog nadmetanja.
        /// </summary>
        public Guid TipJavnogNadmetanjaID { get; set; }

        /// <summary>
        /// Izlicitirana cena.
        /// </summary>
        public int IzlicitiranaCena { get; set; }

        /// <summary>
        /// Period zakupa.
        /// </summary>
        public int PeriodZakupa { get; set; }

        /// <summary>
        /// Broj ucesnika javnog nadmetanja.
        /// </summary>
        public int BrojUcesnika { get; set; }

        /// <summary>
        /// Visina dopune depozita.
        /// </summary>
        public int VisinaDopuneDepozita { get; set; }

        /// <summary>
        /// Krug javnog nadmetanja.
        /// </summary>
        public int Krug { get; set; }

        /// <summary>
        /// Status javnog nadmetanja.
        /// </summary>
        public Guid StatusNadmetanjaID { get; set; }

        /// <summary>
        /// ID adrese odrzavanja nadmetanja.
        /// </summary>
        public Guid AdresaID { get; set; }

        /// <summary>
        /// ID ovlascenog lica-licitanta.
        /// </summary>
        public Guid OvlascenoLiceID { get; set; }

        /// <summary>
        /// ID najboljeg ponudjaca.
        /// </summary>
        public Guid KupacID { get; set; }
    }
}
