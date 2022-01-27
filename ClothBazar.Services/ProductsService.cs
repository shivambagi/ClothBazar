using ClothBazar.DataBase;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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

        public List<Product> SearchProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID,int? sortBy)
        {
            using (var context = new CBContext())
            {
                var products = context.Products.ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.Name.ToLower().Contains(searchTerm)).ToList();
                }

                if(categoryID.HasValue)
                {
                    products = products.Where(x => x.Category.ID == categoryID.Value).ToList();
                }

                if (minimumPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minimumPrice.Value).ToList();
                }

                if (maximumPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maximumPrice.Value).ToList();
                }
                if(sortBy.HasValue)
                {
                    switch(sortBy.Value)
                    {
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        case 4:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                    }
                }
                return products;
            }
        }

        public int GetMaximumPrice() 
        {
            using (var context = new CBContext())
            {
                return (int)context.Products.Max(x => x.Price);
            }
        }

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

        public List<Product> GetProducts(int pageNo, int pageSize)//overloaded method
        {
            //pageSize = int.Parse(ConfigurationsService.Instance.GetConfig("ListingPageSize").Value);

            using (var context = new CBContext())
            {
                return context.Products.OrderBy(s => s.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(s => s.Category).ToList();
            }
        }

        public List<Product> GetProductsByCategory(int categoryID, int pageSize)
        {
            using (var context = new CBContext())
            {
                return context.Products.Where(x => x.Category.ID == categoryID).OrderByDescending(x => x.ID).Take(pageSize).Include(x => x.Category).ToList();
            }
        }

        public List<Product> GetLatestProducts(int numberOfProducts)
        {
            using (var context = new CBContext())
            {

                //return context.Products.ToList();

                ////Use below code for eager loading, not mandatory to mark category property virtual in product class just make use of Include() 
                return context.Products.OrderByDescending(x=> x.ID).Take(numberOfProducts).Include(s => s.Category).ToList();
            }
        }


        public void SaveProduct(Product product)
        {
            using (var context = new CBContext())
            {

                try
                {
                    //// use below to not allow creation of new category while adding products (unchanged) approach 1
                    context.Entry(product.Category).State = System.Data.Entity.EntityState.Unchanged;

                    context.Products.Add(product);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }


               
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
