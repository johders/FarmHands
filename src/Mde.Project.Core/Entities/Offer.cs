using Mde.Project.Core.Enums;

namespace Mde.Project.Core.Entities
{
    public class Offer
    {
		//public Offer(Guid id, Guid productId, Guid farmId, Unit unit, decimal price, bool isAvailable = false, bool isOrganic = false)
		//{
		//	Id = id;
		//	Product = product;
		//	Unit = unit;
		//	Price = price;
		//	IsAvailable = isAvailable;
		//	IsOrganic = isOrganic;
		//}

		public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
		public Unit Unit { get; set; }
        public Guid FarmId { get; set; }
        public Farm Farm { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsOrganic { get; set; }

    }
}
