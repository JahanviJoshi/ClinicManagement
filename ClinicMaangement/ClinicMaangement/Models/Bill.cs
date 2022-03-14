using System;
using System.Collections.Generic;

namespace ClinicMaangement.Models
{
    public partial class Bill
    {
        public int Billno { get; set; }
        public int? No { get; set; }
        public string? Name { get; set; }
        public int? Fees { get; set; }
        public DateTime? Date { get; set; }

        public virtual RegisterPatient? NoNavigation { get; set; }
    }
}
