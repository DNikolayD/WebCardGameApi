namespace WebCardGame.Common
{
    public static class AutoMapper
    {
        public static object MapTo(this object entity, Type type)
        {
            var result = (Type)Activator.CreateInstance(type);
            result.GetProperties().Where(p => p.CanRead && p.CanWrite && p.GetType().IsValueType).ToList().ForEach(p => p.SetValue(p, entity.GetType().GetProperty(p.Name).GetValue(entity)));
            result.GetProperties().Where(p => p.CanWrite && p.CanRead && p.GetType().IsGenericType).ToList().FindAll(p => p.GetValue(p) != null).ForEach(p =>
            {
                p.SetValue(p, (entity.GetType().GetProperty(p.Name).GetValue(entity) as List<IBaseEntity>).Select(e => e.MapTo(p.GetType())));
            });

            return result;
        }
    }
}
