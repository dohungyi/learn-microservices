using System.Collections.Concurrent;
using System.Reflection;
using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using Serilog;
using SharedKernel.Libraries;
using Enum = SharedKernel.Application.Enum;

namespace Catalog.Infrastructure.Persistence;

public class ApplicationDbContextSeed
{
    private readonly IServiceProvider _provider;
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _accessor;

    public ApplicationDbContextSeed(ApplicationDbContext context,
        IHttpContextAccessor accessor,
        IServiceProvider provider
    )
    {
        _provider = provider;
        _context = context;
        _accessor = accessor;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsMySql())
                await _context.Database.MigrateAsync();
        }
        catch (Exception e)
        {
            // Logging.Error("An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
            await _context.CommitAsync();
        }
        catch (Exception e)
        {
            // Logging.Error("An error occurred while seeding the database;");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        if (!_context.Weights.Any())
        {
            var weightCategories = new List<Weight>
            {
                new Weight { Code = "G", Description = "Gam" },
                new Weight { Code = "KG", Description = "Kilogram" },
            };

            _context.Weights.AddRange(weightCategories);
            await _context.SaveChangesAsync();
        }

        if (!_context.Provinces.Any())
        {
            await ReadAndSeedLocationsAsync();
        }

        await SeedSupplierAsync();

        await SeedCategoryAsync();
    }

    private async Task SeedAssetAsync()
    {
        if (await _context.Assets.AnyAsync()) return;

        var assets = new List<Asset>()
        {
            new Asset
            {
                FileName = "images/catalog/f4f59825-0800-4357-a7aa-ab235ee3375e.png",
                OriginalFileName = "1.png",
                Description = String.Empty,
                FileExtension = ".png",
                Size = 46510,
                Type = Enum.FileType.Image
            },
            new Asset
            {
                FileName = "images/catalog/efc0ae67-28b2-47e0-ac02-c91ce79f47d3.png",
                OriginalFileName = "2.png",
                Description = String.Empty,
                FileExtension = ".png",
                Size = 26306,
                Type = Enum.FileType.Image
            },
            new Asset
            {
                FileName = "images/catalog/54a430ed-fbb0-4761-b246-fd20fc9e1768.png",
                OriginalFileName = "3.png",
                Description = String.Empty,
                FileExtension = ".png",
                Size = 55967,
                Type = Enum.FileType.Image
            },
            new Asset
            {
                FileName = "images/catalog/fbf1cf79-e9c3-433d-aafb-7aeb988d5fbc.png",
                OriginalFileName = "4.png",
                Description = String.Empty,
                FileExtension = ".png",
                Size = 54327,
                Type = Enum.FileType.Image
            },
            new Asset
            {
                FileName = "images/catalog/3d70a09d-1eb5-43f1-a69f-14ab39c73092.png",
                OriginalFileName = "5.png",
                Description = String.Empty,
                FileExtension = ".png",
                Size = 57662,
                Type = Enum.FileType.Image
            },
            new Asset
            {
                FileName = "images/catalog/7d739402-639d-4011-b7e3-f22a227ad37a.png",
                OriginalFileName = "6.png",
                Description = String.Empty,
                FileExtension = ".png",
                Size = 44240,
                Type = Enum.FileType.Image
            },
            new Asset
            {
                FileName = "images/catalog/2a10b1d9-7953-44bf-a5b2-5c6420ea110b.png",
                OriginalFileName = "7.png",
                Description = String.Empty,
                FileExtension = ".png",
                Size = 60560,
                Type = Enum.FileType.Image
            },
            new Asset
            {
                FileName = "images/catalog/4e6c5e69-4b1b-4d72-8b66-a7001d629b06.png",
                OriginalFileName = "8.png",
                Description = String.Empty,
                FileExtension = ".png",
                Size = 56619,
                Type = Enum.FileType.Image
            },
            new Asset
            {
                FileName = "images/catalog/9415ae3a-cf33-467a-82ac-fee231eaae07.png",
                OriginalFileName = "9.png",
                Description = String.Empty,
                FileExtension = ".png",
                Size = 76041,
                Type = Enum.FileType.Image
            },
            new Asset
            {
                FileName = "images/catalog/d8efd53f-1d5e-4552-9b0c-623de671825f.png",
                OriginalFileName = "10.png",
                Description = String.Empty,
                FileExtension = ".png",
                Size = 43492,
                Type = Enum.FileType.Image
            },
            new Asset
            {
                FileName = "images/catalog/438e5423-993c-434c-b5f4-fdd6180bec21.png",
                OriginalFileName = "11.png",
                Description = String.Empty,
                FileExtension = ".png",
                Size = 32053,
                Type = Enum.FileType.Image
            },
            new Asset
            {
                FileName = "images/catalog/4854f6e1-b735-4f6f-8937-faecd9478d8e.png",
                OriginalFileName = "12.png",
                Description = String.Empty,
                FileExtension = ".png",
                Size = 40818,
                Type = Enum.FileType.Image
            },
            new Asset
            {
                FileName = "images/catalog/7f5fe912-8630-4751-b48a-c9fbfd1f9fae.png",
                OriginalFileName = "13.png",
                Description = String.Empty,
                FileExtension = ".png",
                Size = 50974,
                Type = Enum.FileType.Image
            },
            new Asset
            {
                FileName = "images/catalog/f2b23b52-b232-47f5-bbbf-da7d83e1ce1a.png",
                OriginalFileName = "14.png",
                Description = String.Empty,
                FileExtension = ".png",
                Size = 47584,
                Type = Enum.FileType.Image
            },
            new Asset
            {
                FileName = "images/catalog/60d0a10b-cae6-49a0-8de7-58a7c957d184.png",
                OriginalFileName = "15.png",
                Description = String.Empty,
                FileExtension = ".png",
                Size = 34217,
                Type = Enum.FileType.Image
            },
            new Asset
            {
                FileName = "images/catalog/6f0d11ba-11c1-4704-8039-d8a189694830.png",
                OriginalFileName = "16.png",
                Description = String.Empty,
                FileExtension = ".png",
                Size = 60610,
                Type = Enum.FileType.Image
            },
            new Asset
            {
                FileName = "images/catalog/e3de60fa-d5dc-432f-925a-0fe3f168957f.png",
                OriginalFileName = "17.png",
                Description = String.Empty,
                FileExtension = ".png",
                Size = 24576,
                Type = Enum.FileType.Image
            },
            new Asset
            {
                FileName = "images/catalog/2924460f-bdb9-468f-be95-19077ce5bfe2.png",
                OriginalFileName = "18.png",
                Description = String.Empty,
                FileExtension = ".png",
                Size = 54016,
                Type = Enum.FileType.Image
            }
            ,
            new Asset
            {
                FileName = "images/catalog/e36924a9-a83e-41c0-ade0-a85fb17e4151.png",
                OriginalFileName = "19.png",
                Description = String.Empty,
                FileExtension = ".png",
                Size = 87716,
                Type = Enum.FileType.Image
            },
            new Asset
            {
                FileName = "images/catalog/eb102843-df32-44d9-ada7-fe5352ce4b32.png",
                OriginalFileName = "20.png",
                Description = String.Empty,
                FileExtension = ".png",
                Size = 30214,
                Type = Enum.FileType.Image
            }
        };
        
        await _context.Assets.AddRangeAsync(assets);
        await _context.SaveChangesAsync();
    }
    
    private async Task SeedCategoryAsync()
    {
        if (await _context.Categories.AnyAsync()) return;

        var categories = new List<Category>()
        {
            new Category
            {
                Code = "CAT001",
                Name = "Thời Trang Nam",
                Alias = "thoi-trang-nam",
                Description = "Danh mục thời trang nam",
                Level = 1,
                FileName = "images/catalog/54a430ed-fbb0-4761-b246-fd20fc9e1768.png",
                OrderNumber = 1,
                Status = true,
                Path = "/thoi-trang-nam",
                ParentId = null
            },
            new Category
            {
                Code = "CAT002",
                Name = "Thời Trang Nữ",
                Alias = "thoi-trang-nu",
                Description = "Danh mục thời trang nữ",
                Level = 1,
                FileName = "images/catalog/efc0ae67-28b2-47e0-ac02-c91ce79f47d3.png",
                OrderNumber = 2,
                Status = true,
                Path = "/thoi-trang-nu",
                ParentId = null
            },
            new Category
            {
                Code = "CAT003",
                Name = "Điện Thoại & Phụ Kiện",
                Alias = "dien-thoai-phu-kien",
                Description = "Danh mục điện thoại và phụ kiện",
                Level = 1,
                FileName = "images/catalog/f4f59825-0800-4357-a7aa-ab235ee3375e.png",
                OrderNumber = 3,
                Status = true,
                Path = "/dien-thoai-phu-kien",
                ParentId = null
            },
            new Category
            {
                Code = "CAT004",
                Name = "Mẹ & Bé",
                Alias = "me-be",
                Description = "Danh mục mẹ và bé",
                Level = 1,
                FileName = "images/catalog/fbf1cf79-e9c3-433d-aafb-7aeb988d5fbc.png",
                OrderNumber = 4,
                Status = true,
                Path = "/me-be",
                ParentId = null
            },
            new Category
            {
                Code = "CAT005",
                Name = "Thiết Bị Điện Tử",
                Alias = "thiet-bi-dien-tu",
                Description = "Danh mục thiết bị điện tử",
                Level = 1,
                FileName = "images/catalog/3d70a09d-1eb5-43f1-a69f-14ab39c73092.png",
                OrderNumber = 5,
                Status = true,
                Path = "/thiet-bi-dien-tu",
                ParentId = null
            },
            new Category
            {
                Code = "CAT006",
                Name = "Nhà Cửa & Đời Sống",
                Alias = "nha-cua-doi-song",
                Description = "Danh mục nhà cửa và đời sống",
                Level = 1,
                FileName = "images/catalog/7d739402-639d-4011-b7e3-f22a227ad37a.png",
                OrderNumber = 6,
                Status = true,
                Path = "/nha-cua-doi-song",
                ParentId = null
            },
            new Category
            {
                Code = "CAT007",
                Name = "Máy Tính & Laptop",
                Alias = "may-tinh-laptop",
                Description = "Danh mục máy tính và laptop",
                Level = 1,
                FileName = "images/catalog/2a10b1d9-7953-44bf-a5b2-5c6420ea110b.png",
                OrderNumber = 7,
                Status = true,
                Path = "/may-tinh-laptop",
                ParentId = null
            },
            new Category
            {
                Code = "CAT008",
                Name = "Sắc Đẹp",
                Alias = "sac-dep",
                Description = "Danh mục sắc đẹp",
                Level = 1,
                FileName = "images/catalog/4e6c5e69-4b1b-4d72-8b66-a7001d629b06.png",
                OrderNumber = 8,
                Status = true,
                Path = "/sac-dep",
                ParentId = null
            },
            new Category
            {
                Code = "CAT009",
                Name = "Máy Ảnh & Máy Quay Phim",
                Alias = "may-anh-may-quay-phim",
                Description = "Danh mục máy ảnh và máy quay phim",
                Level = 1,
                FileName = "images/catalog/9415ae3a-cf33-467a-82ac-fee231eaae07.png",
                OrderNumber = 9,
                Status = true,
                Path = "/may-anh-may-quay-phim",
                ParentId = null
            },
            new Category
            {
                Code = "CAT010",
                Name = "Sức Khỏe",
                Alias = "suc-khoe",
                Description = "Danh mục sức khỏe",
                Level = 1,
                FileName = "images/catalog/d8efd53f-1d5e-4552-9b0c-623de671825f.png",
                OrderNumber = 10,
                Status = true,
                Path = "/suc-khoe",
                ParentId = null
            },
            new Category
            {
                Code = "CAT011",
                Name = "Đồng Hồ",
                Alias = "dong-ho",
                Description = "Danh mục đồng hồ",
                Level = 1,
                FileName = "images/catalog/438e5423-993c-434c-b5f4-fdd6180bec21.png",
                OrderNumber = 11,
                Status = true,
                Path = "/dong-ho",
                ParentId = null
            },
            new Category
            {
                Code = "CAT012",
                Name = "Giày Dép Nữ",
                Alias = "giay-dep-nu",
                Description = "Danh mục giày dép nữ",
                Level = 1,
                FileName = "images/catalog/4854f6e1-b735-4f6f-8937-faecd9478d8e.png",
                OrderNumber = 12,
                Status = true,
                Path = "/giay-dep-nu",
                ParentId = null
            },
            new Category
            {
                Code = "CAT013",
                Name = "Giày Dép Nam",
                Alias = "giay-dep-nam",
                Description = "Danh mục giày dép nam",
                Level = 1,
                FileName = "images/catalog/7f5fe912-8630-4751-b48a-c9fbfd1f9fae.png",
                OrderNumber = 13,
                Status = true,
                Path = "/giay-dep-nam",
                ParentId = null
            },
            new Category
            {
                Code = "CAT014",
                Name = "Túi Ví Nữ",
                Alias = "tui-vi-nu",
                Description = "Danh mục túi ví nữ",
                Level = 1,
                FileName = "images/catalog/f2b23b52-b232-47f5-bbbf-da7d83e1ce1a.png",
                OrderNumber = 14,
                Status = true,
                Path = "/tui-vi-nu",
                ParentId = null
            },
            new Category
            {
                Code = "CAT015",
                Name = "Thiết Bị Điện Gia Dụng",
                Alias = "thiet-bi-dien-gia-dung",
                Description = "Danh mục thiết bị điện gia dụng",
                Level = 1,
                FileName = "images/catalog/60d0a10b-cae6-49a0-8de7-58a7c957d184.png",
                OrderNumber = 15,
                Status = true,
                Path = "/thiet-bi-dien-gia-dung",
                ParentId = null
            },
            new Category
            {
                Code = "CAT016",
                Name = "Phụ Kiện & Trang Sức Nữ",
                Alias = "phu-kien-trang-suc-nu",
                Description = "Danh mục phụ kiện và trang sức nữ",
                Level = 1,
                FileName = "images/catalog/6f0d11ba-11c1-4704-8039-d8a189694830.png",
                OrderNumber = 16,
                Status = true,
                Path = "/phu-kien-trang-suc-nu",
                ParentId = null
            },
            new Category
            {
                Code = "CAT017",
                Name = "Thể Thao & Du Lịch",
                Alias = "the-thao-du-lich",
                Description = "Danh mục thể thao và du lịch",
                Level = 1,
                FileName = "images/catalog/e3de60fa-d5dc-432f-925a-0fe3f168957f.png",
                OrderNumber = 17,
                Status = true,
                Path = "/the-thao-du-lich",
                ParentId = null
            },
            new Category
            {
                Code = "CAT018",
                Name = "Bách Hóa Online",
                Alias = "bach-hoa-online",
                Description = "Danh mục bách hóa trực tuyến",
                Level = 1,
                FileName = "images/catalog/2924460f-bdb9-468f-be95-19077ce5bfe2.png",
                OrderNumber = 18,
                Status = true,
                Path = "/bach-hoa-online",
                ParentId = null
            },
            new Category
            {
                Code = "CAT019",
                Name = "Ô Tô & Xe Máy & Xe Đạp",
                Alias = "oto-xe-may-xe-dap",
                Description = "Danh mục ô tô, xe máy và xe đạp",
                Level = 1,
                FileName = "images/catalog/e36924a9-a83e-41c0-ade0-a85fb17e4151.png",
                OrderNumber = 19,
                Status = true,
                Path = "/oto-xe-may-xe-dap",
                ParentId = null
            },
            new Category
            {
                Code = "CAT020",
                Name = "Nhà Sách Online",
                Alias = "nha-sach-online",
                Description = "Danh mục nhà sách trực tuyến",
                Level = 1,
                FileName = "images/catalog/eb102843-df32-44d9-ada7-fe5352ce4b32.png",
                OrderNumber = 20,
                Status = true,
                Path = "/nha-sach-online",
                ParentId = null
            }
        };

        await _context.Categories.AddRangeAsync(categories);
        await _context.SaveChangesAsync();
    }

    private async Task SeedSupplierAsync()
    {
        if (await _context.Suppliers.AnyAsync()) return;

        var suppliers = new List<Supplier>
        {
            new Supplier()
            {
                Code = "NCC001",
                Name = "Apple",
                Alias = "apple",
                Description = "Nhà cung cấp điện thoại Apple",
                Delegate = "Đỗ Chí Hùng",
                Bank = "MB Bank",
                AccountNumber = "0976580418",
                BankAddress = "Tầng 1 - Toà Nhà 17T2 - Đ.Hoàng Đạo Thuý - Trung Hoà - Cầu Giấy - Hà Nội",
                AddressOne = "Đông Kết, Khoái Châu, Hưng Yên",
                AddressTwo = "301 Kim Mã, Ba Đình, Hà Nội",
                Phone = "0976580418",
                Fax = "833000",
                NationCode = "0084",
                ProvinceCode = "01",
                DistrictCode = "001"
            },
            new Supplier()
            {
                Code = "NCC002",
                Name = "Sam sung",
                Alias = "sam-sung",
                Description = "Nhà cung cấp điện thoại Samsung",
                Delegate = "Kim Jong-un",
                Bank = "Vietcombank",
                AccountNumber = "1234567890",
                BankAddress = "Tầng 2 - Toà Nhà 20T3 - Phố Lê Văn Lương - Thanh Xuân - Hà Nội",
                AddressOne = "Số 10, Đường Số 5, Quận 7, TP. Hồ Chí Minh",
                AddressTwo = "45 Đinh Tiên Hoàng, Quận 1, TP. Hồ Chí Minh",
                Phone = "0987654321",
                Fax = "833001",
                NationCode = "0084",
                ProvinceCode = "02",
                DistrictCode = "002"
            },
            new Supplier()
            {
                Code = "NCC003",
                Name = "LG",
                Alias = "lg",
                Description = "Nhà cung cấp thiết bị điện tử LG",
                Delegate = "Park Jung-min",
                Bank = "Shinhan Bank",
                AccountNumber = "0987654321",
                BankAddress = "Tầng 3 - Toà Nhà 15T5 - Đường Phạm Hùng - Mỹ Đình - Nam Từ Liêm - Hà Nội",
                AddressOne = "Số 25, Đường Số 6, Quận 9, TP. Hồ Chí Minh",
                AddressTwo = "89 Lý Thường Kiệt, Quận Hoàn Kiếm, Hà Nội",
                Phone = "0933334444",
                Fax = "833002",
                NationCode = "0084",
                ProvinceCode = "03",
                DistrictCode = "003"
            },
            // Thêm nhà cung cấp thứ tư
            new Supplier()
            {
                Code = "NCC004",
                Name = "Xiaomi",
                Alias = "xiaomi",
                Description = "Nhà cung cấp thiết bị điện tử Xiaomi",
                Delegate = "Lei Jun",
                Bank = "Techcombank",
                AccountNumber = "2009876543",
                BankAddress = "Tầng 4 - Toà Nhà 25T4 - Đường Nguyễn Chí Thanh - Đống Đa - Hà Nội",
                AddressOne = "Lô D, Khu công nghệ cao, Quận 9, TP. Hồ Chí Minh",
                AddressTwo = "100 Bà Triệu, Quận Hai Bà Trưng, Hà Nội",
                Phone = "0945678901",
                Fax = "833003",
                NationCode = "0084",
                ProvinceCode = "04",
                DistrictCode = "004"
            },
            new Supplier()
            {
                Code = "NCC005",
                Name = "Huawei",
                Alias = "huawei",
                Description = "Nhà cung cấp thiết bị mạng và điện thoại Huawei",
                Delegate = "Ren Zhengfei",
                Bank = "HSBC Bank",
                AccountNumber = "1122334455",
                BankAddress = "Tầng 5 - Toà Nhà 30T5 - Đường Trần Duy Hưng - Cầu Giấy - Hà Nội",
                AddressOne = "Khu công nghệ cao, Quận Thanh Xuân, Hà Nội",
                AddressTwo = "22 Lê Lợi, Quận 1, TP. Hồ Chí Minh",
                Phone = "0912345678",
                Fax = "833004",
                NationCode = "0084",
                ProvinceCode = "05",
                DistrictCode = "005"
            },
            new Supplier()
            {
                Code = "NCC006",
                Name = "Sony",
                Alias = "sony",
                Description = "Nhà cung cấp thiết bị điện tử Sony",
                Delegate = "Kenichiro Yoshida",
                Bank = "Sumitomo Mitsui Banking Corporation",
                AccountNumber = "5566778899",
                BankAddress = "Tầng 6 - Toà Nhà 33T6 - Đường Lý Thường Kiệt - Hoàn Kiếm - Hà Nội",
                AddressOne = "15 Đường 3/2, Quận 10, TP. Hồ Chí Minh",
                AddressTwo = "67 Hàng Bài, Quận Hoàn Kiếm, Hà Nội",
                Phone = "0967890123",
                Fax = "833005",
                NationCode = "0084",
                ProvinceCode = "06",
                DistrictCode = "006"
            },

            new Supplier()
            {
                Code = "NCC007",
                Name = "Asus",
                Alias = "asus",
                Description = "Nhà cung cấp thiết bị điện tử và máy tính Asus",
                Delegate = "Samson Hu",
                Bank = "Taiwan Cooperative Bank",
                AccountNumber = "9988776655",
                BankAddress = "Tầng 7 - Toà Nhà 37T7 - Đường Xuân Thủy - Cầu Giấy - Hà Nội",
                AddressOne = "23 Thái Hà, Đống Đa, Hà Nội",
                AddressTwo = "88 Cao Thắng, Quận 3, TP. Hồ Chí Minh",
                Phone = "0987654323",
                Fax = "833006",
                NationCode = "0084",
                ProvinceCode = "07",
                DistrictCode = "007"
            },

            new Supplier()
            {
                Code = "NCC008",
                Name = "Dell",
                Alias = "dell",
                Description = "Nhà cung cấp máy tính và thiết bị điện tử Dell",
                Delegate = "Michael Dell",
                Bank = "Citibank",
                AccountNumber = "2233445566",
                BankAddress = "Tầng 8 - Toà Nhà 40T8 - Đường Nguyễn Chí Thanh - Đống Đa - Hà Nội",
                AddressOne = "49 Lê Ngọc Hân, Hai Bà Trưng, Hà Nội",
                AddressTwo = "34 Lê Duẩn, Quận 1, TP. Hồ Chí Minh",
                Phone = "0976543210",
                Fax = "833007",
                NationCode = "0084",
                ProvinceCode = "08",
                DistrictCode = "008"
            },

            new Supplier()
            {
                Code = "NCC009",
                Name = "Lenovo",
                Alias = "lenovo",
                Description = "Nhà cung cấp máy tính và thiết bị điện tử Lenovo",
                Delegate = "Yang Yuanqing",
                Bank = "China Construction Bank",
                AccountNumber = "6655778899",
                BankAddress = "Tầng 9 - Toà Nhà 45T9 - Đường Trần Phú - Ba Đình - Hà Nội",
                AddressOne = "5 Đinh Tiên Hoàng, Quận Hoàn Kiếm, Hà Nội",
                AddressTwo = "60 Nguyễn Huệ, Quận 1, TP. Hồ Chí Minh",
                Phone = "0912345679",
                Fax = "833008",
                NationCode = "0084",
                ProvinceCode = "09",
                DistrictCode = "009"
            },

            new Supplier()
            {
                Code = "NCC010",
                Name = "HP",
                Alias = "hp",
                Description = "Nhà cung cấp máy tính và thiết bị điện tử HP",
                Delegate = "Enrique Lores",
                Bank = "Wells Fargo",
                AccountNumber = "1233211234",
                BankAddress = "Tầng 10 - Toà Nhà 50T10 - Đường Trần Hưng Đạo - Hoàn Kiếm - Hà Nội",
                AddressOne = "15 Lý Thái Tổ, Quận Hoàn Kiếm, Hà Nội",
                AddressTwo = "75 Nam Kỳ Khởi Nghĩa, Quận 1, TP. Hồ Chí Minh",
                Phone = "0932109876",
                Fax = "833009",
                NationCode = "0084",
                ProvinceCode = "10",
                DistrictCode = "010"
            },
            new Supplier()
            {
                Code = "NCC011",
                Name = "Acer",
                Alias = "acer",
                Description = "Nhà cung cấp thiết bị điện tử và máy tính Acer",
                Delegate = "Jason Chen",
                Bank = "ANZ Bank",
                AccountNumber = "3344556677",
                BankAddress = "Tầng 11 - Toà Nhà 55T11 - Đường Lê Lợi - Hoàn Kiếm - Hà Nội",
                AddressOne = "22 Thanh Niên, Tây Hồ, Hà Nội",
                AddressTwo = "18 Phạm Ngọc Thạch, Quận 3, TP. Hồ Chí Minh",
                Phone = "0943210987",
                Fax = "833010",
                NationCode = "0084",
                ProvinceCode = "11",
                DistrictCode = "011"
            },

            new Supplier()
            {
                Code = "NCC012",
                Name = "Toshiba",
                Alias = "toshiba",
                Description = "Nhà cung cấp thiết bị điện tử và máy tính Toshiba",
                Delegate = "Nobuaki Kurumatani",
                Bank = "Mizuho Bank",
                AccountNumber = "5566887799",
                BankAddress = "Tầng 12 - Toà Nhà 60T12 - Đường Điện Biên Phủ - Ba Đình - Hà Nội",
                AddressOne = "89 Cầu Giấy, Quận Cầu Giấy, Hà Nội",
                AddressTwo = "33 Lê Duẩn, Quận 1, TP. Hồ Chí Minh",
                Phone = "0987654324",
                Fax = "833011",
                NationCode = "0084",
                ProvinceCode = "12",
                DistrictCode = "012"
            },

            new Supplier()
            {
                Code = "NCC013",
                Name = "Panasonic",
                Alias = "panasonic",
                Description = "Nhà cung cấp thiết bị điện tử Panasonic",
                Delegate = "Kazuhiro Tsuga",
                Bank = "Bank of Tokyo-Mitsubishi UFJ",
                AccountNumber = "6677889900",
                BankAddress = "Tầng 13 - Toà Nhà 65T13 - Đường Trần Nhân Tông - Hai Bà Trưng - Hà Nội",
                AddressOne = "50 Lý Quốc Sư, Hoàn Kiếm, Hà Nội",
                AddressTwo = "27 Lê Quý Đôn, Quận 3, TP. Hồ Chí Minh",
                Phone = "0976543214",
                Fax = "833012",
                NationCode = "0084",
                ProvinceCode = "13",
                DistrictCode = "013"
            },
            new Supplier()
            {
                Code = "NCC014",
                Name = "Google",
                Alias = "google",
                Description = "Nhà cung cấp dịch vụ công nghệ thông tin và sản phẩm kỹ thuật số",
                Delegate = "Sundar Pichai",
                Bank = "Silicon Valley Bank",
                AccountNumber = "2244668800",
                BankAddress = "Tầng 14 - Toà Nhà 14 Silicon - Đường Phạm Hùng - Nam Từ Liêm - Hà Nội",
                AddressOne = "Khu Công nghệ cao, Quận 9, TP. Hồ Chí Minh",
                AddressTwo = "52 Lý Thường Kiệt, Hoàn Kiếm, Hà Nội",
                Phone = "0912233445",
                Fax = "833014",
                NationCode = "0084",
                ProvinceCode = "14",
                DistrictCode = "014"
            },

            new Supplier()
            {
                Code = "NCC015",
                Name = "Facebook",
                Alias = "facebook",
                Description = "Nhà cung cấp dịch vụ mạng xã hội và quảng cáo trực tuyến",
                Delegate = "Mark Zuckerberg",
                Bank = "Bank of America",
                AccountNumber = "5566778899",
                BankAddress = "Tầng 15 - Toà Nhà 15A - Đường Lê Hồng Phong - Ba Đình - Hà Nội",
                AddressOne = "38 Yên Phụ, Tây Hồ, Hà Nội",
                AddressTwo = "90 Nguyễn Huệ, Quận 1, TP. Hồ Chí Minh",
                Phone = "0987654321",
                Fax = "833015",
                NationCode = "0084",
                ProvinceCode = "15",
                DistrictCode = "015"
            },
            new Supplier()
            {
                Code = "NCC014",
                Name = "Google",
                Alias = "google",
                Description = "Nhà cung cấp thiết bị điện tử và dịch vụ công nghệ thông tin Google",
                Delegate = "Sundar Pichai",
                Bank = "Bank of America",
                AccountNumber = "7788990011",
                BankAddress = "Tầng 14 - Toà Nhà 70T14 - Đường Bà Triệu - Hai Bà Trưng - Hà Nội",
                AddressOne = "35 Nguyễn Chí Thanh, Đống Đa, Hà Nội",
                AddressTwo = "48 Thảo Điền, Quận 2, TP. Hồ Chí Minh",
                Phone = "0912345671",
                Fax = "833013",
                NationCode = "0084",
                ProvinceCode = "14",
                DistrictCode = "014"
            },

            new Supplier()
            {
                Code = "NCC015",
                Name = "Microsoft",
                Alias = "microsoft",
                Description = "Nhà cung cấp phần mềm và dịch vụ công nghệ thông tin Microsoft",
                Delegate = "Satya Nadella",
                Bank = "JPMorgan Chase Bank",
                AccountNumber = "1122334455",
                BankAddress = "Tầng 15 - Toà Nhà 75T15 - Đường Nguyễn Du - Hoàn Kiếm - Hà Nội",
                AddressOne = "18B Phố Huế, Hai Bà Trưng, Hà Nội",
                AddressTwo = "22 Lê Lai, Quận 1, TP. Hồ Chí Minh",
                Phone = "0987654325",
                Fax = "833014",
                NationCode = "0084",
                ProvinceCode = "15",
                DistrictCode = "015"
            },

            new Supplier()
            {
                Code = "NCC016",
                Name = "IBM",
                Alias = "ibm",
                Description = "Nhà cung cấp phần mềm và dịch vụ công nghệ thông tin IBM",
                Delegate = "Arvind Krishna",
                Bank = "Citi Bank",
                AccountNumber = "5566778899",
                BankAddress = "Tầng 16 - Toà Nhà 80T16 - Đường Lê Thanh Nghị - Hai Bà Trưng - Hà Nội",
                AddressOne = "50A Đường Võ Văn Tần, Quận 3, TP. Hồ Chí Minh",
                AddressTwo = "30 Lê Thánh Tôn, Quận 1, TP. Hồ Chí Minh",
                Phone = "0943210988",
                Fax = "833015",
                NationCode = "0084",
                ProvinceCode = "16",
                DistrictCode = "016"
            },

            new Supplier()
            {
                Code = "NCC017",
                Name = "Oracle",
                Alias = "oracle",
                Description = "Nhà cung cấp phần mềm và dịch vụ công nghệ thông tin Oracle",
                Delegate = "Safra Catz",
                Bank = "Wells Fargo",
                AccountNumber = "7788990012",
                BankAddress = "Tầng 17 - Toà Nhà 85T17 - Đường Trần Hưng Đạo - Hoàn Kiếm - Hà Nội",
                AddressOne = "15A Lý Thái Tổ, Quận Hoàn Kiếm, Hà Nội",
                AddressTwo = "44 Lê Lai, Quận 1, TP. Hồ Chí Minh",
                Phone = "0912345672",
                Fax = "833016",
                NationCode = "0084",
                ProvinceCode = "17",
                DistrictCode = "017"
            },

            new Supplier()
            {
                Code = "NCC018",
                Name = "Cisco",
                Alias = "cisco",
                Description = "Nhà cung cấp thiết bị mạng Cisco",
                Delegate = "Chuck Robbins",
                Bank = "Bank of China",
                AccountNumber = "9988776656",
                BankAddress = "Tầng 18 - Toà Nhà 90T18 - Đường Lê Ngọc Hân - Hoàn Kiếm - Hà Nội",
                AddressOne = "10B Đường Điện Biên Phủ, Ba Đình, Hà Nội",
                AddressTwo = "25 Lê Thánh Tôn, Quận 1, TP. Hồ Chí Minh",
                Phone = "0987654326",
                Fax = "833017",
                NationCode = "0084",
                ProvinceCode = "18",
                DistrictCode = "018"
            },

            new Supplier()
            {
                Code = "NCC019",
                Name = "Adobe",
                Alias = "adobe",
                Description = "Nhà cung cấp phần mềm đồ họa Adobe",
                Delegate = "Shantanu Narayen",
                Bank = "Standard Chartered Bank",
                AccountNumber = "1122334456",
                BankAddress = "Tầng 19 - Toà Nhà 95T19 - Đường Lê Đại Hành - Hai Bà Trưng - Hà Nội",
                AddressOne = "25 Lê Duẩn, Quận 1, TP. Hồ Chí Minh",
                AddressTwo = "40 Hàng Bài, Quận Hoàn Kiếm, Hà Nội",
                Phone = "0912345673",
                Fax = "833018",
                NationCode = "0084",
                ProvinceCode = "19",
                DistrictCode = "019"
            },

            new Supplier()
            {
                Code = "NCC020",
                Name = "Nokia",
                Alias = "nokia",
                Description = "Nhà cung cấp thiết bị di động và mạng Nokia",
                Delegate = "Pekka Lundmark",
                Bank = "Deutsche Bank",
                AccountNumber = "5566778890",
                BankAddress = "Tầng 20 - Toà Nhà 100T20 - Đường Hàm Nghi - Nam Từ Liêm - Hà Nội",
                AddressOne = "68A Nguyễn Huệ, Quận 1, TP. Hồ Chí Minh",
                AddressTwo = "10 Hàng Bài, Quận Hoàn Kiếm, Hà Nội",
                Phone = "0943210989",
                Fax = "833019",
                NationCode = "0084",
                ProvinceCode = "20",
                DistrictCode = "020"
            },
        };

        await _context.Suppliers.AddRangeAsync(suppliers);
        await _context.SaveChangesAsync();
    }

    private async Task ReadAndSeedLocationsAsync()
    {
        string relativePath = "Excels/province-district-ward.xlsx";

        var dirPath = Assembly.GetExecutingAssembly().Location;
        string fullPath = Path.Combine(Path.GetDirectoryName(dirPath), relativePath);
        if (!File.Exists(fullPath))
        {
            return;
        }

        FileInfo fileInfo = new FileInfo(fullPath);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using var package = new ExcelPackage(fileInfo);
        await package.LoadAsync(fileInfo);
        var worksheet = package.Workbook.Worksheets[0];

        var row = 2;
        LocationProvince currentProvince = null;
        LocationDistrict currentDistrict = null;
        while (true)
        {
            var provinceName = worksheet.Cells[row, 1].Text;
            var provinceCode = worksheet.Cells[row, 2].Text;

            var districtName = worksheet.Cells[row, 3].Text;
            var districtCode = worksheet.Cells[row, 4].Text;

            var wardName = worksheet.Cells[row, 5].Text;
            var wardCode = worksheet.Cells[row, 6].Text;


            if (currentProvince != null && currentProvince.Code != provinceCode)
            {
                await _context.Provinces.AddAsync(currentProvince);
                await _context.SaveChangesAsync();

                currentProvince = null;
                currentDistrict = null;
            }

            if (currentProvince == null || currentProvince.Code != provinceCode)
            {
                currentProvince = new LocationProvince
                {
                    Code = provinceCode,
                    Name = provinceName,
                    Slug = provinceName.ToUnsignString(),
                    Type = LocationType.Province,
                    Districts = new List<LocationDistrict>()
                };
            }

            if (currentDistrict != null && currentDistrict.Code != districtCode)
            {
                currentProvince.Districts.Add(currentDistrict);
                currentDistrict = null;
            }

            if (currentDistrict == null || currentDistrict.Code != districtCode)
            {
                currentDistrict = new LocationDistrict
                {
                    Code = districtCode,
                    Name = districtName,
                    Slug = districtName.ToUnsignString(),
                    Type = LocationType.District,
                    Province = currentProvince,
                    Wards = new List<LocationWard>()
                };
            }

            currentDistrict.Wards.Add(new LocationWard
            {
                Code = wardCode,
                Name = wardName,
                Slug = wardName.ToUnsignString(),
                Type = LocationType.Ward,
                District = currentDistrict
            });


            if (row == worksheet.Dimension.Rows) break; // Nếu đã đọc hết dữ liệu
            row++;
        }
    }
}