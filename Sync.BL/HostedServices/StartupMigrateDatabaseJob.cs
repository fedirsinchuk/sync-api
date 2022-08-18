using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sync.DAL;
using Serilog;

namespace Sync.BL.HostedServices;

public class StartupMigrateDatabaseJob: IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public StartupMigrateDatabaseJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            Log.Information("Applying db migrations for {context} ...", typeof(SyncDataBaseContext));

            await MigrateDatabase(cancellationToken);

            Log.Information("Migrations successfully applied");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error while applying db migrations for {context}", typeof(SyncDataBaseContext));
            throw;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private async Task MigrateDatabase(CancellationToken cancellationToken)
    {
        using var serviceScope = _serviceScopeFactory.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<SyncDataBaseContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);
    }
}