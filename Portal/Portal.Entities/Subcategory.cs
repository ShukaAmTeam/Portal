namespace Portal.Entities
{
    public class Subcategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
    }
}