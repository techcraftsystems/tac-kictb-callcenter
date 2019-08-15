using System;
using Core.Service;
using Microsoft.AspNetCore.Http;

namespace Core.Models {
    public class Screening {
        public long Id { get; set; }
        public Record Record { get; set; }
        public ScreeningOutcome Outcome { get; set; }
        public bool Cough { get; set; }
        public bool Fever { get; set; }
        public bool Contact { get; set; }
        public bool WeightLoss { get; set; }
        public bool Playfullness { get; set; }
        public bool Breathless { get; set; }
        public bool ChestPains { get; set; }
        public bool NightSweats { get; set; }
        public string Comments { get; set; }
        public string OtherCondition { get; set; }

        public Screening() {
            Id = 0;
            Record = new Record();
            Outcome = new ScreeningOutcome();
        }

        public Screening Save(HttpContext context) {
            return new ContactService(context).SaveScreening(this);
        }
    }

    public class ScreeningOutcome {
        public long Id { get; set; }
        public string Name { get; set; }

        public ScreeningOutcome() {
            Id = 0;
            Name = "";
        }
    }
}
