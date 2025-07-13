using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class House
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Number { get; set; } = string.Empty;

        [ForeignKey("Street")]
        public int StreetId { get; set; }

        [JsonIgnore]
        public Street? Street { get; set; } = null!;

        public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
    }
}
