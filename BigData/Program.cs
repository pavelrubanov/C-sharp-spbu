using System;
using System.ComponentModel.Design;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;

namespace BigData
{
    internal class Program
    {
        Dictionary<Actor, HashSet<Movie> > actorsMovies = new();
        Dictionary<string, HashSet<Movie>> tagsMovies = new();
        Dictionary<string, Movie> moviesImdb = new();
        Dictionary<string, Movie> movies = new();
        Dictionary<string, Actor> actors = new();
        Dictionary<string, string> tags = new();
        public static void WriteFilm (Movie movie)
        {
            Console.WriteLine("\nНазвание RU: " + movie.NameRU);
            Console.WriteLine("Название US: " + movie.NameUS);
            Console.WriteLine("Актеры:");
            foreach (var actor in movie.Actors)
            {
                Console.Write(actor.Name + ", ");
            }
            Console.WriteLine("\nТэги:");
            foreach(var tag in movie.Tags)
            {
                Console.Write(tag + ", ");
            }
            Console.WriteLine();
        }

        private void ReadInfo()
        {
            string filePath = "C:\\Users\\USER\\source\\repos\\semester2\\BigData\\ml-latest\\MovieCodes_IMDB.tsv";
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    
                    string[] fields = line.Split('\t');
                    if (fields[3] == "RU" || fields[4] == "RU")
                    {
                        string imdbId = fields[0];
                        if (moviesImdb.ContainsKey(imdbId))
                        {
                            moviesImdb[imdbId].NameRU = fields[2];
                        }
                        else
                        {
                            Movie movie = new Movie();
                            movie.NameRU = fields[2];
                            movie.imdbId = fields[0];
                            moviesImdb[movie.imdbId] = movie;
                        }
                    }
                    if (fields[3] == "US" || fields[4] == "US")
                    {
                        string imdbId = fields[0];
                        if (moviesImdb.ContainsKey(imdbId))
                        {
                            moviesImdb[imdbId].NameUS = fields[2];
                        }
                        else
                        {
                            Movie movie = new Movie();
                            movie.NameUS = fields[2];
                            movie.imdbId = fields[0];
                            moviesImdb[movie.imdbId] = movie;
                        }
                    }
                }
            }

            string filePath1 = "C:\\Users\\USER\\source\\repos\\semester2\\BigData\\ml-latest\\ActorsDirectorsNames_IMDB.txt";
            using (StreamReader reader = new StreamReader(filePath1))
            {
                string line;
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null )
                {
                    string[] fields = line.Split('\t');
                    Actor actor = new Actor();
                    actor.Id = fields[0];
                    actor.Name = fields[1];
                    actors[actor.Id] = actor;
                }
            }

            string filePath2 = "C:\\Users\\USER\\source\\repos\\semester2\\BigData\\ml-latest\\ActorsDirectorsCodes_IMDB.tsv";
            using (StreamReader reader = new StreamReader(filePath2))
            {
                string line;
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split('\t');
                    string filmIMDBid = fields[0];
                    string actorId = fields[2];
                    if (moviesImdb.ContainsKey(filmIMDBid) && actors.ContainsKey(actorId))
                    {
                        moviesImdb[filmIMDBid].Actors.Add(actors[actorId]);
                        if (!actorsMovies.ContainsKey(actors[actorId]))
                        {
                            actorsMovies[actors[actorId]] = new();
                        }
                        actorsMovies[actors[actorId]].Add(moviesImdb[filmIMDBid]);
                    }
                    
                }
            }

            string filePath3 = "C:\\Users\\USER\\source\\repos\\semester2\\BigData\\ml-latest\\Ratings_IMDB.tsv";
            using (StreamReader reader = new StreamReader(filePath3))
            {
                string line;
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split('\t');
                    string filmIMDBid = fields[0];
                    string rate = fields[1];
                    int numVotes = int.Parse(fields[2]);
                    if (moviesImdb.ContainsKey(filmIMDBid))
                    {
                        moviesImdb[filmIMDBid].Rate = rate;
                        moviesImdb[filmIMDBid].numVotes = numVotes;
                    }
                }
            }

            string filePath4 = "C:\\Users\\USER\\source\\repos\\semester2\\BigData\\ml-latest\\links_IMDB_MovieLens.csv";
            using (StreamReader reader = new StreamReader(filePath4))
            {
                string line;
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');
                    string movieId = fields[0];
                    string imdbId = "tt" + fields[1];
                    if (moviesImdb.ContainsKey(imdbId))
                    {
                        moviesImdb[imdbId].movieId = movieId;
                        movies[movieId] = moviesImdb[imdbId];
                    } 
                }
            }

            string filePath5 = "C:\\Users\\USER\\source\\repos\\semester2\\BigData\\ml-latest\\TagCodes_MovieLens.csv";
            using (StreamReader reader = new StreamReader(filePath5))
            {
                string line;
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');
                    string tagId = fields[0];
                    string tag = fields[1];
                    tags[tagId] = tag;
                }
            }

            string filePath6 = "C:\\Users\\USER\\source\\repos\\semester2\\BigData\\ml-latest\\TagScores_MovieLens.csv";
            using (StreamReader reader = new StreamReader(filePath6))
            {
                string line;
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');
                    string movieId = fields[0];
                    string tagId = fields[1];
                    double relevance = Convert.ToDouble(fields[2], CultureInfo.InvariantCulture);
                    if (relevance > 0.5 && movies.ContainsKey(movieId) && tags.ContainsKey(tagId)) 
                    {
                        movies[movieId].Tags.Add(tags[tagId]);
                        if (!tagsMovies.ContainsKey(tags[tagId]))
                        {
                            tagsMovies[tags[tagId]] = new();
                        }
                        tagsMovies[tags[tagId]].Add(movies[movieId]);
                    }
                }
            }
        }
        public static void Main(string[] args)
        {
            Program p = new Program();
            p.ReadInfo();
            while (true)
            {
                Console.WriteLine(
                    "\n\nКакую информацию вы хотите получить? \n" +
                    "1) О фильме \n" +
                    "2) Об актере-режиссере \n" +
                    "3) О конкретном тэге");
               switch (Console.ReadLine())
                {
                    case "1":
                        {
                            Console.WriteLine("Введите название фильма");
                            string name = Console.ReadLine();
                            try
                            {
                                Movie movie = p.movies.Where(t => t.Value.NameRU == name || t.Value.NameUS == name)
                                    .Select(t => t.Value).First();
                                WriteFilm(movie);
                            }
                            catch(Exception)
                            {
                                Console.WriteLine("Ничего не найдено");
                            }
                            break;
                        }
                    case "2":
                        {
                            Console.WriteLine("Введите имя актера");
                            string name = Console.ReadLine();
                            try
                            {
                                Actor actor = p.actors.Where(t => t.Value.Name == name).Select(t => t.Value).First();
                                Console.WriteLine("Имя: " + actor.Name);
                                Console.WriteLine("Фильмы:");
                                foreach (var movie in p.actorsMovies[actor])
                                {
                                    WriteFilm(movie);
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Ничего не найдено");
                            }
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("Введите тэг");
                            string tag = Console.ReadLine();
                            try
                            {
                                foreach (var film in p.tagsMovies[tag])
                                {
                                    WriteFilm(film);
                                }
                            }
                            catch(Exception)
                            {
                                Console.WriteLine("Ничего не найдено");
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Неверный ввод");
                            break;
                        }
                }
            }
        }
    }
}