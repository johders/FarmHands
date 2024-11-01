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
				new Farm(Guid.Parse("10000000-0000-0000-0000-000000000004"), "Maple Leaf Gardens", "A picturesque farm with an array of maple trees, offering fresh produce and locally produced maple syrup straight from the source.", 51.2670, 3.0569, new List<Product>(), "https://cdn.pixabay.com/photo/2023/08/24/12/21/farming-8210675_1280.jpg"),
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
				//Maple Leaf Gardens
				new Offer
				{
					Id = Guid.Parse("20000000-0000-0000-0000-000000000010"),
					Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000009")),
					Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000004")),
					Variant = "Russet",
					Description = "Our russet potatoes are grown with care to achieve a hearty, earthy taste and a satisfyingly fluffy texture. " +
					"With their thick, rough skins and dense, starchy flesh, russets are the perfect choice for baking, mashing, " +
					"and frying—they hold up beautifully in any hearty dish. These are the potatoes that give you crispy, " +
					"golden fries, pillowy mashed potatoes, and perfectly baked jackets with that delightful balance of crispy " +
					"skin and soft inside. When you pick russets from our farm, you’re getting potatoes that have been nurtured from seed to harvest, " +
					"ensuring quality, freshness, and flavor in every bite. Perfect for everything from weeknight dinners to holiday feasts!",
					Unit = Unit.Kilogram,
					Price = 2.15m,
					IsAvailable = true,
					IsOrganic = false,
					OfferImageUrl = "https://cdn.pixabay.com/photo/2018/05/29/23/18/potato-3440360_1280.jpg"
				},

				new Offer
				{
					Id = Guid.Parse("20000000-0000-0000-0000-000000000011"),
					Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000005")),
					Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000004")),
					Variant = "Roma",
					Description = "At our farm, we grow Romas with a firm, meaty texture and a perfectly balanced, rich flavor. " +
					"With their oblong shape and deep red hue, these tomatoes are packed with less juice and fewer seeds, making them ideal for sauces, " +
					"salsas, and canning. Our Roma tomatoes are nurtured under the sun to bring out their natural sweetness and subtle tang. " +
					"They cook down beautifully, creating thick, flavorful sauces, and they’re also delicious fresh—adding a vibrant, " +
					"savory taste to salads, sandwiches, and bruschetta. When you choose our farm’s Roma tomatoes, you’re getting quality, " +
					"flavor, and versatility in every bite!",
					Unit = Unit.Kilogram,
					Price = 3.4m,
					IsAvailable = true,
					IsOrganic = false,
					OfferImageUrl = "https://cdn.pixabay.com/photo/2017/09/05/06/39/roma-tomatoes-2716569_1280.jpg"
				},

				new Offer
				{
					Id = Guid.Parse("20000000-0000-0000-0000-000000000012"),
					Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000005")),
					Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000004")),
					Variant = "San Marzano",
					Description = "Grown with exceptional care, these Italian favorites are prized for their rich, sweet flavor and low acidity, " +
					"making them perfect for creating authentic sauces, soups, and Italian dishes. Our San Marzanos have a distinct oblong " +
					"shape, thick skin, and fewer seeds, giving you more of the rich, dense flesh that transforms into a beautifully " +
					"smooth, velvety sauce. Handpicked at peak ripeness, our San Marzano tomatoes bring you a taste of " +
					"Italy in every bite. Whether you’re making a classic marinara, a comforting tomato soup, or a fresh " +
					"bruschetta, these tomatoes add unparalleled depth and flavor. With our farm’s San Marzano tomatoes, " +
					"elevate every dish with the quality that chefs and home cooks alike cherish!",
					Unit = Unit.Kilogram,
					Price = 4.1m,
					IsAvailable = true,
					IsOrganic = false,
					OfferImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/TomateSanMarzano.jpg/450px-TomateSanMarzano.jpg"
				},

				new Offer
				{
					Id = Guid.Parse("20000000-0000-0000-0000-000000000013"),
					Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000002")),
					Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000004")),
					Variant = "Nantes",
					Description = "These carrots are grown on our farm with care to bring out their signature smooth, " +
					"cylindrical shape and vibrant orange color. Nantes carrots are known for their tender, almost coreless texture, " +
					"making them wonderfully juicy and easy to bite into, whether fresh or cooked. These beauties are excellent for snacking, " +
					"adding to salads, or roasting to caramelized perfection. Their sweetness intensifies when cooked, and they blend seamlessly " +
					"into soups and stews. When you buy Nantes carrots from our farm, you’re choosing the best in flavor, quality, and freshness. " +
					"Perfect for families, chefs, and anyone who loves a carrot with pure, natural sweetness!",
					Unit = Unit.Kilogram,
					Price = 4.1m,
					IsAvailable = true,
					IsOrganic = false,
					OfferImageUrl = "https://cdn.pixabay.com/photo/2017/05/13/15/23/carrot-2309814_1280.jpg"
				},

				new Offer
				{
					Id = Guid.Parse("20000000-0000-0000-0000-000000000014"),
					Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000001")),
					Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000004")),
					Variant = "Granny Smith",
					Description = "Renowned for their bright green skin and crisp texture, these apples are a delightful combination of tartness and " +
					"sweetness that will invigorate your taste buds. Each bite delivers a refreshing crunch, " +
					"making them perfect for snacking straight from the orchard! When you choose our Granny Smith apples, you’re choosing quality and freshness from our farm to your table. " +
					"Experience the crisp, tangy goodness that has made Granny Smith a beloved favorite for generations!",
					Unit = Unit.Kilogram,
					Price = 4.1m,
					IsAvailable = true,
					IsOrganic = false,
					OfferImageUrl = "https://cdn.pixabay.com/photo/2017/07/01/21/15/apple-2462753_1280.jpg"
				},


				// Green Valley Orchards
				new Offer
				{
					Id = Guid.Parse("20000000-0000-0000-0000-000000000008"),
					Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000001")),
					Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000001")),
					Variant = "Fuji",
					Description = "Fuji apples have a flavor that’s mellow and smooth, ideal for snacking, baking, or tossing into salads. " +
					"Because they stay fresh for weeks, they’re a reliable, delicious choice for your kitchen. Come try a sample! " +
					"We’re confident you’ll fall in love with the taste and quality of our farm-fresh Fujis.",
					Unit = Unit.Kilogram,
					Price = 2.1m,
					IsAvailable = true,
					IsOrganic = false,
					OfferImageUrl = "https://cdn.pixabay.com/photo/2017/09/06/03/50/apple-2720105_1280.jpg"
				},

				new Offer
				{
					Id = Guid.Parse("20000000-0000-0000-0000-000000000009"),
					Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000007")),
					Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000001")),
					Unit = Unit.Kilogram,
					Variant = "Albion",
					Description = "Let us introduce you to our Albion strawberries—one of the sweetest, juiciest berries you’ll ever taste! " +
					"Grown with care right here on our farm, these strawberries are large, beautifully red, and bursting with flavor. " +
					"Albions are special because they’re firm and hold up well, making them perfect for everything from fresh snacking to " +
					"delicious desserts. We guarantee each berry is handpicked for the best taste and quality. Come try trem, you won’t be disappointed!",
					Price = 16.5m,
					IsAvailable = true,
					IsOrganic = false,
					OfferImageUrl = "https://cdn.pixabay.com/photo/2020/04/22/17/24/strawberry-5079237_960_720.jpg"
				},


				// Sunny Acres Farm
				new Offer
				{
					Id = Guid.Parse("20000000-0000-0000-0000-000000000001"),
					Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000005")),
					Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000002")),
					Unit = Unit.Kilogram,
					Variant = "Sweet 100",
					Description = "A burst of sweetness in every bite! These bright red, perfectly round tomatoes are known for their incredible flavor and juicy texture, " +
					"making them ideal for snacking right off the vine or adding vibrant color to your favorite salads. Sweet 100s are famous for " +
					"producing hundreds of little gems per plant, so you get a bounty of flavor all season long. Our Sweet 100s are hand-picked to ensure only the ripest, " +
					"sweetest tomatoes make it to you. Whether you're a tomato lover or just looking to add a pop of natural sweetness to your meals, Sweet 100s are the perfect choice. " +
					"Come pick up a basket today and taste the sunshine in every bite!",
					Price = 1.89m,
					IsAvailable = true,
					IsOrganic = false,
					OfferImageUrl = "https://cdn.pixabay.com/photo/2014/11/20/21/17/tomatoes-539909_1280.jpg"
				},
                new Offer
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                    Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000009")),
                    Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000002")),
                    Unit = Unit.Kilogram,
					Variant = "Yukon Gold",
					Description = "Every cook’s dream and an absolute staple in the kitchen. With their thin, golden skin and creamy, buttery texture, these potatoes are incredibly versatile, " +
					"perfect for everything from fluffy mashed potatoes to golden-roasted sides. Yukon Golds have a naturally rich, slightly sweet flavor that makes every dish shine, whether " +
					"you’re cooking up a cozy stew or a crisp batch of homemade fries. Grown with care here on our farm, our Yukon Golds are freshly harvested to deliver that just-dug " +
					"flavor and quality straight to your table. Pick up a bag today, and bring home the warmth and satisfaction only Yukon Golds can add to your meals!",
					Price = 1.50m,
					OfferImageUrl = "https://cdn.pixabay.com/photo/2016/09/01/19/30/potatoes-1637280_1280.jpg"
				},
                new Offer
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000003"),
                    Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000014")),
                    Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000002")),
                    Unit = Unit.Piece,
					Variant = "Romanesco",
					Description = "A true masterpiece of nature. With its stunning spiral pattern and bright green color, Romanesco isn’t just a vegetable; it’s a conversation piece! " +
					"Each head has a unique, mesmerizing fractal shape that makes it as beautiful as it is delicious. Romanesco has a mild, nutty flavor with a hint of sweetness, making it " +
					"perfect for roasting, steaming, or enjoying raw in salads. It holds its texture well, adding a satisfying crunch to any dish. Packed with vitamins and minerals, " +
					"Romanesco is as nutritious as it is eye-catching. Pick one up today, and experience a new level of taste and visual appeal on your plate!",
					Price = 1.60m,
                    IsAvailable = true,
                    IsOrganic = true,
					OfferImageUrl = "https://cdn.pixabay.com/photo/2015/03/14/13/59/vegetables-673181_1280.jpg"
				},


				// Riverside Pastures
				new Offer
				{
					Id = Guid.Parse("20000000-0000-0000-0000-000000000004"),
					Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000003")),
					Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000003")),
					Unit = Unit.Dozen,
					Variant = "Pasture-raised",
					Description = "A genuine taste of nature and quality. Our hens roam freely over lush pastures, foraging for a natural diet of grasses and insects, which gives their " +
					"eggs a vibrant orange yolk and a rich, flavorful taste that you won’t find in conventional eggs. These eggs are packed with nutrients, boasting higher levels of " +
					"omega-3s and vitamins, thanks to the hens' healthy, active lifestyle. When you crack open one of our pasture-raised eggs, you’ll see and taste the difference right away. " +
					"They’re perfect for everything from hearty breakfasts to delicious baked goods. Pick up a dozen and experience the wholesome, farm-fresh quality that only pasture-raised eggs can offer. You’ll never go back!",
					Price = 5.5m,
					IsAvailable = true,
					IsOrganic = true,
					OfferImageUrl = "https://cdn.pixabay.com/photo/2022/07/26/13/55/protein-7345935_1280.jpg"
				},
				new Offer
				{
					Id = Guid.Parse("20000000-0000-0000-0000-000000000005"),
					Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000004")),
					Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000003")),
					Unit = Unit.Litre,
					Variant = "Whole Milk",
					Description = "Pure, creamy goodness straight from our happy cows to your table. Our whole milk is minimally processed " +
					"and packed with essential nutrients, making it a wholesome choice for your family. With its rich, creamy texture and " +
					"delightful flavor, it’s perfect for drinking, cooking, and baking. Our cows roam freely on lush pastures, grazing on " +
					"natural grass, which contributes to the exceptional taste and quality of our milk. You’ll notice the difference in every " +
					"glass, with a taste that’s fresh and full-bodied, just like milk used to be. Whether you’re pouring it over your morning " +
					"cereal, using it in your favorite recipes, or enjoying it on its own, our farm-fresh whole milk is a delicious and " +
					"nutritious addition to your daily routine. Come by and grab a jug today—you’ll be supporting local farming while " +
					"enjoying the best milk nature has to offer!",
					Price = 2.50m,
					IsAvailable = true,
					IsOrganic = true,
					OfferImageUrl = "https://cdn.pixabay.com/photo/2017/07/05/15/41/milk-2474993_1280.jpg"
				},


				//Lavender Lane
				new Offer
				{
					Id = Guid.Parse("20000000-0000-0000-0000-000000000006"),
					Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000007")),
					Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000007")),
					Unit = Unit.Kilogram,
					Variant = "Seascape",
					Description = "a true delight for your taste buds! Known for their exceptional sweetness and juiciness, " +
					"these vibrant red berries are perfect for snacking, baking, or adding to your favorite salads. Seascape strawberries are " +
					"unique because they’re ever-bearing, which means you can enjoy fresh strawberries from late spring through fall! " +
					"Each berry is plump, flavorful, and bursting with that sweet, tangy taste you love. They’re also easy to grow, " +
					"making them a favorite among home gardeners and professional growers alike. Hand-picked at the peak of ripeness, " +
					"our Seascape strawberries are a delicious way to support local agriculture. Whether you’re enjoying them fresh, making jams, " +
					"or adding them to desserts, these strawberries will bring a taste of summer to your table all year round. " +
					"Come visit us and pick up a basket of Seascape strawberries today—you won’t be disappointed!",
					Price = 14m,
					IsAvailable = true,
					IsOrganic = true,
					OfferImageUrl = "https://cdn.pixabay.com/photo/2021/07/01/16/36/strawberries-6379817_1280.jpg"
				},
				new Offer
				{
					Id = Guid.Parse("20000000-0000-0000-0000-000000000007"),
					Product = products.FirstOrDefault(p => p.Id == Guid.Parse("00000000-0000-0000-0000-000000000008")),
					Farm = farms.FirstOrDefault(f => f.Id == Guid.Parse("10000000-0000-0000-0000-000000000007")),
					Unit = Unit.Piece,
					Variant = "Lavender honey",
					Description = "Crafted from the nectar of our vibrant lavender fields, this honey is not just sweet; it’s an aromatic " +
					"experience. With its light golden color and floral notes, our lavender honey captures the essence of summer blooms " +
					"in every jar. Each spoonful is a delightful blend of mild sweetness and the calming scent of lavender, making it perfect " +
					"for drizzling over yogurt, adding to your favorite teas, or enhancing baked goods. Whether you’re looking to elevate your " +
					"culinary creations or simply enjoy a soothing treat, our lavender honey is the perfect choice. Not only does it taste " +
					"amazing, but it also offers potential health benefits, including antioxidants and soothing properties. " +
					"It makes a wonderful gift for friends or a luxurious addition to your pantry. " +
					"Come visit us and take home a jar of our farm-fresh lavender honey today—you won’t want to miss this unique " +
					"flavor experience!",
					Price = 7.50m,
					IsAvailable = true,
					IsOrganic = true,
					OfferImageUrl = "https://cdn.pixabay.com/photo/2014/05/23/18/15/honey-352205_1280.jpg"
				},
			};

			return offers;
		}

		public static IEnumerable<ApplicationUser> SeedUsers()
		{
			IEnumerable<ApplicationUser> users = new List<ApplicationUser>
			{
				new ApplicationUser
				{
					Id = Guid.Parse("30000000-0000-0000-0000-000000000001"),
					Name = "Jefke Delaplace",
					UserName = "JDTester",
					Password = "password",
					FavoriteFarms = new List<FavoriteFarm>(),
					FavoriteProducts = new List<FavoriteProduct>(),
				}
			};

			return users;
		}

		public static IEnumerable<FavoriteFarm> SeedFavoriteFarms()
        {
            IEnumerable<FavoriteFarm> favoriteFarms = new List<FavoriteFarm>
            {
                new FavoriteFarm{ 
					Id = Guid.Parse("40000000-0000-0000-0000-000000000001"),
					UserId = Guid.Parse("30000000-0000-0000-0000-000000000001"),
                    FarmId = Guid.Parse("10000000-0000-0000-0000-000000000002")
                },
                new FavoriteFarm{
                    Id = Guid.Parse("40000000-0000-0000-0000-000000000002"),
                    UserId = Guid.Parse("30000000-0000-0000-0000-000000000001"),
                    FarmId = Guid.Parse("10000000-0000-0000-0000-000000000007")
                },
                new FavoriteFarm{
                    Id = Guid.Parse("40000000-0000-0000-0000-000000000003"),
                    UserId = Guid.Parse("30000000-0000-0000-0000-000000000001"),
                    FarmId = Guid.Parse("10000000-0000-0000-0000-000000000003")
                },
            };

            return favoriteFarms;
        }

		public static IEnumerable<FavoriteProduct> SeedFavoriteProducts()
		{
			IEnumerable<FavoriteProduct> favoriteProducts = new List<FavoriteProduct>
			{
				new FavoriteProduct{
					Id = Guid.Parse("50000000-0000-0000-0000-000000000001"),
					UserId = Guid.Parse("30000000-0000-0000-0000-000000000001"),
					ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001")
				},
				new FavoriteProduct{
					Id = Guid.Parse("50000000-0000-0000-0000-000000000002"),
					UserId = Guid.Parse("30000000-0000-0000-0000-000000000001"),
					ProductId = Guid.Parse("00000000-0000-0000-0000-000000000014")
				},
				new FavoriteProduct{
					Id = Guid.Parse("50000000-0000-0000-0000-000000000003"),
					UserId = Guid.Parse("30000000-0000-0000-0000-000000000001"),
					ProductId = Guid.Parse("00000000-0000-0000-0000-000000000010")
				},
			};

			return favoriteProducts;
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
