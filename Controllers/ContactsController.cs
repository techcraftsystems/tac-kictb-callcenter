using System;
using Core.Models;
using Core.Service;
using Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Core.Controllers {
    [Authorize]
    public class ContactsController : Controller {
        [BindProperty]
        public ContactsRegistrationViewModel Regs { get; set; }

        [Route("/contacts")]
        public IActionResult Index(ContactsViewModel model) {
            model.Contacts = new ContactService().GetContacts();
            return View(model);
        }

        [Route("/contacts/{uuid}")]
        public IActionResult Contact(string uuid, ContactsProfileViewModel model) {
            model.Contact = new ContactService().GetContact(uuid);
            return View(model);
        }

        [Route("/contacts/registration")]
        public IActionResult Registration(int src, string phone, string uuid, ContactsRegistrationViewModel model, ContactService Service) {
            if (!string.IsNullOrEmpty(uuid)) {
                Contacts contact = new ContactService().GetContact(uuid);
                if (contact != null)
                    return LocalRedirect("/contacts/" + contact.Uuid);
            }

            if (!string.IsNullOrEmpty(phone))
                model.Contact.Telephone = phone;


            model.Record.Source = new RecordSource { Id = src };
            model.Units = Service.GetCommunityUnitsAutocomplete();
            model.Caller = Service.GetCallerTypeIEnumerable();
            model.Gender = Service.GetGenderIEnumerable();
            model.Type = Service.GetRecordTypeIEnumerable();
            model.Source = Service.GetRecordSourceIEnumerable();
            model.Referral = Service.GetReferralTypeIEnumerable();
            model.Outcomes = Service.GetScreeningOutcomesIEnumerable();
            model.Facilities = Service.GetFacilitiesAutocomplete();
            model.Conditions = Service.GetOtherConditionsAutocomplete();

            return View(model);
        }

        [HttpPost]
        public IActionResult NewRegistration() {
            Contacts contact = Regs.Contact;
            contact.DateOfBirth = DateTime.Now.AddYears(0 - Convert.ToInt32(contact.Age));
            contact.Save(HttpContext);

            CommunityUnit unit = new CommunityUnit();
            string[] tokens = Regs.Unit.Split(' ');
            if (!string.IsNullOrEmpty(Regs.Unit) && tokens.Length > 1) {
                int.TryParse(tokens[0], out int idnt);
                unit.Id = idnt;
            }

            Referee referee = new Referee();
            tokens = Regs.Referee.Split(' ');
            if (!string.IsNullOrEmpty(Regs.Referee) && tokens.Length > 1) {
                int.TryParse(tokens[0], out int idnt);
                referee.Id = idnt;
            }

            Facility facility = new Facility();
            tokens = Regs.Facility.Split(' ');
            if (!string.IsNullOrEmpty(Regs.Facility) && tokens.Length > 1) {
                int.TryParse(tokens[0], out int idnt);
                facility.Id = idnt;
            }

            Record record = Regs.Record;
            record.Contact = contact;
            record.Referee = referee;
            record.Unit = unit;
            record.Save(HttpContext);

            Screening screen = Regs.Screen;
            screen.Record = record;
            screen.Save(HttpContext);

            Appointment appointment = Regs.Appointment;
            appointment.Contact = contact;
            appointment.Facility = facility;
            appointment.Source = new AppointmentSource { Id = 1 };
            appointment.Service = new AppointmentService { Id = 1 };
            appointment.Save(HttpContext);

            return LocalRedirect("/contacts/");
        }

        public JObject GetRefereesAutocomplete(int catg, ContactService Service) {
            return Service.GetRefereesAutocomplete(catg);
        }
    }
}
