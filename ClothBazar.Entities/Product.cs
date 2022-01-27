using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Entities
{
    public class Product : BaseEntity
    {
        //ID,Name, Desc are inherited from BaseEntity class which are common to both Product and Category
        [Range(1,10000000)]
        public decimal Price { get; set; }
        //public int CategoryID { get; set; }//add this ID for saving the categories and not allowing duplicates approach 2
        public virtual Category Category { get; set; }
        public string ImageURL { get; set; }
    }
}
