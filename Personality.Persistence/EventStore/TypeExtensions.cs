using System;
using System.Linq;

namespace Personality.Persistence.EventStore
{
    /// <summary>
    /// Расширения типа
    /// </summary>
    public static class TypeExtensions
    {
        //https://stackoverflow.com/questions/8868119/find-all-parent-types-both-base-classes-and-interfaces
        public static bool InheritsFrom(this Type type, Type baseType)
        {
            if (type == null)
            {
                return false;
            }

            if (baseType == null)
            {
                return type.IsInterface || type == typeof(object);
            }

            if (baseType.IsInterface)
            {
                return type.GetInterfaces().Contains(baseType);
            }

            var currentType = type;
            while (currentType != null)
            {
                if (currentType.BaseType == baseType)
                {
                    return true;
                }

                currentType = currentType.BaseType;
            }

            return false;
        }
    }
}
