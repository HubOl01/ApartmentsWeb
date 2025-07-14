namespace Web.Models
{
    public class Street
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int HouseCount { get; set; }
        public List<House> Houses { get; set; } = new List<House>();
    }
}
