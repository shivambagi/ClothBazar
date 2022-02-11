using ClothBazar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web
{
    public interface IULIPValidationService
    {
        string ULIPValidation(ResultULIPViewModel model);
    }
    public class ULIPValidationService : IULIPValidationService
    {
        //#region Singleton
        ////public static property which will be returned after checking if an instance exists or not
        //public static ULIPValidationService Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new ULIPValidationService();
        //        }
        //        return instance;
        //    }
        //}
        //private static ULIPValidationService instance { get; set; } // static private property,this is going to hold reference to single created instance

        //private ULIPValidationService() //private and parameterless ctor
        //{
        //}
        //#endregion
        public string ULIPValidation(ResultULIPViewModel model)
        {
            if (model.Name.Length < 10)
            {
                return "Name is less";
            }

            return "";
        }
    }
}