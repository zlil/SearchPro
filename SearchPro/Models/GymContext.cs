using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SearchPro.Models
{
    public class GymContext : DbContext
    {

        public DbSet<Gyms> Gyms { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Courses> Courses { get; set; }
    }
}