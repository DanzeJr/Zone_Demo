using GeoAPI.Geometries;
using NetTopologySuite.IO.Converters;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zone_Demo.Models
{
    public class Zone
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "geometry")]
        public IGeometry Geometry { get; set; }
    }
}
