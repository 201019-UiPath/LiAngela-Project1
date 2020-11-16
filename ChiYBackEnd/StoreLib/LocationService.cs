using System.Collections.Generic;

using StoreDB.Models;
using StoreDB.Repos;

namespace StoreLib
{
    public class LocationService : ILocationService
    {
        private ILocationRepo repo;

        public LocationService(ILocationRepo repo)
        {
            this.repo = repo;
        }

        public List<Location> GetAllLocations()
        {
            return repo.GetAllLocations();
        }

        public Location GetLocationById(int locationId)
        {
            return repo.GetLocationById(locationId);
        }
    }
}