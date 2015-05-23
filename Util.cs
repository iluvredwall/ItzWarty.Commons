using System;
using System.Linq;
using System.Reflection;

namespace Dargon.Commons
{

   public unsafe static class Util
   {
      /// <summary>
      /// Gets the attribute of Enum value
      /// </summary>
      /// <typeparam name="TAttribute"></typeparam>
      /// <param name="enumValue"></param>
      /// <returns></returns>
      public static TAttribute GetAttributeOrNull<TAttribute>(this Enum enumValue)
         where TAttribute : Attribute
      {
         var enumType = enumValue.GetType();
         var memberInfo = enumType.GetTypeInfo().DeclaredMembers.First(member => member.Name.Equals(enumValue.ToString()));
         var attributes = memberInfo.GetCustomAttributes(typeof(TAttribute), false);
         return (TAttribute)attributes.FirstOrDefault();
      }

      public static TAttribute GetAttributeOrNull<TAttribute>(this object instance)
         where TAttribute : Attribute
      {
         var attributes = instance.GetType().GetTypeInfo().GetCustomAttributes(typeof(TAttribute), false);
         return (TAttribute)attributes.FirstOrDefault();
      }

      

      public static TValue KeepExisting<TKey, TValue>(TKey key, TValue value) {
         return value;
      }
   }
}
