using System;
using System.Collections.Generic;
using System.Text;

namespace Infosys.PolyclinicDAL.Models
{
    public class AppointmentDoctorName
    {
        public int AppointmentNo { get; set; }
        public string DoctorName { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime DateofAppointment { get; set; }
    }
}
