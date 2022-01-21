using ClothBazar.DataBase;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{
    public class ProductsService
    {
        public Product GetProduct(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Products.Include(x => x.Category).Where(x => x.ID == ID).FirstOrDefault();
            }
        }

        public List<Product> GetProducts()
        {
            using (var context = new CBContext())
            {
                
                //return context.Products.ToList();

                ////Use below code for eager loading, not mandatory to mark category property virtual in product class just make use of Include() 
                return context.Products.Include(s => s.Category).ToList();
            }
        }
        public void SaveProduct(Product product)
        {
            using (var context = new CBContext())
            {
                //// use below to not allow creation of new category while adding products (unchanged) approach 1
                context.Entry(product.Category).State = System.Data.Entity.EntityState.Unchanged;

                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProduct(int ID)
        {
            using (var context = new CBContext())
            {
                var product = context.Products.Find(ID);

                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
    }
}
