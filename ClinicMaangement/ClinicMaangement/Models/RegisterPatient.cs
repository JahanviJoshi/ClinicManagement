using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicMaangement.Models
{
    public partial class RegisterPatient
    {
        public RegisterPatient()
        {
            Bills = new HashSet<Bill>();
            PatientMedicalInfos = new HashSet<PatientMedicalInfo>();
        }

        public int No { get; set; }
        public string? Name { get; set; }
        public string Gender { get; set; } = null!;
        public int Age { get; set; }
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;

        
        [Phone]
        public long Phoneno { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<PatientMedicalInfo> PatientMedicalInfos { get; set; }
    }
}
