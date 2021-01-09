using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargilMesakemSQLITE
{
    class Cars :IPoco
    {
        public long ID { get; set; }
        public string Manufacturer { get; set; }
        public long Year { get; set; }
        public string Model { get; set; }
        public Cars( string manufacturer, int year, string model)
        {
            Manufacturer = manufacturer;
            Year = year;
            Model = model;
        }

        public Cars()
        {

        }
        public override string ToString()
        {
            return $"{Newtonsoft.Json.JsonConvert.SerializeObject(this)}";
        }
      
        public override bool Equals(object obj)
        {
            Cars c = (Cars)obj;
            return this.ID ==c.ID;
        }
        public static bool operator ==(Cars poco1, Cars poco2)
        {
            return poco1.ID == poco2.ID;
        }
        public static bool operator !=(Cars poco1, Cars poco2)
        {
            return !(poco1.ID == poco2.ID);
        }

        public override int GetHashCode()
        {
            return (int)ID;
        }
        
    }
}
