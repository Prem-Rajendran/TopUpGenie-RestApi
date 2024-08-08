using System;
using Microsoft.AspNetCore.Http;
using Moq;
using TopUpGenie.Common;

namespace TopUpGenie.Services.UnitTest
{
	public static class DependencyProvider
	{
		public static RequestContext RequestContext => GetRequestContext();

        public static User User => GetDummyValidUser();

        public static Transaction Transaction => GetValidTransaction();


        private static RequestContext GetRequestContext()
		{
            var httpContext = new DefaultHttpContext();
            return new RequestContext(httpContext);
        }

        private static User GetDummyValidUser()
        {
            return new User
            {
                Name = "xxxx",
                Password = "password123",
                PhoneNumber = "1111111",
                Verified = true,
                IsAdmin = false,
                Balance = 1000,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        private static Transaction GetValidTransaction()
        {
            return new Transaction
            {
                UserId = 1,
                BeneficiaryId = 1,
                TopUpOptionId = 6,
                TransactionAmount = 75,
                TransactionFee = 1,
                TotalTransactionAmount = 76,
                TransactionStatus = DataAccess.Enums.TransactionStatus.SUCCESS,
                TransactionDate = DateTime.Now,
                Messages = "SomeMessage"
            };
        }
    }
}

