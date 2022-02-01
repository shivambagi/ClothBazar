using ClothBazar.DataBase;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{
    public class ShopService
    {
        #region Singleton
        //public static property which will be returned after checking if an instance exists or not
        public static ShopService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShopService();
                }
                return instance;
            }
        }
        private static ShopService instance { get; set; } // static private property,this is going to hold reference to single created instance

        private ShopService() //private and parameterless ctor
        {
        }
        #endregion


        public int SaveOrder(Order order)
        {
            using (var context = new CBContext())
            {
                context.Orders.Add(order);
                return context.SaveChanges();
            }
        }


    }
}
