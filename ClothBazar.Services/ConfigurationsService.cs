using ClothBazar.DataBase;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{
    public class ConfigurationsService
    {
        #region Singleton
        //public static property which will be returned after checking if an instance exists or not
        public static ConfigurationsService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConfigurationsService();
                }
                return instance;
            }
        }
        private static ConfigurationsService instance { get; set; } // static private property,this is going to hold reference to single created instance
        private ConfigurationsService() //private and parameterless ctor
        {
        }
        #endregion

        public Config GetConfig(string key)
        {
            using (var context = new CBContext())
            {
                //return context.Configurations.Where(x => x.Key == key).FirstOrDefault();
                return context.Configurations.Find(key);
            }
        }
    }
}
