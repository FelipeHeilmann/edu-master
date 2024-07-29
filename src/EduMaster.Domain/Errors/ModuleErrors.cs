namespace EduMaster.Domain.Errors;
public class ModuleErrors
{
    public static Shared.Error ModuleWithSameName => Shared.Error.Conflict("Module.With.Same.Name", "There is already a module with this name");        
    public static Shared.Error ModuleNotFound => Shared.Error.NotFound("Module.Not.Found", "The module was not found");        
}
