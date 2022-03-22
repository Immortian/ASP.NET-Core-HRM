namespace HRM.Persistence
{
    public static class DBInitializer
    {
        public static void Initialize(HRMDBContext context)
        {
            context.Database.EnsureCreated();
            //Scaffold-DbContext -force 'Data Source=LAPTOP-5FUCQ052;Initial Catalog=HRM; Trusted_Connection=True' Microsoft.EntityFrameworkCore.SqlServer
        }
    }
}
