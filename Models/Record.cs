using System;
using Core.Service;
using Microsoft.AspNetCore.Http;

namespace Core.Models
{
    public class Record {
        public long Id { get; set; }
        public Contacts Contact { get; set; }
        public RecordCaller Caller { get; set; }
        public RecordType Type { get; set; }
        public RecordSource Source { get; set; }
        public DateTime AddedOn { get; set; }
        public Users AddedBy { get; set; }
        public Referee Referee { get; set; }
        public RefereeType Referral { get; set; }
        public CommunityUnit Unit { get; set; }
        public string CallerName { get; set; }
        public string CallerPhone { get; set; }
        public string Description { get; set; }

        public Record() {
            Id = 0;
            Description = "";

            Contact = new Contacts();
            Caller = new RecordCaller();
            Type = new RecordType();
            Unit = new CommunityUnit();
            Source = new RecordSource();
            Referee = new Referee();
            Referral = new RefereeType();

            AddedOn = DateTime.Now;
            AddedBy = new Users();
        }

        public Record Save(HttpContext context) {
            return new ContactService(context).SaveRecord(this);
        }
    }

    public class RecordSource {
        public long Id { get; set; }
        public string Name { get; set; }

        public RecordSource() {
            Id = 0;
            Name = "";
        }
    }

    public class RecordType {
        public long Id { get; set; }
        public string Name { get; set; }

        public RecordType() {
            Id = 0;
            Name = "";
        }
    }

    public class RecordCaller {
        public long Id { get; set; }
        public string Name { get; set; }

        public RecordCaller() {
            Id = 0;
            Name = "";
        }
    }
}
