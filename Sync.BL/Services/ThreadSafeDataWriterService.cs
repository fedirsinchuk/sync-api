using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sync.BL.Models;
using Sync.DAL;
using Sync.DAL.Entities;

namespace Sync.BL.Services;

public interface IThreadSafeDataWriterService
{
    Task Update(Value value, CancellationToken token);
}

public class ThreadSafeDataWriterService : IThreadSafeDataWriterService
{
    private readonly IServiceProvider _provider;

    public ThreadSafeDataWriterService(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task Update(Value value, CancellationToken token)
    {
        using var scope = _provider.CreateScope();
        
        var _context = scope.ServiceProvider.GetService<SyncDataBaseContext>();
        
        var entity = await _context.Set<Data>().FindAsync(value.DbId);
        
        entity.Values += value.Index + "; ";
        
        _context.Entry(entity).State = EntityState.Modified;
        
        await _context.SaveChangesAsync(token);
    }
}