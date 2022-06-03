namespace HRM.Domain
{
    public partial class PassportInfo
    {
        public PassportInfo()
        {
            Candidates = new HashSet<Candidate>();
            Employees = new HashSet<Employee>();
            Dismissals = new HashSet<Dismissal>();
        }

        public int PassportId { get; set; }
        public int PassportSerial { get; set; }
        public int PassportNumber { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Lastname { get; set; }
        public string? State { get; set; }
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public int House { get; set; }
        public int? Buinding { get; set; }
        public int Apartment { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Dismissal> Dismissals { get; set; }
    }
}
