using System.Collections.Generic;
using Core.Models;
using Core.Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;

namespace Core.ViewModel {
    public class ContactsViewModel {
        public List<Contacts> Contacts { get; set; }
        public ContactsViewModel() {
            Contacts = new List<Contacts>();
        }
    }

    public class ContactsRegistrationViewModel {
        private readonly ContactService Service = new ContactService();
        public Contacts Contact { get; set; }
        public Record Record { get; set; }
        public Screening Screen { get; set; }
        public Appointment Appointment { get; set; }

        public JObject Facilities { get; set; }
        public JObject Conditions { get; set; }
        public JObject Units { get; set; }
        public string Unit { get; set; }
        public string Referee { get; set; }
        public string Facility { get; set; }

        public List<SelectListItem> Gender { get; set; }
        public List<SelectListItem> Source { get; set; }
        public List<SelectListItem> Type { get; set; }
        public List<SelectListItem> Caller { get; set; }
        public List<SelectListItem> Referral { get; set; }
        public List<SelectListItem> Outcomes { get; set; }

        public ContactsRegistrationViewModel() {
            Contact = new Contacts();
            Record = new Record();
            Screen = new Screening();

            Units = new JObject();
            Conditions = new JObject();
            Facilities = new JObject();
            Unit = "";
            Referee = "";
        }
    }

    public class ContactsProfileViewModel {
        public Contacts Contact { get; set; }

        public ContactsProfileViewModel() {
            Contact = new Contacts();
        }
    }


}
