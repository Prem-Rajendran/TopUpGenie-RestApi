using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TopUpGenie.DataAccess;
using TopUpGenie.DataAccess.DataContext;
using TopUpGenie.DataAccess.Repository;

namespace TopUpGenie.Services.UnitTest.DataLayer
{
	public class UserRepositoryTests
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
        public async Task GetUserByPhoneNumber()
        {
            var unitOfWork = CreateUnitOfWork();
            var loggerMock = new Mock<ILogger<Repository<User>>>();
            var repository = new UserRepository(_dbContext, loggerMock.Object);

            var user = DependencyProvider.User;
            await repository.AddAsync(user);
            await unitOfWork.CompleteAsync();

            var entity = await repository.GetUserByPhoneNumber(user.PhoneNumber);

            Assert.NotNull(entity);
            Assert.Equal(entity.PhoneNumber, user.PhoneNumber);
        }
    }
}

