using System;
using System.Collections.Generic;
using Core.Models;

namespace Core.ViewModel
{
    public class FacilityViewModel
    {
        public Facility Facility { get; set; }
        public List<Facility> Facilities { get; set; }

        public FacilityViewModel() {
            Facility = new Facility();
            Facilities = new List<Facility>();
        }
    }
}
