using Mde.Project.Core.Entities;
using Mde.Project.Core.Enums;

namespace Mde.Project.Core.Data
{
	public static class Seeder
	{
		public static IEnumerable<Product> SeedProducts()
		{
			IEnumerable<Product> products = new List<Product>
				{
					new Product(Guid.Parse("00000000-0000-0000-0000-000000000001"), "Apples", "Freshly picked, juicy apples.","https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS1jkLfZNwVPgxhe8FlmI3gZo6GIYhkzOiftg&s"),
					new Product(Guid.Parse("00000000-0000-0000-0000-000000000002"), "Carrots", "Organic carrots, straight from the farm.", "https://ucarecdn.com/459eb7be-115a-4d85-b1d8-deaabc94c643/-/format/auto/-/preview/3000x3000/-/quality/lighter/"),
					new Product(Guid.Parse("00000000-0000-0000-0000-000000000003"), "Free-Range Eggs", "Dozen of farm fresh eggs.", "https://cdn.britannica.com/94/151894-050-F72A5317/Brown-eggs.jpg"),
					new Product(Guid.Parse("00000000-0000-0000-0000-000000000004"), "Milk", "Fresh cow milk, non-pasteurized.", "https://t3.ftcdn.net/jpg/03/30/09/16/360_F_330091642_6AniY6wGxENL6WCzdrHpLhu3Y2HrcWuY.jpg"),
					new Product(Guid.Parse("00000000-0000-0000-0000-000000000005"), "Tomatoes", "Plump and ripe tomatoes.", "https://www.uvm.edu/content/shared/files/styles/1200/public/uvm-extension-cultivating-healthy-communities/tomatoes2-e.jpg?t=rpri8o"),
					new Product(Guid.Parse("00000000-0000-0000-0000-000000000006"), "Pumpkin", "Seasonal pumpkins, great for pies.", "https://upload.wikimedia.org/wikipedia/commons/5/5c/FrenchMarketPumpkinsB.jpg"),
					new Product(Guid.Parse("00000000-0000-0000-0000-000000000007"), "Strawberries", "Sweet and juicy strawberries.", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSRXKcGr0rnAeasxCbSBGSPlWo8rOGnTJM7hQ&s"),
					new Product(Guid.Parse("00000000-0000-0000-0000-000000000008"), "Honey", "Organic honey from local bees.", "https://3.imimg.com/data3/NG/IC/MY-11241820/lychee-honey-250x250.jpg"),
					new Product(Guid.Parse("00000000-0000-0000-0000-000000000009"), "Potatoes", "Golden potatoes, perfect for mashing.", "https://live2giveorganics.nz/cdn/shop/products/E1CA67E3-4A64-4388-9454-F9AC247D0693_1_201_a.jpg?v=1667075696&width=1904"),
					new Product(Guid.Parse("00000000-0000-0000-0000-000000000010"), "Spinach", "Fresh spinach leaves, great for salads.", "https://www.realfoodco.co.za/cdn/shop/products/spinach_1024x1024.jpg?v=1580301409"),
					new Product(Guid.Parse("00000000-0000-0000-0000-000000000011"), "Oranges", "Citrusy and sweet oranges.", "https://www.sunshinecooperative.co.uk/wp-content/uploads/2018/09/oranges-2100108_1280.jpg"),
					new Product(Guid.Parse("00000000-0000-0000-0000-000000000012"), "Zucchini", "Fresh zucchini, straight from the garden.", "https://www.edenbrothers.com/cdn/shop/products/squash-organic-dark-green-zucchini-shk-1.jpg?v=1653502252"),
					new Product(Guid.Parse("00000000-0000-0000-0000-000000000013"), "Cherries", "Sweet and tart cherries, perfect for snacks.", "https://www.edible-garden.co/wp-content/uploads/2024/05/Cherries.jpg"),
					new Product(Guid.Parse("00000000-0000-0000-0000-000000000014"), "Broccoli", "Crisp and green broccoli heads.", "https://www.groworganic.com/cdn/shop/products/broccoli-calabrese.jpg?v=1570226464&width=800"),
					new Product(Guid.Parse("00000000-0000-0000-0000-000000000015"), "Peaches", "Delicious peaches, full of flavor.", "https://www.froghollow.com/cdn/shop/products/gold_dust_3_lr_sq_a7ab3913-b5a1-42b3-8374-6deda4e3411a_300x300.jpg?v=1691172993"),
					new Product(Guid.Parse("00000000-0000-0000-0000-000000000016"), "Lettuce", "Crisp, green lettuce for salads.", "https://wearelittlefarms.com/cdn/shop/products/image_35b45e84-cb02-4af8-96e0-af532e97ed6a_1500x.heic?v=1668155846"),
					new Product(Guid.Parse("00000000-0000-0000-0000-000000000017"), "Watermelon", "Large, juicy watermelons.", "https://delvinfarms.com/wp-content/uploads/2015/08/DSC_0276.jpg"),
					new Product(Guid.Parse("00000000-0000-0000-0000-000000000018"), "Garlic", "Organic garlic bulbs.", "https://natureandnurtureseeds.com/cdn/shop/collections/IMG_2367_garlic_website.jpg?v=1679510965"),
					new Product(Guid.Parse("00000000-0000-0000-0000-000000000019"), "Corn", "Sweet corn on the cob, fresh from the field.", "https://cdn.shopify.com/s/files/1/0569/0615/4154/files/iStock-119709782-9-scaled.jpg"),
					new Product(Guid.Parse("00000000-0000-0000-0000-000000000020"), "Cabbage", "Green cabbage heads, perfect for slaw.", "https://www.seedway.com/app/uploads/2019/05/Reaction-bj-4.jpg")
				};

			return products;
		}

		public static IEnumerable<Farm> SeedFarms()
		{
			IEnumerable<Farm> farms = new List<Farm>
			{
				new Farm(Guid.Parse("10000000-0000-0000-0000-000000000001"), "Green Valley Orchards", "Nestled in the hills, this family-owned orchard specializes in organic apples, pears, and seasonal berries, grown with sustainable farming practices.", 51.2564, 3.0432, new List<Product>(), "https://statbel.fgov.be/sites/default/files/styles/news_full/public/images/landbouw/8.2%20Biologische%20landbouw/AdobeStock_200879536.jpeg?itok=9JTAiqrQ"),
				new Farm(Guid.Parse("10000000-0000-0000-0000-000000000002"), "Sunny Acres Farm", "Known for its sprawling sunflower fields and fresh vegetables, this farm offers a delightful experience with pick-your-own options.", 51.2593, 3.0481, new List<Product>(), "https://www.nationsencyclopedia.com/photos/belgium-agriculture-1547.jpg"),
				new Farm(Guid.Parse("10000000-0000-0000-0000-000000000003"), "Riverside Pastures", "Situated along the riverbank, Riverside Pastures is famous for its pasture-raised eggs, dairy products, and heritage breed livestock.", 51.2666, 3.0395, new List<Product>(), "https://www.theclimakers.org/wp/wp-content/uploads/2021/11/Boerenbond-Belgian-Farmers-Union.png"),
				new Farm(Guid.Parse("10000000-0000-0000-0000-000000000004"), "Maple Leaf Gardens", "A picturesque farm with an array of maple trees, offering fresh produce and locally produced maple syrup straight from the source.", 51.2670, 3.0569, new List<Product>(), "https://www.shutterstock.com/image-photo/dark-clouds-over-field-young-600nw-2179706511.jpg"),
				new Farm(Guid.Parse("10000000-0000-0000-0000-000000000005"), "Harvest Moon Homestead", "This small yet abundant farm is dedicated to cultivating heirloom tomatoes, peppers, and herbs with an emphasis on organic and permaculture techniques.", 51.2752, 3.0497, new List<Product>(), "https://growingformarket.com/custom/2022%20Issues/may%202022/cas/web%204%20CSA%20members%20Klara%20and%20Marijke%20harvesting.jpg"),
				new Farm(Guid.Parse("10000000-0000-0000-0000-000000000006"), "Breezy Hill Farm", "With a focus on sustainable grains and legumes, Breezy Hill Farm is a local favorite for artisanal bread flour and fresh beans.", 51.2557, 3.0604, new List<Product>(), "https://lh-images.us-east-1.linodeobjects.com/4005.jpg"),
				new Farm(Guid.Parse("10000000-0000-0000-0000-000000000007"), "Lavender Lane", "A serene haven with rows of aromatic lavender, this farm also produces lavender-infused honey, soaps, and teas for a unique farm experience.", 51.2473, 3.0270, new List<Product>(), "https://diplomatie.belgium.be/sites/default/files/styles/fluid_image/public/2022-04/shutterstock_436506028.jpg?itok=0vXbgD-t"),
			};

			return farms;
		}

		public static IEnumerable<Offer> SeedFarmOffers()
		{
			var products = SeedProducts();
			var farms = SeedFarms();

			IEnumerable<Offer> offers = new List<Offer>
			{
				new Offer
				{
					Id = Guid.Parse("20000000-0000-0000-0000-000000000001"),
					Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000005")),
					Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000002")),
					Unit = Unit.Kilogram,
					Price = 1.89m,
					IsAvailable = true,
					IsOrganic = false
                },
                new Offer
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                    Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000009")),
                    Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000002")),
                    Unit = Unit.Kilogram,
                    Price = 1.50m
                },
                new Offer
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000003"),
                    Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000014")),
                    Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000002")),
                    Unit = Unit.Piece,
                    Price = 1.60m,
                    IsAvailable = true,
                    IsOrganic = true
                },

				new Offer
				{
					Id = Guid.Parse("20000000-0000-0000-0000-000000000004"),
					Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000003")),
					Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000003")),
					Unit = Unit.Dozen,
					Price = 5.5m,
					IsAvailable = true,
					IsOrganic = true
				},
				new Offer
				{
					Id = Guid.Parse("20000000-0000-0000-0000-000000000005"),
					Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000004")),
					Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000003")),
					Unit = Unit.Litre,
					Price = 2.50m,
					IsAvailable = true,
					IsOrganic = true
				},

				new Offer
				{
					Id = Guid.Parse("20000000-0000-0000-0000-000000000006"),
					Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000007")),
					Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000007")),
					Unit = Unit.Kilogram,
					Price = 14m,
					IsAvailable = true,
					IsOrganic = true
				},
				new Offer
				{
					Id = Guid.Parse("20000000-0000-0000-0000-000000000007"),
					Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000008")),
					Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000007")),
					Unit = Unit.Piece,
					Price = 7.50m,
					IsAvailable = true,
					IsOrganic = true
				},
			};

