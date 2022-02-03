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
    public class OrderService
    {
        #region Singleton
        //public static property which will be returned after checking if an instance exists or not
        public static OrderService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderService();
                }
                return instance;
            }
        }
        private static OrderService instance { get; set; } // static private property,this is going to hold reference to single created instance

        private OrderService() //private and parameterless ctor
        {
        }
        #endregion

        public List<Order> SearchOrders(string userID,string status, int pageNo, int pageSize)
        {
            using (var context = new CBContext())
            {
                var orders = context.Orders.ToList();

                if (!string.IsNullOrEmpty(userID))
                {
                    orders = orders.Where(x => x.UserID.ToLower().Contains(userID.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(status))
                {
                    orders = orders.Where(x => x.Status.ToLower().Contains(status.ToLower())).ToList();
                }

                return orders.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public List<Order> UserSearchOrders(string currentuserId, int pageNo, int pageSize)
        {
            using (var context = new CBContext())
            {
                var orders = context.Orders.ToList();

                if (!string.IsNullOrEmpty(currentuserId))
                {
                    orders = orders.Where(x => x.UserID.ToLower().Contains(currentuserId.ToLower())).ToList();
                }

                return orders.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public Order GetOrderByID(int ID)
        {
            using (var context = new CBContext())
            {
                var order = context.Orders.Where(x => x.ID == ID).Include(x => x.OrderItems).Include("OrderItems.Product").FirstOrDefault();

                return order;
            }
        }

        public int SearchOrdersCount(string userID,string status)
        {
            using (var context = new CBContext())
            {
                var orders = context.Orders.ToList();

                if (!string.IsNullOrEmpty(userID))
                {
                    orders = orders.Where(x => x.UserID.ToLower().Contains(userID.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(status))
                {
                    orders = orders.Where(x => x.Status.ToLower().Contains(status.ToLower())).ToList();
                }

                return orders.Count();
            }
        }

        public bool UpdateOrderStatus(int ID, string status)
        {
            using (var context = new CBContext())
            {
                var order = context.Orders.Find(ID);

                order.Status = status;

                context.Entry(order).State = EntityState.Modified;

                return context.SaveChanges() > 0;
            }
        }
    }
}
