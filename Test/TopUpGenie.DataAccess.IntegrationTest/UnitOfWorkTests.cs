namespace TopUpGenie.DataAccess.IntegrationTest;

[Collection("Class")]
public class UnitOfWorkTests
{
    private readonly IUnitOfWork unitOfWork;
    private readonly TopUpGenieDbContext dbContext;

	public UnitOfWorkTests()
	{
        dbContext = DependencyHelper.TopUpGenieDbContext;
        unitOfWork = new UnitOfWork(dbContext, new LoggerFactory());
    }

    [Fact]
    public async Task UserRepository_NotNull()
    {
        var users = await unitOfWork.Users.GetAllAsync();
        Assert.NotNull(users);
        Assert.True(users.Any());
    }

    [Fact]
    public async Task BeneficiaryRepository_NotNull()
    {
        var beneficiaries = await unitOfWork.Beneficiaries.GetAllAsync();
        Assert.NotNull(beneficiaries);
        Assert.True(beneficiaries.Any());
    }

    [Fact]
    public async Task SessionsRepository_NotNull()
    {
        var sessions = await unitOfWork.Sessions.GetAllAsync();
        Assert.NotNull(sessions);
        Assert.True(sessions.Any());
    }

    [Fact]
    public async Task TopUpOptionsRepository_NotNull()
    {
        var topUpOptions = await unitOfWork.TopUpOptions.GetAllAsync();
        Assert.NotNull(topUpOptions);
        Assert.True(topUpOptions.Any());
    }

    [Fact]
    public async Task CompleteAsync_SavesChangesToDatabase()
    {
        var userRepository = unitOfWork.Users;

        var user = DependencyHelper.ValidUser;
        await userRepository.AddAsync(user);
        await unitOfWork.CompleteAsync();

        Assert.True(dbContext.Users.Any(u => u.Id != 0 && u.Name == "xxxx"));
    }

    [Fact]
    public async Task BeginTransactionAsync_RollbackAsync_DoesNotSaveChangesToDatabase()
    {
        var userRepository = unitOfWork.Users;

        await unitOfWork.BeginTransactionAsync();
        var user = DependencyHelper.ValidUser;
        await userRepository.AddAsync(user);
        await unitOfWork.RollbackAsync();

        Assert.True(user.Id == 0);
    }

    [Fact]
    public async Task BeginTransactionAsync_CommitAsync_SavesChangesToDatabase()
    {
        var userRepository = unitOfWork.Users;

        await unitOfWork.BeginTransactionAsync();
        var user = DependencyHelper.ValidUser;
        await userRepository.AddAsync(user);
        await unitOfWork.CommitAsync();
        await unitOfWork.CompleteAsync();

        Assert.True(user.Id != 0);
    }
}