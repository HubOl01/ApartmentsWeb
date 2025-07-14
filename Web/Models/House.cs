namespace Web.Models
{
    public class House
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public string FullAddress { get; set; } = "";
        public int ApartmentCount { get; set; }
        public List<Apartment> Apartments { get; set; } = new List<Apartment>();
    }
}
