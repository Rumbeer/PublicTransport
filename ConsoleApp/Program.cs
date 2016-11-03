using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;

namespace ConsoleApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            int a;
            var station = new Station() { Name = "Ceska", Town = "Brno" };
            using (var db = new AppDbContext())
            {
                db.Stations.Add(station);
                db.SaveChanges();
            }
            using (var db = new AppDbContext())
            {
                var st = db.Stations.FirstOrDefault(s => s.Name.Equals(station.Name));
                Console.WriteLine("ID: " + st.ID +", Name: " + st.Name + ", Town: " + st.Town);
                db.Stations.Remove(st);
                db.SaveChanges();
            }
        }
    }
}
