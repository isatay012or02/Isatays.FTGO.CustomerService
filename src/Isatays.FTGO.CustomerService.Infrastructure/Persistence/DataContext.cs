using Isatays.FTGO.CustomerService.Core.Entities;
using Isatays.FTGO.CustomerService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Isatays.FTGO.CustomerService.Infrastructure.Persistence;

public class DataContext : DbContext, IDataContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; } 
}
