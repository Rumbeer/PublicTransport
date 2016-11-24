using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;

namespace DAL
{
    public class AppDbInitializer : DropCreateDatabaseAlways<AppDbContext2>
    {
        public override void InitializeDatabase(AppDbContext2 context)
        {
            base.InitializeDatabase(context);
        }
    }
}
