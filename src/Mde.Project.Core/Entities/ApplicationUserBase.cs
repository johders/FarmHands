using Google.Cloud.Firestore;
using Mde.Project.Core.Data.Firestore;
using Mde.Project.Core.Enums;

namespace Mde.Project.Core.Entities
{
    [FirestoreData]
    public class ApplicationUserBase
	{
        public ApplicationUserBase() { }

        [FirestoreDocumentId]
        public string Uid { get; set; }

        [FirestoreProperty]
        public string Email { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty(ConverterType = typeof(EnumConverter<UserRole>))]
        public UserRole Role { get; set; }
    }
}

