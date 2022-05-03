using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaleMIS.Model
{
	[Serializable]
	public partial class Category
	{
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

	}
}

