using System.Collections.Generic;

using StoreDB.Models;

namespace StoreLib
{
    public interface ILocationService
    {
        List<Location> GetAllLocations();

        Location GetLocationById(int locationId);
    }
}