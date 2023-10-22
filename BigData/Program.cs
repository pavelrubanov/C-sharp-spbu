using System;
using System.Collections.Concurrent;
using System.ComponentModel.Design;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace BigData
{
    internal class Program
    {
        Dictionary<Actor, HashSet<Movie>> actorsMovies = new();
        Dictionary<string, HashSet<Movie>> tagsMovies = new();
        Dictionary<string, Movie> moviesImdb = new();
        Dictionary<string, Movie> movies = new();
        Dictionary<string, Actor> actors = new();
        Dictionary<string, string> tags = new();
        public static void WriteFilm (Movie movie)
        {
            Console.WriteLine("\nНазвание RU: " + movie.NameRU);
            Console.WriteLine("Название US: " + movie.NameUS);
            Console.WriteLine("Рейтинг: " + movie.Rate);
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
        private void ReadMovieCodes_IMDB(BlockingCollection<string> lines)
        {
            string filePath = "C:\\Users\\USER\\source\\repos\\semester2\\BigData\\ml-latest\\MovieCodes_IMDB.tsv";
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
                lines.CompleteAdding();
            }
        }
        private void ProccesMovieCodes_IMDB(BlockingCollection<string> lines)
        {
            foreach(var line in lines.GetConsumingEnumerable())
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
        private void ReadActorsDirectorsNames_IMDB()
        {
            string filePath1 = "C:\\Users\\USER\\source\\repos\\semester2\\BigData\\ml-latest\\ActorsDirectorsNames_IMDB.txt";
            using (StreamReader reader = new StreamReader(filePath1))
            {
                string line;
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    Actor actor = new Actor();
                    actor.Id = line.Substring(0, 9);
                    int delimeter = line.IndexOf('\t', 10);
                    actor.Name = line.Substring(10, delimeter - 10);
                    actors[actor.Id] = actor;
                }
            }
            Console.WriteLine("t2");
        }
        private void ProcessActorsDirectorsNames_IMDB()
        private void ReadActorsDirectorsCodes_IMDB()
        {
            string filePath2 = "C:\\Users\\USER\\source\\repos\\semester2\\BigData\\ml-latest\\ActorsDirectorsCodes_IMDB.tsv";
            using (StreamReader reader = new StreamReader(filePath2))
            {
                string line;
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    string filmIMDBid = line.Substring(0, 9);
                    string actorId = line.Substring(12, 9);
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
            Console.WriteLine("t3");
        }
        private void ReadRatings_IMDB()
        {
            string filePath3 = "C:\\Users\\USER\\source\\repos\\semester2\\BigData\\ml-latest\\Ratings_IMDB.tsv";
            using (StreamReader reader = new StreamReader(filePath3))
            {
                string line;
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    string filmIMDBid = line.Substring(0, 9);
                    double rate = Convert.ToDouble(line.Substring(10, 3), CultureInfo.InvariantCulture);
                    if (moviesImdb.ContainsKey(filmIMDBid))
                    {
                        moviesImdb[filmIMDBid].Rate = rate;
                    }
                }
            }
            Console.WriteLine("t4");
        }
        private void Readlinks_IMDB_MovieLens()
        {
            string filePath4 = "C:\\Users\\USER\\source\\repos\\semester2\\BigData\\ml-latest\\links_IMDB_MovieLens.csv";
            using (StreamReader reader = new StreamReader(filePath4))
            {
                string line;
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    int delimeter = line.IndexOf(',');
                    string movieId = line.Substring(0, delimeter);
                    string imdbId = "tt" + line.Substring(delimeter + 1, 7);
                    if (moviesImdb.ContainsKey(imdbId))
                    {
                        moviesImdb[imdbId].movieId = movieId;
                        movies[movieId] = moviesImdb[imdbId];
                    }
                }
            }
            Console.WriteLine("t5");
        }
        private void ReadTagCodes_MovieLens()
        {
            string filePath5 = "C:\\Users\\USER\\source\\repos\\semester2\\BigData\\ml-latest\\TagCodes_MovieLens.csv";
            using (StreamReader reader = new StreamReader(filePath5))
            {
                string line;
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    int delimeter = line.IndexOf(',');
                    string tagId = line.Substring(0, delimeter);
                    string tag = line.Substring(delimeter + 1);
                    tags[tagId] = tag;
                }
            }
            Console.WriteLine("t6");
        }
        private void ReadTagScores_MovieLens()
        {
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
                Console.WriteLine("t7");
            }
        }
        
        
        private async void ReadInfo()
        {

            Task t1 = Task.Factory.StartNew(() => ReadMovieCodes_IMDB(), TaskCreationOptions.LongRunning);
            Task t2 = Task.Factory.StartNew(() => ReadActorsDirectorsNames_IMDB(), TaskCreationOptions.LongRunning);
            Task t6 = Task.Factory.StartNew(() => ReadTagCodes_MovieLens(), TaskCreationOptions.LongRunning);
            Task t4 = t1.ContinueWith(t => ReadRatings_IMDB()); 
            Task t5 = t1.ContinueWith(t => Readlinks_IMDB_MovieLens());
            Task t3 = Task.WhenAll(t1, t2).ContinueWith(_ => ReadActorsDirectorsCodes_IMDB());
            Task t7 = Task.WhenAll(t5, t6).ContinueWith(_ => ReadTagScores_MovieLens());
            
            
            Task.WaitAll(t1, t2, t3, t4, t5, t6, t7);            
        }
        private void Run()
        {
            ReadInfo();

            /*while (!true)
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
                                Movie movie = movies.AsParallel().Where(t => t.Value.NameRU == name || t.Value.NameUS == name)
                                    .Select(t => t.Value).First();
                                WriteFilm(movie);
                            }
                            catch (Exception)
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
                                Actor actor = actors.Where(t => t.Value.Name == name).Select(t => t.Value).First();
                                Console.WriteLine("Имя: " + actor.Name);
                                Console.WriteLine("Фильмы:");
                                foreach (var movie in actorsMovies[actor])
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
                                foreach (var film in tagsMovies[tag])
                                {
                                    WriteFilm(film);
                                }
                            }
                            catch (Exception)
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
            */
        }
        public static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }
    }
}