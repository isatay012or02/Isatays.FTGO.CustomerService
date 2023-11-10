using Isatays.FTGO.CustomerService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Isatays.FTGO.CustomerService.Core.Interfaces;

public interface IDataContext
{
    DbSet<Customer> Customers { get; set; }

    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