			return offers;
		}

        public static IEnumerable<FavoriteFarm> SeedFavoriteFarms()
        {
            IEnumerable<FavoriteFarm> options = new List<FavoriteFarm>
            {
                new FavoriteFarm{ 
					Id = Guid.Parse("30000000-0000-0000-0000-000000000001"),
					UserId = Guid.Parse("40000000-0000-0000-0000-000000000001"),
                    FarmId = Guid.Parse("10000000-0000-0000-0000-000000000002")
                },
                new FavoriteFarm{
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000002"),
                    UserId = Guid.Parse("40000000-0000-0000-0000-000000000002"),
                    FarmId = Guid.Parse("10000000-0000-0000-0000-000000000007")
                },
                new FavoriteFarm{
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000003"),
                    UserId = Guid.Parse("40000000-0000-0000-0000-000000000003"),
                    FarmId = Guid.Parse("10000000-0000-0000-0000-000000000003")
                },
            };

            return options;
        }

        public static IEnumerable<DietaryOption> SeedDietaryOptions()
		{
			IEnumerable<DietaryOption> options = new List<DietaryOption>
			{
				new DietaryOption(1, "Vegetarian", true),
				new DietaryOption(2, "Vegan", false),
				new DietaryOption(3, "Halal", false),
				new DietaryOption(4, "Gluten Free", true),
				new DietaryOption(5, "Diary free", false),
			};

			return options;
		}

		public static IEnumerable<CuisineOption> SeedCuisineOptions()
		{
			IEnumerable<CuisineOption> options = new List<CuisineOption>
			{
				new CuisineOption(1, "Mexican", "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Flag_of_Mexico.svg/640px-Flag_of_Mexico.svg.png"),
				new CuisineOption(2, "Indian", "https://thumbs.dreamstime.com/b/india-paper-flag-patriotic-background-national-138241478.jpg"),
				new CuisineOption(3, "Japanese", "https://upload.wikimedia.org/wikipedia/en/thumb/9/9e/Flag_of_Japan.svg/1200px-Flag_of_Japan.svg.png"),
				new CuisineOption(4, "Italian", "https://upload.wikimedia.org/wikipedia/en/0/03/Flag_of_Italy.svg"),
				new CuisineOption(5, "Thai", "https://cdn.britannica.com/38/4038-050-BDDBA6AB/Flag-Thailand.jpg"),
			};

			return options;
		}
	}

		
}
