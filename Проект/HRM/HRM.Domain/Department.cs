namespace HRM.Domain
{
    public partial class Department
    {
        public Department()
        {
            DepartmentWorkLoads = new HashSet<DepartmentWorkLoad>();
            Employees = new HashSet<Employee>();
        }

        public int DepartmentId { get; set; }
        public int EmployeeCount { get; set; }
        public string Direction { get; set; } = null!;
        public double BasicMoneyPerHour { get; set; }
        public double TotalMoneyPerHour { get; set; }
        public int? ManagerId { get; set; }

        public virtual Employee? Manager { get; set; }
        public virtual ICollection<DepartmentWorkLoad> DepartmentWorkLoads { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
