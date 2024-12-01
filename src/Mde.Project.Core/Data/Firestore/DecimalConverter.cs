using Google.Cloud.Firestore;

namespace Mde.Project.Core.Data.Firestore
{
    public class DecimalConverter : IFirestoreConverter<decimal>
    {
        public object ToFirestore(decimal value)
        {
            return (double)value;
        }

        public decimal FromFirestore(object value)
        {
            if (value is double doubleValue)
            {
                return (decimal)doubleValue;
            }

            throw new ArgumentException($"Cannot convert {value} to decimal.");
        }
    }
}
