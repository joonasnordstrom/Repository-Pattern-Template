using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PikiouAPI.Domain.Models
{
    public class Orderer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string StreetName { get; set; }
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }
        public string StreetNumber { get; set; }
        public ushort ApartmentNumber { get; set; }
    }
}
