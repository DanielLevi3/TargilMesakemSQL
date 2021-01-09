using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargilMesakemEntityF
{
    class CitiesDAO
    {
        public CitiesDAO()
        {

        }
        public List<City> GetAllCities()
        {
            List<City> result = new List<City>();
            using (TargilMesakemEFEntities entities = new TargilMesakemEFEntities())
            {
                result = entities.Cities.ToList();
            }
            return result;

        }
        public City GetALLCitysByID(long iD)
        {
            City result = null;
            using (TargilMesakemEFEntities entities = new TargilMesakemEFEntities())
            {
                result = entities.Cities.FirstOrDefault(c => c.ID == iD);
            }
            return result;

        }
        public void AddCity(City c)
        {
            using (TargilMesakemEFEntities entities = new TargilMesakemEFEntities())
            {
                entities.Cities.Add(c);
                entities.SaveChanges();
            }
        }
        public void UpdateCity(long iD,string name,long district_id,string mayor,int population)
        {
            using (TargilMesakemEFEntities entities = new TargilMesakemEFEntities())
            {
                entities.Cities.FirstOrDefault(c => c.ID == iD).Name=name;
                entities.Cities.FirstOrDefault(c => c.ID == iD).District_ID=district_id;
                entities.Cities.FirstOrDefault(c => c.ID == iD).Mayor = mayor;
                entities.Cities.FirstOrDefault(c => c.ID == iD).Population = population;
                entities.SaveChanges();
            }

        }
        public void UpdatePopulation(long iD, int population)
        {
            using (TargilMesakemEFEntities entities = new TargilMesakemEFEntities())
            {
                entities.Cities.FirstOrDefault(c => c.ID == iD).Population = population;
                entities.SaveChanges();
            }

        }
        public void RemoveCity(long iD)
        { 
            using (TargilMesakemEFEntities entities = new TargilMesakemEFEntities())
            {
                entities.Cities.Remove(entities.Cities.FirstOrDefault(c => c.ID == iD));
                entities.SaveChanges();
            }
        }
        public List<City> GetAllCitiesThatGotMorePopulationThen(int population)
        {
            List<City> result = null;
            using (TargilMesakemEFEntities entities = new TargilMesakemEFEntities())
            {
              var v= entities.Cities.ToList().Where(c=>c.Population > population);
                result = v.ToList();
            }
            return result;
        }
        public void UpdateDistrictPopulation(long district_id)
        {
            int sum = 0;
            using (TargilMesakemEFEntities entities = new TargilMesakemEFEntities())
            {
                foreach (var item in entities.Cities.ToList())
                {
                    if(item.District_ID==district_id)
                    {
                        sum =(int)item.Population + sum;
                    }
                    entities.Districts.FirstOrDefault(c => c.ID == district_id).Population = sum;
                    entities.SaveChanges();
                }
            }
        }
        //tried to make a function to print the joined tables but failed,
        /* public List<object> GetAllCitiesAndDistricts()
         {
             List<object> result;
             using (TargilMesakemEFEntities entities = new TargilMesakemEFEntities())
             {
                 object o = entities.Cities.Join(entities.Districts,
                  c => c.District_ID,
                  d => d.ID,
                  (c, d) => new
                  {
                      City_Id = c.ID,
                      City_Name = c.Name,
                      City_Population = c.Population,
                      District_ID = d.ID,
                      District_Name = d.Name
                  }).ToList();
                 result = (List<object>)o;               
             }
             return result;

         }
        */
    }
}
