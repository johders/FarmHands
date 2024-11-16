using Mde.Project.Core.Enums;

namespace Mde.Project.Core.Entities
{
    public class Offer
    {
		public string Id { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public string Variant { get; set; }
		public string Description { get; set; }
        public Unit Unit { get; set; }
        public string FarmId { get; set; }
        public Farm Farm { get; set; }
        public decimal Price { get; set; }
        public string OfferImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsOrganic { get; set; }

    }
}
