using Mde.Project.Core.Entities;
using Mde.Project.Core.Enums;

namespace Mde.Project.Core.Services.Models.RequestModels
{
    public class OfferEditRequestModel
    {
		public Guid Id { get; set; }
		public Product Product { get; set; }
		public string Description { get; set; }
		public Unit Unit { get; set; }
		public Farm Farm { get; set; }
		public decimal Price { get; set; }
		public string OfferImageUrl { get { return Product.ImageUrl; } }
		public bool IsAvailable { get; set; }
		public bool IsOrganic { get; set; }
	}
}
