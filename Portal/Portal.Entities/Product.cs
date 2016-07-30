using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type_Id { get; set; }
        public Nullable<int> CostPrice { get; set; }
        public Nullable<int> Price { get; set; }
        public int MeasUnit_Id { get; set; }
        public Nullable<int> TotalCount { get; set; }
        public Nullable<int> AvailableCount { get; set; }
        public Nullable<bool> IsAvailable { get; set; }
        public string ImageUrl { get; set; }
        public Subcategory Subcategory { get; set; }

        //public virtual MeasUnits MeasUnits { get; set; }      
        //public virtual ProductType ProductType { get; set; }
    }
}
