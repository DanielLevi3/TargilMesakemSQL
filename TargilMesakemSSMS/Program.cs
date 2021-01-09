using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargilMesakemSSMS
{
    class Program
    {
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            my_logger.Info("...System coming up...");
            StoresDAO storesDAO = new StoresDAO("Data Source=.;Initial Catalog=TargilMeskamSSMS;Integrated Security=True;");
            Stores s = new Stores("Prada", 2, 1);
            //storesDAO.UpdateStore(s, 2);
            //storesDAO.AddStore(s);
            List<Stores> stores_list = storesDAO.GetAllStores();
            Console.WriteLine("GetAllStores`-");
            stores_list.ForEach(s1=> Console.WriteLine(s1));
            Stores store = storesDAO.GetStoreById(3);
            Console.WriteLine("getstorebyID");
            Console.WriteLine(store);
            List<Stores> stores_list2 = storesDAO.GetAllStoresWithTheSameCatAndFloor(1,2);
            Console.WriteLine("GetAllStoresWithTheSameCatAndFloor");
            stores_list2.ForEach(s2 => Console.WriteLine(s2));
            Console.WriteLine("GetMaxStoresByCategory");
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(storesDAO.GetMaxStoresByCategory()));
            my_logger.Info("...System shutting down...");
        }
    }
}
