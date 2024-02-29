

using Core.Entities;
using Service;

string opt;

BrandService brandService = new BrandService();
ProductService productService = new ProductService();
OrderService orderService = new OrderService();

do
{
    opt = Console.ReadLine();
	switch (opt)
	{
		case "1":
            Console.WriteLine("Name :");
			string name = Console.ReadLine();

			Brand brand = new Brand()
			{
				Name = name
			};
			brandService.Create(brand);
            break;
		default:
			break;
	}
} while (opt !="0");