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

        public int GetCategoriesCount(string search)
        {
            using (var context = new CBContext())
            {
                if (string.IsNullOrEmpty(search) == false)
                {
                    return context.Categories.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).Count();
                }
                return context.Categories.Count();

            }
        }


        public List<Category> GetAllCategories()
        {
            using (var context = new CBContext())
            {                
                return context.Categories.ToList();
            }
        }

        public List<Category> GetCategories(string search,int pageNo)
        {
            int pageSize = int.Parse(ConfigurationsService.Instance.GetConfig("ListingPageSize").Value);

            using (var context = new CBContext())
            {
                if (string.IsNullOrEmpty(search) == false)
                {
                    //combination of serach and paging linq query
                    return context.Categories.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower()))
                        .OrderBy(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Products).ToList();
                }
                return context.Categories.OrderBy(x => x.ID).Skip((pageNo -1) * pageSize).Take(pageSize).Include(x=>x.Products).ToList();                
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
