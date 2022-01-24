using ClothBazar.DataBase;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ClothBazar.Services
{
    public class CategoriesService
    {
        #region Singleton
        //public static property which will be returned after checking if an instance exists or not
        public static CategoriesService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoriesService();
                }
                return instance;
            }
        }
        private static CategoriesService instance { get; set; } // static private property,this is going to hold reference to single created instance
        private CategoriesService() //private and parameterless ctor
        {
        }
        #endregion

        public Category GetCategory(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Categories.Find(ID);
            }
        }
        
        public List<Category> GetCategories()
        {
            using (var context = new CBContext())
            {
                return context.Categories.Include(x=>x.Products).ToList();
                
            }
        }

        public List<Category> GetFeaturedCategories()
        {
            using (var context = new CBContext())
            {
                return context.Categories.Where(x => x.isFeatured && x.ImageURL != null).ToList();
            }
        }
        public void SaveCategory(Category category)
        {
            using (var context = new CBContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var context = new CBContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteCategory(int ID)
        {
            using (var context = new CBContext())
            {
                var category = context.Categories.Find(ID);

                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
