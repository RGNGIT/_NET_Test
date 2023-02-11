using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace _NET_Test.Classes
{
	public class User
	{
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

		public string? Username { get; set; }
		public string? Password { get; set; }
	}
}