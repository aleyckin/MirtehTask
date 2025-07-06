using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
