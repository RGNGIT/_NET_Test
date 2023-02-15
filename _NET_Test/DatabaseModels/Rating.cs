using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using _NET_Test.Misc;

namespace _NET_Test.DatabaseModels
{
    public class Rating
    {
        [Key, Column(Order = 0)]
        [JsonConverter(typeof(IntToStringConverter))]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Range(0, 10)]
        public int Rate { get; set; }

        public string? Text { get; set; }
        public User user { get; set; } = null!;

        [NotMapped]
        public int ProductId { get; set; }
    }
}
