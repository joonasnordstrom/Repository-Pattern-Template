using System.ComponentModel.DataAnnotations;

namespace PikiouAPI.Resources
{
    /// <summary>
    /// Used to map courier data
    /// </summary>
    public class SaveCourierResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
