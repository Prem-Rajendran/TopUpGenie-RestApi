using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TopUpGenie.DataAccess;
using TopUpGenie.DataAccess.DataContext;
using TopUpGenie.DataAccess.Repository;

namespace TopUpGenie.Services.UnitTest.DataLayer
{
	public class BeneficiaryRepositoryTest
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
        public async Task GetCountOfMyActiveBeneficiary_ShouldReturnCorrectCount()
        {
            // Arrange
            var unitOfWork = CreateUnitOfWork();
            var loggerMock = new Mock<ILogger<Repository<Beneficiary>>>();
            var repository = new BenificiaryRepository(_dbContext, loggerMock.Object);

            await unitOfWork.Users.AddAsync(DependencyProvider.User);
            await unitOfWork.CompleteAsync();

            var beneficiary1 = new Beneficiary { Id = 1, Nickname = "1", UserId = 1, CreatedByUserId = 1, IsActive = true };
            var beneficiary2 = new Beneficiary { Id = 2, Nickname = "1", UserId = 1, CreatedByUserId = 1, IsActive = false };
            var beneficiary3 = new Beneficiary { Id = 3, Nickname = "1", UserId = 1, CreatedByUserId = 1, IsActive = false };
            var beneficiary4 = new Beneficiary { Id = 4, Nickname = "1" , UserId = 1, CreatedByUserId = 1, IsActive = true };

            await repository.AddAsync(beneficiary1);
            await repository.AddAsync(beneficiary2);
            await repository.AddAsync(beneficiary3);
            await repository.AddAsync(beneficiary4);
            await unitOfWork.CompleteAsync();

            // Act
            var activeCount = await repository.GetCountOfMyActiveBeneficiary(1);

            // Assert
            Assert.Equal(2, activeCount); // User 1 has 2 active beneficiaries
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllBeneficiariesWithIncludes()
        {
            var unitOfWork = CreateUnitOfWork();
            var loggerMock = new Mock<ILogger<Repository<Beneficiary>>>();
            var repository = new BenificiaryRepository(_dbContext, loggerMock.Object);

            await unitOfWork.Users.AddAsync(DependencyProvider.User);
            await unitOfWork.CompleteAsync();

            var beneficiary1 = new Beneficiary { Id = 1, Nickname = "1", UserId = 1, CreatedByUserId = 1, IsActive = true };
            var beneficiary2 = new Beneficiary { Id = 2, Nickname = "1", UserId = 1, CreatedByUserId = 1, IsActive = false };
            var beneficiary3 = new Beneficiary { Id = 3, Nickname = "1", UserId = 1, CreatedByUserId = 1, IsActive = false };
            var beneficiary4 = new Beneficiary { Id = 4, Nickname = "1", UserId = 1, CreatedByUserId = 1, IsActive = true };

            await repository.AddAsync(beneficiary1);
            await repository.AddAsync(beneficiary2);
            await repository.AddAsync(beneficiary3);
            await repository.AddAsync(beneficiary4);
            await unitOfWork.CompleteAsync();

            // Act
            var beneficiaries = await repository.GetAllAsync();

            // Assert
            Assert.Equal(4, beneficiaries.Count());
            Assert.All(beneficiaries, b => Assert.NotNull(b.BeneficiaryUser));
            Assert.All(beneficiaries, b => Assert.NotNull(b.CreatedByUser));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnBeneficiaryWithIncludes()
        {
            var unitOfWork = CreateUnitOfWork();
            var loggerMock = new Mock<ILogger<Repository<Beneficiary>>>();
            var repository = new BenificiaryRepository(_dbContext, loggerMock.Object);

            await unitOfWork.Users.AddAsync(DependencyProvider.User);
            await unitOfWork.CompleteAsync();

            var beneficiary1 = new Beneficiary { Id = 1, Nickname = "1", UserId = 1, CreatedByUserId = 1, IsActive = true };
            var beneficiary2 = new Beneficiary { Id = 2, Nickname = "1", UserId = 1, CreatedByUserId = 1, IsActive = false };
            var beneficiary3 = new Beneficiary { Id = 3, Nickname = "1", UserId = 1, CreatedByUserId = 1, IsActive = false };
            var beneficiary4 = new Beneficiary { Id = 4, Nickname = "1", UserId = 1, CreatedByUserId = 1, IsActive = true };

            await repository.AddAsync(beneficiary1);
            await repository.AddAsync(beneficiary2);
            await repository.AddAsync(beneficiary3);
            await repository.AddAsync(beneficiary4);
            await unitOfWork.CompleteAsync();

            // Act
            var fetchedBeneficiary = await repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(fetchedBeneficiary);
            Assert.NotNull(fetchedBeneficiary?.BeneficiaryUser);
            Assert.NotNull(fetchedBeneficiary?.CreatedByUser);
        }
    }
}
