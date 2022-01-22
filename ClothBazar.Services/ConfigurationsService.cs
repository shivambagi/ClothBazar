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
        ////public static property which will be returned after checking if an instance exists or not
        //public static ConfigurationsService ClassObject {
        //    get
        //    {
        //        if (privateInMemoryObject == null)
        //        {
        //            privateInMemoryObject = new ConfigurationsService(); 
        //        }
        //        return privateInMemoryObject;
        //    }
        //}
        
        //private static ConfigurationsService privateInMemoryObject { get; set; } // static private property,this is going to hold reference to single created instance
        //private ConfigurationsService() //private and parameterless ctor
        //{
        //}

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
