using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaleMIS.Model
{
    [Serializable]
    public partial class Order
    {
        [Key,MaxLength(20)]
        public string OrderNo { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Nullable<DateTime> OrderDate { get; set; }

        public decimal? Total { get; set; }

        public bool? Status { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

    }
}
