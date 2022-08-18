using Sync.DAL.Entities;

namespace Sync.BL.Validators;

public static class BasicValidators
{
    public static void UpdateNewDataValidator(Data model)
    {
        AddNewDataValidator(model);

        if (model.Id == 0)
            throw new ArgumentNullException(nameof(model.Id));
    }
    
    public static void AddNewDataValidator(Data model)
    {
        if (model is null)
            throw new ArgumentNullException(nameof(model));

        if (string.IsNullOrEmpty(model.Name))
            throw new ArgumentNullException(nameof(model.Name));
    }
    
    public static void EntityNotFoundValidator(Data model, Data entity)
    {
        if (entity is null)
            throw new ArgumentException($"Entity not found in DataBase with Id: {model.Id}");
    }
    
    public static void EntityNotFoundValidator(int id, Data entity)
    {
        if (entity is null)
            throw new ArgumentException($"Entity not found in DataBase with Id: {id}");
    }
    
}