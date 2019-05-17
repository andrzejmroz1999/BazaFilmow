using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using MySql.Data.MySqlClient;


namespace BazaFilmow
{
    public partial class Baza : Window
    {
        List<Aktor> aktor = PobierzAktorow();

        List<Film> film = PobierzFilmy();

        List<FilmAktor> film_aktor = PobierzFilmIdAktorId(); //pobiera tabelę film_aktor z bazy danych do listy
        private static List<FilmAktor> PobierzFilmIdAktorId()
        {
            List<FilmAktor> filmy_aktorzy = new List<FilmAktor>();
            string konfiguracja = "data source=localhost;port=3306;username=root;password=Zizou130";
            MySqlConnection laczBaze = new MySqlConnection(konfiguracja);
            MySqlCommand zapytanie = new MySqlCommand("SELECT * FROM sakila.film_actor;", laczBaze);
            MySqlDataReader odczytanie;
            try
            {
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(zapytanie);
                laczBaze.Open();
                odczytanie = zapytanie.ExecuteReader();
                while (odczytanie.Read())
                {
                    FilmAktor film_aktor = new FilmAktor();
                    film_aktor.AktorId = int.Parse(odczytanie.GetString(0));
                    film_aktor.FilmId = int.Parse(odczytanie.GetString(1));                  
                    filmy_aktorzy.Add(film_aktor);
                }
            }
            catch (Exception komunikat)
            {
                MessageBox.Show(komunikat.Message);
            }
            return filmy_aktorzy;
        }
        private static List<Film> PobierzFilmy()
        {
            List<Film> filmy = new List<Film>();
            string konfiguracja = "data source=localhost;port=3306;username=root;password=Zizou130";
            MySqlConnection laczBaze = new MySqlConnection(konfiguracja);
            MySqlCommand zapytanie = new MySqlCommand("SELECT * FROM sakila.film;", laczBaze);
            MySqlDataReader odczytanie;
            try
            {
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(zapytanie);
                laczBaze.Open();
                odczytanie = zapytanie.ExecuteReader();
                while (odczytanie.Read())
                {
                    DateTime dt = new DateTime();
                    Film film = new Film();
                    film.FilmId= int.Parse(odczytanie.GetString(0));
                    film.Title = odczytanie.GetString(1);
                    film.Description = odczytanie.GetString(2);
                    film.ReleaseYear = int.Parse(odczytanie.GetString(3));
                    film.LanguageId = int.Parse(odczytanie.GetString(4));                  
                    film.OrginalLanguageId = 1;                                
                    film.RentalDuration = int.Parse(odczytanie.GetString(6));
                    film.RentalRate = double.Parse(odczytanie.GetString(7));
                    film.ReplacementCost = double.Parse(odczytanie.GetString(8));
                    film.Rating = odczytanie.GetString(9);
                    film.SpecialFeatures = odczytanie.GetString(10);
                    film.LastUpdateOfFilm = dt = Convert.ToDateTime("2006 - 02 - 15 05:03:42");
                    filmy.Add(film);
                }
               
            }
            catch (Exception komunikat)
            {
                MessageBox.Show(komunikat.Message);
            }

            return filmy;
        }
        public Baza()
        {
            InitializeComponent();
            PobierzAktorow();
            PobierzFilmy();
            PobierzFilmIdAktorId();             
        }
        public static List<Aktor> PobierzAktorow()
        {
            List<Aktor> aktorzy = new List<Aktor>();        
            string konfiguracja = "data source=localhost;port=3306;username=root;password=Zizou130";
            MySqlConnection laczBaze = new MySqlConnection(konfiguracja);
            MySqlCommand zapytanie = new MySqlCommand("SELECT * FROM sakila.actor;", laczBaze);
            MySqlDataReader odczytanie;           
            try
            {
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(zapytanie);
                laczBaze.Open();
                odczytanie = zapytanie.ExecuteReader();
                while (odczytanie.Read())
                {
                    DateTime dt = new DateTime();
                    Aktor aktor = new Aktor
                    {
                        ActorID = int.Parse(odczytanie.GetString(0)),
                        FirstName = odczytanie.GetString(1),
                        LastName = odczytanie.GetString(2),
                        LastUpdateOfActor = dt = Convert.ToDateTime(odczytanie.GetString(3))
                    };
                    aktorzy.Add(aktor);                   
                }
            }
            catch (Exception komunikat)
            {
                MessageBox.Show(komunikat.Message);
            }
            return aktorzy;
        }
        int i = 0; // zmienna pomocnicza do wyświetlania kontolek
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }    
        private void TabelaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (i % 2 == 0)
            {
                Tabela.Visibility = Visibility.Visible;         
            }
            else
            {
                Tabela.Visibility = Visibility.Hidden;              
            }
            i++;
        }
        private void AktorzyBtn_Click(object sender, RoutedEventArgs e)
        {
            WyswietlFimyBtn.Visibility = Visibility.Visible;
            WyswietlAktorowBtn.Visibility = Visibility.Collapsed;
            Tabela.Visibility = Visibility.Visible;
            WyswietlAktorow();
            LiczbaRekrdowTxt.Text = aktor.Count.ToString();
        }
        private void WyswietlAktorow()
        {
            string konfiguracja = "data source=localhost;port=3306;username=root;password=Zizou130";
            MySqlConnection laczBaze = new MySqlConnection(konfiguracja);
            MySqlCommand zapytanie = new MySqlCommand("SELECT * FROM sakila.actor;", laczBaze);
            try
            {
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(zapytanie);
                DataTable tabela = new DataTable();
                dataAdapter.Fill(tabela);
                Tabela.ItemsSource = tabela.DefaultView;
                dataAdapter.Update(tabela);
            }
            catch (Exception komunikat)
            {
                MessageBox.Show(komunikat.Message);
            }
        }  //pobiera dane bezposrednio z bazy danych    
        private void WyswietlFilmy()
        {
            string konfiguracja = "data source=localhost;port=3306;username=root;password=Zizou130";
            MySqlConnection laczBaze = new MySqlConnection(konfiguracja);
            MySqlCommand zapytanie = new MySqlCommand("SELECT * FROM sakila.film;", laczBaze);
            try
            {
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(zapytanie);
                DataTable tabela = new DataTable();
                dataAdapter.Fill(tabela);
                Tabela.ItemsSource = tabela.DefaultView;
                dataAdapter.Update(tabela);
            }
            catch (Exception komunikat)
            {
                MessageBox.Show(komunikat.Message);
            }
        }  //pobiera dane bezposrednio z bazy danych
        private void OpcjeBtn_Click(object sender, RoutedEventArgs e)
        {
            Tabela.ItemsSource = film_aktor;
            Tabela.Visibility = Visibility.Visible;          
        }
        private void SzukajAktoraBtn_Click(object sender, RoutedEventArgs e)
        {
            WyswietlFimyBtn.Visibility = Visibility.Visible;
            WyswietlAktorowBtn.Visibility = Visibility.Collapsed;
            Tabela.Visibility = Visibility.Visible;
            Tabela.ItemsSource = aktor.Where(x => (x.FirstName.ToLower().StartsWith(SzukajTxt.Text.ToLower())) || x.LastName.ToLower().StartsWith(SzukajTxt.Text.ToLower())).ToList();
            LiczbaRekrdowTxt.Text = aktor.Where(x => (x.FirstName.ToLower().StartsWith(SzukajTxt.Text.ToLower())) || x.LastName.ToLower().StartsWith(SzukajTxt.Text.ToLower())).Count().ToString();
        }
        private void SzukajFilmBtn_Click(object sender, RoutedEventArgs e)
        {
            WyswietlAktorowBtn.Visibility = Visibility.Visible;
            WyswietlFimyBtn.Visibility = Visibility.Collapsed;
            Tabela.Visibility = Visibility.Visible;
            Tabela.ItemsSource = film.Where(x => (x.Title.ToLower().StartsWith(SzukajTxt.Text.ToLower()))).ToList();
            LiczbaRekrdowTxt.Text = film.Where(x => (x.Title.ToLower().StartsWith(SzukajTxt.Text.ToLower()))).Count().ToString();
        }
        private void WyswietlAktorowBtn_Click(object sender, RoutedEventArgs e)
        {
            WyswietlFimyBtn.Visibility = Visibility.Visible;
            WyswietlAktorowBtn.Visibility = Visibility.Collapsed;

            Aktor actor = new Aktor();
            List<Aktor> actors = new List<Aktor>();
            List<FilmAktor> FM = new List<FilmAktor>();
            int filmID = 0;           
            try
            {
               filmID = Tabela.SelectedIndex+1;
               FM = (film_aktor.Where(x => x.FilmId == filmID)).ToList();
                foreach (var item in FM)
                {                   
                    foreach (var i in aktor.Where(x => x.ActorID == item.AktorId).ToList())
                    {
                        actors.Add(i);
                    }
                }       
                Tabela.ItemsSource = actors;
                LiczbaRekrdowTxt.Text = actors.Count.ToString();
                AktualnyRekordTxt.Text = (Tabela.SelectedIndex + 1).ToString();
            }
            catch (Exception komunikat)
            {
                MessageBox.Show(komunikat.Message);
            }
        }
        private void WyswietlFimyBtn_Click(object sender, RoutedEventArgs e)
        {
            WyswietlFimyBtn.Visibility = Visibility.Collapsed;
            WyswietlAktorowBtn.Visibility = Visibility.Visible;
            Film f = new Film();
            List<Film> films = new List<Film>();
            List<FilmAktor> FM = new List<FilmAktor>();
            int AktorID = 0;
            try
            {
                AktorID = Tabela.SelectedIndex + 1;
                FM = (film_aktor.Where(x => x.AktorId == AktorID)).ToList();
                foreach (var item in FM)
                {
                    foreach (var i in film.Where(x => x.FilmId == item.FilmId).ToList())
                    {
                        films.Add(i);
                    }
                }
                Tabela.ItemsSource = films;
                LiczbaRekrdowTxt.Text = films.Count.ToString();
                AktualnyRekordTxt.Text = (Tabela.SelectedIndex + 1).ToString();
            }
            catch (Exception komunikat)
            {
                MessageBox.Show(komunikat.Message);
            }
        }
        private void FlimyBtn_Click(object sender, RoutedEventArgs e)
        {
            WyswietlAktorowBtn.Visibility = Visibility.Visible;
            WyswietlFimyBtn.Visibility = Visibility.Collapsed;
            Tabela.Visibility = Visibility.Visible;
            WyswietlFilmy();
            LiczbaRekrdowTxt.Text = film.Count.ToString();
            AktualnyRekordTxt.Text = (Tabela.SelectedIndex + 1).ToString();
        }
        private void Info_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                AktualnyRekordTxt.Text = (Tabela.SelectedIndex + 1).ToString();
            }
            catch (Exception komunikat)
            {
                MessageBox.Show(komunikat.Message);
            }
        }
        private void OpisFilmuBtn_Click(object sender, RoutedEventArgs e)
        {
           List<Film> f = new  List<Film>();
            Tabela.Visibility = Visibility.Collapsed;
            f = film.Where(x => x.FilmId == (Tabela.SelectedIndex + 1)).ToList();
            foreach (var item in f)
            {
                OpisFilmuTxt.Text = "Opis filmu: " + item.Description.ToString();
                TytulTxt.Text = "Tytuł filmu: " + item.Title.ToString();
            }
            TytulTxt.IsReadOnly = true;
            OpisFilmuTxt.IsReadOnly = true;
        }
    }
   
 
}