using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using _NET_Test.Misc;

namespace _NET_Test.DatabaseModels
{
    public class Actor: IComparable<Actor>
    {

        public int CompareTo(Actor otherActor) => this.Ratings.Count > otherActor.Ratings.Count ? -1 : 1;

        [Key, Column(Order = 0)]
        [JsonConverter(typeof(IntToStringConverter))]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;

        public List<Rating> Ratings { get; set; } = new List<Rating>();

        [JsonIgnore]
        public List<ActorMovie> Movies { get; set; } = null!;
    }
}
