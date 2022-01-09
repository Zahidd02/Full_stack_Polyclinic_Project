using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Infosys.PolyclinicDAL.Models
{
    public partial class Appointments
    {
        public int AppointmentNo { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public DateTime DateofAppointment { get; set; }

        [JsonIgnore]
        public virtual Doctors Doctor { get; set; }
        [JsonIgnore]
        public virtual Patients Patient { get; set; }
    }
}
