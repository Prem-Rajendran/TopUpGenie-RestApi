using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TopUpGenie.DataAccess;
using TopUpGenie.DataAccess.DataContext;

namespace TopUpGenie.Services.UnitTest.DataLayer
{
	public class TransactionUnitOfWorkTests
	{
        private TransactionDbContext _dbContext;

        private TransactionUnitOfWork CreateUnitOfWork()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<TransactionDbContext>()
                .UseSqlite(connection)
                .Options;

            _dbContext = new TransactionDbContext(options);
            _dbContext.Database.EnsureCreated();
            return new TransactionUnitOfWork(_dbContext, new LoggerFactory());
        }

        [Fact]
        public void Check_Properties_is_Not_Null()
        {
            TransactionUnitOfWork unitOfWork = CreateUnitOfWork();
            Assert.NotNull(unitOfWork.Transactions);
        }

        [Fact]
        public async Task CompleteAsync_ShouldReturnTrue_WhenSaveChangesSucceeds()
        {
            // Arrange
           TransactionUnitOfWork unitOfWork = CreateUnitOfWork();

            // Act
            var result = await unitOfWork.CompleteAsync();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CompleteAsync_ShouldReturnFalse_WhenSaveChangesFails()
        {
            // Arrange
            TransactionUnitOfWork unitOfWork = CreateUnitOfWork();
            _dbContext.Database.EnsureCreated(); // Make sure database is created

            // Add an entity to cause SaveChanges to fail
            await unitOfWork.Transactions.AddAsync(new Transaction());

            // Act
            var result = await unitOfWork.CompleteAsync();

            // Assert
            Assert.False(result);
        }
    }
}

