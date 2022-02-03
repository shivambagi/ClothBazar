using ClothBazar.Services;
using ClothBazar.Web.Models;
using ClothBazar.Web.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace ClothBazar.Web.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }        

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult UsersList(string search, int? pageNo)
        {
            UsersListViewModel model = new UsersListViewModel();
            model.SearchTerm = search;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            int pageSize = int.Parse(ConfigurationsService.Instance.GetConfig("ListingPageSize").Value);            

            var totalRecords = db.Users.Count();
            
            if (!string.IsNullOrEmpty(search))
            {
                model.Users = db.Users.Include(x => x.Roles).Where(x=>x.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            else
            {
                model.Users = db.Users.Include(x => x.Roles).ToList();
            }

            

                model.Pager = new Pager(totalRecords, pageNo, pageSize);

            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Edit(string ID)
        {
            EditUserViewModel model = new EditUserViewModel();

            var user = db.Users.Where(x => x.Id == ID).FirstOrDefault();

            model.ID = ID;
            model.Name = user.Name;
            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            model.UserRole = user.Roles.FirstOrDefault();
            model.Roles = db.Roles.ToList();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditUserViewModel model)
        {
            var existingUser = db.Users.Where(x => x.Id == model.ID).FirstOrDefault();
            existingUser.Name = model.Name;
            existingUser.Email = model.Email;
            existingUser.PhoneNumber = model.PhoneNumber;
            var existingUserRole = UserManager.GetRoles(existingUser.Id);
            UserManager.RemoveFromRole(existingUser.Id, existingUserRole[0].ToString());
            var TochangeRole = db.Roles.Where(x => x.Id == model.RoleID).FirstOrDefault();
            UserManager.AddToRole(existingUser.Id, TochangeRole.Name);
            db.Entry(existingUser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("UsersList");
        }

        [HttpPost]
        public ActionResult Delete(string ID)
        {
            var user = db.Users.Find(ID);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("UsersList");
        }
    }
}