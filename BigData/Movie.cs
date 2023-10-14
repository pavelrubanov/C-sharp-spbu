using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigData
{
    internal class Movie
    {
        public string NameRU { get; set; }
        public string NameUS { get; set; }
        public HashSet<Actor> Actors = new();
        public string Producer { get; set; }
        public HashSet<string> Tags = new();
        public int numVotes { get; set; }
        public double Rate { get;  set; }
        public string imdbId { get; set; }
        public string movieId { get; set; }
        public Movie()
        {

        }
    }
}
