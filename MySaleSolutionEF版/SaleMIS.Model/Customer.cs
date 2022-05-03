using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaleMIS.Model
{
	[Serializable]
	public partial class Customer
	{
        [Key]
        public int CustomerId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string Contact { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        public string Email { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        [MaxLength(6)]
        public string ZipCode { get; set; }

	}
}

