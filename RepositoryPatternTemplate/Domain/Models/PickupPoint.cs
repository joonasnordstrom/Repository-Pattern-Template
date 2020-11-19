using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PikiouAPI.Domain.Models
{
    public class PickupPoint
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourierId { get; set; }
        public Courier Courier { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string StreetNumber { get; set; }
        public ushort ApartmentNumber { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Length of country ISO code is 2 characters.")]
        public string CounrtryCodeISO { get; set; }
        public int LockersAvailable { get; set; }
    }
}
