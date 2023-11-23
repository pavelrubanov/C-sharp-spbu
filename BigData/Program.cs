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
        Dictionary<Tag, HashSet<Movie>> tagsMovies = new();
        Dictionary<string, Movie> moviesImdb = new();
        Dictionary<string, Movie> movies = new();
        Dictionary<string, Actor> actors = new();
        Dictionary<string, Tag> tags = new();
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
            string filePath = "..\\..\\..\\ml-latest\\MovieCodes_IMDB.tsv"; 
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
            Console.WriteLine("t1");
        }
        private void ProcessMovieCodes_IMDB(BlockingCollection<string> lines)
        {
            foreach (var line in lines.GetConsumingEnumerable())
            {
                if (line.Contains("US") || line.Contains("RU"))
                {
                    string[] fields = line.Split('\t');
                    string imdbId = fields[0];

                    if (fields[3] == "RU" || fields[4] == "RU")
                    {

                        if (moviesImdb.ContainsKey(imdbId))
                        {
                            lock(moviesImdb)
                            {
                                moviesImdb[imdbId].NameRU = fields[2];
                            }
                        }
                        else
                        {
                            Movie movie = new Movie();
                            movie.NameRU = fields[2];
                            movie.imdbId = fields[0];
                            lock(moviesImdb)
                            {
                                moviesImdb[movie.imdbId] = movie;
                            }
                        }
                    }
                    if (fields[3] == "US" || fields[4] == "US")
                    {
                        if (moviesImdb.ContainsKey(imdbId))
                        {
                            lock(moviesImdb)
                            {
                                moviesImdb[imdbId].NameUS = fields[2];
                            }
                            
                        }
                        else
                        {
                            Movie movie = new Movie();
                            movie.NameUS = fields[2];
                            movie.imdbId = fields[0];
                            lock(moviesImdb)
                            {
                                moviesImdb[movie.imdbId] = movie;
                            }
                            
                        }
                    }
                }
            }
            Console.WriteLine("t1 procces done");
        }

        private void ReadActorsDirectorsNames_IMDB(BlockingCollection<string> lines)
        {
            string filePath = "..\\..\\..\\ml-latest\\ActorsDirectorsNames_IMDB.txt";
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
            Console.WriteLine("t2");
        }
        private void ProcessActorsDirectorsNames_IMDB(BlockingCollection<string> lines)
        {
            foreach (var line in lines.GetConsumingEnumerable())
            {
                Actor actor = new Actor();

                var lineSpan = line.AsSpan();

                var index = lineSpan.IndexOf('\t');
                actor.Id = lineSpan.Slice(0, index).ToString();
                lineSpan = lineSpan.Slice(index + 1);

                index = lineSpan.IndexOf('\t');
                actor.Name = lineSpan.Slice(0, index).ToString();

                lock(actors)
                {
                    actors[actor.Id] = actor;
                }                
            }
            Console.WriteLine("t2 procces done");

        }
        private void ReadActorsDirectorsCodes_IMDB(BlockingCollection<string> lines)
        {
            string filePath = "..\\..\\..\\ml-latest\\ActorsDirectorsCodes_IMDB.tsv";
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
            Console.WriteLine("t3");
        }
        private void ProcessActorsDirectorsCodes_IMDB(BlockingCollection<string> lines)
        {
            foreach (var line in lines.GetConsumingEnumerable())
            {
                var lineSpan = line.AsSpan();
                var index = lineSpan.IndexOf('\t');
                string filmIMDBid = lineSpan.Slice(0, index).ToString();
                lineSpan = lineSpan.Slice(index + 1);

                index = lineSpan.IndexOf("\t");
                lineSpan = lineSpan.Slice(index + 1);

                index = lineSpan.IndexOf("\t");
                string actorId = lineSpan.Slice(0, index).ToString();

                if (moviesImdb.ContainsKey(filmIMDBid) && actors.ContainsKey(actorId))
                {
                    lock (moviesImdb)
                    {
                        moviesImdb[filmIMDBid].Actors.Add(actors[actorId]);
                    }
                    
                    if (!actorsMovies.ContainsKey(actors[actorId]))
                    {
                        lock (actorsMovies)
                        {
                            actorsMovies[actors[actorId]] = new();
                        }                        
                    }
                    lock(actorsMovies)
                    {
                        actorsMovies[actors[actorId]].Add(moviesImdb[filmIMDBid]);
                    }                    
                }
            }
            Console.WriteLine("t3 procces done");
        }

        private void ReadRatings_IMDB(BlockingCollection<string> lines)
        {
            string filePath = "..\\..\\..\\ml-latest\\Ratings_IMDB.tsv";
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
            Console.WriteLine("t4");
        }
        private void ProcessRatings_IMDB(BlockingCollection<string> lines)
        {
            foreach (var line in lines.GetConsumingEnumerable())
            {
                var lineSpan = line.AsSpan();

                int index = lineSpan.IndexOf('\t');
                string filmIMDBid = lineSpan.Slice(0, index).ToString();

                lineSpan = lineSpan.Slice(index + 1);
                index = lineSpan.IndexOf('\t');
                double rate = Convert.ToDouble(lineSpan.Slice(0, index).ToString(), CultureInfo.InvariantCulture);

                if (moviesImdb.ContainsKey(filmIMDBid))
                {
                    lock(moviesImdb)
                    {
                        moviesImdb[filmIMDBid].Rate = rate;
                    }
                    
                }
            }
            Console.WriteLine("t4 procces done");

        }
        private void Readlinks_IMDB_MovieLens(BlockingCollection<string> lines)
        {
            string filePath = "..\\..\\..\\ml-latest\\links_IMDB_MovieLens.csv";
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
            Console.WriteLine("t5");
        }
        private void Processlinks_IMDB_MovieLens(BlockingCollection<string> lines)
        {
            foreach(var line in lines.GetConsumingEnumerable())
            {
                var lineSpan = line.AsSpan();

                int index = lineSpan.IndexOf(',');
                string movieId = lineSpan.Slice(0, index).ToString();

                lineSpan = lineSpan.Slice(index + 1);
                index = lineSpan.IndexOf(',');
                string imdbId = "tt" + lineSpan.Slice(0, index).ToString();


                if (moviesImdb.ContainsKey(imdbId))
                {
                    lock (moviesImdb[imdbId])
                    {
                        moviesImdb[imdbId].movieId = movieId;
                    }
                    lock (movies)
                    {
                        movies[movieId] = moviesImdb[imdbId];
                    }
                }
            }
            Console.WriteLine("t5 procces done");
        }
        private void ReadTagCodes_MovieLens(BlockingCollection<string> lines)
        {
            string filePath = "..\\..\\..\\ml-latest\\TagCodes_MovieLens.csv";
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
            Console.WriteLine("t6");
        }
        private void ProcessTagCodes_MovieLens(BlockingCollection<string> lines)
        {
            foreach(var line in lines.GetConsumingEnumerable())
            {
                var lineSpan = line.AsSpan();

                int index = lineSpan.IndexOf(',');
                string tagId = lineSpan.Slice(0, index).ToString();
                lineSpan = lineSpan.Slice(index + 1);

                string tagName = lineSpan.ToString();
                
                lock(tags)
                {
                    tags[tagId] = new Tag(tagName, tagId);
                }
            }
            Console.WriteLine("t6 procces done");
        }
        private void ReadTagScores_MovieLens(BlockingCollection<string> lines)
        {
            string filePath = "..\\..\\..\\ml-latest\\TagScores_MovieLens.csv";
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
                lines.CompleteAdding();
                Console.WriteLine("t7");
            }
        }
        private void ProcessTagScores_MovieLens(BlockingCollection<string> lines)
        {
            foreach(var line in lines.GetConsumingEnumerable())
            {
                string[] fields = line.Split(',');
                string movieId = fields[0];
                string tagId = fields[1];
                string relevanceString = fields[2];
                if (relevanceString.Length > 4)
                {
                    relevanceString = relevanceString.Substring(0, 4);
                }
                double relevance = Convert.ToDouble(relevanceString, CultureInfo.InvariantCulture);
                if (relevance > 0.5 && movies.ContainsKey(movieId) && tags.ContainsKey(tagId))
                {
                    lock(movies)
                    {
                        movies[movieId].Tags.Add(tags[tagId]);
                    }
                    
                    if (!tagsMovies.ContainsKey(tags[tagId]))
                    {
                        lock(tagsMovies)
                        {
                            tagsMovies[tags[tagId]] = new();
                        }
                        
                    }

                    lock(tagsMovies)
                    {
                        tagsMovies[tags[tagId]].Add(movies[movieId]);
                    }
                }
            }
            Console.WriteLine("t7 procces done");
        }

        private void ReadInfo()
        {
            BlockingCollection<string> MovieCodes_IMDBlines = new();
            BlockingCollection<string> ActorsDirectorsNames_IMDBlines = new();
            BlockingCollection<string> ActorsDirectorsCodes_IMDBlines = new();
            BlockingCollection<string> Ratings_IMDBlines = new();
            BlockingCollection<string> links_IMDB_MovieLenslines = new();
            BlockingCollection<string> TagCodes_MovieLenslines = new();
            BlockingCollection<string> TagScores_MovieLenslines = new();

            Task t1 = Task.Factory.StartNew
                (() => ReadMovieCodes_IMDB(MovieCodes_IMDBlines));
            Task t2 = Task.Factory.StartNew
                (() => ReadActorsDirectorsNames_IMDB(ActorsDirectorsNames_IMDBlines), TaskCreationOptions.LongRunning);
            Task t6 = Task.Factory.StartNew(
                () => ReadTagCodes_MovieLens(TagCodes_MovieLenslines));
            Task t4 = Task.Factory.StartNew(() => ReadRatings_IMDB(Ratings_IMDBlines));
            Task t5 = Task.Factory.StartNew(() => Readlinks_IMDB_MovieLens(links_IMDB_MovieLenslines));
            Task t3 = Task.Factory.StartNew(() => ReadActorsDirectorsCodes_IMDB(ActorsDirectorsCodes_IMDBlines), TaskCreationOptions.LongRunning);
            Task t7 = Task.Factory.StartNew(() => ReadTagScores_MovieLens(TagScores_MovieLenslines), TaskCreationOptions.LongRunning);

            int n = Environment.ProcessorCount;

            List<Task> t6Processes = new();
            for (int i = 0; i < 1; i++)
            {
                t6Processes.Add(Task.Factory.StartNew(
                () => ProcessTagCodes_MovieLens(TagCodes_MovieLenslines)));
            }

            List<Task> t1Processes = new();
            for (int i = 0; i < n; i++)
            {
                t1Processes.Add(Task.Factory.StartNew
                (() => ProcessMovieCodes_IMDB(MovieCodes_IMDBlines)));
            }

            List<Task> t4Processes = new();
            for (int i = 0; i < n; i++)
            {
                t4Processes.Add(Task.WhenAll(t1Processes).ContinueWith(_ => ProcessRatings_IMDB(Ratings_IMDBlines)));
            }

            List<Task> t5Processes = new();
            for (int i = 0; i < n; i++)
            {
                t5Processes.Add(Task.WhenAll(t1Processes).ContinueWith(_ => Processlinks_IMDB_MovieLens(links_IMDB_MovieLenslines)));
            }

            List<Task> t2Processes = new();
            for (int i = 0; i < n; i++)
            {
                t2Processes.Add(Task.Factory.StartNew(
                () => ProcessActorsDirectorsNames_IMDB(ActorsDirectorsNames_IMDBlines)));
            }

            List<Task> t3Processes = new();
            for (int i = 0; i < n; i++)
            {
                t3Processes.Add(Task.WhenAll(t2Processes.Concat(t1Processes)).ContinueWith(_ => ProcessActorsDirectorsCodes_IMDB(ActorsDirectorsCodes_IMDBlines)));
            }

            List<Task> t7Processes = new();
            for (int i = 0; i < n; i++)
            {
                t7Processes.Add(Task.WhenAll(t5Processes.Concat(t6Processes)).ContinueWith(_ => ProcessTagScores_MovieLens(TagScores_MovieLenslines)));
            }

            Task.WaitAll(t1Processes.ToArray());
            Task.WaitAll(t2Processes.ToArray());
            Task.WaitAll(t3Processes.ToArray());
            Task.WaitAll(t4Processes.ToArray());
            Task.WaitAll(t5Processes.ToArray());
            Task.WaitAll(t6Processes.ToArray());
            Task.WaitAll(t7Processes.ToArray());
            Task.WaitAll(t1, t2, t3, t4, t5, t6, t7);

            Console.Write("end reading and processing");
        }

        private void Run()
        {
            ReadInfo();

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
                            string tagName = Console.ReadLine();
                            Tag? tag = tags.Where(t => t.Value.Name == tagName).Select(t => t.Value).FirstOrDefault();
                            if (tag != null)
                            {
                                foreach (var film in tagsMovies[tag])
                                {
                                    WriteFilm(film);
                                }
                            }
                            else
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
        public static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }
    }
}