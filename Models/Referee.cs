using System;
namespace Core.Models
{
    public class Referee {
        public long Id { get; set; }
        public string Name { get; set; }
        public RefereeType Type { get; set; }
        public CommunityUnit Unit { get; set; }

        public Referee() {
            Id = 0;
            Name = "";
            Type = new RefereeType();
            Unit = new CommunityUnit();
        }
    }

    public class RefereeType {
        public long Id { get; set; }
        public string Name { get; set; }

        public RefereeType() {
            Id = 0;
            Name = "";
        }
    }
}
