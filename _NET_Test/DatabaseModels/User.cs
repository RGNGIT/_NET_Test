using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using _NET_Test.Misc;

namespace _NET_Test.DatabaseModels
{
    [Index("Username", IsUnique = true, Name = "Username")]
	public class User
	{
        
        [Key, Column(Order = 0)]
        [JsonConverter(typeof(IntToStringConverter))]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		
		public string? Username { get; set; }

        [JsonIgnore]
        public string? Password { get; set; }
		[JsonIgnore]
		public DateTime DateOfReg { get; set; } = DateTime.Now;
	}

	public class AuthUser
	{
        [JsonConverter(typeof(IntToStringConverter))]
        public int Id { get; set; }

		public string Username { get; set; } = null!;
    }
}