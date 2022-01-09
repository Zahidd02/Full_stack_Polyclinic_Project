using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infosys.PolyclinicDAL.Models
{
    public class DoctorAppointmentCombine
    {
        public string DoctorName { get; set; }
        public string Specialization { get; set; }

        [Key]
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public int AppointmentNo { get; set; }
    }
}
