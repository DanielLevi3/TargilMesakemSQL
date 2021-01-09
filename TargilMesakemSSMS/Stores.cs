using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargilMesakemSSMS
{
    class Stores
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
        public long Categories_ID { get; set; }

        public Stores(string name, int floor, int categories_ID)
        {
            Name = name;
            Floor = floor;
            Categories_ID = categories_ID;
        }

        public Stores()
        {

        }
        public override string ToString()
        {
            return $"{Newtonsoft.Json.JsonConvert.SerializeObject(this)}";
        }

    }
}
