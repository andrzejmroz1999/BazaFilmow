using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazaFilmow
{
   public class Aktor
    {
        public int ActorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastUpdateOfActor { get; set; }

        internal static Aktor DodajDoListy(MySqlDataReader odczytanie)
        {
            return new Aktor
            {
                ActorID = int.Parse(odczytanie.GetString(1))
            };
        }
    }

}
