using DTOLayer.Models;
using System.Collections;

namespace DTOLayer.Factories
{
    public class UniversalDtoFactory
    {
        public static UniversalDTO CreateFromObject(object obj, IEnumerable<string> propertyNames, IDictionary<string, IEnumerable<string>> includedProperties = null)
        {
            var dto = new UniversalDTO();
            var type = obj.GetType();

            foreach (var propertyName in propertyNames)
            {
                var property = type.GetProperty(propertyName);
                if (property != null)
                {
                    var value = property.GetValue(obj);
                    if (value is IEnumerable && !(value is string))
                    {
                        var collection = (IEnumerable)value;
                        var list = new List<UniversalDTO>();
                        foreach (var item in collection)
                        {
                            list.Add(CreateFromObject(item, includedProperties != null && includedProperties.ContainsKey(propertyName) ? includedProperties[propertyName] : item.GetType().GetProperties().Select(p => p.Name)));
                        }
                        dto.SetProperty(propertyName, list);
                    }
                    else
                    {
                        dto.SetProperty(propertyName, value);
                    }
                }
            }

            return dto;
        }

        public static List<UniversalDTO> CreateListFromObjects<T>(IEnumerable<T> objects, List<string> propertiesToInclude = null, IDictionary<string, IEnumerable<string>> includedProperties = null)
        {
            var dtoList = new List<UniversalDTO>();

            if (objects == null)
            {
                throw new ArgumentNullException(nameof(objects), "Objects parameter must not be null.");
            }

            foreach (T obj in objects)
            {
                UniversalDTO dto = CreateFromObject(obj, propertiesToInclude, includedProperties);
                dtoList.Add(dto);
            }

            return dtoList;
        }
    }

}
