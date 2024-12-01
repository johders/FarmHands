using Google.Cloud.Firestore;

namespace Mde.Project.Core.Data.Firestore
{
    public class EnumConverter<T> : IFirestoreConverter<T> where T : struct, Enum
    {
        public T FromFirestore(object value)
        {
            if (value is string stringValue && Enum.TryParse<T>(stringValue, out var result))
            {
                return result;
            }

            throw new ArgumentException($"Cannot convert {value} to {typeof(T).Name}");
        }

        public object ToFirestore(T value) => value.ToString();
    }
}
