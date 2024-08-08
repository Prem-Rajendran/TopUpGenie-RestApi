using System;
using TopUpGenie.Common;

namespace TopUpGenie.Services.UnitTest
{
    public class TransactionServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ITransactionUnitOfWork> _transactionUnitOfWorkMock;
        private readonly Mock<IExternalService> _externalServiceMock;
        private readonly TransactionService _transactionService;

        public TransactionServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _transactionUnitOfWorkMock = new Mock<ITransactionUnitOfWork>();
            _externalServiceMock = new Mock<IExternalService>();
            _transactionService = new TransactionService(_unitOfWorkMock.Object, _transactionUnitOfWorkMock.Object, _externalServiceMock.Object);
        }

        [Fact]
        public async Task BeginTransact_Success()
        {
            // Arrange
            var user = new User { Id = 1, Verified = true };
            var beneficiary = new Beneficiary { Id = 1, BeneficiaryUser = new User { Id = 2 } };
            var topUpOption = new TopUpOption { TopUpOptionId = 1, Amount = 100 };
            var response = new GenericServiceResponse<bool> { Status = Common.Enums.Status.Unknown };

            _transactionUnitOfWorkMock.Setup(tuow => tuow.Transactions.AddAsync(It.IsAny<Transaction>())).ReturnsAsync(true);
            _transactionUnitOfWorkMock.Setup(tuow => tuow.CompleteAsync()).ReturnsAsync(true);
            _unitOfWorkMock.Setup(uow => uow.BeginTransactionAsync()).Returns(Task.CompletedTask);
            _externalServiceMock.Setup(es => es.GetUserBalanceAsync(user.Id)).ReturnsAsync(200);
            _externalServiceMock.Setup(es => es.DebitUserAccountAsync(user.Id, 101)).ReturnsAsync(true);
            _externalServiceMock.Setup(es => es.CreditUserAccountAsync(beneficiary.BeneficiaryUser.Id, 100)).ReturnsAsync(true);
            _unitOfWorkMock.Setup(uow => uow.CompleteAsync()).ReturnsAsync(true);
            _unitOfWorkMock.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _transactionService.BeginTransact(response, user, beneficiary, topUpOption);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task BeginTransact_InsufficientBalance()
        {
            // Arrange
            var user = new User { Id = 1, Verified = true };
            var beneficiary = new Beneficiary { Id = 1, BeneficiaryUser = new User { Id = 2 } };
            var topUpOption = new TopUpOption { TopUpOptionId = 1, Amount = 100 };
            var response = new GenericServiceResponse<bool> { Status = Common.Enums.Status.Unknown };

            _transactionUnitOfWorkMock.Setup(tuow => tuow.Transactions.AddAsync(It.IsAny<Transaction>())).ReturnsAsync(true);
            _transactionUnitOfWorkMock.Setup(tuow => tuow.CompleteAsync()).ReturnsAsync(true);
            _unitOfWorkMock.Setup(uow => uow.BeginTransactionAsync()).Returns(Task.CompletedTask);
            _externalServiceMock.Setup(es => es.GetUserBalanceAsync(user.Id)).ReturnsAsync(50);

            // Act
            var result = await _transactionService.BeginTransact(response, user, beneficiary, topUpOption);

            // Assert
            Assert.False(result);
            Assert.Contains(response.Messages, m => m.ErrorCode == ErrorCodes.TRANSACTION_INSUFICIENT_BALANCE);
        }

        [Fact]
        public async Task BeginTransact_DebitFailed()
        {
            // Arrange
            var user = new User { Id = 1, Verified = true };
            var beneficiary = new Beneficiary { Id = 1, BeneficiaryUser = new User { Id = 2 } };
            var topUpOption = new TopUpOption { TopUpOptionId = 1, Amount = 100 };
            var response = new GenericServiceResponse<bool> { Status = Common.Enums.Status.Unknown };

            _transactionUnitOfWorkMock.Setup(tuow => tuow.Transactions.AddAsync(It.IsAny<Transaction>())).ReturnsAsync(true);
            _transactionUnitOfWorkMock.Setup(tuow => tuow.CompleteAsync()).ReturnsAsync(true);
            _unitOfWorkMock.Setup(uow => uow.BeginTransactionAsync()).Returns(Task.CompletedTask);
            _externalServiceMock.Setup(es => es.GetUserBalanceAsync(user.Id)).ReturnsAsync(200);
            _externalServiceMock.Setup(es => es.DebitUserAccountAsync(user.Id, 101)).ReturnsAsync(false);

            // Act
            var result = await _transactionService.BeginTransact(response, user, beneficiary, topUpOption);

            // Assert
            Assert.False(result);
            Assert.Contains(response.Messages, m => m.ErrorCode == ErrorCodes.TRANSACTION_DEBIT_FAILED);
        }

        [Fact]
        public async Task BeginTransact_CreditFailed()
        {
            // Arrange
            var user = new User { Id = 1, Verified = true };
            var beneficiary = new Beneficiary { Id = 1, BeneficiaryUser = new User { Id = 2 } };
            var topUpOption = new TopUpOption { TopUpOptionId = 1, Amount = 100 };
            var response = new GenericServiceResponse<bool> { Status = Common.Enums.Status.Unknown };

            _transactionUnitOfWorkMock.Setup(tuow => tuow.Transactions.AddAsync(It.IsAny<Transaction>())).ReturnsAsync(true);
            _transactionUnitOfWorkMock.Setup(tuow => tuow.CompleteAsync()).ReturnsAsync(true);
            _unitOfWorkMock.Setup(uow => uow.BeginTransactionAsync()).Returns(Task.CompletedTask);
            _externalServiceMock.Setup(es => es.GetUserBalanceAsync(user.Id)).ReturnsAsync(200);
            _externalServiceMock.Setup(es => es.DebitUserAccountAsync(user.Id, 101)).ReturnsAsync(true);
            _externalServiceMock.Setup(es => es.CreditUserAccountAsync(beneficiary.BeneficiaryUser.Id, 100)).ReturnsAsync(false);

            // Act
            var result = await _transactionService.BeginTransact(response, user, beneficiary, topUpOption);

            // Assert
            Assert.False(result);
            Assert.Contains(response.Messages, m => m.ErrorCode == ErrorCodes.TRANSACTION_CREDIT_FAILED);
        }
    }

}

