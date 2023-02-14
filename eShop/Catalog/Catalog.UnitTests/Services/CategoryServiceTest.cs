using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Responses;

namespace Catalog.UnitTests.Services
{
    public class CategoryServiceTest
    {
        private readonly ICategoryService _catalogService;

        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CategoryService>> _logger;
        private readonly Mock<IMapper> _mapper;

        private readonly Category _catalogCategory = new Category()
        {
            Name = "Test",
            TypeId = 1
        };

        public CategoryServiceTest()
        {
            _categoryRepository = new Mock<ICategoryRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CategoryService>>();
            _mapper = new Mock<IMapper>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogService = new CategoryService(_dbContextWrapper.Object, _logger.Object, _categoryRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testId = 1;

            _categoryRepository.Setup(s => s.AddAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(testId);

            // act
            var result = await _catalogService.AddAsync(_catalogCategory.Name, _catalogCategory.Id);

            // assert
            result.Should().Be(testId);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            int? testId = null;

            _categoryRepository.Setup(s => s.AddAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(testId);

            // act
            var result = await _catalogService.AddAsync(_catalogCategory.Name, _catalogCategory.Id);

            // assert
            result.Should().Be(testId);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // arrange
            var testId = 1;
            _categoryRepository.Setup(s => s.DeleteAsync(It.Is<int>(i => i == testId))).ReturnsAsync(true);

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
            _categoryRepository.Setup(s => s.DeleteAsync(It.Is<int>(i => i.Equals(testId)))).Returns((Func<UniversalDeleteResponse>)null!);

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

            var catalogCategoryDtoSuccess = new CategoryDto()
            {
                Name = "Test"
            };

            _categoryRepository.Setup(s => s.UpdateAsync(
                It.Is<int>(i => i.Equals(testId)),
                It.Is<string>(i => i.Equals(testProperty)),
                It.Is<string>(i => i.Equals(testValue)))).ReturnsAsync(_catalogCategory);
            _mapper.Setup(s => s.Map<CategoryDto>(It.Is<Category>(i => i.Equals(_catalogCategory)))).Returns(catalogCategoryDtoSuccess);

            // act
            var result = await _catalogService.UpdateAsync(testId, testProperty, testValue);

            // assert
            result.Should().NotBeNull();
            result?.Name.Should().Be(catalogCategoryDtoSuccess.Name);
        }

        [Fact]
        public async Task UpdateAsync_Failed()
        {
            // arrange
            var testId = 1000000;
            var testProperty = "testProperty";
            var testValue = "testValue";

            _categoryRepository.Setup(s => s.UpdateAsync(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Returns((Func<UpdateCategoryResponse>)null!);

            // act
            var result = await _catalogService.UpdateAsync(testId, testProperty, testValue);

            // assert
            result.Should().BeNull();
        }
    }
}
