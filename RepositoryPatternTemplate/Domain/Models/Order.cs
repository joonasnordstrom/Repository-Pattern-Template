using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PikiouAPI.Domain.Models
{
    public class Order
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Description="Price of delivery")]
        public float DeliveryPrice { get; set; }
        [Required]
        public int OrdererId { get; set; }
        public int ECommerceId { get; set; }
        public ECommerce ECommerce { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public virtual IList<Delivery> Deliveries { get; set; }
    }
}
