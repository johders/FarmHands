using Mde.Project.Core.Entities;
using Mde.Project.Core.Enums;
using System.Collections.ObjectModel;

namespace Mde.Project.Core.Data
{
	public static class Seeder
	{
		public static IEnumerable<Product> SeedProducts()
		{
			IEnumerable<Product> products = new List<Product>
				{
					new Product(Guid.Parse("ed424125-427c-4959-bcf8-4dd73b2691e1"), "Apples", "Freshly picked, juicy apples.","https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS1jkLfZNwVPgxhe8FlmI3gZo6GIYhkzOiftg&s"),
					new Product(Guid.Parse("406af47f-1bef-4b6d-86d1-c55cfefdc85d"), "Carrots", "Organic carrots, straight from the farm.", "https://ucarecdn.com/459eb7be-115a-4d85-b1d8-deaabc94c643/-/format/auto/-/preview/3000x3000/-/quality/lighter/"),
					new Product(Guid.Parse("cf387bb6-f724-4718-b3f3-09613a4022f3"), "Free-Range Eggs", "Dozen of farm fresh eggs.", "https://cdn.britannica.com/94/151894-050-F72A5317/Brown-eggs.jpg"),
					new Product(Guid.Parse("75197c7e-eac1-402b-b187-f2974f78dc85"), "Milk", "Fresh cow milk, non-pasteurized.", "https://t3.ftcdn.net/jpg/03/30/09/16/360_F_330091642_6AniY6wGxENL6WCzdrHpLhu3Y2HrcWuY.jpg"),
					new Product(Guid.Parse("12bc9732-e3a4-4c14-ad94-214a0e6a9592"), "Tomatoes", "Plump and ripe tomatoes.", "https://www.uvm.edu/content/shared/files/styles/1200/public/uvm-extension-cultivating-healthy-communities/tomatoes2-e.jpg?t=rpri8o"),
					new Product(Guid.Parse("70ff9cd2-82d8-4235-b10d-56e7c9c54af2"), "Pumpkin", "Seasonal pumpkins, great for pies.", "https://upload.wikimedia.org/wikipedia/commons/5/5c/FrenchMarketPumpkinsB.jpg"),
					new Product(Guid.Parse("bf522a56-aecf-48ba-9516-1fc626d6fb64"), "Strawberries", "Sweet and juicy strawberries.", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSRXKcGr0rnAeasxCbSBGSPlWo8rOGnTJM7hQ&s"),
					new Product(Guid.Parse("8e35ca1e-5616-4e26-a39b-6c1244b8cf8c"), "Honey", "Organic honey from local bees.", "https://3.imimg.com/data3/NG/IC/MY-11241820/lychee-honey-250x250.jpg"),
					new Product(Guid.Parse("158649b9-0ab4-4eca-93b4-b0ff95669596"), "Potatoes", "Golden potatoes, perfect for mashing.", "https://live2giveorganics.nz/cdn/shop/products/E1CA67E3-4A64-4388-9454-F9AC247D0693_1_201_a.jpg?v=1667075696&width=1904"),
					new Product(Guid.Parse("d1f7f8d4-c5d2-49e4-bd01-17be46663db8"), "Spinach", "Fresh spinach leaves, great for salads.", "https://www.realfoodco.co.za/cdn/shop/products/spinach_1024x1024.jpg?v=1580301409"),
					new Product(Guid.Parse("123bb355-334f-4dcc-88eb-eab5966b22f4"), "Oranges", "Citrusy and sweet oranges.", "https://www.sunshinecooperative.co.uk/wp-content/uploads/2018/09/oranges-2100108_1280.jpg"),
					new Product(Guid.Parse("6a26b82a-dca3-4328-b293-249a8aa52b73"), "Zucchini", "Fresh zucchini, straight from the garden.", "https://www.edenbrothers.com/cdn/shop/products/squash-organic-dark-green-zucchini-shk-1.jpg?v=1653502252"),
					new Product(Guid.Parse("1b06db28-629f-4a09-b87d-7961154a6d5e"), "Cherries", "Sweet and tart cherries, perfect for snacks.", "https://www.edible-garden.co/wp-content/uploads/2024/05/Cherries.jpg"),
					new Product(Guid.Parse("0a1312b0-5310-47c8-99c9-2ea00e027481"), "Broccoli", "Crisp and green broccoli heads.", "https://www.groworganic.com/cdn/shop/products/broccoli-calabrese.jpg?v=1570226464&width=800"),
					new Product(Guid.Parse("21efe678-ed19-4e4e-951c-5efdbdcfbabb"), "Peaches", "Delicious peaches, full of flavor.", "https://www.froghollow.com/cdn/shop/products/gold_dust_3_lr_sq_a7ab3913-b5a1-42b3-8374-6deda4e3411a_300x300.jpg?v=1691172993"),
					new Product(Guid.Parse("03080775-9ed4-4b27-8aa5-75f034bf0b3b"), "Lettuce", "Crisp, green lettuce for salads.", "https://wearelittlefarms.com/cdn/shop/products/image_35b45e84-cb02-4af8-96e0-af532e97ed6a_1500x.heic?v=1668155846"),
					new Product(Guid.Parse("b5c41144-66ff-4e87-9ea3-657a36a8d21e"), "Watermelon", "Large, juicy watermelons.", "https://delvinfarms.com/wp-content/uploads/2015/08/DSC_0276.jpg"),
					new Product(Guid.Parse("597df337-9651-4c87-90cc-1363043b8893"), "Garlic", "Organic garlic bulbs.", "https://natureandnurtureseeds.com/cdn/shop/collections/IMG_2367_garlic_website.jpg?v=1679510965"),
					new Product(Guid.Parse("8f1398ea-d223-4dcf-8a16-db61c48ca42d"), "Corn", "Sweet corn on the cob, fresh from the field.", "https://cdn.shopify.com/s/files/1/0569/0615/4154/files/iStock-119709782-9-scaled.jpg"),
					new Product(Guid.Parse("9d2015e1-1065-4c8a-85de-d552e14580bc"), "Cabbage", "Green cabbage heads, perfect for slaw.", "https://www.seedway.com/app/uploads/2019/05/Reaction-bj-4.jpg")
				};

			return products;
		}

		public static IEnumerable<Offer> SeedFarmOffers()
		{
			var products = SeedProducts();

			IEnumerable<Offer> offers = new List<Offer>
			{
				new Offer(Guid.Parse("d0c33cc1-bf85-47fc-b438-2bde03ef7c61"), products.ElementAt(1), Unit.Kilogram, 1.89m, false, true),
				new Offer(Guid.Parse("8000422f-cfe9-4b5e-941d-c18a2a28e0ec"), products.ElementAt(8), Unit.Kilogram, 1.50m),
				new Offer(Guid.Parse("18b02357-ac0d-41da-9d57-aab403e9896d"), products.ElementAt(13),Unit.Piece, 1.60m, true, true),
				new Offer(Guid.Parse("cd8b3a94-895b-4fb9-9080-ed7c497fd388"), products.ElementAt(3), Unit.Litre, 1.20m),
				new Offer(Guid.Parse("b8337eee-87a5-43db-8a5b-f951bbb0e661"), products.ElementAt(6), Unit.Kilogram, 6.99m, true, true),
				new Offer(Guid.Parse("d0c33cc1-bf85-47fc-b438-4bde03ef7c61"), products.ElementAt(1), Unit.Kilogram, 1.89m, false, true),
				new Offer(Guid.Parse("8000422f-cfe9-4b5e-941d-e18a2a28e0ec"), products.ElementAt(8), Unit.Kilogram, 1.50m),
				new Offer(Guid.Parse("18b02357-ac0d-41da-9d57-aab403e9896d"), products.ElementAt(13),Unit.Piece, 1.60m, true, true),
				new Offer(Guid.Parse("cd8b3a94-895b-4fb9-9080-5d7c497fd388"), products.ElementAt(3), Unit.Litre, 1.20m),
				new Offer(Guid.Parse("b8337eee-87a5-43db-8a5b-7951bbb0e661"), products.ElementAt(6), Unit.Kilogram, 6.99m, true, true),
			};

			return offers;
		}

		public static IEnumerable<Farm> SeedFarms()
		{
			IEnumerable<Farm> farms = new List<Farm>
			{
				new Farm(Guid.Parse("a83588f4-6607-465a-ad5c-de9dd9d9b39e"), "Green Valley Orchards", "Nestled in the hills, this family-owned orchard specializes in organic apples, pears, and seasonal berries, grown with sustainable farming practices.", 51.2564, 3.0432, new List<Product>(), "https://statbel.fgov.be/sites/default/files/styles/news_full/public/images/landbouw/8.2%20Biologische%20landbouw/AdobeStock_200879536.jpeg?itok=9JTAiqrQ"),
				new Farm(Guid.Parse("38c44d86-0ea1-43fa-a110-fe898cb06390"), "Sunny Acres Farm", "Known for its sprawling sunflower fields and fresh vegetables, this farm offers a delightful experience with pick-your-own options.", 51.2593, 3.0481, new List<Product>(), "https://www.nationsencyclopedia.com/photos/belgium-agriculture-1547.jpg"),
				new Farm(Guid.Parse("b81fde22-a9c4-4356-bef8-363b6562c579"), "Riverside Pastures", "Situated along the riverbank, Riverside Pastures is famous for its pasture-raised eggs, dairy products, and heritage breed livestock.", 51.2666, 3.0395, new List<Product>(), "https://www.theclimakers.org/wp/wp-content/uploads/2021/11/Boerenbond-Belgian-Farmers-Union.png"),
				new Farm(Guid.Parse("025716cb-a545-4dfd-a721-cae04daa6b8d"), "Maple Leaf Gardens", "A picturesque farm with an array of maple trees, offering fresh produce and locally produced maple syrup straight from the source.", 51.2670, 3.0569, new List<Product>(), "https://www.shutterstock.com/image-photo/dark-clouds-over-field-young-600nw-2179706511.jpg"),
				new Farm(Guid.Parse("09354ca2-7d09-49fc-8281-6ae9f61a7680"), "Harvest Moon Homestead", "This small yet abundant farm is dedicated to cultivating heirloom tomatoes, peppers, and herbs with an emphasis on organic and permaculture techniques.", 51.2752, 3.0497, new List<Product>(), "https://growingformarket.com/custom/2022%20Issues/may%202022/cas/web%204%20CSA%20members%20Klara%20and%20Marijke%20harvesting.jpg"),
				new Farm(Guid.Parse("2724591e-efed-4cb4-8cb5-0c38311b3954"), "Breezy Hill Farm", "With a focus on sustainable grains and legumes, Breezy Hill Farm is a local favorite for artisanal bread flour and fresh beans.", 51.2557, 3.0604, new List<Product>(), "https://lh-images.us-east-1.linodeobjects.com/4005.jpg"),
				new Farm(Guid.Parse("d24b62ee-0cb9-4932-ac3d-71e0779251bf"), "Lavender Lane", "A serene haven with rows of aromatic lavender, this farm also produces lavender-infused honey, soaps, and teas for a unique farm experience.", 51.2473, 3.0270, new List<Product>(), "https://diplomatie.belgium.be/sites/default/files/styles/fluid_image/public/2022-04/shutterstock_436506028.jpg?itok=0vXbgD-t"),

				//new Farm(Guid.Parse("29fa465d-8653-48f8-9060-93061134d688"), "Farm 8", "A local farm offering fresh produce.", 51.2221, 0.3149, new List<Product>()),
				//new Farm(Guid.Parse("7c41c7a3-4542-4de6-88bf-cf574e824b2f"), "Farm 9", "A local farm offering fresh produce.", 51.3245, -4.5705, new List<Product>()),
				//new Farm(Guid.Parse("0f26b5b6-d65d-46b2-876a-407cebfbf0b8"), "Farm 10", "A local farm offering fresh produce.", 51.3260, 3.4212, new List<Product>()),
				//new Farm(Guid.Parse("925c4d5d-493a-4112-bcab-96587bf8125e"), "Farm 11", "A local farm offering fresh produce.", 51.2017, 11.6482, new List<Product>()),
				//new Farm(Guid.Parse("1cbe667f-ff40-4ff9-9c02-2619f9475417"), "Farm 12", "A local farm offering fresh produce.", 51.3575, 0.3322, new List<Product>()),
				//new Farm(Guid.Parse("cb04903e-d5d4-4eac-8996-f32c942801f9"), "Farm 13", "A local farm offering fresh produce.", 51.2114, -1.0887, new List<Product>()),
				//new Farm(Guid.Parse("7794f0c6-d708-411d-a31e-62630f7712ce"), "Farm 14", "A local farm offering fresh produce.", 51.2253, 5.6703, new List<Product>()),
				//new Farm(Guid.Parse("e05f92ad-11b8-42ae-87cb-0f98edeee4ec"), "Farm 15", "A local farm offering fresh produce.", 51.2120, 12.2946, new List<Product>())
			};

			return farms;
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
