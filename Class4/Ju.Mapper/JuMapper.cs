namespace Ju.Mapper
{
    public static class JuMapper
    {
        public static TDestination Map<TDestination>(object source)
        {
            if (source == null)
            {
                return default;
            }
            var target = Activator.CreateInstance<TDestination>();
            var sourceProperties = source.GetType().GetProperties();
            var targetProperties = typeof(TDestination).GetProperties();
            foreach (var targetProperty in targetProperties)
            {
                var sourceProperty = sourceProperties.FirstOrDefault(p => p.Name == targetProperty.Name);
                if (sourceProperty != null && targetProperty.CanWrite)
                {
                    var value = sourceProperty.GetValue(source);
                    targetProperty.SetValue(target, value);
                }
            }
            return target;
        }
    }
}
