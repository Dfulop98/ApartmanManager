using DTOLayer.Models;

namespace DTOLayer.Factories
{
    public class UniversalDtoFactory
    {
        public static UniversalDTO CreateFromObject(object obj, IEnumerable<string> propertyNames)
        {
            var dto = new UniversalDTO();
            var type = obj.GetType();

            foreach (var propertyName in propertyNames)
            {
                var property = type.GetProperty(propertyName);
                if (property != null)
                {
                    var value = property.GetValue(obj);
                    dto.SetProperty(propertyName, value);
                }
            }

            return dto;
        }
        public static List<UniversalDTO> CreateListFromObjects<T>(IEnumerable<T> objects, List<string> propertiesToInclude = null)
        {
            var dtoList = new List<UniversalDTO>();

            foreach (T obj in objects)
            {
                UniversalDTO dto = CreateFromObject(obj, propertiesToInclude);
                dtoList.Add(dto);
            }

            return dtoList;
        }
    }

}
