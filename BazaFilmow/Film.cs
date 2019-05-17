using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazaFilmow
{
   public class Film
    {
        public int FilmId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public int LanguageId { get; set; }
        public int OrginalLanguageId { get; set; }
        public int RentalDuration { get; set; }
        public double RentalRate { get; set; }
        public double ReplacementCost { get; set; }
        public string Rating { get; set; }
        public string SpecialFeatures { get; set; }
        public DateTime LastUpdateOfFilm { get; set; }

    }
}
