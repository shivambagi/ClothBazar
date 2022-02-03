using ClothBazar.Entities;
using ClothBazar.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModels
{
    public class OrderViewModel
    {
        public List<Order> Orders { get; set; }
        public Pager Pager { get; set; }
        public string UserID { get; set; }
        public string Status { get; set; }
    }

    public class OrderDetailsViewModel
    {
        public Order Order { get; set; }
        public ApplicationUser OrderBy { get; set; }
        public List<string> AvailableStatuses { get; set; }
    }

    public class UserOrderViewModel
    {
        public List<Order> Orders { get; set; }
        public Pager Pager { get; set; }
        public string Name { get; set; }
    }
}