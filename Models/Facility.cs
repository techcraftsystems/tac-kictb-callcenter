using System;
namespace Core.Models
{
    public class Facility {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool GXpt { get; set; }
        public bool XRay { get; set; }
        public bool Tbc { get; set; }
        public string GeoLocation { get; set; }
        public string Description { get; set; }

        public Facility() {
            Id = 0;
            Code = "";
            Name = "";
            GeoLocation = "[]";
            Description = "";
        }
    }
}
