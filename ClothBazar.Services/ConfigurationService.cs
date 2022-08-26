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

        public int PageSize()
        {
            using (var context = new CBContext())
            {
                var pageSizeConfig = context.Configurations.Find("PageSize");

                //if pagesize value is added in db it value should be return otherwise it will 10.
                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 5;
            }
        }

        public int ShopPageSize()
        {
            using (var context = new CBContext())
            {
                var pageSizeConfig = context.Configurations.Find("ShopPageSize");

                //if pagesize value is added in db it value should be return otherwise it will 6
                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 6;
            }
        }
    }
}
