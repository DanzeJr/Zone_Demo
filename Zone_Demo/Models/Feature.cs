using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zone_Demo.Models
{
    public class Feature
    {
        public string Type { get; set; } = "Polygon";

        public Properties Properties { get; set; } = new Properties();

        public Geometry Geometry { get; set; }
    }
}
