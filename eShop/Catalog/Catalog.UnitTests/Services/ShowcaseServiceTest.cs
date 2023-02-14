using System.Linq;
using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Responses;
using Moq;

namespace Catalog.UnitTests.Services;

public class ShowcaseServiceTest
{
    private readonly IShowcaseService _catalogService;

    private readonly Mock<ITypeRepository> _typeRepository;
    private readonly Mock<IBrandRepository> _brandRepository;
    private readonly Mock<ICategoryRepository> _categoryRepository;
    private readonly Mock<IClothingRepository> _clothingRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<ShowcaseService>> _logger;

    public ShowcaseServiceTest()
    {
        _typeRepository = new Mock<ITypeRepository>();
        _brandRepository = new Mock<IBrandRepository>();
        _categoryRepository = new Mock<ICategoryRepository>();
        _clothingRepository = new Mock<IClothingRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<ShowcaseService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new ShowcaseService(_dbContextWrapper.Object, _logger.Object, _brandRepository.Object, _typeRepository.Object, _categoryRepository.Object, _clothingRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetByPageAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 4;
        var testTotalCount = 12;

        var pagingPaginatedItemsSuccess = new PaginatedItems<Clothing>()
        {
            Content = new List<Clothing>()
            {
                new Clothing()
                {
                    Name = "TestName",
                },
                new Clothing()
                {
                    Name = "TestName2",
                }
            },
            Count = testTotalCount,
        };

        var clothingSuccess = new Clothing()
        {
            Name = "TestName"
        };

        var clothingDtoSuccess = new ClothingDto()
        {
            Name = "TestName"
        };

        _clothingRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize),
            It.IsAny<int?>(),
            It.IsAny<int?>())).ReturnsAsync(pagingPaginatedItemsSuccess);

        _mapper.Setup(s => s.Map<ClothingDto>(
            It.Is<Clothing>(i => i.Equals(clothingSuccess)))).Returns(clothingDtoSuccess);

        // act
        var result = await _catalogService.GetByPageAsync(testPageSize, testPageIndex);

        // assert
        result?.Should().NotBeNull();
        result?.Content.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
    }

    [Fact]
    public async Task GetByPageAsync_Failed()
    {
        // arrange
        var testPageIndex = 1000;
        var testPageSize = 10000;

        _clothingRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize),
            It.IsAny<int?>(),
            It.IsAny<int?>())).Returns((Func<PaginatedItemResponse<ClothingDto>>)null!);

        // act
        var result = await _catalogService.GetByPageAsync(testPageSize, testPageIndex, null);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetClothingByIdAsync_Success()
    {
        // arrange
        var testId = 1;
        var clothingSuccess = new Clothing()
        {
            Name = "TestName"
        };
        var clothingDtoSuccess = new ClothingDto()
        {
            Name = "test"
        };

        _clothingRepository.Setup(s => s.GetByIdAsync(It.Is<int>(i => i == testId))).ReturnsAsync(clothingSuccess);
        _mapper.Setup(s => s.Map<ClothingDto>(It.Is<Clothing>(i => i.Equals(clothingSuccess)))).Returns(clothingDtoSuccess);

        // act
        var result = await _catalogService.GetClothingByIdAsync(testId);

        // assert
        result.Should().NotBeNull();
        result?.Name.Should().NotBeNull();
    }

    [Fact]
    public async Task GetClothingByIdAsync_Failed()
    {
        // arrange
        var testId = 100000;
        _clothingRepository.Setup(s => s.GetByIdAsync(It.Is<int>(i => i.Equals(testId)))).Returns((Func<ClothingDto>)null!);

        // act
        var result = await _catalogService.GetClothingByIdAsync(testId);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAllBrandsAsync_Success()
    {
        var brands = new List<Brand>()
        {
            new Brand()
            {
                Name = "Test"
            }
        };
        var brandSuccess = new Brand()
        {
            Name = "Test"
        };
        var brandDtoSuccess = new BrandDto()
        {
            Name = "Test"
        };

        _brandRepository.Setup(s => s.GetAllAsync()).ReturnsAsync(brands);

        _mapper.Setup(s => s.Map<BrandDto>(
            It.Is<Brand>(i => i.Equals(brandSuccess)))).Returns(brandDtoSuccess);

    // act
        var result = await _catalogService.GetAllBrandsAsync();

    // assert
        result.Should().NotBeNull();
        result.LongCount().Should().Be(1);
    }

    [Fact]
    public async Task GetAllBrandsAsync_Failed()
    {
        // arrange
        _brandRepository.Setup(s => s.GetAllAsync()).Returns((Func<PaginatedItemResponse<BrandDto>>)null!);

        // actUniversalGetItemsResponse
        var result = await _catalogService.GetAllBrandsAsync();

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAllTypesAsync_Success()
    {
        var types = new List<TypeOfClothing>()
        {
            new TypeOfClothing()
            {
                Name = "Test"
            }
        };
        var typeSuccess = new TypeOfClothing()
        {
            Name = "Test"
        };
        var typeDtoSuccess = new TypeDto()
        {
            Name = "Test"
        };

        _typeRepository.Setup(s => s.GetAllAsync()).ReturnsAsync(types);

        _mapper.Setup(s => s.Map<TypeDto>(
            It.Is<TypeOfClothing>(i => i.Equals(typeSuccess)))).Returns(typeDtoSuccess);

        // act
        var result = await _catalogService.GetAllTypesAsync();

        // assert
        result.Should().NotBeNull();
        result.LongCount().Should().Be(1);
    }

    [Fact]
    public async Task GetAllTypesAsync_Failed()
    {
        // arrange
        _typeRepository.Setup(s => s.GetAllAsync()).Returns((Func<PaginatedItemResponse<TypeDto>>)null!);

        // actUniversalGetItemsResponse
        var result = await _catalogService.GetAllTypesAsync();

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAllCategoriesAsync_Success()
    {
        var categories = new List<Category>()
        {
            new Category()
            {
                Name = "Test"
            }
        };
        var categorySuccess = new Category()
        {
            Name = "Test"
        };
        var categoryDtoSuccess = new CategoryDto()
        {
            Name = "Test"
        };

        _categoryRepository.Setup(s => s.GetAllAsync()).ReturnsAsync(categories);

        _mapper.Setup(s => s.Map<CategoryDto>(
            It.Is<Category>(i => i.Equals(categorySuccess)))).Returns(categoryDtoSuccess);

        // act
        var result = await _catalogService.GetAllCategoriesAsync();

        // assert
        result.Should().NotBeNull();
        result.LongCount().Should().Be(1);
    }

    [Fact]
    public async Task GetAllCategoriesAsync_Failed()
    {
        // arrange
        _categoryRepository.Setup(s => s.GetAllAsync()).Returns((Func<PaginatedItemResponse<CategoryDto>>)null!);

        // actUniversalGetItemsResponse
        var result = await _catalogService.GetAllCategoriesAsync();

        // assert
        result.Should().BeNull();
    }
}