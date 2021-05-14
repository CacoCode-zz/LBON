using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using LBON.EntityFrameworkCore.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LBON.Helper;

namespace LBON.EntityFrameworkCore.Extensions
{
    public static class ExtendableObjectExtensions
    {
        public static T GetData<T>([NotNull] this IExtendableObject extendableObject, [NotNull] string name, bool handleType = false)
        {
            return extendableObject.GetData<T>(
                name,
                handleType
                    ? new JsonSerializer { TypeNameHandling = TypeNameHandling.All }
                    : JsonSerializer.CreateDefault()
            );
        }

        public static T GetData<T>([NotNull] this IExtendableObject extendableObject, [NotNull] string name, [CanBeNull] JsonSerializer jsonSerializer)
        {
            if (extendableObject.ExtensionData == null)
            {
                return default(T);
            }

            var json = JObject.Parse(extendableObject.ExtensionData);

            var prop = json[name];
            if (prop == null)
            {
                return default(T);
            }

            if (TypeHelper.IsPrimitiveExtendedIncludingNullable(typeof(T)))
            {
                return prop.Value<T>();
            }
            else
            {
                return (T)prop.ToObject(typeof(T), jsonSerializer ?? JsonSerializer.CreateDefault());
            }
        }



        public static void SetData<T>([NotNull] this IExtendableObject extendableObject, [NotNull] string name, [CanBeNull] T value, bool handleType = false)
        {
            extendableObject.SetData(
                name,
                value,
                handleType
                    ? new JsonSerializer { TypeNameHandling = TypeNameHandling.All }
                    : JsonSerializer.CreateDefault()
            );
        }

        public static void SetData<T>([NotNull] this IExtendableObject extendableObject, [NotNull] string name, [CanBeNull] T value, [CanBeNull] JsonSerializer jsonSerializer)
        {
            if (extendableObject == null)
            {
                throw new ArgumentNullException(nameof(extendableObject));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (jsonSerializer == null)
            {
                jsonSerializer = JsonSerializer.CreateDefault();
            }

            if (extendableObject.ExtensionData == null)
            {
                if (EqualityComparer<T>.Default.Equals(value, default(T)))
                {
                    return;
                }

                extendableObject.ExtensionData = "{}";
            }

            var json = JObject.Parse(extendableObject.ExtensionData);

            if (value == null || EqualityComparer<T>.Default.Equals(value, default(T)))
            {
                if (json[name] != null)
                {
                    json.Remove(name);
                }
            }
            else if (TypeHelper.IsPrimitiveExtendedIncludingNullable(value.GetType()))
            {
                json[name] = new JValue(value);
            }
            else
            {
                json[name] = JToken.FromObject(value, jsonSerializer);
            }

            var data = json.ToString(Formatting.None);
            if (data == "{}")
            {
                data = null;
            }

            extendableObject.ExtensionData = data;
        }

        public static bool RemoveData([NotNull] this IExtendableObject extendableObject, string name)
        {
            if (extendableObject == null)
            {
                throw new ArgumentNullException(nameof(extendableObject));
            }

            if (extendableObject.ExtensionData == null)
            {
                return false;
            }

            var json = JObject.Parse(extendableObject.ExtensionData);

            var token = json[name];
            if (token == null)
            {
                return false;
            }

            json.Remove(name);

            var data = json.ToString(Formatting.None);
            if (data == "{}")
            {
                data = null;
            }

            extendableObject.ExtensionData = data;

            return true;
        }
    }
}
