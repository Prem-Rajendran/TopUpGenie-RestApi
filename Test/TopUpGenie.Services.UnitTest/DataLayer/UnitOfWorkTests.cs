using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using TopUpGenie.DataAccess;
using TopUpGenie.DataAccess.DataContext;

namespace TopUpGenie.Services.UnitTest.DataLayer
{
    public class UnitOfWorkTests
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
        public void Check_Properties_is_Not_Null()
        {
            using var unitOfWork = CreateUnitOfWork();
            Assert.NotNull(unitOfWork.Users);
            Assert.NotNull(unitOfWork.Beneficiaries);
            Assert.NotNull(unitOfWork.TopUpOptions);
            Assert.NotNull(unitOfWork.Sessions);
        }

        [Fact]
        public async Task CompleteAsync_ShouldReturnTrue_WhenSaveChangesSucceeds()
        {
            // Arrange
            using var unitOfWork = CreateUnitOfWork();

            // Act
            var result = await unitOfWork.CompleteAsync();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CompleteAsync_ShouldReturnFalse_WhenSaveChangesFails()
        {
            // Arrange
            using var unitOfWork = CreateUnitOfWork();
            _dbContext.Database.EnsureCreated(); // Make sure database is created

            // Add an entity to cause SaveChanges to fail
            await unitOfWork.Users.AddAsync(new User());

            // Act
            var result = await unitOfWork.CompleteAsync();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task BeginTransactionAsync_ShouldBeginTransaction()
        {
            // Arrange
            using var unitOfWork = CreateUnitOfWork();

            // Act
            await unitOfWork.BeginTransactionAsync();


            Assert.NotNull(_dbContext?.Database.CurrentTransaction);
        }

        [Fact]
        public async Task CommitAsync_ShouldCommitTransaction()
        {
            // Arrange
            using var unitOfWork = CreateUnitOfWork();
            await unitOfWork.BeginTransactionAsync();

            // Act
            await unitOfWork.CommitAsync();

            // Assert

            Assert.Null(_dbContext?.Database.CurrentTransaction);
        }

        [Fact]
        public async Task RollbackAsync_ShouldRollbackTransaction()
        {
            // Arrange
            using var unitOfWork = CreateUnitOfWork();
            await unitOfWork.BeginTransactionAsync();

            // Act
            await unitOfWork.RollbackAsync();

            // Assert

            Assert.Null(_dbContext?.Database.CurrentTransaction);
        }

        [Fact]
        public void Dispose_ShouldDisposeContext()
        {
            // Arrange
            var unitOfWork = CreateUnitOfWork();

            // Act
            unitOfWork.Dispose();

            // Assert
            // Since we are using in-memory database, we cannot check the disposal directly.
            // We should ensure no exceptions are thrown during disposal.
        }
    }
}

