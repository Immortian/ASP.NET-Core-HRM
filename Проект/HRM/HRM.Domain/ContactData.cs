namespace HRM.Domain
{
    public partial class ContactData
    {
        public ContactData()
        {
            Employees = new HashSet<Employee>();
        }

        public int ContactDataId { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public virtual Candidate Candidate { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
