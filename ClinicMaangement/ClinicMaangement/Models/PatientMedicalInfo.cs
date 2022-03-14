using System;
using System.Collections.Generic;

namespace ClinicMaangement.Models
{
    public partial class PatientMedicalInfo
    {
        public int? No { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Weight { get; set; }
        public double Bp { get; set; }
        public double CholestrolHdl { get; set; }
        public double CholestrolLdc { get; set; }
        public double SugarFast { get; set; }
        public double SugarPost { get; set; }
        public string MedicineSubscription { get; set; } = null!;
        public DateTime AppointmentDate { get; set; }

        public virtual RegisterPatient? NoNavigation { get; set; }
    }
}
