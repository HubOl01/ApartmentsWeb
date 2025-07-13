using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Apartment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public float Area { get; set; }

        [ForeignKey("House")]
        public int HouseId { get; set; }

        [JsonIgnore]
        public House? House { get; set; } = null!;
    }
}
