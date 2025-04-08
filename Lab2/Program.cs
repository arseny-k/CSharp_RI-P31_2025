using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2ConsoleApp
{
    public class Genre
    {
        public int GenreId { get; set; } // Код жанру
        public string Name { get; set; } // Найменування
        public string Description { get; set; } // Опис
    }

    public class Film
    {
        public int FilmId { get; set; } // Код фільму
        public string Title { get; set; } // Найменування
        public int GenreId { get; set; } // Код жанру
        public TimeSpan Duration { get; set; } // Тривалість
        public string ProducerCompany { get; set; } // Фірма виробник
        public string Country { get; set; } // Країна виробник
        public bool IsDeleted { get; set; } = false;
    }

    public class Program
    {
        private static List<Genre> genres = new();
        private static List<Film> films = new();

        public static void Main(string[] args)
        {
            SeedGenres(); // Populate with initial data
            ShowMenu();
        }

        private static void SeedGenres()
        {
            genres.AddRange(new List<Genre>
            {
                new Genre { GenreId = 1, Name = "Action", Description = "Exciting, fast-paced films." },
                new Genre { GenreId = 2, Name = "Comedy", Description = "Humorous films." },
                new Genre { GenreId = 3, Name = "Drama", Description = "Serious, character-driven films." },
                new Genre { GenreId = 4, Name = "Horror", Description = "Scary, thrilling films." },
                new Genre { GenreId = 5, Name = "Fantasy", Description = "Magical, imaginative films." }
            });
        }

        private static void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Add Film");
                Console.WriteLine("2. Display All Films");
                Console.WriteLine("3. Display Film By ID");
                Console.WriteLine("4. Delete Film");
                Console.WriteLine("5. Display Active Films");
                Console.WriteLine("6. Display Deleted Films");
                Console.WriteLine("7. Exit");

                Console.Write("Select an option: ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1: AddFilm(); break;
                        case 2: DisplayAllFilms(); break;
                        case 3: DisplayFilmById(); break;
                        case 4: DeleteFilm(); break;
                        case 5: DisplayActiveFilms(); break;
                        case 6: DisplayDeletedFilms(); break;
                        case 7: return;
                        default: Console.WriteLine("Invalid choice."); break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Enter a number.");
                }
            }
        }

        private static void AddFilm()
        {
            Console.Write("Enter Film Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Genre ID: ");
            if (!int.TryParse(Console.ReadLine(), out int genreId) || !genres.Any(g => g.GenreId == genreId))
            {
                Console.WriteLine("Invalid Genre ID.");
                return;
            }

            Console.Write("Enter Duration (in minutes): ");
            if (!double.TryParse(Console.ReadLine(), out double minutes))
            {
                Console.WriteLine("Invalid duration.");
                return;
            }

            Console.Write("Enter Producer Company: ");
            string producer = Console.ReadLine();

            Console.Write("Enter Country: ");
            string country = Console.ReadLine();

            var film = new Film
            {
                FilmId = films.Count + 1,
                Title = title,
                GenreId = genreId,
                Duration = TimeSpan.FromMinutes(minutes),
                ProducerCompany = producer,
                Country = country
            };

            films.Add(film);
            Console.WriteLine("Film added successfully.");
        }

        private static void DisplayAllFilms()
        {
            Console.WriteLine("\nAll Films:");
            foreach (var film in films)
            {
                DisplayFilm(film);
            }
        }

        private static void DisplayFilmById()
        {
            Console.Write("Enter Film ID: ");
            if (int.TryParse(Console.ReadLine(), out int filmId))
            {
                var film = films.FirstOrDefault(f => f.FilmId == filmId);
                if (film != null) DisplayFilm(film);
                else Console.WriteLine("Film not found.");
            }
        }

        private static void DisplayFilm(Film film)
        {
            Console.WriteLine($"ID: {film.FilmId}, Title: {film.Title}, Genre ID: {film.GenreId}, Duration: {film.Duration}, Producer: {film.ProducerCompany}, Country: {film.Country}, Deleted: {film.IsDeleted}");
        }

        private static void DeleteFilm()
        {
            Console.Write("Enter Film ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int filmId))
            {
                var film = films.FirstOrDefault(f => f.FilmId == filmId);
                if (film != null)
                {
                    film.IsDeleted = true;
                    Console.WriteLine("Film marked as deleted.");
                }
                else Console.WriteLine("Film not found.");
            }
        }

        private static void DisplayActiveFilms()
        {
            Console.WriteLine("\nActive Films:");
            foreach (var film in films.Where(f => !f.IsDeleted))
            {
                DisplayFilm(film);
            }
        }

        private static void DisplayDeletedFilms()
        {
            Console.WriteLine("\nDeleted Films:");
            foreach (var film in films.Where(f => f.IsDeleted))
            {
                DisplayFilm(film);
            }
        }
    }
}
