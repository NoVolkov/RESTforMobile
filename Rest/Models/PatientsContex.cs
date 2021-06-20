using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest.Models
{
    public class PatientsContex : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public PatientsContex(DbContextOptions<PatientsContex> options):base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
