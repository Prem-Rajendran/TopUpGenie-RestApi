using System;
using TopUpGenie.Common;

namespace TopUpGenie.Services.UnitTest
{
    public class TopUpServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ITransactionService> _transactionService;
        private readonly ITopUpService _topUpService;

        public TopUpServiceTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _transactionService = new Mock<ITransactionService>();

            _topUpService = new TopUpService(_unitOfWork.Object, _transactionService.Object);
        }

        [Fact]
        public async Task ListTopUpOptions_Success()
        {
            
            var topUpOptions = new List<TopUpOption> { new TopUpOption() };
            var response = new GenericServiceResponse<IEnumerable<TopUpOption>> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.TopUpOptions.GetAllAsync()).ReturnsAsync(topUpOptions);

            
            var result = await _topUpService.ListTopUpOptions();

            
            Assert.Equal(Common.Enums.Status.Success, result.Status);
            Assert.Equal(topUpOptions, result.Data);
        }

        [Fact]
        public async Task ListTopUpOptions_NoOptionsFound()
        {
            
            var response = new GenericServiceResponse<IEnumerable<TopUpOption>> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.TopUpOptions.GetAllAsync()).ReturnsAsync((IEnumerable<TopUpOption>)null);

            
            var result = await _topUpService.ListTopUpOptions();

            
            Assert.Equal(Common.Enums.Status.Failure, result.Status);
            Assert.Contains(result.Messages, m => m.ErrorCode == ErrorCodes.TOPUP_LIST_OPTIONS_FAILED);
        }

        [Fact]
        public async Task ListTopUpOptions_ExceptionThrown()
        {
            
            var response = new GenericServiceResponse<IEnumerable<TopUpOption>> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.TopUpOptions.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

            
            var result = await _topUpService.ListTopUpOptions();

            
            Assert.Equal(Common.Enums.Status.Unknown, result.Status);
            Assert.Contains(result.Messages, m => m.ErrorCode == ErrorCodes.TOPUP_LIST_OPTIONS_EXCEPTION);
        }

        [Fact]
        public async Task TopUpTransaction_Success()
        {
            
            var user = new User { Id = 1 };
            var beneficiary = new Beneficiary { Id = 1 };
            var topUpOption = new TopUpOption { TopUpOptionId = 1 };

            int userId = 1;
            var context = DependencyProvider.RequestContext;
            context.UserId = userId;

            var requestModel = new InitiateTransactionRequestModel { BeneficiaryId = 1, TopUpOptionId = 1 };
            var response = new GenericServiceResponse<bool> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.Users.GetByIdAsync(context.UserId)).ReturnsAsync(user);
            _unitOfWork.Setup(uow => uow.Beneficiaries.GetByIdAsync(requestModel.BeneficiaryId)).ReturnsAsync(beneficiary);
            _unitOfWork.Setup(uow => uow.TopUpOptions.GetByIdAsync(requestModel.TopUpOptionId)).ReturnsAsync(topUpOption);
            _transactionService.Setup(ts => ts.BeginTransact(It.IsAny<IResponse<bool>>(), user, beneficiary, topUpOption)).ReturnsAsync(true);

            
            var result = await _topUpService.TopUpTransaction(context, requestModel);

            
            Assert.Equal(Common.Enums.Status.Success, result.Status);
            Assert.True(result.Data);
        }

        [Fact]
        public async Task TopUpTransaction_Failure()
        {
            
            var user = new User { Id = 1 };
            var beneficiary = new Beneficiary { Id = 1 };
            var topUpOption = new TopUpOption { TopUpOptionId = 1 };

            int userId = 1;
            var context = DependencyProvider.RequestContext;
            context.UserId = userId;

            var requestModel = new InitiateTransactionRequestModel { BeneficiaryId = 1, TopUpOptionId = 1 };
            var response = new GenericServiceResponse<bool> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.Users.GetByIdAsync(context.UserId)).ReturnsAsync((User)null);
            _unitOfWork.Setup(uow => uow.Beneficiaries.GetByIdAsync(requestModel.BeneficiaryId)).ReturnsAsync((Beneficiary)null);
            _unitOfWork.Setup(uow => uow.TopUpOptions.GetByIdAsync(requestModel.TopUpOptionId)).ReturnsAsync((TopUpOption)null);
            _transactionService.Setup(ts => ts.BeginTransact(It.IsAny<IResponse<bool>>(), user, beneficiary, topUpOption)).ReturnsAsync(false);

            
            var result = await _topUpService.TopUpTransaction(context, requestModel);

            
            Assert.Equal(Common.Enums.Status.Failure, result.Status);
            Assert.Contains(result.Messages, m => m.ErrorCode == ErrorCodes.TOPUP_TRANSACTION_FAILED);
        }

        [Fact]
        public async Task TopUpTransaction_ExceptionThrown()
        {
            
            int userId = 1;
            var context = DependencyProvider.RequestContext;
            context.UserId = userId;

            var requestModel = new InitiateTransactionRequestModel { BeneficiaryId = 1, TopUpOptionId = 1 };
            var response = new GenericServiceResponse<bool> { Status = Common.Enums.Status.Unknown };

            _unitOfWork.Setup(uow => uow.Users.GetByIdAsync(context.UserId)).ThrowsAsync(new Exception("Database error"));
            _unitOfWork.Setup(uow => uow.Beneficiaries.GetByIdAsync(requestModel.BeneficiaryId)).ThrowsAsync(new Exception("Database error"));
            _unitOfWork.Setup(uow => uow.TopUpOptions.GetByIdAsync(requestModel.TopUpOptionId)).ThrowsAsync(new Exception("Database error"));
            _transactionService.Setup(ts => ts.BeginTransact(It.IsAny<IResponse<bool>>(), It.IsAny<User>(), It.IsAny<Beneficiary>(), It.IsAny<TopUpOption>())).ThrowsAsync(new Exception("Service error"));

            
            var result = await _topUpService.TopUpTransaction(context, requestModel);

            
            Assert.Equal(Common.Enums.Status.Unknown, result.Status);
            Assert.Contains(result.Messages, m => m.ErrorCode == ErrorCodes.TOPUP_TRANSACTION_EXCEPTION);
        }
    }
}

