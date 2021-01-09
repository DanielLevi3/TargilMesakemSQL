using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargilMesakemSQLITE
{
    class Program
    {
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            my_logger.Info("...System coming up...");
            string conn = @"Data Source = C:\\Users\\levid\\TargilMesakem.db;";
            Cars_DAO cars_d = new Cars_DAO(conn);
            Cars g = new Cars( "Italy", 2001, "Lamborghini Diablo");
            //cars_d.AddCars(g);
            Cars t1 = new Cars("Japan", 2015, "Evo");
            //List<Cars> cars_list = new List<Cars>();
            //cars_list = cars_d.GetAllCars();
            //Console.WriteLine("GetAllCars");
            //cars_list.ForEach(c => Console.WriteLine(c));
            //List<Cars> cars_list2 = new List<Cars>();
            //Console.WriteLine("GetAllCarsSameManufacturer");
            //cars_list2 = cars_d.GetAllCarsSameManufacturer("Germany");
            //cars_list2.ForEach(c => Console.WriteLine(c));
            List<object> test_list = new List<object>();
            Console.WriteLine("GetAllAndTests");
            test_list = cars_d.GetAllCarsAndTests();
            test_list.ForEach(c =>Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(c)));
            // cars_d.UpdateCars(t1, 5);
            //cars_d.RemoveCars(6);
            my_logger.Info("...System shutting down...");
        }


    }
}
