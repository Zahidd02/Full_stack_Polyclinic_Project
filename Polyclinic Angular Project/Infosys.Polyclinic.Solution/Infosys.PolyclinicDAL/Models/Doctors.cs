using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Infosys.PolyclinicDAL.Models
{
    public partial class Doctors
    {
        public Doctors()
        {
            Appointments = new HashSet<Appointments>();
        }

        public string DoctorId { get; set; }
        public string Specialization { get; set; }
        public string DoctorName { get; set; }
        public decimal Fees { get; set; }
        [JsonIgnore]
        public virtual ICollection<Appointments> Appointments { get; set; }
    }
}
