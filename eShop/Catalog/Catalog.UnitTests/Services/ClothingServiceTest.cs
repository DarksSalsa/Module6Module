using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Responses;

namespace Catalog.UnitTests.Services
{
    public class ClothingServiceTest
    {
        private readonly IClothingService _catalogService;

        private readonly Mock<IClothingRepository> _clothingRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<ClothingService>> _logger;
        private readonly Mock<IMapper> _mapper;

        private readonly Clothing _catalogType = new Clothing()
        {
            Name = "Test",
            Color = "Test",
            AvailableStock = 100,
            BrandId = 1,
            CategoryId = 1,
            Image = "Test",
            Season = "Test",
            Size = "Test",
            Price = 100M
        };

        public ClothingServiceTest()
        {
            _clothingRepository = new Mock<IClothingRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<ClothingService>>();
            _mapper = new Mock<IMapper>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogService = new ClothingService(_dbContextWrapper.Object, _logger.Object, _clothingRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testId = 1;

            _clothingRepository.Setup(s => s.AddAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(testId);

            // act
            var result = await _catalogService.AddAsync(_catalogType.Name, _catalogType.Color, _catalogType.Size, _catalogType.CategoryId, _catalogType.BrandId, _catalogType.Price, _catalogType.AvailableStock, _catalogType.Image, _catalogType.Season);

            // assert
            result.Should().Be(testId);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            int? testId = null;

            _clothingRepository.Setup(s => s.AddAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(testId);

            // act
            var result = await _catalogService.AddAsync(_catalogType.Name, _catalogType.Color, _catalogType.Size, _catalogType.CategoryId, _catalogType.BrandId, _catalogType.Price, _catalogType.AvailableStock, _catalogType.Image, _catalogType.Season);

            // assert
            result.Should().Be(testId);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // arrange
            var testId = 1;
            _clothingRepository.Setup(s => s.DeleteAsync(It.Is<int>(i => i == testId))).ReturnsAsync(true);

            // act
            var result = await _catalogService.DeleteAsync(testId);

            // assert
            result.Should().Be(true);
        }

        [Fact]
        public async Task DeleteAsync_Failed()
        {
            // arrange
            var testId = 10000;
            _clothingRepository.Setup(s => s.DeleteAsync(It.Is<int>(i => i.Equals(testId)))).Returns((Func<UniversalDeleteResponse>)null!);

            // act
            var result = await _catalogService.DeleteAsync(It.Is<int>(i => i.Equals(testId)));

            // assert
            result.Should().Be(false);
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            // arrange
            var testId = 1;
            var testProperty = "testProperty";
            var testValue = "testValue";

            var catalogClothingDtoSuccess = new ClothingDto()
            {
                Name = "Test"
            };

            _clothingRepository.Setup(s => s.UpdateAsync(
                It.Is<int>(i => i.Equals(testId)),
                It.Is<string>(i => i.Equals(testProperty)),
                It.Is<string>(i => i.Equals(testValue)))).ReturnsAsync(_catalogType);
            _mapper.Setup(s => s.Map<ClothingDto>(It.Is<Clothing>(i => i.Equals(_catalogType)))).Returns(catalogClothingDtoSuccess);

            // act
            var result = await _catalogService.UpdateAsync(testId, testProperty, testValue);

            // assert
            result.Should().NotBeNull();
            result?.Name.Should().Be(catalogClothingDtoSuccess.Name);
        }

        [Fact]
        public async Task UpdateAsync_Failed()
        {
            // arrange
            var testId = 1000000;
            var testProperty = "testProperty";
            var testValue = "testValue";

            _clothingRepository.Setup(s => s.UpdateAsync(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Returns((Func<UpdateClothingResponse>)null!);

            // act
            var result = await _catalogService.UpdateAsync(testId, testProperty, testValue);

            // assert
            result.Should().BeNull();
        }
    }
}
