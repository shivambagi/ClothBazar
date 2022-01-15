using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Entities
{
    public class Product : BaseEntity
    {
        //ID,Name, Desc are inherited from BaseEntity class which are common to both Product and Category
        public decimal Price { get; set; }
        public Category Category { get; set; }
    }
}
