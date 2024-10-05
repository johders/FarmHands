using Mde.Project.Core.Entities;
using Mde.Project.Core.Enums;
using System.Collections.ObjectModel;

namespace Mde.Project.Core.Data
{
	public static class Seeder
	{
		public static ObservableCollection<Product> SeedProducts()
		{
			ObservableCollection<Product> products = new ObservableCollection<Product>
				{
					new Product(Guid.Parse("ed424125-427c-4959-bcf8-4dd73b2691e1"), "Apples", "Freshly picked, juicy apples.", Unit.Kilogram, 3.99m, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS1jkLfZNwVPgxhe8FlmI3gZo6GIYhkzOiftg&s"),
					new Product(Guid.Parse("406af47f-1bef-4b6d-86d1-c55cfefdc85d"), "Carrots", "Organic carrots, straight from the farm.", Unit.Kilogram, 1.89m, "https://ucarecdn.com/459eb7be-115a-4d85-b1d8-deaabc94c643/-/format/auto/-/preview/3000x3000/-/quality/lighter/"),
					new Product(Guid.Parse("cf387bb6-f724-4718-b3f3-09613a4022f3"), "Free-Range Eggs", "Dozen of farm fresh eggs.", Unit.Dozen, 4.50m, "https://cdn.britannica.com/94/151894-050-F72A5317/Brown-eggs.jpg"),
					new Product(Guid.Parse("75197c7e-eac1-402b-b187-f2974f78dc85"), "Milk", "Fresh cow milk, non-pasteurized.", Unit.Litre, 1.20m, "https://t3.ftcdn.net/jpg/03/30/09/16/360_F_330091642_6AniY6wGxENL6WCzdrHpLhu3Y2HrcWuY.jpg"),
					new Product(Guid.Parse("12bc9732-e3a4-4c14-ad94-214a0e6a9592"), "Tomatoes", "Plump and ripe tomatoes.", Unit.Kilogram, 2.50m, "https://www.uvm.edu/content/shared/files/styles/1200/public/uvm-extension-cultivating-healthy-communities/tomatoes2-e.jpg?t=rpri8o"),
					new Product(Guid.Parse("70ff9cd2-82d8-4235-b10d-56e7c9c54af2"), "Pumpkin", "Seasonal pumpkins, great for pies.", Unit.Piece, 5.99m, "https://upload.wikimedia.org/wikipedia/commons/5/5c/FrenchMarketPumpkinsB.jpg"),
					new Product(Guid.Parse("bf522a56-aecf-48ba-9516-1fc626d6fb64"), "Strawberries", "Sweet and juicy strawberries.", Unit.Kilogram, 6.99m, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSRXKcGr0rnAeasxCbSBGSPlWo8rOGnTJM7hQ&s"),
					new Product(Guid.Parse("8e35ca1e-5616-4e26-a39b-6c1244b8cf8c"), "Honey", "Organic honey from local bees.", Unit.Litre, 10.00m, "https://3.imimg.com/data3/NG/IC/MY-11241820/lychee-honey-250x250.jpg"),
					new Product(Guid.Parse("158649b9-0ab4-4eca-93b4-b0ff95669596"), "Potatoes", "Golden potatoes, perfect for mashing.", Unit.Kilogram, 1.50m, "https://live2giveorganics.nz/cdn/shop/products/E1CA67E3-4A64-4388-9454-F9AC247D0693_1_201_a.jpg?v=1667075696&width=1904"),
					new Product(Guid.Parse("d1f7f8d4-c5d2-49e4-bd01-17be46663db8"), "Spinach", "Fresh spinach leaves, great for salads.", Unit.Kilogram, 2.99m, "https://www.realfoodco.co.za/cdn/shop/products/spinach_1024x1024.jpg?v=1580301409"),
					new Product(Guid.Parse("123bb355-334f-4dcc-88eb-eab5966b22f4"), "Oranges", "Citrusy and sweet oranges.", Unit.Kilogram, 3.50m, "https://www.sunshinecooperative.co.uk/wp-content/uploads/2018/09/oranges-2100108_1280.jpg"),
					new Product(Guid.Parse("6a26b82a-dca3-4328-b293-249a8aa52b73"), "Zucchini", "Fresh zucchini, straight from the garden.", Unit.Kilogram, 1.80m, "https://www.edenbrothers.com/cdn/shop/products/squash-organic-dark-green-zucchini-shk-1.jpg?v=1653502252"),
					new Product(Guid.Parse("1b06db28-629f-4a09-b87d-7961154a6d5e"), "Cherries", "Sweet and tart cherries, perfect for snacks.", Unit.Kilogram, 8.00m, "https://www.edible-garden.co/wp-content/uploads/2024/05/Cherries.jpg"),
					new Product(Guid.Parse("0a1312b0-5310-47c8-99c9-2ea00e027481"), "Broccoli", "Crisp and green broccoli heads.", Unit.Kilogram, 3.00m, "https://www.groworganic.com/cdn/shop/products/broccoli-calabrese.jpg?v=1570226464&width=800"),
					new Product(Guid.Parse("21efe678-ed19-4e4e-951c-5efdbdcfbabb"), "Peaches", "Delicious peaches, full of flavor.", Unit.Kilogram, 5.50m, "https://www.froghollow.com/cdn/shop/products/gold_dust_3_lr_sq_a7ab3913-b5a1-42b3-8374-6deda4e3411a_300x300.jpg?v=1691172993"),
					new Product(Guid.Parse("03080775-9ed4-4b27-8aa5-75f034bf0b3b"), "Lettuce", "Crisp, green lettuce for salads.", Unit.Piece, 1.25m, "https://wearelittlefarms.com/cdn/shop/products/image_35b45e84-cb02-4af8-96e0-af532e97ed6a_1500x.heic?v=1668155846"),
					new Product(Guid.Parse("b5c41144-66ff-4e87-9ea3-657a36a8d21e"), "Watermelon", "Large, juicy watermelons.", Unit.Piece, 7.00m, "https://delvinfarms.com/wp-content/uploads/2015/08/DSC_0276.jpg"),
					new Product(Guid.Parse("597df337-9651-4c87-90cc-1363043b8893"), "Garlic", "Organic garlic bulbs.", Unit.Kilogram, 6.00m, "https://natureandnurtureseeds.com/cdn/shop/collections/IMG_2367_garlic_website.jpg?v=1679510965"),
					new Product(Guid.Parse("8f1398ea-d223-4dcf-8a16-db61c48ca42d"), "Corn", "Sweet corn on the cob, fresh from the field.", Unit.Piece, 0.99m, "https://cdn.shopify.com/s/files/1/0569/0615/4154/files/iStock-119709782-9-scaled.jpg"),
					new Product(Guid.Parse("9d2015e1-1065-4c8a-85de-d552e14580bc"), "Cabbage", "Green cabbage heads, perfect for slaw.", Unit.Kilogram, 2.00m, "https://www.seedway.com/app/uploads/2019/05/Reaction-bj-4.jpg")
				};

			return products;

		}
	}

		
}
