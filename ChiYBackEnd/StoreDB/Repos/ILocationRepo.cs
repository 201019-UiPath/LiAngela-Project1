using System.Collections.Generic;

using StoreDB.Models;

namespace StoreDB.Repos
{
    public interface ILocationRepo
    {
         List<Location> GetAllLocations();

         Location GetLocationById(int locationId);
    }
}