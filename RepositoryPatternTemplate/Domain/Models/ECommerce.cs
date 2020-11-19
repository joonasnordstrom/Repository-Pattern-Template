using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PikiouAPI.Domain.Models
{
    public class ECommerce
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
        public string Name { get; set; }
        [Required]
        public string BussinessId { get; set; }
        public virtual IList<Order> Orders{ get; set; }
    }
}
