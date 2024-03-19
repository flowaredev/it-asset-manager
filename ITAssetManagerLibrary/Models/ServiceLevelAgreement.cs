using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAssetManagerLibrary.Models
{
    public enum ServiceLevel
    {
        None = 0,
        Poor = 4,
        Insufficient = 8,
        Average = 12,
        Good = 16,
        Excellent = 20
    }

    public class ServiceLevelAgreement
    {
        public int Id { get; set; }

        [Required]
        public DateOnly EndDateOfMonth { get; set; }

        [Required]
        public double DisabilityHoursAverage { get; set; }

        [Required]
        public ServiceLevel DisabilityHoursLevel { get; set; } = ServiceLevel.None;

        [Required]
        public double UptimeRate { get; set; } = 1.0;

        [Required]
        public ServiceLevel UptimeLevel { get; set; } = ServiceLevel.Excellent;

        [Required]
        public double RoutineCheckRate { get; set; }

        [Required]
        public ServiceLevel RoutineCheckLevel { get; set; } = ServiceLevel.Poor;

        [Required]
        public double TechnicalSupportCompletionRate { get; set; }

        [Required]
        public ServiceLevel TechnicalSupportLevel { get; set; } = ServiceLevel.None;

        [Required]
        public int SecurityIssues { get; set; }

        [Required]
        public ServiceLevel SecurityIssuesLevel { get; set; } = ServiceLevel.Excellent;

        [Timestamp]
        public byte[] Version { get; set; } = [];
    }
}
