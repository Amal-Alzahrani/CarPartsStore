namespace CarPartsStore.Models
{
    public class City
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public ICollection<Company> Companies { get; set; }
        
    }
}
