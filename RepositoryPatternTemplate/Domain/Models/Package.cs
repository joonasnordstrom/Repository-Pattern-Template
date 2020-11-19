using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PikiouAPI.Domain.Models
{
    public class Package
    {
        [Required]
        public int Id { get; set; }
        public Delivery Delivery { get; set; }
        [Required]
        public string BarCode { get; set; }
        [MaxLength(128, ErrorMessage ="Maximum length of product is 128 characters.")]
        public string Description { get; set; }
        public uint ProductsInPackage { get; set; }
        public float WeightKg { get; set; }
        public uint LengthCm { get; set; }
        public uint WidthCm { get; set; }
        public uint HeigthCm { get; set; }

        [Required]
        public int ECommerceId { get; set; }
        public ECommerce ECommerce { get; set; }
    }
}