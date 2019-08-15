using System;
using Core.Service;
using Microsoft.AspNetCore.Http;

namespace Core.Models
{
    public class Contacts
    {
        public long Id { get; set; }
        public int Complete { get; set; }
        public int Favorite { get; set; }
        public string Uuid { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Description { get; set; }
        public string PhysicalAddress { get; set; }
        public string Image { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime AddedOn { get; set; }
        public Users AddedBy { get; set; }

        public Contacts() {
            Id = 0;
            Complete = 0;
            Favorite = 0;
            Uuid = "";
            Name = "";
            Age = "";
            Gender = "";
            Email = "";
            Telephone = "";
            Description = "";
            PhysicalAddress = "";
            Image = "";
            DateOfBirth = DateTime.Now;
            AddedOn = DateTime.Now;
            AddedBy = new Users();
        }

        public string GetAge() {
            int age = DateTime.Now.Year - DateOfBirth.Year;
            if (DateOfBirth > DateTime.Now.AddYears(-age)) age--;

            return age + "yrs";
        }

        public Contacts Save(HttpContext context) {
            return new ContactService(context).SaveContact(this);
        }
    }
}
