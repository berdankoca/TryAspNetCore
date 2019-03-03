using System.Linq;
using System.Reflection;
using AutoMapper;

public class DomainProfile : Profile
{
    public DomainProfile()
    {
        //We can create a automapper attribute for mapping instance
        var types = Assembly.GetExecutingAssembly().GetTypes();
        var dtoTypes = types.Where(t => t.Name.EndsWith("Dto"));
        foreach (var dtoType in dtoTypes)
        {
            var type = types.FirstOrDefault(t => t.Name == dtoType.Name.Replace("Dto", ""));
            if (type == null)
                return;

            CreateMap(type, dtoType);
            CreateMap(dtoType, type);
        }
    }

}