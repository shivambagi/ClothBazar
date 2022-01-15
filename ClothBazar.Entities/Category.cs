using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Entities
{
    public class Category : BaseEntity
    {
        //ID,Name, Desc are inherited from BaseEntity class which are common to both Product and Category
        public List<Product> Products { get; set; }

    }
}
