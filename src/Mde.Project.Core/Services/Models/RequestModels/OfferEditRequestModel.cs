using Mde.Project.Core.Entities;
using Mde.Project.Core.Enums;

namespace Mde.Project.Core.Services.Models.RequestModels
{
    public class OfferEditRequestModel
    {
		public string Id { get; set; }
		public Product Product { get; set; }
		public string Description { get; set; }
		public string Variant { get; set; }
		public Unit Unit { get; set; }
		public Farm Farm { get; set; }
		public decimal Price { get; set; }
		public string OfferImageUrl { get; set; }
		public bool IsAvailable { get; set; }
		public bool IsOrganic { get; set; }
	}
}
