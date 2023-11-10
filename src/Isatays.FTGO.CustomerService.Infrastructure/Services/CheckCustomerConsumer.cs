using Isatays.FTGO.CustomerService.Core.Entities;
using Isatays.FTGO.CustomerService.Core.Interfaces;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Isatays.FTGO.CustomerService.Infrastructure.Services;

public class CheckCustomerConsumer : IConsumer<Order>
{
    private readonly IDataContext _dataContext;

    public CheckCustomerConsumer(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task Consume(ConsumeContext<Order> context)
    {
        var result = await _dataContext
                        .Customers
                        .Where(c => c.Id == context.Message.Id
                            && c.Name == context.Message.Name
                            && c.Email == context.Message.Email)
                        .FirstOrDefaultAsync();

        if (result is not null)
        {
            result.IsAvailable = true;
            await _dataContext.SaveChangesAsync(context.CancellationToken);
        }
    }
}
