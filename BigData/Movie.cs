using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigData
{
    public class Movie
    {
        public string? NameRU { get; set; }
        public string? NameUS { get; set; }
        public List<Actor> Actors = new();
        public List<Tag> Tags = new();
        public double? Rate { get;  set; }
        public string? imdbId { get; set; }
        [Key] public string movieId { get; set; }
        public Movie()
        {

        }
    }
}
