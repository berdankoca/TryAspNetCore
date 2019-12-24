using AutoMapper;
using AutoMapper.EquivalencyExpression;

namespace TryAspNetCore.EventManagement
{
    public class EventManagementProfile : Profile
    {
        public EventManagementProfile()
        {
            CreateMap<Event, EventDto>();
            CreateMap<EventDto, Event>().EqualityComparison((source, target) => source.Id == target.Id);

            CreateMap<EventRegistration, EventRegistrationDto>();
            CreateMap<EventRegistrationDto, EventRegistration>().EqualityComparison((source, target) => source.Id == target.Id);

            //We can create a automapper attribute for mapping instance
            // var types = Assembly.GetExecutingAssembly().GetTypes();
            // var dtoTypes = types.Where(t => t.Name.EndsWith("Dto"));
            // foreach (var dtoType in dtoTypes)
            // {
            //     var type = types.FirstOrDefault(t => t.Name == dtoType.Name.Replace("Dto", ""));
            //     if (type == null)
            //         return;

            //     CreateMap(type, dtoType);
            //     var sourceParameterExpression = Expression.Parameter(dtoType, "source");
            //     var targetParameterExpression = Expression.Parameter(type, "target");

            //     var sourcePropertyExpression = Expression.Property(sourceParameterExpression, dtoType.GetProperty("Id"));
            //     var targetPropertyExpression = Expression.Property(targetParameterExpression, type.GetProperty("Id"));
            //     var equalExpression = Expression.Equal(sourcePropertyExpression, targetPropertyExpression);

            //     var funcGenericType = typeof(Func<,,>).MakeGenericType(dtoType, type, typeof(bool));
            //     var lambdaMethodInfo = typeof(Expression).GetMethod("Lambda", 1, new Type[] { typeof(Expression), typeof(IEnumerable<ParameterExpression>) }).MakeGenericMethod(funcGenericType);
            //     // var lambdaMethodInfo = Expression.Lambda(funcGenericType, equalExpression, new[] { sourceParameterExpression, targetParameterExpression }).Compile();
            //     var expressionLambdaResult = lambdaMethodInfo.Invoke(null, new object[] { equalExpression, new ParameterExpression[] { sourceParameterExpression, targetParameterExpression } });

            //     var equalityComparisonMethod = typeof(AutoMapper.EquivalencyExpression.EquivalentExpressions)
            //         .GetMethods()
            //         .FirstOrDefault(m => m.Name == "EqualityComparison")
            //         .MakeGenericMethod(dtoType, type);

            //     var x = CreateMap(dtoType, type);
            //     equalityComparisonMethod.Invoke(x, new object[] { x, expressionLambdaResult });


            //     // CreateMap<Event, EventDto>().EqualityComparison
            // }
        }

    }
}