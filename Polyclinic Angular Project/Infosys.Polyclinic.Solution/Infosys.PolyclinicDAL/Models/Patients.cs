using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Infosys.PolyclinicDAL.Models
{
    public partial class Patients
    {
        public Patients()
        {
            Appointments = new HashSet<Appointments>();
        }

        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public byte Age { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }

        [JsonIgnore]
        public virtual ICollection<Appointments> Appointments { get; set; }
    }
}
