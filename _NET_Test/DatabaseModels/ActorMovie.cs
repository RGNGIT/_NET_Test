using _NET_Test.Misc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _NET_Test.DatabaseModels
{
    public class ActorMovie
    {
        [Key, Column(Order = 0)]
        [JsonConverter(typeof(IntToStringConverter))]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ActorId { get; set; }
        public Actor Actor { get; set; } = null!;
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
    }
}
