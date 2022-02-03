using ClothBazar.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModels
{
    public class UsersListViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public Pager Pager { get; set; }
        public string SearchTerm { get; set; }
    }

    public class EditUserViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IdentityUserRole UserRole { get; set; }
        public string RoleID { get; set; }
        public ICollection<IdentityRole> Roles { get; set; }
    }
}