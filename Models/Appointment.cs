using System;
using Core.Service;
using Microsoft.AspNetCore.Http;

namespace Core.Models
{
    public class Appointment
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public AppointmentSource Source { get; set; }
        public AppointmentService Service { get; set; }
        public Contacts Contact { get; set; }
        public Facility Facility { get; set; }
        public Referee CHEWs { get; set; }
        public bool ShowedUp { get; set; }
        public string Description { get; set; }

        public Appointment() {
            Id = 0;

            Source = new AppointmentSource();
            Service = new AppointmentService();
            Contact = new Contacts();
            Facility = new Facility();
            CHEWs = new Referee();
            ShowedUp = false;
            Description = "";
        }

        public Appointment Save(HttpContext context) {
            return new ContactService(context).SaveAppointment(this);
        }
    }

    public class AppointmentSource {
        public long Id { get; set; }
        public string Name { get; set; }

        public AppointmentSource() {
            Id = 0;
            Name = "";
        }
    }

    public class AppointmentService {
        public long Id { get; set; }
        public string Name { get; set; }

        public AppointmentService() {
            Id = 0;
            Name = "";
        }
    }
}
