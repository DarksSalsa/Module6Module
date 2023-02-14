using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Responses;
using Infrastructure.Core.Services.Interfaces;

namespace Catalog.UnitTests.Services
{
    public class TypeServiceTest
    {
        private readonly ITypeService _catalogService;

        private readonly Mock<ITypeRepository> _typeRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<TypeService>> _logger;
        private readonly Mock<IMapper> _mapper;

        private readonly TypeOfClothing _catalogType = new TypeOfClothing()
        {
            Name = "Test"
        };

        public TypeServiceTest()
        {
            _typeRepository = new Mock<ITypeRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<TypeService>>();
            _mapper = new Mock<IMapper>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogService = new TypeService(_dbContextWrapper.Object, _logger.Object, _typeRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testId = 1;

            _typeRepository.Setup(s => s.AddAsync(It.IsAny<string>())).ReturnsAsync(testId);

            // act
            var result = await _catalogService.AddAsync(_catalogType.Name);

            // assert
            result.Should().Be(testId);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            int? testId = null;

            _typeRepository.Setup(s => s.AddAsync(It.IsAny<string>())).ReturnsAsync(testId);

            // act
            var result = await _catalogService.AddAsync(_catalogType.Name);

            // assert
            result.Should().Be(testId);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // arrange
            var testId = 1;
            _typeRepository.Setup(s => s.DeleteAsync(It.Is<int>(i => i == testId))).ReturnsAsync(true);

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
            _typeRepository.Setup(s => s.DeleteAsync(It.Is<int>(i => i.Equals(testId)))).Returns((Func<UniversalDeleteResponse>)null!);

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

            var catalogTypeDtoSuccess = new TypeDto()
            {
                Name = "Test"
            };

            _typeRepository.Setup(s => s.UpdateAsync(
                It.Is<int>(i => i.Equals(testId)),
                It.Is<string>(i => i.Equals(testProperty)),
                It.Is<string>(i => i.Equals(testValue)))).ReturnsAsync(_catalogType);
            _mapper.Setup(s => s.Map<TypeDto>(It.Is<TypeOfClothing>(i => i.Equals(_catalogType)))).Returns(catalogTypeDtoSuccess);

            // act
            var result = await _catalogService.UpdateAsync(testId, testProperty, testValue);

            // assert
            result.Should().NotBeNull();
            result?.Name.Should().Be(catalogTypeDtoSuccess.Name);
        }

        [Fact]
        public async Task UpdateAsync_Failed()
        {
            // arrange
            var testId = 1000000;
            var testProperty = "testProperty";
            var testValue = "testValue";

            _typeRepository.Setup(s => s.UpdateAsync(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Returns((Func<UpdateTypeResponse>)null!);

            // act
            var result = await _catalogService.UpdateAsync(testId, testProperty, testValue);

            // assert
            result.Should().BeNull();
        }
    }
}
