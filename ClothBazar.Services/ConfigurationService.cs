using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{
    public class ConfigurationService
    {
        #region Singleton
        public static ConfigurationService Instance
        {
            get
            {
                if (instance == null) instance = new ConfigurationService();

                return instance;
            }
        }

        private static ConfigurationService instance { get; set; }

        private ConfigurationService()
        {
        }
        #endregion

        public Configuration GetConfigs(string key)
        {
            using (var context = new CBContext())
            {
                return context.Configurations.Where(x => x.Key == key ).FirstOrDefault();

               // return context.Configurations.Find(key);
            }
        }
    }
}
