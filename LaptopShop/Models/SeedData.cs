using LaptopShop.Data;
using LaptopShop.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace LaptopShop.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LaptopDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<LaptopDbContext>>()))
            {
                // nếu bảng Category chưa có dữ liệu thì thêm dữ liệu
                if (!context.Categories.Any())
                {
                    context.Categories.AddRangeAsync(
                         new Category { CategoryName = "Gaming", Description = "", Active = true },
                         new Category { CategoryName = "Sinh viên - Văn phòng", Description = "", Active = true },
                         new Category { CategoryName = "Thiết kế đồ hoạ", Description = "", Active = true },
                         new Category { CategoryName = "Mỏng nhẹ", Description = "", Active = true },
                         new Category { CategoryName = "Doanh nhân", Description = "", Active = true },
                         new Category { CategoryName = "Sản phẩm yêu thích", Description = "", Active = false },
                         new Category { CategoryName = "Sản phẩm khuyến mãi", Description = "", Active = false }
                    );
                    context.SaveChanges();
                }

                // nếu bảng Product chưa có dữ liệu thì thêm dữ liệu
                if (!context.Products.Any())
                {
                    List<Category> categories = context.Categories.ToList();
                    context.Products.AddRange(new SeedProduct().GenerateLaptop(50, categories));
                    context.SaveChanges();
                }
            }
        }
    }

    // dùng để sinh ngẫu nhiên các Laptop
    public class SeedProduct
    {
        // Brand
        private List<string> Brands = new List<string>() { "Acer", "Apple", "Asus", "Dell", "HP", "Lenovo", "Microsoft", "MSI" };

        // Name
        private List<string> AcerLaptops = new List<string>()  // Acer laptop
        {
            "Acer Aspire 5 A515-56", "Acer Swift 3 SF314-59", "Acer Nitro 5 AN515-56", "Acer Predator Triton 500 PT515-52", "Acer Chromebook Spin 713 CP713-3W-57JZ"
        };

        private List<string> MacbookLaptops = new List<string>() // Apple (Macbook)
        {
            "MacBook Air M1 2020 13\"", "MacBook Air M2 2022 13\"", "MacBook Pro M1 2020 13\"", "MacBook Pro M2 2023 14\"", "MacBook Pro M2 2023 16\""
        };

        private List<string> AsusLaptops = new List<string>()  // Asus laptop
        {
            "Asus ZenBook UX425EA", "Asus VivoBook S15 S533EQ", "Asus ROG Zephyrus G14 GA401QM", "Asus TUF Gaming A15 FA506IV", "Asus ExpertBook B9 B9400CEA"
        };

        private List<string> DellLaptops = new List<string>() // Dell
        {
            "Dell XPS 13 9310", "Dell Inspiron 15 7000 2-in-1", "Dell G5 15 SE (2021)", "Dell Precision 7550", "Dell Latitude 9420"
        };

        private List<string> HPLaptops = new List<string>() // HP
        {
            "HP Spectre x360", "HP Envy 13 (2021)", "HP Pavilion x360 (2021)", "HP Omen 15 (2021)", "HP Elite Dragonfly G2"
        };

        private List<string> LenovoLaptops = new List<string>() // Lenovo laptop
        {
            "Lenovo ThinkPad X1 Carbon Gen 9", "Lenovo Yoga 9i (14-inch)",  "Lenovo Legion 5 Pro", "Lenovo IdeaPad Flex 5i Chromebook", "Lenovo ThinkBook Plus Gen 2"
        };

        private List<string> SurfaceLaptops = new List<string>() { "Surface Laptop 4", "Surface Book 3", "Surface Pro 7+", "Surface Go 2", "Surface Pro X" }; // Microsoft (Surface) laptop

        private List<string> MSILaptops = new List<string>() { "MSI Stealth 15M", "MSI Creator 15", "MSI GE75 Raider", "MSI GL65 Leopard", "MSI GP65 Leopard" }; // MSI laptop

        // Monitor
        private List<string> Monitors = new List<string>()
        {
            "14.0 inch, 1920 x 1080 Pixels, IPS, 60 Hz",
            "15.6 inch, FHD (1920 x 1080), IPS, 144 Hz",
            "16.0 inch, 1920 x 1200 Pixels, WVA, 60 Hz",
            "16.1 inch, 1920 x 1080 Pixels, IPS, 144 Hz",
            "15.6 inch, 2560 x 1440 Pixels, IPS, 165 Hz",
            "13.0 inch Chính:, 2880 x 1920 Pixels, IPS, 120 Hz",
            "15.6 inch Chính:, FHD (1920 x 1080), IPS, 60 Hz",
            "16.2 inch, 3456 x 2234 Pixels, OLED, 120 Hz"
        };

        // CPU
        private List<string> CPUs = new List<string>()
        {
            "Intel, Core i5, 12500H",
            "AMD, Ryzen 5, 5600H",
            "Intel, Core i7, 12700H",
            "AMD, Ryzen 7, 5800H",
            "Intel, Core i9, 12900HK",
            "Apple, M1",
            "Apple, M2, 8 - Core",
            "Apple, M2 Pro, 12-Core"
        };

        // RAM
        private List<string> RAMs = new List<string>()
        {
            "8 GB (1 thanh 8 GB), DDR4, 3200 MHz",
            "8 GB (1 thanh 8 GB), LPDDR4X, 4267 MHz",
            "8 GB, DDR4, 3200 MHz, (2 khe RAM)",
            "16 GB (2 thanh 8 GB), DDR4, 2933 MHz",
            "16 GB (2 thanh 8 GB), LPDDR4, 4266 MHz",
            "16 GB (2 thanh 8 GB), DDR5, 4800 MHz"
        };

        // Storage
        private List<string> Storages = new List<string>() { "SSD 128 GB", "SSD 256 GB", "SSD 512 GB", "SSD 1 TB" };

        // Graphics
        private List<string> Graphics = new List<string>()
        {
            "NVIDIA GeForce RTX 3050 4GB; Intel Iris Xe Graphics",
            "NVIDIA GeForce RTX 3050 4GB; Intel UHD Graphics",
            "NVIDIA GeForce RTX 3050 Ti 4 GB; Intel Iris Xe Graphics",
            "NVIDIA GeForce RTX 3050 Ti 4 GB",
            "Intel Iris Xe Graphics",
            "Apple M1 GPU 7 nhân",
            "Apple M2 Pro 19-core GPU"
        };

        // Color
        private List<string> Colors = new List<string>() { "Bạc", "Đen", "Xám", "Bạc, Đen", "Bạc, Xám", "Đen, Xám" };

        // Battery Infor
        private List<string> Batteries = new List<string>()
        {
            "Lithium polymer", "130 W, 6 Cell", "Lithium-ion, 57.5 W, 4 Cell", "Lithium polymer, 140 W, 22 Giờ", "Lithium-ion 3 viên, 48 Wh"
        };

        // Description
        private List<string> Desciptions = new List<string>()
        {
            "Hàng chính hãng", "Máy mới 95%", "Máy thiết kế đẹp", "Máy khỏe, chạy mượt", "Đại hạ giá", "Bảo hành trọn đời"
        };

        // Price
        private List<decimal> Prices = new List<decimal>() { 27990000, 65990000, 25990000, 26990000, 17990000, 22990000, 32990000, 15990000 };

        // Generate laptop
        public List<Product> GenerateLaptop(int n, List<Category> categories)
        {
            List<Product> Laptops = new List<Product>();
            Random random = new Random();
            for (int i = 1; i <= n; i++)
            {
                Product product = new Product();
                // Brand
                int x = random.Next(10);
                x = x * i % 8;
                product.Brand = Brands[x];
                product.OperatingSystem = "Windows 11 Home"; // Operating System
                x %= 5;
                switch (product.Brand)
                {
                    case "Acer":
                        product.Name = AcerLaptops[x]; // Name
                        product.Image = "/img/laptops/" + product.Brand + ".jpg"; // image
                        break;
                    case "Asus":
                        product.Name = AsusLaptops[x]; // Name
                        product.Image = "/img/laptops/" + product.Brand + ".jpg"; // image
                        break;
                    case "Dell":
                        product.Name = DellLaptops[x]; // Name
                        product.Image = "/img/laptops/" + product.Brand + ".jpg"; // image
                        break;
                    case "HP":
                        product.Name = HPLaptops[x]; // Name
                        product.Image = "/img/laptops/" + product.Brand + ".jpg"; // image
                        break;
                    case "Lenovo":
                        product.Name = LenovoLaptops[x]; // Name
                        product.Image = "/img/laptops/" + product.Brand + ".jpg"; // image
                        break;
                    case "Microsoft":
                        product.Name = SurfaceLaptops[x]; // Name
                        product.Image = "/img/laptops/" + product.Brand + ".jpg"; // image
                        break;
                    case "MSI":
                        product.Name = MSILaptops[x]; // Name
                        product.Image = "/img/laptops/" + product.Brand + ".jpg"; // image
                        break;
                    default:
                        product.Name = MacbookLaptops[x];
                        product.Image = "/img/laptops/" + product.Brand + ".jpg"; // image
                        product.OperatingSystem = "MacOS Ventura"; // Operating System
                        break;
                }
                // Monitor
                x = random.Next(10);
                x = x * i % 8;
                product.Monitor = Monitors[x];
                // CPU
                x = random.Next(10);
                x = x * i % 8;
                product.CPU = CPUs[x];
                // Storage
                x = random.Next(10);
                x = x * i % 4;
                product.Storage = Storages[x];
                // RAM
                x = random.Next(10);
                x = x * i % 6;
                product.RAM = RAMs[x];
                // Graphics
                x = random.Next(10);
                x = x * i % 7;
                product.Graphics = Graphics[x];
                // Weight
                product.Weight = (x / 10.0) + 1.5;
                // Color
                x = random.Next(10);
                x = x * i % 6;
                product.Color = Colors[x];
                // Battery Infor
                x = random.Next(10);
                x = x * i % 5;
                product.Battery = Batteries[x];
                // Description
                x = random.Next(10);
                x = x * i % 6;
                product.Description = Desciptions[x];
                // Price
                x = random.Next(10);
                x = x * i % 8;
                product.Price = Prices[x];
                // Quantity
                product.Quantity = x + 10;
                // Warranty
                product.Warranty = (byte)(x + 12);
                // Category Id
                x = random.Next(10);
                x = x * i % categories.Count();
                product.Category = categories[x];
                product.CategoryId = product.Category.CategoryId;

                Laptops.Add(product);
            }
            return Laptops;
        }
    }
}
