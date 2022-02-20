using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Entities
{
    /// <summary>
    /// Entitet javnog nadmetanja
    /// </summary>
    public class JavnoNadmetanje
    {
        /// <summary>
        /// ID javnog nadmetanja.
        /// </summary>
        [Key]
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
        [ForeignKey("TipJavnogNadmetanja")]
        public Guid TipJavnogNadmetanjaID { get; set; }
        public TipJavnogNadmetanja TipJavnogNadmetanja { get; set; }

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
        [ForeignKey("StatusNadmetanja")]
        public Guid StatusNadmetanjaID { get; set; }
        public StatusNadmetanja StatusNadmetanja { get; set; }

        /// <summary>
        /// ID adrese odrzavanja nadmetanja.
        /// </summary>
        public Guid AdresaID { get; set; }

        /// <summary>
        /// ID ovlascenog lica-licitanta.
        /// </summary>
        public Guid OvlascenoLiceID { get; set; }

        /// <summary>
        /// Lista ID-jeva parcela.
        /// </summary>
        [NotMapped]
        public List<Guid> ParceleID { get; set; }

        /// <summary>
        /// ID najboljeg ponudjaca.
        /// </summary>
        public Guid KupacID { get; set; }

        /// <summary>
        /// Lista ID-jeva prijavljenih kupaca.
        /// </summary>
        [NotMapped]
        public List<Guid> KupciID { get; set; }
    }
}
