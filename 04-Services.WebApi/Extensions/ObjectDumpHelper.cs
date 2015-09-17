using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace _04_Services.WebApi.Extensions
{
    /// <summary>
    /// general dump helper 
    /// </summary>
    public class ObjectDumpHelper
    {
        /// <summary>
        /// Clean the File from invalid characters
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>valid file name</returns>
        public static string CleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars()
                .Aggregate(fileName,
                    (current, c) => current.Replace(c.ToString(CultureInfo.InvariantCulture), string.Empty));
        }

        /// <summary>
        /// has the class defined the attribute?
        /// </summary>
        /// <param name="typeClass"></param>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public static bool HasAttribute(Type typeClass, Type attributeType)
        {
            return Attribute.IsDefined(typeClass, attributeType);
        }

        /// <summary>
        /// return name of given object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>return name of given object</returns>
        public static string GetObjectName(object obj)
        {
            return (obj.GetType().IsGenericType && obj is IEnumerable) ? (obj as IEnumerable).AsQueryable().ElementType.Name : obj.GetType().Name;
        }


        /// <summary>
        /// set whether the object is final and should be dump into text or it should get deep into inspection so the
        /// recursion should be called again 
        /// </summary>
        /// <param name="type">type of the object under inspection</param>
        /// <param name="memberInfo"></param>
        /// <returns>true - do not go deeper inside the object, false - go deeper into object and dump the object properties</returns>
        public static bool IsObjectFinal(Type type, MemberInfo memberInfo)
        {
            return type.IsValueType
                   || (type.IsSerializable )
                   || type == typeof(string)
                   //|| (memberInfo.Module.Assembly).FullName.StartsWith("EntityFramework")
                   || memberInfo.Name.StartsWith("_entityWrapper");
        }

        /// <summary>
        /// exclude the entity proxy objects
        /// </summary>
        /// <param name="type"></param>
        /// <returns>true - exclude the object type, false - do not exclude</returns>
        public static bool ExcludeObject(Type type)
        {
            return type.FullName.StartsWith("System.Data.Entity.Core.Objects.Internal");
        }
    }
}
