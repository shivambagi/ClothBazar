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
        #region Singleton
        //public static property which will be returned after checking if an instance exists or not
        public static ProductsService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductsService();
                }
                return instance;
            }
        }
        private static ProductsService instance { get; set; } // static private property,this is going to hold reference to single created instance
        private ProductsService() //private and parameterless ctor
        {
        }
        #endregion

        public Product GetProduct(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Products.Include(x => x.Category).Where(x => x.ID == ID).FirstOrDefault();
            }
        }

        public List<Product> GetProducts(List<int> IDs)
        {
            List<Product> p = new List<Product>();

            foreach (var id in IDs)
            {
                p.Add(GetProduct(id));
            }

            return p;
            //using (var context = new CBContext())
            //{
            //    //return context.Products.Where(x => IDs.Contains(x.ID)).ToList();
            //}
        }

        public List<Product> GetProducts(int pageNo)
        {
            int pageSize = int.Parse(ConfigurationsService.Instance.GetConfig("ListingPageSize").Value);

            using (var context = new CBContext())
            {

                //return context.Products.ToList();

                ////Use below code for eager loading, not mandatory to mark category property virtual in product class just make use of Include() 
                //return context.Products.Include(s => s.Category).ToList();

                ////for pagination modified above return with take and skip
                ////orderby(mandatory for skip)-->skip-->take-->INclude-->List
                return context.Products.OrderBy(s => s.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(s => s.Category).ToList();
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
