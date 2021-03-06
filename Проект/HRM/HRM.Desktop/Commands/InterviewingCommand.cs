using System;
using System.ComponentModel.DataAnnotations;

namespace HRM.Desktop.Commands
{
    public class InterviewingCommand
    {
        [Required]
        public int CandidateId { get; set; }
        [Required]
        public bool IsPassed { get; set; }
        [Required]
        public DateTime PassDate { get; set; }
        public int DepartmentId { get; set; }
    }
}
