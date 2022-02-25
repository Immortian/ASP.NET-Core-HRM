using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Persistence
{
    public static class DBInitializer
    {
        public static void Initialize(CRMContext context)
        {
            context.Database.EnsureCreated();
            //Scaffold-DbContext -force 'Data Source=LAPTOP-5FUCQ052;Initial Catalog=Call_centerTest; Trusted_Connection=True' Microsoft.EntityFrameworkCore.SqlServer
        }
    }
}
