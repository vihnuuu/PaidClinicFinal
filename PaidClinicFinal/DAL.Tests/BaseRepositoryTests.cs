using DAL.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace DAL.Tests
{
    public class BaseRepositoryTests
    {
        private readonly Mock<DbSet<TestEntity>> _mockDbSet;
        private readonly Mock<DbContext> _mockContext;
        private readonly BaseRepository<TestEntity> _repository;

        public BaseRepositoryTests()
        {
            // Мок для DbSet
            _mockDbSet = new Mock<DbSet<TestEntity>>();

            // Налаштування контексту для повернення змоканого DbSet
            _mockContext = new Mock<DbContext>();
            _mockContext.Setup(c => c.Set<TestEntity>()).Returns(_mockDbSet.Object);

            // Ініціалізація базового репозиторію з використанням двійників
            _repository = new Mock<BaseRepository<TestEntity>>(_mockContext.Object).Object;
        }

       

        [Fact]
        public async Task AddAsync_ShouldAddEntity()
        {
            // Arrange
            var testEntity = new TestEntity { Id = 3, Name = "Entity3" };

            // Act
            await _repository.AddAsync(testEntity);

            // Assert
            _mockDbSet.Verify(m => m.AddAsync(testEntity, default), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntityById()
        {
            // Arrange
            var testEntity = new TestEntity { Id = 1, Name = "Entity1" };
            _mockDbSet.Setup(m => m.FindAsync(1)).ReturnsAsync(testEntity);

            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Entity1", result.Name);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveEntity()
        {
            // Arrange
            var testEntity = new TestEntity { Id = 1, Name = "Entity1" };
            _mockDbSet.Setup(m => m.FindAsync(1)).ReturnsAsync(testEntity);

            // Act
            await _repository.DeleteAsync(1);

            // Assert
            _mockDbSet.Verify(m => m.Remove(testEntity), Times.Once);
        }

        // Тестова сутність
        public class TestEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
}
