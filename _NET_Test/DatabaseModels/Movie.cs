using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _NET_Test.DatabaseModels
{
    public class Movie
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public List<Rating> Ratings { get; set; } = new List<Rating>();
        public List<Actor> Cast { get; set; } = new List<Actor>();
    }
}
