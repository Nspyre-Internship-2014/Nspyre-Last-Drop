using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LastDropDBOperations
{
    class DatabaseController
    {

        //TODO: Make lists of all the objects locally in a new repository class

        SqlConnection con;
        DatabaseMemoryStoring store;

        public DatabaseController(String connectionString)
        {
            store = new DatabaseMemoryStoring();
            con = new SqlConnection(connectionString);
        }

        //Here is a test operation that adds the plant from the database into a local list and then finds one with a given name
        public Plant getPlantByName(String name){
             List<Plant> plants=new List<Plant>();
            plants=store.StoreInClassPlant(con);
            foreach (Plant plant in plants)
                if (plant.Name == name)
                    return plant;
            return null;
        }
    }
}
