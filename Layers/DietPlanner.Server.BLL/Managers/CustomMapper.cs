using System.Linq;
using System.Reflection;

using DietPlanner.Server.BLL.Interfaces;
using DietPlanner.Shared.ExtensionMethods;

namespace DietPlanner.Server.BLL.Managers
{
    public class CustomMapper : ICustomMapper
    {
        public T Map<T, D>(D dto, T entity)
        {
            PropertyInfo[] entityProperties = entity.GetType().GetProperties();
            PropertyInfo[] dtoTypeProperties = dto.GetType().GetProperties();

            foreach (PropertyInfo dtoProperty in dtoTypeProperties)
            {
                if (dtoProperty.IsNull())
                    continue;
                PropertyInfo entityProperty = entityProperties
                    .FirstOrDefault(x =>
                    x.Name.Equals(dtoProperty.Name) &&
                    dtoProperty.CanRead &&
                    x.CanWrite &&
                    x.PropertyType.IsPublic &&
                    dtoProperty.PropertyType.IsPublic &&
                    x.PropertyType.Equals(dtoProperty.PropertyType));
           
                entityProperty?.SetValue(entity, dtoProperty.GetValue(dto));
            }
            return entity;

        }
    }
}
