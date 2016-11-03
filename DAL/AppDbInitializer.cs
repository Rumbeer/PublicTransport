using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;

namespace DAL
{
    public class AppDbInitializer : DropCreateDatabaseAlways<AppDbContext>
    {
        public override void InitializeDatabase(AppDbContext context)
        {
            base.InitializeDatabase(context);
        }
    }
}
