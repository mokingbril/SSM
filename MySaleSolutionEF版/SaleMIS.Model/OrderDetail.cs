using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaleMIS.Model
{
    [Serializable]
    public partial class OrderDetail
    {
        [Key]
        public long DetailsId { get; set; }

        [Required, MaxLength(20)]
        public string OrderNo { get; set; }

        [Required]
        public int ProductId { get; set; }

        public int? Quantity { get; set; }

        public int? Discount { get; set; }

        [ForeignKey("OrderNo")]
        public virtual Order Order { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Products { get; set; }
    }
}
