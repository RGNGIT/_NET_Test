using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using _NET_Test.Misc;

namespace _NET_Test.DatabaseModels
{
    public class Movie: IComparable<Movie>
    {

        public int CompareTo(Movie otherMovie) => this.Ratings.Count > otherMovie.Ratings.Count ? -1 : 1;

        [Key, Column(Order = 0)]
        [JsonConverter(typeof(IntToStringConverter))]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public List<Rating> Ratings { get; set; } = new List<Rating>();

        [JsonIgnore]
        public List<ActorMovie> Actors { get; set; } = null!;
    }
}
