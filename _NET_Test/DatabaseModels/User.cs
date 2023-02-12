using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace _NET_Test.DatabaseModels
{
	[Index("Username", IsUnique = true, Name = "Username")]
	public class User
	{
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		
		public string? Username { get; set; }
		public string? Password { get; set; }
		public DateTime DateOfReg { get; set; } = DateTime.Now;
	}
}