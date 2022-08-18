using Microsoft.EntityFrameworkCore;
using Sync.BL.HostedServiceQueue;
using Sync.BL.HostedServices;
using Sync.BL.Services;
using Sync.DAL;
using Sync.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var _dbConnection = Environment.GetEnvironmentVariable("DbConnection");


builder.Services.AddDbContext<SyncDataBaseContext>(
    options => options.UseNpgsql(_dbConnection,optionsBuilder => optionsBuilder.MigrationsAssembly("Sync.DAL")));

builder.Services.AddScoped<IDataRepository, DataRepository>();

builder.Services.AddScoped<IDataService, DataService>();

builder.Services.AddHostedService<StartupMigrateDatabaseJob>();

builder.Services.AddHostedService<DbWriter>();

builder.Services.AddSingleton<DbWriterQueue>();

builder.Services.AddSingleton<IThreadSafeWriter>(sp => sp.GetService<DbWriterQueue>() );

builder.Services.AddSingleton<IThreadSafeReader>(sp => sp.GetService<DbWriterQueue>() );

builder.Services.AddSingleton<IThreadSafeDataWriterService, ThreadSafeDataWriterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();