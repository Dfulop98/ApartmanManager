namespace DTOLayer.Models
{
    public class UniversalDTO
    {
        public Dictionary<string, object> Properties { get; set; }

        public UniversalDTO()
        {
            Properties = new Dictionary<string, object>();
        }

        public T GetProperty<T>(string propertyName)
        {
            if (Properties.TryGetValue(propertyName, out object value))
            {
                return (T)value;
            }

            return default(T);
        }

        public void SetProperty<T>(string propertyName, T value)
        {
            Properties[propertyName] = value;
        }
    }

}
