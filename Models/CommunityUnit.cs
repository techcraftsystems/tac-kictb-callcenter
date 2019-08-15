using System;
namespace Core.Models
{
    public class CommunityUnit {
        public long Id { get; set; }
        public string Name { get; set; }

        public CommunityUnit() {
            Id = 0;
            Name = "";
        }
    }
}
