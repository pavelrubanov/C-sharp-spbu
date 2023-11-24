using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigData
{
    internal class Parser
    {
        private Dictionary<string, Movie> moviesImdb = new();
        public Dictionary<string, Movie> movies { get; private set; } = new();
        public Dictionary<string, Actor> actors { get; private set; } = new();
        public Dictionary<string, Tag> tags { get; private set; } = new();
        
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
                            lock (moviesImdb)
                            {
                                moviesImdb[imdbId].NameRU = fields[2];
                            }
                        }
                        else
                        {
                            Movie movie = new Movie();
                            movie.NameRU = fields[2];
                            movie.imdbId = fields[0];
                            lock (moviesImdb)
                            {
                                moviesImdb[movie.imdbId] = movie;
                            }
                        }
                    }
                    if (fields[3] == "US" || fields[4] == "US")
                    {
                        if (moviesImdb.ContainsKey(imdbId))
                        {
                            lock (moviesImdb)
                            {
                                moviesImdb[imdbId].NameUS = fields[2];
                            }

                        }
                        else
                        {
                            Movie movie = new Movie();
                            movie.NameUS = fields[2];
                            movie.imdbId = fields[0];
                            lock (moviesImdb)
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

                lock (actors)
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
                    lock (moviesImdb)
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
            foreach (var line in lines.GetConsumingEnumerable())
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
            foreach (var line in lines.GetConsumingEnumerable())
            {
                var lineSpan = line.AsSpan();

                int index = lineSpan.IndexOf(',');
                string tagId = lineSpan.Slice(0, index).ToString();
                lineSpan = lineSpan.Slice(index + 1);

                string tagName = lineSpan.ToString();

                lock (tags)
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
            foreach (var line in lines.GetConsumingEnumerable())
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
                    lock (movies)
                    {
                        movies[movieId].Tags.Add(tags[tagId]);
                    }
                }
            }
            Console.WriteLine("t7 procces done");
        }

        public void ReadInfo()
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

    }
}
