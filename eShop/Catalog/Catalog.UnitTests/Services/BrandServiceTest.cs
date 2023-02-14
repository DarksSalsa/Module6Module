using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Responses;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Core.Services.Interfaces;

namespace Catalog.UnitTests.Services
{
    public class BrandServiceTest
    {
        private readonly IBrandService _catalogService;

        private readonly Mock<IBrandRepository> _brandRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<BrandService>> _logger;
        private readonly Mock<IMapper> _mapper;

        private readonly Brand _catalogBrand = new Brand()
        {
            Name = "Test"
        };

        public BrandServiceTest()
        {
            _brandRepository = new Mock<IBrandRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<BrandService>>();
            _mapper = new Mock<IMapper>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogService = new BrandService(_dbContextWrapper.Object, _logger.Object, _brandRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testId = 1;

            _brandRepository.Setup(s => s.AddAsync(It.IsAny<string>())).ReturnsAsync(testId);

            // act
            var result = await _catalogService.AddAsync(_catalogBrand.Name);

            // assert
            result.Should().Be(testId);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            int? testId = null;

            _brandRepository.Setup(s => s.AddAsync(It.IsAny<string>())).ReturnsAsync(testId);

            // act
            var result = await _catalogService.AddAsync(_catalogBrand.Name);

            // assert
            result.Should().Be(testId);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // arrange
            var testId = 1;
            _brandRepository.Setup(s => s.DeleteAsync(It.Is<int>(i => i == testId))).ReturnsAsync(true);

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
            _brandRepository.Setup(s => s.DeleteAsync(It.Is<int>(i => i.Equals(testId)))).Returns((Func<UniversalDeleteResponse>)null!);

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

            var catalogBrandDtoSuccess = new BrandDto()
            {
                Name = "Test"
            };

            _brandRepository.Setup(s => s.UpdateAsync(
                It.Is<int>(i => i.Equals(testId)),
                It.Is<string>(i => i.Equals(testProperty)),
                It.Is<string>(i => i.Equals(testValue)))).ReturnsAsync(_catalogBrand);
            _mapper.Setup(s => s.Map<BrandDto>(It.Is<Brand>(i => i.Equals(_catalogBrand)))).Returns(catalogBrandDtoSuccess);

            // act
            var result = await _catalogService.UpdateAsync(testId, testProperty, testValue);

            // assert
            result.Should().NotBeNull();
            result?.Name.Should().Be(catalogBrandDtoSuccess.Name);
        }

        [Fact]
        public async Task UpdateAsync_Failed()
        {
            // arrange
            var testId = 1000000;
            var testProperty = "testProperty";
            var testValue = "testValue";

            _brandRepository.Setup(s => s.UpdateAsync(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Returns((Func<UpdateBrandResponse>)null!);

            // act
            var result = await _catalogService.UpdateAsync(testId, testProperty, testValue);

            // assert
            result.Should().BeNull();
        }
    }
}
