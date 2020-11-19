using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PikiouAPI.Domain.Models
{
    public class Courier
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string BussinessId { get; set; }
        public string APIKey { get; set; }
        IList<Order> Orders { get; set; }
        IList<PickupPoint> PickupPoints { get; set; }
    }
}
