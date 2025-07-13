using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Street
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [ForeignKey("City")]
        public int CityId { get; set; }

        [JsonIgnore]
        public City? City { get; set; } = null!;

        public ICollection<House> Houses { get; set; } = new List<House>();
    }
}
