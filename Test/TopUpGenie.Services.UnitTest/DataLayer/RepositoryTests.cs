using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Xml;
using TopUpGenie.DataAccess;
using TopUpGenie.DataAccess.DataContext;
using TopUpGenie.DataAccess.Repository;

namespace TopUpGenie.Services.UnitTest.DataLayer
{
    public class RepositoryTests
    {
        private TopUpGenieDbContext _dbContext;

        private UnitOfWork CreateUnitOfWork()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<TopUpGenieDbContext>()
                .UseSqlite(connection)
                .Options;

            _dbContext = new TopUpGenieDbContext(options);
            _dbContext.Database.EnsureCreated();
            return new UnitOfWork(_dbContext, new LoggerFactory());
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntity()
        {
            // Arrange
            var unitOfWork = CreateUnitOfWork();
            var loggerMock = new Mock<ILogger<Repository<User>>>();
            var repository = new Repository<User>(_dbContext, loggerMock.Object);

            var entity = new User { Id = 1, Name = "Test Entity" };

            // Act
            var result = await repository.AddAsync(entity);
            var addedEntity = await repository.GetByIdAsync(entity.Id);

            // Assert
            Assert.True(result);
            Assert.NotNull(addedEntity);
            Assert.Equal("Test Entity", addedEntity?.Name);
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            var unitOfWork = CreateUnitOfWork();
            var loggerMock = new Mock<ILogger<Repository<User>>>();
            var repository = new Repository<User>(_dbContext, loggerMock.Object);

            var entity = new User { Id = 1, Name = "Test Entity" };
            await repository.AddAsync(entity);

            // Act
            var deleteResult = repository.Delete(entity);
            var deletedEntity = await repository.GetByIdAsync(entity.Id);

            // Assert
            Assert.True(deleteResult);
            Assert.Null(deletedEntity);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEntities()
        {
            // Arrange
            var unitOfWork = CreateUnitOfWork();
            var loggerMock = new Mock<ILogger<Repository<User>>>();
            var repository = new Repository<User>(_dbContext, loggerMock.Object);

            await repository.AddAsync(DependencyProvider.User);
            await unitOfWork.CompleteAsync();

            // Act
            var entities = await repository.GetAllAsync();

            // Assert
            Assert.Single(entities);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity()
        {
            // Arrange
            var unitOfWork = CreateUnitOfWork();
            var loggerMock = new Mock<ILogger<Repository<User>>>();
            var repository = new Repository<User>(_dbContext, loggerMock.Object);

            var entity = new User { Id = 1, Name = "Test Entity" };
            await repository.AddAsync(entity);

            // Act
            var fetchedEntity = await repository.GetByIdAsync(entity.Id);

            // Assert
            Assert.NotNull(fetchedEntity);
            Assert.Equal("Test Entity", fetchedEntity?.Name);
        }

        [Fact]
        public async Task Update_ShouldUpdateEntity()
        {
            // Arrange
            var unitOfWork = CreateUnitOfWork();
            var loggerMock = new Mock<ILogger<Repository<User>>>();
            var repository = new Repository<User>(_dbContext, loggerMock.Object);

            var entity = new User { Id = 1, Name = "Initial Name" };
            await repository.AddAsync(entity);

            entity.Name = "Updated Name";

            // Act
            var updateResult = repository.Update(entity);
            var updatedEntity = await repository.GetByIdAsync(entity.Id);

            // Assert
            Assert.True(updateResult);
            Assert.Equal("Updated Name", updatedEntity?.Name);
        }
    }
}

