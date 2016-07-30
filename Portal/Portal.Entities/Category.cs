using System.Collections.Generic;

namespace Portal.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Subcategory> Subcategories { get; set; }
    }
}