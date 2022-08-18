using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sync.BL.HostedServiceQueue;
using Sync.BL.Services;
using Sync.DAL;
using Sync.DAL.Entities;

namespace Sync.BL.HostedServices
{
    public class DbWriter : BackgroundService
    {
        private readonly IThreadSafeReader _reader;
        private readonly IThreadSafeDataWriterService _threadSafeDataWriterService;
        
        public DbWriter(IThreadSafeReader reader, IServiceProvider provider,  IThreadSafeDataWriterService threadSafeDataWriterService)
        {
            _reader = reader;
            _threadSafeDataWriterService = threadSafeDataWriterService;
        }
    
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var _value = await _reader.ReadAsync();
                await _threadSafeDataWriterService.Update(_value,stoppingToken);
            }
        }
    }
}

