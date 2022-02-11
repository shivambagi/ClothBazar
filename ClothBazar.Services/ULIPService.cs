using ClothBazar.DataBase;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{    
    public class ULIPService 
    {
        #region Singleton
        //public static property which will be returned after checking if an instance exists or not
        public static ULIPService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ULIPService();
                }
                return instance;
            }
        }
        private static ULIPService instance { get; set; } // static private property,this is going to hold reference to single created instance

        CBContext context;
        private ULIPService() //private and parameterless ctor
        {
            context = new CBContext();
        }
        #endregion
    }
}
