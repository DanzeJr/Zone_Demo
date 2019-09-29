using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zone_Demo.Models
{
    public interface IZoneRepository
    {
        IEnumerable<Zone> GetZones(int take = 10, int skip = 0);

        Task<Zone> FindZoneAsync(int id);

        Task<Zone> AddZoneAsync(Zone zone);

        Task<Zone> UpdateZoneAsync(Zone zone);

        Task DeleteZoneAsync(Zone zone);

        Task SaveChangesAsync();
    }
}
