using System;
using System.Collections.Generic;

namespace Infosys.PolyclinicDAL.Models
{
    public partial class Appointments
    {
        public int AppointmentNo { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public DateTime DateofAppointment { get; set; }

        public virtual Doctors Doctor { get; set; }
        public virtual Patients Patient { get; set; }
    }
}
