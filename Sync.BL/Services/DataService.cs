using Sync.BL.Validators;
using Sync.DAL.Entities;
using Sync.DAL.Repositories;

namespace Sync.BL.Services;

public interface IDataService
{
    Task<Data> GetById(int id);
    Task<IEnumerable<Data>> GetAllAsync();
    Task AddAsync(Data model);
    Task UpdateAsync(Data model);
    Task RemoveAsync(Data model);
}

public class DataService : IDataService
{
    private readonly IDataRepository _dataRepository;

    public DataService(IDataRepository dataRepository)
    {
        _dataRepository = dataRepository;
    }

    public async Task<Data> GetById(int id)
    {
        var result =await _dataRepository.GetByIdAsync(id);

        BasicValidators.EntityNotFoundValidator(id, result);
        
        return result;
    }
    public async Task<IEnumerable<Data>> GetAllAsync()
    {
        return await _dataRepository.GetAllAsync();
    }
    public async Task AddAsync(Data model)
    {
        BasicValidators.AddNewDataValidator(model);

        await _dataRepository.AddAsync(model);
    }
    public async Task UpdateAsync(Data model)
    {
        BasicValidators.UpdateNewDataValidator(model);

        var entity = await _dataRepository.GetByIdAsync(model.Id);

        BasicValidators.EntityNotFoundValidator(model, entity);

        entity.Name = model.Name;
        entity.Values = model.Values;
        
        await _dataRepository.UpdateAsync(entity);
    }
    public async Task RemoveAsync(Data model)
    {
        var entity = await _dataRepository.GetByIdAsync(model.Id);
        
        BasicValidators.EntityNotFoundValidator(model, entity);
        
        await _dataRepository.RemoveAsync(entity);
    }
    
  
}