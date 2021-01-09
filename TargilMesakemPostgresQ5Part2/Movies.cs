using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargilMesakemPostgresQ5Part2
{
    class Movies:IPOCO
    { 

        public long ID { get; set; }
        public string Name { get; set; }
        public DateTime Release_Date { get; set; }
        public long Genre_ID { get; set; }
        public Movies()
        {

        }

        public Movies(string name, DateTime release_Date, long genre_ID)
        {
            Name = name;
            Release_Date = release_Date;
            Genre_ID = genre_ID;
        }

        public override string ToString()
        {
            return $"{Newtonsoft.Json.JsonConvert.SerializeObject(this)}";
        }
       }
    }
