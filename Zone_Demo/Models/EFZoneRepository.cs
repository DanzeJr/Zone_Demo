using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zone_Demo.Models
{
    public class EFZoneRepository : IZoneRepository
    {
        private readonly AppDbContext context;

        public EFZoneRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Zone> GetZones(int take = 10, int skip = 0)
        {
            return context.Zones.Take(10).Skip(0).ToArray();
        }

        public async Task<Zone> FindZoneAsync(int id)
        {
            return await context.Zones.FindAsync(id);
        }

        public async Task<Zone> AddZoneAsync(Zone zone)
        {
            context.Zones.Add(zone);
            await context.SaveChangesAsync();
            return zone;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<Zone> UpdateZoneAsync(Zone zone)
        {
            context.Zones.Update(zone);
            await context.SaveChangesAsync();
            return zone;
        }

        public async Task DeleteZoneAsync(Zone zone)
        {
            context.Zones.Remove(zone);
            await context.SaveChangesAsync();
        }
    }
}
